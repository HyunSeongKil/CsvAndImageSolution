namespace ClassLibrary
{
    /// <summary>
    /// csv를 dto로 바인드
    /// </summary>
    public class CsvDto
    {
        /// <summary>
        /// 구분. 이벤트토지|지목제외...
        /// </summary>
        public string Gbn { get; set; }

        /// <summary>
        /// pnu
        /// </summary>
        public string Pnu { get; set; }

        /// <summary>
        /// 시도
        /// </summary>
        public string Sd { get; set; }

        /// <summary>
        /// 시군구
        /// </summary>
        public string Sgg { get; set; }

        /// <summary>
        /// 읍면동
        /// </summary>
        public string Emd { get; set; }

        /// <summary>
        /// 본번
        /// </summary>
        public string BonNo { get; set; }

        /// <summary>
        /// 부번
        /// </summary>
        public string BuNo { get; set; }

        /// <summary>
        /// 지번
        /// </summary>
        public string JinNo { get; set; }

        /// <summary>
        /// 필지
        /// </summary>
        public string Pilji { get; set; }

        /// <summary>
        /// 공시지가
        /// </summary>
        public double LandPrice { get; set; }

        /// <summary>
        /// m2당 가격
        /// </summary>
        public double PricePerM2 { get; set; }

        /// <summary>
        /// m2 총가격
        /// </summary>
        public double TotalPrice { get; set; }

        /// <summary>
        /// 필지 코드
        /// </summary>
        public string PiljiCode { get; set; }

        /// <summary>
        /// 필지 명
        /// </summary>
        public string PiljiName { get; set; }

        /// <summary>
        /// 지목
        /// </summary>
        public string Jimok { get; set; }   

        /// <summary>
        /// vt
        /// </summary>
        public double Vt { get; set; }

        /// <summary>
        /// 정제된 vt
        /// </summary>
        public int RefinedVt { get; set; }

    }
}