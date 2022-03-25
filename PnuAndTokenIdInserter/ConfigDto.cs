using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnuAndTokenIdInserter
{
    internal class ConfigDto
    {
        public string CsvFile { get; set; }
        public string DbHost { get; set; }
        public string DbPort { get; set; }
        public string DbUsername { get; set; }
        public string DbPassword { get; set; }

        public string DbDatabase { get; set; }

        override public string ToString()
        {
            return $"CsvFile: {CsvFile}\tDbHost: {DbHost}\tDbPort: {DbPort}\tDbUsername: {DbUsername}\tDbPassword: {DbPassword}\tDbDatabase: {DbDatabase}";
        }
    }
}
