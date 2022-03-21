namespace ClassLibrary
{
    public class Util
    {
        /// <summary>
        /// vt가 계산된 csv파일의 내용을 Dto 목록으로 변환
        /// csv 양식은 ClassLibrary.Asset.template.주소데이터생성용.csv 참고
        /// </summary>
        /// <param name="csvFile"></param>
        /// <returns></returns>
        public static List<CsvDto> ParseCsvFile(string csvFile)
        {
            List<CsvDto> dtos = new();

            string[] lines = File.ReadAllLines(csvFile);

            List<string> x = lines.OrderBy(x => x).ToList();

            for (int i = 1; i < x.Count; i++)
            {
                string line = x[i];

                if (null == line || 0 == line.Trim().Length || line.StartsWith('#'))
                {
                    continue;
                }

                dtos.Add(CsvLineToDto(line));
            }

            return dtos;
        }

        /// <summary>
        /// vt가 계산된 line 정보를 Dto로 변환
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static CsvDto CsvLineToDto(string line)
        {
            string[] arr = line.Split(',');
            //for(int i=0; i<arr.Length; i++)
            //{
            //    Console.WriteLine($"{i}\t{arr[i]}");
            //}

            return new()
            {
                Gbn = arr[0],
                Pnu = arr[1],
                Sd = arr[2],
                Sgg = arr[3],
                Emd = arr[4],
                BonNo = arr[5],
                BuNo = arr[6],
                JinNo = arr[7],
                Pilji = arr[8],
                LandPrice = double.Parse(arr[9]),
                PricePerM2 = double.Parse(arr[10]),
                TotalPrice = double.Parse(arr[11]),
                PiljiCode = arr[12],
                PiljiName = arr[13],
                Jimok = arr[14],
                Vt = double.Parse(arr[15]),
                RefinedVt = int.Parse(arr[16]),
            };
        }
    }
}
