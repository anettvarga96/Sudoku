using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SudokuV2
{
    class Cell : Button
    {
        public int Value { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool isStartingCell = false;

        public void Clear()
        {
            this.Content = "";
        }

    }
}
