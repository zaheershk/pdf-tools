using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using System;
using System.Linq;

namespace zs.tools.pdf
{
    class Program
    {
        static void Main(string[] args)
        {
            // validate if input arguments were supplied.
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter the command-line arguments.");
                Console.WriteLine(@"Usage: dotnet pdf-merge.dll C:\result.pdf C:\file1.pdf C:\file2.pdf");
                return;
            }

            MergePdf(args[0], args.Skip(1).ToArray()); //todo - cleanup/validate
        }

        private static void MergePdf(string destFilePath, params string[] srcFilesPath)
        {
            //Initialize PDF document with output intent
            PdfDocument destPdf = new PdfDocument(new PdfWriter(destFilePath));
            PdfMerger merger = new PdfMerger(destPdf);

            foreach (string filePath in srcFilesPath)
            {
                PdfDocument srcPdf = new PdfDocument(new PdfReader(filePath));
                merger.Merge(srcPdf, 1, srcPdf.GetNumberOfPages());
                srcPdf.Close();
            }

            destPdf.Close();
        }
    }
}