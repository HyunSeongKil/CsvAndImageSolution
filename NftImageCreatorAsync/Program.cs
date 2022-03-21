using ClassLibrary;
using System.Diagnostics;
using System.Drawing;
using System.Text.Json;

namespace NftImageCreatorAsync
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PrintHelp();

            if (0 == args.Length)
            {
                return;
            }

            new Program().Process(args);

        }

        /// <summary>
        /// 처리
        /// </summary>
        /// <param name="args"></param>
        private void Process(string[] args)
        {
            NftImageCreateConfig config = ParseConfig(args[0]);

            //
            if (!Valid(config))
            {
                return;
            }


            // 데이터 읽기
            List<AddressDataDto> dataDtos = ToDtos(config.DataFile);

            IEnumerable<AddressDataDto[]> enums = ChunkDatas(dataDtos, config.AsyncCo);

            List<Task> tasks = new();


            foreach (AddressDataDto[] dtos in enums)
            {
                Task t = ProcessAsync($"t-{tasks.Count}", dtos, config);
                tasks.Add(t);

                Thread.Sleep(1000);
            }


            // 모두 끝날때까지 대기
            tasks.ForEach(x =>
            {
                x.Wait();
            });
        }


        /// <summary>
        /// 비동기갯수로 전체 데이터를 나누기
        /// </summary>
        /// <param name="dataDtos"></param>
        /// <param name="asyncCo"></param>
        /// <returns></returns>
        private IEnumerable<AddressDataDto[]> ChunkDatas(List<AddressDataDto> dataDtos, int asyncCo)
        {
            double chunkSize = Math.Ceiling((double)(dataDtos.Count / asyncCo));

            return dataDtos.Chunk(Convert.ToInt16(chunkSize));
        }

        /// <summary>
        /// 비동기 처리
        /// </summary>
        /// <param name="gbn"></param>
        /// <param name="dtos"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private Task ProcessAsync(string gbn, AddressDataDto[] dtos, NftImageCreateConfig config)
        {
            return Task.Run(() =>
            {
                Stopwatch sw = new();
                sw.Start();
                int i = -1;

                foreach (AddressDataDto dto in dtos)
                {
                    VtImage vtImage = GetVtImageByVt(dto, config.VtImages);
                    if (null == vtImage)
                    {
                        continue;
                    }

                    Image image = Image.FromFile($"{vtImage.ImageFile}");
                    DrawString(image, GetAddressString(dto));


                    // 이미지 파일로 저장
                    SaveImage(dto, config, vtImage, image);


                    if (0 == ++i % 100)
                    {
                        Util.Log(gbn, $"{i}/{dtos.Length}", $"{sw.ElapsedMilliseconds / 1000}초");
                    }
                }

                sw.Stop();
                Util.Log(gbn, "완료", $"전체(개): {dtos.Length}", $"소요시간(초): {sw.ElapsedMilliseconds / 1000}");
            });
        }

        //private void Log(params object[] args)
        //{
        //    Console.Write(DateTime.Now);
        //    foreach (object o in args)
        //    {
        //        Console.Write($"\t{o}");
        //    }
        //    Console.WriteLine("");
        //}


        /// <summary>
        /// 이미지 파일로 저장
        /// </summary>
        /// <param name="dataDto"></param>
        /// <param name="config"></param>
        /// <param name="vtImage"></param>
        /// <param name="image"></param>
        private void SaveImage(AddressDataDto dataDto, NftImageCreateConfig config, VtImage vtImage, Image? image)
        {
            //
            string filename = $"{dataDto.Pnu}_{dataDto.Vt}_{Path.GetFileName(vtImage.ImageFile)}";

            // 이미지 파일로 저장
            image?.Save($"{config.OutPath}/{filename}");
        }

        /// <summary>
        /// 이미지에 문자열 쓰기
        /// </summary>
        /// <param name="image"></param>
        /// <param name="str"></param>
        private void DrawString(Image image, string str)
        {
            Graphics g = Graphics.FromImage(image);

            // 글자 쓰기
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            //g.DrawString("KR, SEOUL", new Font("Poppins", 36), new SolidBrush(Color.White), new Point(85, 388));
            g.DrawString(str, new Font("Spoqa Han Sans Neo Bold", 28), new SolidBrush(Color.White), new Point(39, 620));

            g.Dispose();
        }

        private string GetAddressString(AddressDataDto dto)
        {
            return $"{dto.Sd} {dto.Sgg} {dto.Emd} {dto.Bonbun}{GetBuBunString(dto.Bubun)}";
        }

        /// <summary>
        /// 부번이 0인 경우 공백 리턴
        /// </summary>
        private string GetBuBunString(string bubun)
        {
            if ("0".Equals(bubun) || 0 == bubun.Trim().Length)
            {
                return "";
            }

            return $"-{bubun}";
        }

        /// <summary>
        /// vt에 맞는 이미지 정보 조회
        /// </summary>
        /// <param name="dataDto"></param>
        /// <param name="vtImages"></param>
        /// <returns></returns>
        private VtImage? GetVtImageByVt(AddressDataDto dataDto, List<VtImage> vtImages)
        {
            return vtImages.Find(x =>
                x.MinVt <= dataDto.Vt && dataDto.Vt < x.MaxVt
            );
        }


        /// <summary>
        /// csv파일 파싱하여 Dto 목록으로 변환하기
        /// </summary>
        /// <param name="csvFile"></param>
        /// <returns></returns>
        private List<AddressDataDto> ToDtos(string csvFile)
        {
            string[] lines = File.ReadAllLines(csvFile);

            List<AddressDataDto> dataDtos = new();
            foreach (string line in lines)
            {
                if (null == line || 0 == line.Trim().Length)
                {
                    continue;
                }

                // 주석
                if (line.StartsWith('#'))
                {
                    continue;
                }

                string[] arr = line.Split('^');
                AddressDataDto dataDto = new();
                dataDtos.Add(dataDto);

                dataDto.Pnu = arr[0];
                BindAddress(dataDto, arr[1].Split('|'));
                dataDto.Vt = int.Parse(arr[2]);
            }

            return dataDtos;
        }

        private void BindAddress(AddressDataDto dataDto, string[] arr)
        {
            dataDto.Sd = arr[0];
            dataDto.Sgg = arr[1];
            dataDto.Emd = arr[2];
            dataDto.Bonbun = arr[3];
            dataDto.Bubun = arr[4];
        }

        private bool Valid(NftImageCreateConfig config)
        {
            if (!Directory.Exists(config.OutPath))
            {
                Directory.CreateDirectory(config.OutPath);
            }

            // TODO


            return true;
        }

        /// <summary>
        /// json파일 파싱 후 dto에 담기
        /// </summary>
        /// <param name="configFile"></param>
        /// <returns></returns>
        private NftImageCreateConfig ParseConfig(string configFile)
        {
            string json = File.ReadAllText(configFile);
            JsonDocument doc = JsonDocument.Parse(json);

            NftImageCreateConfig config = new();

            config.DataFile = doc.RootElement.GetProperty("DataFile").ToString();
            config.OutPath = doc.RootElement.GetProperty("OutPath").ToString();

            foreach (JsonElement el in doc.RootElement.GetProperty("VtImages").EnumerateArray())
            {
                config.VtImages.Add(new VtImage
                {
                    ImageFile = el.GetProperty("ImageFile").ToString(),
                    MinVt = double.Parse(el.GetProperty("MinVt").ToString()),
                    MaxVt = double.Parse(el.GetProperty("MaxVt").ToString())
                });
            }


            Util.Log("CsvFile", config.DataFile);
            Util.Log("OutPath", config.OutPath);
            Util.Log("VtImages", config.VtImages.Count);

            return config;
        }

        private static void PrintHelp()
        {
            Console.WriteLine("사용법)");
            Console.WriteLine("dotnet NftImageCreatorAsync.dll args[0]");
            Console.WriteLine("\targs[0] : config file full path. ex) c:\\temp\\config.json");
            Console.WriteLine("");
            Console.WriteLine("예)");
            Console.WriteLine(@"dotnet NftImageCreatorAsync.dll c:\temp\config.json");

        }
    }
}