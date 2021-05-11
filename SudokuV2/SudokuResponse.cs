using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuV2
{
    class SudokuResponse
    {

        public bool response { get; set; }
        public string size { get; set; }
        public Cell[] squares { get; set; }

    }
}
