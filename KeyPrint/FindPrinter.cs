using System.Collections.Generic;
using System.Drawing.Printing;


namespace KeyPrint
{
    public class FindPrinter
    {
        private static PrintDocument fPrintDocument = new PrintDocument();

        ///<summary>
        ///获取本地默认打印机名称
        ///</summary>
        public static string DefaultPrinter
        {
            get { return fPrintDocument.PrinterSettings.PrinterName; }
        }


        /// <summary>
        ///  获取本地打印机的列表，第一项就是默认打印机
        /// </summary>
        public static List<string> GetLocalPrinter()
        {
            List<string> fPrinters = new List<string>();
            fPrinters.Add(DefaultPrinter);  //默认打印机出现在列表的第一项
            foreach (string fPrinterName in PrinterSettings.InstalledPrinters)
            {
                if (!fPrinters.Contains(fPrinterName))
                    fPrinters.Add(fPrinterName);
            }
            return fPrinters;
        }
    }
}
