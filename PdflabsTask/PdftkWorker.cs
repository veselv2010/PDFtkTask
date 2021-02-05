using System;

namespace PdflabsTask
{
    public class PdftkWorker
    {
        const string _processName = "pdftk";
        private string _workPath;
        public PdftkWorker(string workPath)
        {
            _workPath = workPath;
        }

        public void SplitPdf(string filename)
        {
            startCmd(filename + " burst output " + _workPath + "/page_%02d.pdf");
        }

        public void JoinPdfs()
        {
            startCmd(_workPath + "\\*.pdf cat output merge.pdf");
        }

        public void PdfToJpges(string filename, bool isRgb)
        {
            string rgbCommand = isRgb ? "RGB" : "LinearGray";
            System.Diagnostics.Process.Start("CMD.exe", $"/C convert -colorspace {rgbCommand} -interlace none -density 300 {filename} image-%02d.jpg");
        }

        private void startCmd(string command)
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C " + _processName + " " + command);
        }
    }
}
