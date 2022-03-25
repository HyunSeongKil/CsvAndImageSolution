using ClassLibrary;
using Npgsql;
using System.Diagnostics;
using System.Text.Json;


namespace PnuAndTokenIdInserter
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
            Stopwatch sw = new();
            sw.Start();

            
            using (var conn = program.CreateConnection(configDto)) 
            {
                conn.Open();

                using(var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    program.DoInsert(cmd, dtos);
                }

                conn.Close();
            }

            sw.Stop();
            Util.Log("Done", $"소요시간(초) : {sw.ElapsedMilliseconds/1000}");
        }

        private static void PrintHelp()
        {
            Console.WriteLine("사용법)");
            Console.WriteLine("dotnet PnuAndTokenIdInserter.dll args[0]");
            Console.WriteLine("\targs[0] : config json file. ex)c:\\temp\\config.json");
        }

        private int DoInsert(NpgsqlCommand cmd, List<PnuTokenIdDto> dtos)
        {
            for (int i = 0; i < dtos.Count; i++)
            {
                DoInsert(cmd, dtos[i]);

                if (0 == i % 100)
                {
                    Util.Log($"{i}/{dtos.Count}", "inserted");
                }
            }

            Util.Log($"전체: {dtos.Count}", "inserted");
            return dtos.Count;
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
                DbDatabase = doc.RootElement.GetProperty("DbDatabase").ToString()
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

                i++;
            }

            Util.Log($"{dtos.Count}", "Parsed");
            return dtos;
        }
    }
}