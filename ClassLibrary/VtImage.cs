using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class VtImage
    {
        /// <summary>
        /// 이미지 파일
        /// </summary>
        public string ImageFile { get; set; }

        /// <summary>
        /// 최소 vt. inlcude
        /// </summary>
        public double MinVt { get; set; }

        /// <summary>
        /// 최대 vt. exclude
        /// </summary>
        public double MaxVt { get; set; }
    }
}
