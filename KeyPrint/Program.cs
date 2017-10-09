using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HttpRequest;
using KeyPrint;


namespace KeyPrint
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //  MessageBox.Show(Properties.Settings.Default.url); 


            //  MessageBox.Show(m.status);
            //    MessageBox.Show(m.printaaa);
            Circulate ci = new Circulate();
            ci.Forcirculate();

            DoPrint dp = new DoPrint();
          //  dp.startprint();

          //     Application.EnableVisualStyles();
          //   Application.SetCompatibleTextRenderingDefault(false);
           //   Application.Run(new Form1());
        }
    }
   
}
