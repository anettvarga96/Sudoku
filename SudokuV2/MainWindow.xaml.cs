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
using System.Net.Http;
using System.Text.Json;

namespace SudokuV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Cell[,] cells = new Cell[9, 9];
        DifficultyLevel difficultyLevel;

        Cell selectedCell = null;
        Button numberButton;

        int[] whiteBg = new int[3] { 3, 4, 5 };

        public MainWindow()
        {
            InitializeComponent();
            createGrid();
            drawButtons();
        }

        #region INITIALIZATION
        private void createGrid()
        {

            int[] thickBorder = new int[3] { 0, 3, 6 };
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j] = new Cell();
                    cells[i, j].x = i;
                    cells[i, j].y = j;

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

                    cells[i, j].Click += new RoutedEventHandler(Cell_Button_Click);

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

        #endregion

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            switch (comboBox.SelectedItem.ToString().Split(' ').Last())
            {
                case "Easy":
                    difficultyLevel = DifficultyLevel.Easy;
                    break;
                case "Medium":
                    difficultyLevel = DifficultyLevel.Medium;
                    break;
                case "Hard":
                    difficultyLevel = DifficultyLevel.Hard;
                    break;
            }

        }

        private async void NewGame(object sender, RoutedEventArgs e)
        {
            if (difficultyLevel != 0)
            {
                ClearBoard();

                HttpClient client = new HttpClient();

                string response = await client.GetStringAsync("http://www.cs.utep.edu/cheon/ws/sudoku/new/?size=9&level=" + (int)difficultyLevel);

                SudokuResponse sudokuResponse = JsonSerializer.Deserialize<SudokuResponse>(response);

                if (sudokuResponse.response)
                {
                    foreach (var item in sudokuResponse.squares)
                    {
                        cells[item.x, item.y].value = item.value;
                        cells[item.x, item.y].Content = item.value;
                        cells[item.x, item.y].isStartingCell = true;
                        cells[item.x, item.y].isBlocked = true;
                    }
                }

                SudokuSolver sudokuSolver = new SudokuSolver(cells);
                sudokuSolver.solveSudoku();
            }
            else
                MessageBox.Show("A difficulty level must be selected first!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void ResetGame(object sender, RoutedEventArgs e)
        {
            ClearBoard();
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    if (cells[i, j].isStartingCell)
                    {
                        cells[i, j].Content = cells[i, j].value;
                    }
                }
            }
        }

        private void ShowSolution(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j].Content = cells[i, j].value;
                }
            }
        }


        public void Cell_Button_Click(object sender, RoutedEventArgs e)
        {
            ClearSelection();

            selectedCell = sender as Cell;
            selectedCell.Background = Brushes.LightGray;
        }

        public void Number_Button_Click(object sender, RoutedEventArgs e)
        {
            numberButton = sender as Button;

            if (selectedCell != null && !selectedCell.isBlocked)
            {
                selectedCell.Content = numberButton.Content;

                if (selectedCell.value == (int)selectedCell.Content)
                {
                    selectedCell.isBlocked = true;
                    ClearSelection();
                }

                else
                    selectedCell.Background = Brushes.Red;

            }

            else
            {
                MessageBox.Show("A cell must be selected first!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }



        #region CLEAR METHODS
        private void ClearCell(object sender, RoutedEventArgs e)
        {
            if (selectedCell != null && !selectedCell.isBlocked)
            {
                selectedCell.Clear();
            }

            ClearSelection();
        }

        private void ClearBoard()
        {
            //problem with newgame/resetgame
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    cells[i, j].Clear();
                }
            }

            ClearSelection();
        }        


        private void ClearSelection()
        {
            if (selectedCell != null)
            {
                if (selectedCell.isBlocked || selectedCell.Content.ToString() == "")
                {
                    if (whiteBg.Contains(selectedCell.x) ^ whiteBg.Contains(selectedCell.y))
                    {
                        selectedCell.Background = Brushes.AntiqueWhite;
                    }
                    else
                        selectedCell.Background = Brushes.RosyBrown;
                }
                
                selectedCell = null;
            }
        }

        #endregion
    }
}
