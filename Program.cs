using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfMultiPartSplit
{
    class Program
    {
        static void Main(string[] args)
        {
            var welcomeMessage = "Hello Hari. This is a sample PDF compression reader.";
            var filePath = @"C:\Users\BALAKH\Desktop\Organise this\Pdfs\codpaste-teachingpack.pdf";

            Console.WriteLine(welcomeMessage);

            GetFileSize(filePath);
            MultipartPdfSplit(filePath);

            Console.ReadLine();
        }

        private static void MultipartPdfSplit(string pdfFilePath)
        {
            var outputPath = @"C:\Users\BALAKH\Desktop\PdfTest\SplittedPdfFiles";
            var pageNameSuffix = 0;
            var reader = new PdfReader(pdfFilePath);
            var file = new FileInfo(pdfFilePath);
            var pdfFileName = file.Name.Substring(0, file.Name.LastIndexOf(".")) + " - Part ";


            for (var pageNumber = 1; pageNumber <= reader.NumberOfPages; pageNumber++)
            {
                pageNameSuffix++;
                var newPdfFileName = string.Format(pdfFileName + "{0}", pageNameSuffix);
                SplitPageSave(pdfFilePath, outputPath, pageNumber, newPdfFileName);
            }
        }


        private static void GetFileSize(string pdfFilePath)
        {
            var file = new FileInfo(pdfFilePath);
            var size = file.Length;

            var index = 0;
            for (; size > 1024; index++)
                size /= 1024;

            var sizeInfo = size.ToString("0" + new[] { "B", "KB", "MB", "GB", "TB" }[index]);
            Console.WriteLine(sizeInfo);
        }

        private static void SplitPageSave(string pdfFilePath, string outputPath, int page, string pdfFileName)
        {
            using (var reader = new PdfReader(pdfFilePath))
            {
                var document = new Document();
                var copy = new PdfCopy(document, new FileStream(outputPath + "\\" + pdfFileName + ".pdf", FileMode.Create));
                document.Open();
                copy.AddPage(copy.GetImportedPage(reader, page));
                document.Close();
            }
        }
    }
}