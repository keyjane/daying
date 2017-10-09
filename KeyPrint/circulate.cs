using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using HttpRequest;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using KeyPrint.Properties;

namespace KeyPrint
{
    public class Circulate
    {
        public void Forcirculate()
        {
            try
            {
               // new Thread();

                while (true) {
                    Http hp = new Http();
                    // Product m = JsonConvert.DeserializeObject<Product>(hp.HttpRequest());
                    //MessageBox.Show(hp.HttpRequest());
                    if (hp.HttpRequest() != "none")
                    {
                        //传值打印机
                        DoPrint DoP = new DoPrint();
                        DoP.startprint(hp.HttpRequest());
                    }                 
                  //  int res = m.menu.Length;
                  // res.ToString;
                  // string s1 = res.ToString();
                  //   MessageBox.Show(s1);

                      Thread.Sleep(5000);
                }

            }
            catch (WebException e)
            {
            
            }
        }           
    }
    public class DoPrint
    {
        public static string print_title_words ;
        public void startprint(string m)
        {
            

            Product mm = JsonConvert.DeserializeObject<Product>(m);

            Settings.Default.PrintTemp = m;
            Settings.Default.Save();




            //循环打印机
            Product print_class = JsonConvert.DeserializeObject<Product>(m);
            for (int a = 0; a < print_class.print.Length; a++) {
                //实例化打印对象  
                PrintDocument printDocument1 = new PrintDocument();

                if (print_class.print[a][0]=="2") {
                    //选择打印机
                    printDocument1.PrinterSettings.PrinterName = print_class.print[a][1];
                }           
               
                //设置打印用的纸张,当设置为Custom的时候，可以自定义纸张的大小  
                printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custum", 200, 500);

                if (print_class.print[a][2] == "1")
                {
                    //注册PrintPage事件，打印每一页时会触发该事件 
                    print_title_words = print_class.print[a][3];
                    printDocument1.PrintPage += new PrintPageEventHandler(this.PrintDocument_PrintPage_cf);
                }
                else {
                    print_title_words = print_class.print[a][3];
                    printDocument1.PrintPage += new PrintPageEventHandler(this.PrintDocument_PrintPage_qt);
                }
               
                //初始化打印预览对话框对象  
                PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
                //将printDocument1对象赋值给打印预览对话框的Document属性  
                printPreviewDialog1.Document = printDocument1;
                //直接开始打印
                //  printDocument1.Print();//开始打印
                
             

                //打开打印预览对话框 
                DialogResult result = printPreviewDialog1.ShowDialog();
                if (result == DialogResult.OK) {
                    printDocument1.Print();//开始打印

                    //打印后推送给服务器
                    Http hp = new Http();
                    string url = Settings.Default.url;
                    //   MessageBox.Show(url + "?printid=" + print_class.id);
                    hp.HttpRequestPost(url + "?printid=" + print_class.id);
                }               
            }       

        }
        private void PrintDocument_PrintPage_cf(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            StringFormat geshi = new StringFormat();
            geshi.Alignment = StringAlignment.Center; //居中
            // key.Alignment = StringAlignment.Far; //右对齐
            string content = print_title_words;
            Rectangle square = new Rectangle(0, 20, 200, 200); //矩形框
            Font fontkey = new Font("宋体", 10.5F); //字体
            Brush colour = Brushes.Black;  //颜色
            e.Graphics.DrawString(content, fontkey, colour, square, geshi);

            string content_menu = "序号    名称     单价   数量";
            Rectangle square1 = new Rectangle(0, 50, 200, 200); //矩形框
            Font fontkey1 = new Font("楷体", 8, FontStyle.Bold); //字体    
            e.Graphics.DrawString(content_menu, fontkey1, colour, square1, geshi);

            string content_hg = "---------------------------";
            Rectangle square2 = new Rectangle(0, 61, 200, 200); //矩形框
            Font content_fon = new Font("宋体", 10); //字体 
            e.Graphics.DrawString(content_hg, content_fon, colour, square2, geshi);
         
            //得到打印数据
            String menu_for_print = Settings.Default.PrintTemp;
            Product class_menu  = JsonConvert.DeserializeObject<Product>(menu_for_print);

         ///    MessageBox.Show(class_menu.menu[0][0]);
            //打印间隔高
            int high_print;
            for (int a = 0; a < class_menu.menu.Length; a ++)
            {
                //  MessageBox.Show(class_menu.menu[a][0]);
                high_print = 60 + (a + 1) * 16;

                Rectangle square_for = new Rectangle(0, high_print, 152, 200); //矩形框
                e.Graphics.DrawString(class_menu.menu[a][0], content_fon, colour, square_for, geshi);

                Rectangle square_for_num = new Rectangle(0, high_print, 60, 200); //矩形框序号
                e.Graphics.DrawString((a+1).ToString(), content_fon, colour, square_for_num, geshi);

                Rectangle square_for_price = new Rectangle(0, high_print, 255, 200); //矩形框金额
                e.Graphics.DrawString(class_menu.menu[a][1], content_fon, colour, square_for_price, geshi);

                Rectangle square_for_count = new Rectangle(0, high_print, 335, 200); //矩形框数量
                e.Graphics.DrawString(class_menu.menu[a][2], content_fon, colour, square_for_count, geshi);
            }
            //   e.Graphics.DrawString(GetPrintSW().ToString(), fontkey, colour, square1, geshi);

            //下横杠
            Rectangle square_hg1 = new Rectangle(0, 60+(class_menu.menu.Length+1)*16, 200, 200); //矩形框 
            e.Graphics.DrawString(content_hg, content_fon, colour, square_hg1, geshi);
        }
        private void PrintDocument_PrintPage_qt(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            StringFormat geshi = new StringFormat();
            geshi.Alignment = StringAlignment.Center; //居中
            // key.Alignment = StringAlignment.Far; //右对齐
            string content = print_title_words;
            Rectangle square = new Rectangle(0, 20, 200, 200); //矩形框
            Font fontkey = new Font("宋体", 10.5F); //字体
            Brush colour = Brushes.Black;  //颜色
            e.Graphics.DrawString(content, fontkey, colour, square, geshi);

            string content_menu = "序号    名称     单价   数量";
            Rectangle square1 = new Rectangle(0, 50, 200, 200); //矩形框
            Font fontkey1 = new Font("楷体", 8, FontStyle.Bold); //字体    
            e.Graphics.DrawString(content_menu, fontkey1, colour, square1, geshi);

            string content_hg = "---------------------------";
            Rectangle square2 = new Rectangle(0, 61, 200, 200); //矩形框
            Font content_fon = new Font("宋体", 10); //字体 
            e.Graphics.DrawString(content_hg, content_fon, colour, square2, geshi);

            //得到打印数据
            String menu_for_print = Settings.Default.PrintTemp;
            Product class_menu = JsonConvert.DeserializeObject<Product>(menu_for_print);

            ///    MessageBox.Show(class_menu.menu[0][0]);
            //打印间隔高
            int high_print;
            for (int a = 0; a < class_menu.menu.Length; a++)
            {
                //  MessageBox.Show(class_menu.menu[a][0]);
                high_print = 60 + (a + 1) * 16;

                Rectangle square_for = new Rectangle(0, high_print, 152, 200); //矩形框
                e.Graphics.DrawString(class_menu.menu[a][0], content_fon, colour, square_for, geshi);

                Rectangle square_for_num = new Rectangle(0, high_print, 60, 200); //矩形框序号
                e.Graphics.DrawString((a + 1).ToString(), content_fon, colour, square_for_num, geshi);

                Rectangle square_for_price = new Rectangle(0, high_print, 255, 200); //矩形框金额
                e.Graphics.DrawString(class_menu.menu[a][1], content_fon, colour, square_for_price, geshi);

                Rectangle square_for_count = new Rectangle(0, high_print, 335, 200); //矩形框数量
                e.Graphics.DrawString(class_menu.menu[a][2], content_fon, colour, square_for_count, geshi);
            }
            //   e.Graphics.DrawString(GetPrintSW().ToString(), fontkey, colour, square1, geshi);

            //下横杠
            Rectangle square_hg1 = new Rectangle(0, 60 + (class_menu.menu.Length + 1) * 16, 200, 200); //矩形框 
            e.Graphics.DrawString(content_hg, content_fon, colour, square_hg1, geshi);
            //小计
            // 小计￥190
            string content_menu_xj = "小计";
            Rectangle square_xj = new Rectangle(0, 70 + (class_menu.menu.Length + 1) * 16, 70, 200); //矩形框
            Font fontkey_xj = new Font("宋体", 10); //字体   
            e.Graphics.DrawString(content_menu_xj, fontkey_xj, colour, square_xj, geshi);

            // 小计￥190
            string content_menu_xj_money = "￥"+ class_menu.xj;
            Rectangle square_xj_money = new Rectangle(0, 70 + (class_menu.menu.Length + 1) * 16, 300, 200); //矩形框
            e.Graphics.DrawString(content_menu_xj_money, fontkey_xj, colour, square_xj_money, geshi);

            //小计下横杠
            Rectangle square_xjhg = new Rectangle(0, 80 + (class_menu.menu.Length + 1) * 16, 200, 200); //矩形框 
            e.Graphics.DrawString(content_hg, content_fon, colour, square_xjhg, geshi);

            //附件相关
            string content_menu_fj = "序号    名称     单价   数量";
            Rectangle square_fj = new Rectangle(0, 92 + (class_menu.menu.Length + 1) * 16, 200, 200); //矩形框
         //   Font fontkey1 = new Font("楷体", 8, FontStyle.Bold); //字体    
            e.Graphics.DrawString(content_menu_fj, fontkey1, colour, square_fj, geshi);
            Rectangle square_fj_hg = new Rectangle(0, 100 + (class_menu.menu.Length + 1) * 16, 200, 200); //矩形框
            e.Graphics.DrawString(content_hg, content_fon, colour, square_fj_hg, geshi);

            //附件打印间隔高
            int high_print_fj;
            for (int a = 0; a < class_menu.fu.Length; a++)
            {
                //  MessageBox.Show(class_menu.menu[a][0]);
                high_print_fj = 100 + (class_menu.menu.Length + 1) * 16 + (a + 1) * 16;

                Rectangle square_for = new Rectangle(0, high_print_fj, 152, 200); //矩形框
                e.Graphics.DrawString(class_menu.fu[a][0], content_fon, colour, square_for, geshi);

                Rectangle square_for_num = new Rectangle(0, high_print_fj, 60, 200); //矩形框序号
                e.Graphics.DrawString((a + 1).ToString(), content_fon, colour, square_for_num, geshi);

                Rectangle square_for_price = new Rectangle(0, high_print_fj, 255, 200); //矩形框金额
                e.Graphics.DrawString(class_menu.fu[a][1], content_fon, colour, square_for_price, geshi);

                Rectangle square_for_count = new Rectangle(0, high_print_fj, 335, 200); //矩形框数量
                e.Graphics.DrawString(class_menu.fu[a][2], content_fon, colour, square_for_count, geshi);

               //附件下横杠
                Rectangle square_fj_xia = new Rectangle(0, 100 + (class_menu.menu.Length + class_menu.fu.Length +2 ) * 16, 200, 200); //矩形框 
                e.Graphics.DrawString(content_hg, content_fon, colour, square_fj_xia, geshi);

                string content_menu_gj = "合计";
                Rectangle square_gj = new Rectangle(0, 110 + (class_menu.menu.Length + class_menu.fu.Length + 2) * 16,70, 200); //矩形框
               // Font fontkey_xj = new Font("宋体", 10); //字体   
                e.Graphics.DrawString(content_menu_gj, fontkey_xj, colour, square_gj, geshi);

                string content_menu_gj_money = "￥" + class_menu.hj;
                Rectangle square_gj_money = new Rectangle(0, 110 + (class_menu.menu.Length + class_menu.fu.Length + 2) * 16, 300, 200); //矩形框
                                                                                                                                  // Font fontkey_xj = new Font("宋体", 10); //字体   
                e.Graphics.DrawString(content_menu_gj_money, fontkey_xj, colour, square_gj_money, geshi);

            }
        }
      

    }
    public class Product
    {
        public string status { get; set; }
        public string id { get; set; }
        public string count { get; set; }
        public string ifprint { get; set; }
        public string url { get; set; }
        public string printaaa { get; set; }
        public string xj { get; set; }
        public string hj { get; set; }
        public string[][] print { get; set; }
        public string[][] menu { get; set; }
        public string[][] fu { get; set; }
    }
}
