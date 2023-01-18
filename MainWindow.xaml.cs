using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
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
using UniqueLengthAmountWords;

namespace FindWordsButWithGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Whats your opinion on lasagna? whoever reads this.
    /// </summary>
    public partial class MainWindow : Window
    {

        


        public MainWindow()
        {
            InitializeComponent();
            ReadSearch.progress = Update;

        }

        private void Update(int a, int b)
        {
            double value = (double)((double)((double)a / (double)b) * 100);
            Application.Current.Dispatcher.Invoke(() =>
            {
                BarProgress.Value = value;
            });
        }

        bool running = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (running)
            {
                MessageBox.Show("I am busy");
                return;
            }

            running = true;

            if (filepath == null)
            {
                MessageBox.Show("I need a file.");
                return;
            }



            int a, b;
            try
            {
                a = Convert.ToInt32(LetterAmount.Text);
                b = Convert.ToInt32(WordAmount.Text);
            } catch(Exception ex)
            {
                MessageBox.Show("Invalid numbers.");
                return;
            }
            OutputBox.Text = String.Empty;
            ReadSearch.Length = a;
            ReadSearch.AmountOfWords = b;
            ReadSearch.Clear();
            ReadSearch.ReadFile(filepath);

            Thread t = new Thread(new ThreadStart(Search));

            t.Start();
        }


        private void Search()
        {
            try
            {
                ReadSearch.SearchClever();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ReadSearch.result.ForEach(x => OutputBox.Text += "\n" + x);
                    ResultBox.Text = ReadSearch.result.Count.ToString();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            running = false;

        }
        string filepath;
        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            bool? response = ofd.ShowDialog();
            if (response == true)
            {
                filepath = ofd.FileName;
            }
        }
    }
}
