using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace LabWork28
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = @"C:\";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Текст в формате rtf (*.rtf)|*.rtf|Все файлы (*.*)|*.*";
            TextRange documentTextRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            if (openFileDialog.ShowDialog() == true)
            {
                using FileStream fileStream = File.Open(openFileDialog.FileName, FileMode.Open);
                if (System.IO.Path.GetExtension(openFileDialog.FileName).ToLower() == ".rtf")
                {
                    documentTextRange.Load(fileStream, System.Windows.DataFormats.Rtf);
                }
                else if (System.IO.Path.GetExtension(openFileDialog.FileName).ToLower() == ".txt")
                {
                    documentTextRange.Load(fileStream, System.Windows.DataFormats.Text);
                }
                else
                {
                    documentTextRange.Text = File.ReadAllText(openFileDialog.FileName);
                }
            }
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "Текстовый файл (*.txt)|*.txt|RTF-файл (*.rtf)|*.rtf";
            saveFileDialog.InitialDirectory = path;

            if (saveFileDialog.ShowDialog() == true)
            {
                TextRange documentTextRange = new TextRange(
                    richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

                using FileStream fileStream = File.Create(saveFileDialog.FileName);
                if (System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower() == ".rtf")
                {
                    documentTextRange.Save(fileStream, System.Windows.DataFormats.Rtf);
                }
                else
                {
                    documentTextRange.Save(fileStream, System.Windows.DataFormats.Text);
                }
            }
        }

        private void ColorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new();
            if(colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox.Foreground = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));
            }
        }

        private void FontMenuItem_Click(object sender, RoutedEventArgs e)
        {
            FontDialog fontDialog = new();
            if(fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox.FontFamily = new FontFamily(fontDialog.Font.Name);
                richTextBox.FontSize = fontDialog.Font.Size * 96.0 / 72.0;
                richTextBox.FontWeight = fontDialog.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                richTextBox.FontStyle = fontDialog.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
            }
        }

        private void DefaultFolderMenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new();
            folderBrowserDialog.Description = "Выбирете папку";
            if(folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
