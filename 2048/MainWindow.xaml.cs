using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace _2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<Label> tilesListLabels = new List<Label>(); // interface tiles list
        static List<BigInteger> tilesListBigInteger = new List<BigInteger>(); // logic tiles list
        BigInteger score = new BigInteger(); // Current score
        ScoreTable st = new ScoreTable(); // Best score
        int rows = 8; // rows count
        int cols = 8; // colums count
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            CreateGrid();
            CreateTiles();
            Restart();        
        }
        /// <summary>
        /// Refresh all variables
        /// </summary>
        public void Restart()
        {
            score = 0;
            st = ScoreTable.LoadBestScore();

            for (int i = 0; i < rows * cols; i++)
            {
                tilesListBigInteger[i] = 0;
            }
            GenerateNewTile();
            GenerateNewTile();
            InterfaceUpdate();
        }
        /// <summary>
        /// Creating grid for field
        /// </summary>
        private void CreateGrid()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    ColumnDefinition columDef = new ColumnDefinition();
                    RowDefinition rowDef = new RowDefinition();
                    double tileWidth = 300.0 / rows;
                    double tileHeight = 300.0 / cols;
                    columDef.Width = new GridLength(tileWidth);
                    rowDef.Height = new GridLength(tileHeight);
                    gridField.ColumnDefinitions.Add(columDef);
                    gridField.RowDefinitions.Add(rowDef);
                }
            }
        }
        /// <summary>
        /// Creating tiles for field
        /// </summary>
        public void CreateTiles()
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    tilesListBigInteger.Add(0);
                    Label tile = new Label()
                    {
                        Margin = new Thickness(1),
                        FontSize = 16,
                        FontWeight = FontWeights.Bold,
                        Content = "",
                        VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                        HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                    };
                    Grid.SetColumn(tile, x);
                    Grid.SetRow(tile, y);

                    tilesListLabels.Add(tile);
                    gridField.Children.Add(tile);
                }
            }
        }
        /// <summary>
        /// Create tile in random place
        /// </summary>
        private void GenerateNewTile()
        {
            Random rnd = new Random();
            int tileIndex = 0;
            int zeroTiles = 0;
            for (int i = 0; i < rows * cols; i++)
            {
                if (tilesListBigInteger[i] == 0)
                {
                    zeroTiles++;
                }                
            }
            if (zeroTiles == 0)
            {
                GameOver();
            }   
            zeroTiles = 0;
            do
            {
                tileIndex = rnd.Next(rows * cols);
            } while (tilesListBigInteger[tileIndex] != 0);

            if (rnd.Next(10) == 0)
            {
                tilesListBigInteger[tileIndex] = 4;
            }
            else
            {
                tilesListBigInteger[tileIndex] = 2;
            }
        }
        /// <summary>
        /// End game dialog
        /// </summary>
        private void GameOver()
        {
            ScoreTable scoreTable = new ScoreTable();
            ScoreTable bestScore = new ScoreTable();
            bestScore = ScoreTable.LoadBestScore();
            scoreTable.BestScore = (int)score;

            if (scoreTable.BestScore > bestScore.BestScore)
            {
                ScoreTable.SaveBestScore(scoreTable);
            }
            MessageBox.Show("Low skill, bad luck, so much fails");
            Restart();
            return;
        }
        /// <summary>
        /// Refresh interface
        /// </summary>
        private void InterfaceUpdate()
        {
            for (int i = 0; i < rows * cols; i++)
            {
                if (tilesListBigInteger[i] != 0)
                {
                    tilesListLabels[i].Content = tilesListBigInteger[i];
                }
                else tilesListLabels[i].Content = "";
                string strNumber = Convert.ToString(tilesListBigInteger[i]);
                tilesListLabels[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                switch (strNumber)
                {
                    case "0":
                        {
                            tilesListLabels[i].Background = new SolidColorBrush(Color.FromRgb(200, 200, 200));
                            tilesListLabels[i].FontSize = 16;
                            break;
                        } 
                    case "2":
                        {
                            tilesListLabels[i].Background = new SolidColorBrush(Color.FromRgb(240, 230, 220));
                            tilesListLabels[i].Foreground = new SolidColorBrush(Color.FromRgb(110, 110, 110));
                            tilesListLabels[i].FontSize = 16;
                            break;
                        }
                    case "4":
                        {
                            tilesListLabels[i].Background = new SolidColorBrush(Color.FromRgb(240, 220, 200));
                            tilesListLabels[i].Foreground = new SolidColorBrush(Color.FromRgb(110, 110, 110));
                            tilesListLabels[i].FontSize = 16;
                            break;
                        }
                    case "8":
                        {
                            tilesListLabels[i].Background = new SolidColorBrush(Color.FromRgb(240, 180, 120));
                            tilesListLabels[i].FontSize = 16;
                            break;
                        }
                    case "16":
                        {
                            tilesListLabels[i].Background = new SolidColorBrush(Color.FromRgb(240, 150, 100));
                            tilesListLabels[i].FontSize = 16;
                            break;
                        }
                    case "32":
                        {
                            tilesListLabels[i].Background = new SolidColorBrush(Color.FromRgb(240, 120, 80));
                            tilesListLabels[i].FontSize = 16;
                            break;
                        }
                    case "64":
                        {
                            tilesListLabels[i].Background = new SolidColorBrush(Color.FromRgb(240, 90, 60));
                            tilesListLabels[i].FontSize = 16;
                            break;
                        }
                    case "128":
                        {
                            tilesListLabels[i].Background = new SolidColorBrush(Color.FromRgb(240, 200, 120));
                            tilesListLabels[i].FontSize = 14;
                            break;
                        }
                    case "256":
                        {
                            tilesListLabels[i].Background = new SolidColorBrush(Color.FromRgb(240, 200, 100));
                            tilesListLabels[i].FontSize = 14;
                            break;
                        }
                    case "512":
                        {
                            tilesListLabels[i].Background = new SolidColorBrush(Color.FromRgb(240, 200, 80));
                            tilesListLabels[i].FontSize = 14;
                            break;
                        }
                    case "1024":
                        {
                            tilesListLabels[i].Background = new SolidColorBrush(Color.FromRgb(240, 200, 60));
                            tilesListLabels[i].FontSize = 10;
                            break;
                        }
                    case "2048":
                        {
                            tilesListLabels[i].Background = new SolidColorBrush(Color.FromRgb(240, 200, 40));
                            tilesListLabels[i].FontSize = 10;
                            break;
                        }
                    default:
                        {
                            tilesListLabels[i].Background = new SolidColorBrush(Color.FromRgb(60, 60, 50));
                            tilesListLabels[i].FontSize = 8;
                            break;
                        }
                }
                labelScore.Content = score;
                labelBestScore.Content = st.BestScore;
            }
        }
        /// <summary>
        /// Game logic (move pressed)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.G:
                    {
                        //Testing key (generate more tiles)
                        GenerateNewTile();
                        break;
                    }
                case Key.Up:
                    {
                        for (int i = 0; i < rows * cols; i++)
                        {
                            try
                            {
                                while (tilesListBigInteger[i - rows] == 0 && tilesListBigInteger[i] != 0)
                                {
                                    tilesListBigInteger[i - rows] = tilesListBigInteger[i];
                                    tilesListBigInteger[i] = 0;
                                    i -= rows;
                                }
                                //// maximal concat
                                //while (tilesListBigInteger[i - rows] == tilesListBigInteger[i] && tilesListBigInteger[i] != 0)
                                //{
                                //    tilesListBigInteger[i - rows] += tilesListBigInteger[i];
                                //    tilesListBigInteger[i] = 0;
                                //    i -= rows;
                                //}
                                if (tilesListBigInteger[i - rows] == tilesListBigInteger[i])
                                {
                                    tilesListBigInteger[i - rows] += tilesListBigInteger[i];
                                    tilesListBigInteger[i] = 0;
                                    score += tilesListBigInteger[i - rows];
                                }
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                //nobody cares :)
                            }
                        }
                        break;
                    }
                case Key.Down:
                    {
                        for (int i = rows * cols - 1; i >= 0; i--)
                        {
                            try
                            {
                                while (tilesListBigInteger[i + rows] == 0 && tilesListBigInteger[i] != 0)
                                {
                                    tilesListBigInteger[i + rows] = tilesListBigInteger[i];
                                    tilesListBigInteger[i] = 0;
                                    i += rows;
                                }
                                if (tilesListBigInteger[i + rows] == tilesListBigInteger[i])
                                {
                                    tilesListBigInteger[i + rows] += tilesListBigInteger[i];
                                    tilesListBigInteger[i] = 0;
                                    score += tilesListBigInteger[i + rows];
                                }
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                //nobody cares
                            }
                        }
                        break;
                    }
                case Key.Left:
                    {
                        for (int i = 0; i < rows * cols; i++)
                        {
                            try
                            {
                                while (tilesListBigInteger[i - 1] == 0 && tilesListBigInteger[i] != 0 && i % rows != 0)
                                {
                                    tilesListBigInteger[i - 1] = tilesListBigInteger[i];
                                    tilesListBigInteger[i] = 0;
                                    i--;
                                }
                                if (tilesListBigInteger[i - 1] == tilesListBigInteger[i])
                                {
                                    tilesListBigInteger[i - 1] += tilesListBigInteger[i];
                                    tilesListBigInteger[i] = 0;
                                    score += tilesListBigInteger[i - 1];
                                }
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                //nobody cares
                            }
                        }
                        break;
                    }
                case Key.Right:
                    {
                        for (int i = rows * cols - 1; i >= 0; i--)
                        {
                            try
                            {
                                while (tilesListBigInteger[i + 1] == 0 && tilesListBigInteger[i] != 0 && i % rows != rows - 1)
                                {
                                    tilesListBigInteger[i + 1] = tilesListBigInteger[i];
                                    tilesListBigInteger[i] = 0;
                                    i++;
                                }
                                if (tilesListBigInteger[i + 1] == tilesListBigInteger[i])
                                {
                                    tilesListBigInteger[i + 1] += tilesListBigInteger[i];
                                    tilesListBigInteger[i] = 0;
                                    score += tilesListBigInteger[i + 1];
                                }
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                //nobody cares
                            }
                        }
                        break;
                    }
                default:
                    return;
            }
            GenerateNewTile();
            InterfaceUpdate();
        }
        /// <summary>
        /// Restart button action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelRestart_Click(object sender, RoutedEventArgs e)
        {
            Restart();
        }
        /// <summary>
        /// Close button action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelClose_Click(object sender, RoutedEventArgs e)
        {
            GameOver();
            Close();
        }
    }
}
