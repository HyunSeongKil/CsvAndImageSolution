using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// 주소 데이터
    /// </summary>
    public class AddressDataDto
    {
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
        public string Bonbun { get; set; }

        /// <summary>
        /// 부번
        /// </summary>
        public string Bubun { get; set; }

        /// <summary>
        /// vt
        /// </summary>
        public int Vt { get; set; }
    }
}
