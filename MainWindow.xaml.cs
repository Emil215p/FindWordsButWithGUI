using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using UniqueLengthAmountWords;

namespace FindWordsButWithGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {





            int a, b;
            try
            {
                a = Convert.ToInt32(LetterAmount.Text);
                b = Convert.ToInt32(WordAmount.Text);
            } catch(Exception ex)
            {
                MessageBox.Show("Invalid numbers.");
            }

            ReadSearch.Length = 5;
            ReadSearch.AmountOfWords = 5;
            ReadSearch.Clear();
            ReadSearch.ReadFile(filepath);

            try
            {
                ReadSearch.SearchClever();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            


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
