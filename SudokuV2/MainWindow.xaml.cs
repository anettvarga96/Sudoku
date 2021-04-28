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
        Cell[,] cells = new Cell[9, 9];
        private int[,] solution = new int[9, 9];

        Cell selectedCell = null;
        Button numberButton;

        int[] whiteBg = new int[3] { 3, 4, 5 };

        public MainWindow()
        {
            InitializeComponent();
            createGrid();
            drawButtons();
            createSolution();
        }

        private void createGrid()
        {

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

                    myCanvas.Children.Add(cells[i, j]);
                }
            }
        }
        private void drawButtons()
        {
            Button[] buttons = new Button[9];

            for (int i = 0; i < 9; i++)
            {
                buttons[i] = new Button();
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
            numberButton = sender as Button;

            if ((int)numberButton.Content != solution[selectedCell.X, selectedCell.Y])
            {
                selectedCell.Background = Brushes.DarkRed;
            }
            selectedCell.Content = numberButton.Content;

        }

        private void ClearCell(object sender, RoutedEventArgs e)
        {
            selectedCell.Clear();
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            foreach (var cell in cells)
            {
                cell.Clear();
            }

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
        }

        private void ShowSolution(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j].Content = solution[i, j];
                }
            }
        }
        
        private void createSolution()
        {      
            solution[4, 0] = solution[1, 1] = solution[8, 2] = solution[0, 3] = solution[6, 4] = solution[5, 5] = solution[7, 6] = solution[3, 7] = solution[2, 8] = 1;
            solution[0, 0] = solution[8, 1] = solution[3, 2] = solution[2, 3] = solution[7, 4] = solution[4, 5] = solution[5, 6] = solution[1, 7] = solution[6, 8] = 2;
            solution[7, 0] = solution[2, 1] = solution[4, 2] = solution[5, 3] = solution[8, 4] = solution[1, 5] = solution[0, 6] = solution[6, 7] = solution[3, 8] = 3;
            solution[2, 0] = solution[6, 1] = solution[5, 2] = solution[3, 3] = solution[0, 4] = solution[7, 5] = solution[1, 6] = solution[8, 7] = solution[4, 8] = 4;
            solution[1, 0] = solution[4, 1] = solution[7, 2] = solution[6, 3] = solution[3, 4] = solution[0, 5] = solution[2, 6] = solution[5, 7] = solution[8, 8] = 5;
            solution[6, 0] = solution[5, 1] = solution[0, 2] = solution[1, 3] = solution[4, 4] = solution[8, 5] = solution[3, 6] = solution[2, 7] = solution[7, 8] = 6;
            solution[3, 0] = solution[7, 1] = solution[2, 2] = solution[4, 3] = solution[1, 4] = solution[6, 5] = solution[8, 6] = solution[0, 7] = solution[5, 8] = 7;
            solution[8, 0] = solution[3, 1] = solution[1, 2] = solution[7, 3] = solution[5, 4] = solution[2, 5] = solution[6, 6] = solution[4, 7] = solution[0, 8] = 8;
            solution[5, 0] = solution[0, 1] = solution[6, 2] = solution[8, 3] = solution[2, 4] = solution[3, 5] = solution[4, 6] = solution[7, 7] = solution[1, 8] = 9;
        }
    }
}
