using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SudokuV2
{
    class Cell : Button
    {
        public int? value { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public bool isBlocked { get; set; }
        public bool isStartingCell { get; set; }

        public void Clear()
        {
            this.Content = "";
            this.isBlocked = false;
        }



    }
}
