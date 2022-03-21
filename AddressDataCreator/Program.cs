using ClassLibrary;

namespace AddressDataCreator
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

            List<CsvDto> csvDtos = Util.ParseCsvFile(args[0]);

            // distinct sgg
            ISet<string> sggSet = GetSggSet(csvDtos);


            foreach (string sgg in sggSet.OrderBy(x => x))
            {
                List<CsvDto> subset = GetSubset(csvDtos, sgg);

                Save(subset, sgg, args[1]);
            }
        }

        private static void Save(List<CsvDto> subset, string sgg, string outPath)
        {
            List<string> lines = new()
            {
                "#pnu^시도|시군구|읍면동|본번|부번^refinedVt"
            };

            subset.ForEach(x => { lines.Add($"{x.Pnu}^{x.Sd}|{x.Sgg}|{x.Emd}|{x.BonNo}|{x.BuNo}^{x.RefinedVt}"); });

            File.WriteAllLines(@$"{outPath}\{sgg}.txt", lines);
            Console.WriteLine($"{sgg}\t{lines.Count}");
        }

        private static List<CsvDto> GetSubset(List<CsvDto> csvDtos, string sgg)
        {
            return csvDtos.FindAll(x => x.Sgg.Equals(sgg));
        }

        private static ISet<string> GetSggSet(List<CsvDto> csvDtos)
        {
            ISet<string> set = new HashSet<string>();
            csvDtos.ForEach(x => set.Add(x.Sgg));

            return set;
        }
        private static void PrintHelp()
        {
            Console.WriteLine("사용법) dotnet CsvToNftImageData.dll args[0] args[1]");
            Console.WriteLine("\targs[0] : csv file full path. ex) c:\\temp\\seoul.csv");
            Console.WriteLine("\targs[1] : out path. ex) c:\\temp\\out");
            Console.WriteLine("예)");
            Console.WriteLine("dotnet CsvToNfgImageData.dll c:\\temp\\seoul.csv c:\\temp\\out");
            Console.WriteLine("설명)");
            Console.WriteLine("시군구별로 데이터 파일이 생성됩니다.");
            Console.WriteLine("csv는 refinedVt값이 존재해야 합니다.");
            Console.WriteLine("csv파일의 cell 순서는 CsvDto.cs 파일 참고");

        }
    }

}