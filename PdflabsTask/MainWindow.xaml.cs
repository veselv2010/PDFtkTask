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
using System.Windows.Forms;

namespace PdflabsTask
{
    public partial class MainWindow : Window
    {
        private string _pdfPath;
        private string _outputPath;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectPdfButton_Click(object sender, RoutedEventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                var res = fileDialog.ShowDialog();

                if (res == System.Windows.Forms.DialogResult.Cancel)
                    return;

                string file = fileDialog.FileName;

                _pdfPath = file;
                _outputPath = System.IO.Path.GetDirectoryName(file);

                SelectedPdfPathBox.Text = file;
                OutputPathBox.Text = _outputPath;
            }
        }

        private void OutputPathButton_Click(object sender, RoutedEventArgs e)
        {
            using (var fileDialog = new FolderBrowserDialog())
            {
                var res = fileDialog.ShowDialog();

                if (res == System.Windows.Forms.DialogResult.Cancel)
                    return;

                string outputPath = fileDialog.SelectedPath;

                _outputPath = outputPath;
                OutputPathBox.Text = outputPath;
            }
        }

        private void PdfToPagesButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_outputPath))
            {
                System.Windows.MessageBox.Show("Output is empty");
                return;
            }

            var pdfWorker = new PdftkWorker(_outputPath);
            pdfWorker.SplitPdf(_pdfPath);
        }

        private void MergePdfsButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_outputPath))
            {
                System.Windows.MessageBox.Show("Output is empty");
                return;
            }

            var pdfWorker = new PdftkWorker(_outputPath);
            pdfWorker.JoinPdfs();
        }

        private void PdfToJpegButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_outputPath))
            {
                System.Windows.MessageBox.Show("Output is empty");
                return;
            }

            var pdfWorker = new PdftkWorker(_outputPath);
            pdfWorker.PdfToJpges(_pdfPath, MonochromeCheck.IsChecked.Value);
        }
    }
}
