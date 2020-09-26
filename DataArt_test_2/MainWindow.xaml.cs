using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

namespace DataArt_test_2
{
    /// <summary>
    /// Задание №2
    /// 
    /// Обработка массива
    /// 
    /// Дан массив целых чисел и число K. Необходимо наиболее эффективно способом найти K наибольших
    /// чисел массива в порядке убывания
    /// Входные данные читаются из текстового файла input.txt резульат записывается в output.txt
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private List<int> UseArray;
        private List<int> OutputArray;
        private bool check;
        #endregion

        #region Constructor
        public MainWindow()
        {
            check = false;
            UseArray = new List<int>();
            OutputArray = new List<int>();
            InitializeComponent();
        }
        #endregion

        #region Properties
        public string FilePath { get; set; } = "";
        public string FilePathToSave { get; set; } = "";
        #endregion  

        #region Methods
        private void PrepareCalculate()
        {
            check = false;
            string[] arrayInt;
            UseArray.Clear();
            CalculateArray.Text = "";
            if (EnterArrayTextBox.Text.Length != 0)
            {
                arrayInt = EnterArrayTextBox.Text.Split(' ');
                try
                {
                    foreach (string item in arrayInt.Take(Convert.ToInt32(SizeArrayTextBox.Text)))
                    {
                        UseArray.Add(Convert.ToInt32(item));
                    };
                }
                catch (Exception eEnterArrayTextBox)
                {
                    MessageBoxResult message = MessageBox.Show(eEnterArrayTextBox.Message, "Error", MessageBoxButton.OK);
                    if (message == MessageBoxResult.OK)
                    {
                        check = true;
                    }
                }
                UseArray.Sort();
            }
        }
        private void Calculate()
        {
            if (SizeArrayTextBox.Text.Length != 0)
            {
                try
                {
                    foreach (var item in CalculateMaxInArray(UseArray, Convert.ToInt32(CountFindNumberTextBox.Text)))
                    {
                        CalculateArray.Text = CalculateArray.Text + " " + item;
                    };
                }
                catch (Exception eSizeArrayTextBox)
                {
                    MessageBoxResult message = MessageBox.Show(eSizeArrayTextBox.Message, "Error", MessageBoxButton.OK);
                    if (message == MessageBoxResult.OK)
                    {
                        check = true;
                    }
                }
            }

        }
        private void ReadFromTxt()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text documents (.txt)|*.txt";
                if (openFileDialog.ShowDialog() == true)
                {
                    FilePath = openFileDialog.FileName;
                }
                using (StreamReader sr = new StreamReader(FilePath, System.Text.Encoding.Default))
                {
                    string line;
                    int count = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        count++;
                        switch (count)
                        {
                            case 1:
                                SizeArrayTextBox.Text = line;
                                break;
                            case 2:
                                EnterArrayTextBox.Text = line;
                                break;
                            case 3:
                                CountFindNumberTextBox.Text = line;
                                break;
                        }
                    }
                }
            }
            catch (Exception eReadFromTxt)
            {
                MessageBoxResult message = MessageBox.Show(eReadFromTxt.Message, "Error", MessageBoxButton.OK);
                if (message == MessageBoxResult.OK)
                {
                    check = true;
                }
            }
        }
        private void PathSaveToTxt()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text documents (.txt)|*.txt";
                if (openFileDialog.ShowDialog() == true)
                {
                    FilePathToSave = openFileDialog.FileName;
                }

            }
            catch (Exception eSaveInTxt)
            {
                MessageBoxResult message = MessageBox.Show(eSaveInTxt.Message, "Error", MessageBoxButton.OK);
                if (message == MessageBoxResult.OK)
                {
                    check = true;
                }
            }
        }
        private void SaveToFileTxt()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FilePathToSave, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(CalculateArray.Text);
                }
            }
            catch (Exception eSaveInTxt)
            {
                MessageBoxResult message = MessageBox.Show(eSaveInTxt.Message, "Error", MessageBoxButton.OK);
                if (message == MessageBoxResult.OK)
                {
                    check = true;
                }
            }

        }

        private IEnumerable<int> CalculateMaxInArray(IEnumerable<int> enterArray, int count)
        {
            return enterArray.Skip(enterArray.Count() - count).Take(count).Reverse();
        }
        #endregion

        #region Events
        private void CalculateFromTextBox_Click(object sender, RoutedEventArgs e)
        {
            PrepareCalculate();
            Calculate();
        }

        private void CalculateFromFile_Click(object sender, RoutedEventArgs e)
        {
            PrepareCalculate();
            Calculate();
            SaveToFileTxt();

        }
        #endregion

        private void ChouseInputFile_Click(object sender, RoutedEventArgs e)
        {
            ReadFromTxt();
            ChouseInputFileTextBox.Text = FilePath;
        }

        private void ChouseOutputFile_Click(object sender, RoutedEventArgs e)
        {
            PathSaveToTxt();
            ChouseOutputFileTextBox.Text = FilePathToSave;
        }
    }
}
