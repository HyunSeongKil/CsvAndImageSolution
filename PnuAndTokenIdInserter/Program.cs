using ClassLibrary;
using Npgsql;
using System.Diagnostics;
using System.Text.Json;


namespace LandNftMapper
{
    /// <summary>
    /// 신한ds에서 민팅 완료 후 제공해 주는 csv를 이용하여 pnu vs tokenid 매핑 테이블에 데이터 insert하는 프로그램
    /// csv file sample : pnu_tokenid.csv
    /// config json file sample : pnu_tokenid.config.json
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            PrintHelp();

            if(0 == args.Length)
            {
                Console.WriteLine("아규먼트가 없습니다.");
                return;
            }


            Program program = new();


            //
            ConfigDto configDto = program.ParseConfigJson(args[0]);
            List<PnuTokenIdDto> dtos = program.ParseCsv(configDto.CsvFile);

            //
            IEnumerable<PnuTokenIdDto[]> enums = dtos.Chunk(dtos.Count / configDto.AsyncCo);

            //
            Stopwatch sw = new();
            sw.Start();

            List<Task> tasks = new();

            //
            foreach (PnuTokenIdDto[] arr in enums)
            {
                Task t = program.ProcessAsync(configDto, arr);
                tasks.Add(t);

                Thread.Sleep(100);
            }


            tasks.ForEach(t => t.Wait());


            sw.Stop();
            Util.Log("Done", $"전체(건): {dtos.Count}", $"소요시간(초) : {sw.ElapsedMilliseconds/1000}");
        }

        private Task ProcessAsync(ConfigDto configDto, PnuTokenIdDto[] dtos)
        {
            return Task.Run(() =>
            {
                using (var conn = CreateConnection(configDto))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;

                        DoInsert(cmd, dtos);
                    }

                    conn.Close();
                }
            });
        }

        private static void PrintHelp()
        {
            Console.WriteLine("사용법)");
            Console.WriteLine("dotnet LandNftMapper.dll args[0]");
            Console.WriteLine("\targs[0] : config json file. ex)c:\\temp\\config.json");
        }

        private int DoInsert(NpgsqlCommand cmd, PnuTokenIdDto[] dtos)
        {
            string name = $"t-{DateTime.Now.Millisecond}";

            for (int i = 0; i < dtos.Length; i++)
            {
                DoInsert(cmd, dtos[i]);

                if (0 == i % 100)
                {
                    Util.Log( $"{i}/{dtos.Length}", "inserted");
                }
            }

            Util.Log($"{name}", $"{dtos.Length}", "inserted");
            return dtos.Length;
        }

        private void DoInsert(NpgsqlCommand cmd, PnuTokenIdDto dto)
        {
            cmd.CommandText = "INSERT INTO land_nft_mapng (pnu, token_id) VALUES (@pnu, @tokenId)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("pnu", dto.Pnu);
            cmd.Parameters.AddWithValue("tokenId", dto.TokenId);
           

            cmd.ExecuteNonQuery();
        }

        private NpgsqlConnection CreateConnection(ConfigDto configDto)
        {
            return new NpgsqlConnection($"HOST={configDto.DbHost};PORT={configDto.DbPort};USERNAME={configDto.DbUsername};PASSWORD={configDto.DbPassword};DATABASE={configDto.DbDatabase}");
        }

        private ConfigDto ParseConfigJson(string configFile)
        {
            string json = File.ReadAllText(configFile);
            JsonDocument doc = JsonDocument.Parse(json);

            ConfigDto configDto = new()
            {
                CsvFile = doc.RootElement.GetProperty("CsvFile").ToString(),
                DbHost = doc.RootElement.GetProperty("DbHost").ToString(),
                DbPort = doc.RootElement.GetProperty("DbPort").ToString(),
                DbUsername = doc.RootElement.GetProperty("DbUsername").ToString(),
                DbPassword = doc.RootElement.GetProperty("DbPassword").ToString(),
                DbDatabase = doc.RootElement.GetProperty("DbDatabase").ToString(),
                AsyncCo = int.Parse(doc.RootElement.GetProperty("AsyncCo").ToString())
            };


            Util.Log("CsvFile", configDto);
            return configDto;
        }




        private List<PnuTokenIdDto> ParseCsv(string csvFile)
        {
            List<PnuTokenIdDto> dtos = new();

            string[] lines = File.ReadAllLines(csvFile);


            string line;
            // 첫줄은 제목줄이라 i=1부터 시작함
            for(int i=1; i<lines.Length; i++)
            {
                line = lines[i];

                string[] arr = line.Split(',');
                dtos.Add(new PnuTokenIdDto()
                {
                    Pnu = arr[0],
                    TokenId = arr[1],
                }); 

            }

            Util.Log($"{dtos.Count}", "Parsed");
            return dtos;
        }
    }
}