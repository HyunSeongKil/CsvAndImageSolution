using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// nft image 생성 app용 환경설정
    /// config 파일 양식은 ClassLibrary.Asset.template.config.json 참고
    /// </summary>
    public class NftImageCreateConfig
    {
        /// <summary>
        /// 데이터 파일
        /// </summary>
        public string DataFile { get; set; }

        /// <summary>
        /// 결과 파일 저장 경로
        /// </summary>
        public string OutPath { get; set; }

        /// <summary>
        /// vt image 정보 목록
        /// </summary>

        public List<VtImage> VtImages { get; set; } = new();

        /// <summary>
        /// 동시실행 갯수. default:2
        /// </summary>

        public int AsyncCo { get; set; } = 2;

    }
}
