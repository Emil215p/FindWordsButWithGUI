using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
using System.Xml.Serialization;
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
            ReadSearch.combinationFound += Combination;

        }

        private async void Update(int a, int b)
        {
            Thread.Sleep(10);
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                BarProgress.Value = (double)((double)((double)a / (double)b) * 100);
            });
        }

        private async void Combination(string str)
        {

            Thread.Sleep(10);

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                //OutputBox.Text += ("\n" + str);
                ResultBox.Text = (++amount).ToString();
            });
        }

        bool running = false;
        int amount = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (running)
            {
                MessageBox.Show("I am busy");
                return;
            }
            if (filepath == null)
            {
                MessageBox.Show("I need a file.");
                return;
            }

            int wordsLength, wordNumber;
            try
            {
                wordsLength = Convert.ToInt32(LetterAmount.Text);
                wordNumber = Convert.ToInt32(WordAmount.Text);
            } catch(Exception ex)
            {
                MessageBox.Show("Invalid numbers.");
                return;
            }

            running = true;

            ReadSearch.Length = wordsLength;
            ReadSearch.AmountOfWords = wordNumber;
            ReadSearch.Clear();
            ReadSearch.ReadFile(filepath);

            Thread t = new Thread(new ThreadStart(Search));

            t.Name = "ReadSeachManager";
            t.Start();
        }


        private async void Search()
        {
            try
            {
                await Task.Run(ReadSearch.SearchClever);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            running = false;
            amount = 0;

            Application.Current.Dispatcher.Invoke(() =>
            {
                BarProgress.Value = 0;
            });
           
        }
        string filepath;
        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            bool? response = ofd.ShowDialog();
            if (response == true)
            {
                filepath = ofd.FileName;
                FileSelectBox.Text = ofd.FileName.Substring(ofd.FileName.LastIndexOf("\\") + 1);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = @"Output.txt";
            bool? response = sfd.ShowDialog();
            if (response == true)
            {
                filepath = sfd.FileName;
                Save(sfd.FileName);
            }
        }

        private void Save(string path)
        {
            try
            {

                TextWriter tw = new StreamWriter(path);
                    
                foreach (String s in ReadSearch.result)
                {
                    tw.WriteLine(s);
                }
            
                tw.Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
