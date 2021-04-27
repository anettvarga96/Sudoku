using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Cell selectedCell = null;
        NumberButton numberButton;

        int[] whiteBg = new int[3] { 3, 4, 5 };

        public MainWindow()
        {
            InitializeComponent();
            createGrid();
            drawButtons();
        }

        private void createGrid()
        {
            Cell[,] cells = new Cell[9, 9];


            
            int[] thickBorder = new int[3] { 0, 3, 6 };
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j] = new Cell();
                    cells[i, j].X = i;
                    cells[i, j].Y = j;

                    cells[i, j].Height = 40;
                    cells[i, j].Width = 40;

                    if (whiteBg.Contains(i) ^ whiteBg.Contains(j))
                    {
                        cells[i, j].Background = Brushes.AntiqueWhite;
                    }
                    else
                        cells[i, j].Background = Brushes.RosyBrown;

                    cells[i, j].Margin = new Thickness(i * 40, j * 40, 0, 0);
                    cells[i, j].FontSize = 20;


                    if (thickBorder.Contains(i))
                    {
                        cells[i, j].BorderThickness = new Thickness(3, 1, 1, 1);
                    }
                    if (thickBorder.Contains(j))
                    {
                        cells[i, j].BorderThickness = new Thickness(1, 3, 1, 1);
                    }

                    if (thickBorder.Contains(i) && thickBorder.Contains(j))
                    {
                        cells[i, j].BorderThickness = new Thickness(3, 3, 1, 1);
                    }


                    if (i == 8)
                    {
                        cells[i, j].BorderThickness = new Thickness(1, 1, 3, 1);
                    }
                    if (j == 8)
                    {
                        cells[i, j].BorderThickness = new Thickness(1, 1, 1, 3);
                    }

                    if (thickBorder.Contains(i) && j == 8)
                    {
                        cells[i, j].BorderThickness = new Thickness(3, 1, 1, 3);
                    }
                    if (thickBorder.Contains(j) && i == 8)
                    {
                        cells[i, j].BorderThickness = new Thickness(1, 3, 3, 1);
                    }


                    if (i == 8 && j == 8)
                    {
                        cells[i, j].BorderThickness = new Thickness(1, 1, 3, 3);
                    }

                    cells[i, j].Click += new RoutedEventHandler(this.Cell_Button_Click);

                    myCanvas.Children.Add(cells[i,j]);
                }
            }
        }
        private void drawButtons()
        {
            NumberButton[] buttons = new NumberButton[9];

            for (int i = 0; i < 9; i++)
            {
                buttons[i] = new NumberButton();
                buttons[i].Content = i + 1;
                buttons[i].Margin = new Thickness(i * 40, 0, 0, 0);
                buttons[i].BorderThickness = new Thickness(1);

                buttons[i].Click += new RoutedEventHandler(this.Number_Button_Click);


                numbers.Children.Add(buttons[i]);
            }
        }


        public void Cell_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCell != null)
            {
                if (whiteBg.Contains(selectedCell.X) ^ whiteBg.Contains(selectedCell.Y))
                {
                    selectedCell.Background = Brushes.AntiqueWhite;
                }
                else
                    selectedCell.Background = Brushes.RosyBrown;

                selectedCell = null;
            }
            
            selectedCell = sender as Cell;
            selectedCell.Background = Brushes.LightGray;
        }        
        
        public void Number_Button_Click(object sender, RoutedEventArgs e)
        {
            numberButton = sender as NumberButton;
            selectedCell.Content = numberButton.Content;

        }
    }
}
