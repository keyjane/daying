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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace KeyPrint
{
    public class Circulate
    {
        public string Forcirculate()
        {
            return "正在打印";/*
        //    Thread.Sleep(1000);
          //  Form1 message_to_box = new Form1();
           // Form1.Text();

           // MessageBox.Show("aaaa");
            try
            {
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
                     
                      Thread.Sleep(5000);
                }
            }
            catch (Exception e)
            {
                return "返回数据有问题";
            } */
        }           
    }
    public class DoPrint
    {
        public static string print_title_words ;
        public void startprint(string m)
        {
            try
            {
                Product mm = JsonConvert.DeserializeObject<Product>(m);
            }
            catch(Exception e) {
               // Form1 send = new Form1();
              //  send.send_to_box(e.ToString());
                // MessageBox.Show(e.ToString());
                return;
            }
            Settings.Default.PrintTemp = m;
            Settings.Default.Save();

            //循环打印机
            Product print_class = JsonConvert.DeserializeObject<Product>(m);

            // MessageBox.Show(print_class.print_cookroom[0][0]);
       
            if (print_class.print_cookroom[0][0]!="none") {
                int printcount = 0;
                for (int a = 0; a < print_class.print_cookroom.Length; a++)
                {
                    //实例化打印对象  
                    PrintDocument printDocument1 = new PrintDocument();

                    if (print_class.print_cookroom[a][0] == "2")
                    {
                        //判断系统是否存在该打印机
                        List<String> list = FindPrinter.GetLocalPrinter(); //获得系统中的打印机列表
                        bool if_has_printer = false;
                        foreach (String s in list)
                        {      
                            //  printerComboBox.Items.Add(s); //将打印机名称添加到下拉框中                            
                            if (s == print_class.print_cookroom[a][1]) {
                                if_has_printer = true;
                                printcount++;
                            }
                        }
                        if (!if_has_printer) {
                            MessageBox.Show("打印机不存在");
                            continue;
                        }
                        //判断系统是否存在该打印机end

                        //选择打印机
                        printDocument1.PrinterSettings.PrinterName = print_class.print_cookroom[a][1];
                    }

                    //设置打印用的纸张,当设置为Custom的时候，可以自定义纸张的大小  
                    printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custum", 200, 500);

                    if (print_class.print_cookroom[a][2] == "1")
                    {
                        // MessageBox.Show(print_class.print_cookroom[a][1]);
                        //注册PrintPage事件，打印每一页时会触发该事件 
                        print_title_words = print_class.print_cookroom[a][3];
                        printDocument1.PrintPage += new PrintPageEventHandler(this.PrintDocument_PrintPage_cf);
                    }
                    else if (print_class.print_cookroom[a][2] == "2")
                    {
                        //  MessageBox.Show("11111");
                        print_title_words = print_class.print_cookroom[a][3];
                        printDocument1.PrintPage += new PrintPageEventHandler(this.PrintDocument_PrintPage_yj);
                    }
                    else if (print_class.print_cookroom[a][2] == "3") {
                        print_title_words = print_class.print_cookroom[a][3];
                        printDocument1.PrintPage += new PrintPageEventHandler(this.PrintDocument_PrintPage_jz);
                    }

                    //初始化打印预览对话框对象  
                    PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
                    //将printDocument1对象赋值给打印预览对话框的Document属性  
                    printPreviewDialog1.Document = printDocument1;
                
                    //直接开始打印
                    printDocument1.Print();//开始打印

                    

                   // MessageBox.Show(url + "?printid="  + print_class.id + "&" + Settings.Default.token);

                    /*
                    //打开打印预览对话框 
                    DialogResult result = printPreviewDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        printDocument1.Print();//开始打印

                        //打印后推送给服务器
                     //   Http hp = new Http();
                      //  string url = Settings.Default.url;
                        //   MessageBox.Show(url + "?printid=" + print_class.id);
                      //  hp.HttpRequestPost(url + "?printid=" + print_class.id);
                    }   */
                }
                //打印后推送给服务器
                if (printcount>0)
                {
                    Http hp = new Http();
                    string url = Settings.Default.url;
                    //   MessageBox.Show(url + "?printid=" + print_class.id);
                    hp.HttpRequestPost(url + "?printid=" + print_class.id + "&" + Settings.Default.token);
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
        private void PrintDocument_PrintPage_yj(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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

                //总计下横杠
                Rectangle square_zj_xia = new Rectangle(0, 120 + (class_menu.menu.Length + class_menu.fu.Length + 2) * 16, 200, 200); 
                e.Graphics.DrawString(content_hg, content_fon, colour, square_zj_xia, geshi);

                Font fontkey_sj = new Font("宋体", 8); //字体   
                StringFormat geshixia = new StringFormat();
                geshixia.Alignment = StringAlignment.Near; //居左 
                //时间
                string table_time = "时间:" + class_menu.time;
                Rectangle square_gj_table_time = new Rectangle(10, 130 + (class_menu.menu.Length + class_menu.fu.Length + 2) * 16, 200, 200);
                // Font fontkey_xj = new Font("宋体", 10); //字体           
                e.Graphics.DrawString(table_time, fontkey_sj, colour, square_gj_table_time, geshixia);

                
                //台号
                string table_name = "台号:" + class_menu.table_name;
                //   table_name = table_name + table_name.Length;
                int showtn = GetLength(table_name);        
                Rectangle square_gj_table_name = new Rectangle(10, 145 + (class_menu.menu.Length + class_menu.fu.Length + 2) * 16,200, 200);
                // Font fontkey_xj = new Font("宋体", 10); //字体           
                e.Graphics.DrawString(table_name, fontkey_sj, colour, square_gj_table_name, geshixia);

            }
        }
        private void PrintDocument_PrintPage_jz(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
            string content_menu_xj_money = "￥" + class_menu.xj;
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
                Rectangle square_fj_xia = new Rectangle(0, 100 + (class_menu.menu.Length + class_menu.fu.Length + 2) * 16, 200, 200); //矩形框 
                e.Graphics.DrawString(content_hg, content_fon, colour, square_fj_xia, geshi);

                string content_menu_gj = "实收";
                Rectangle square_gj = new Rectangle(0, 110 + (class_menu.menu.Length + class_menu.fu.Length + 2) * 16, 70, 200); //矩形框
                                                                                                                                 // Font fontkey_xj = new Font("宋体", 10); //字体   
                e.Graphics.DrawString(content_menu_gj, fontkey_xj, colour, square_gj, geshi);

                string content_menu_gj_money = "￥" + class_menu.realpay;
                Rectangle square_gj_money = new Rectangle(0, 110 + (class_menu.menu.Length + class_menu.fu.Length + 2) * 16, 300, 200); //矩形框
                // Font fontkey_xj = new Font("宋体", 10); //字体           
                e.Graphics.DrawString(content_menu_gj_money, fontkey_xj, colour, square_gj_money, geshi);

                //总计下横杠
                Rectangle square_zj_xia = new Rectangle(0, 120 + (class_menu.menu.Length + class_menu.fu.Length + 2) * 16, 200, 200);
                e.Graphics.DrawString(content_hg, content_fon, colour, square_zj_xia, geshi);


                Font fontkey_sj = new Font("宋体", 8); //字体   
                StringFormat geshixia = new StringFormat();
                geshixia.Alignment = StringAlignment.Near; //居左
  
                //时间
                string table_time = "时间:" + class_menu.time;          
                Rectangle square_gj_table_time = new Rectangle(10, 130 + (class_menu.menu.Length + class_menu.fu.Length + 2) * 16, 200, 200);
                // Font fontkey_xj = new Font("宋体", 10); //字体           
                e.Graphics.DrawString(table_time, fontkey_sj, colour, square_gj_table_time, geshixia);


                //台号
                string table_name = "台号:" + class_menu.table_name;
                //   table_name = table_name + table_name.Length;
               
                Rectangle square_gj_table_name = new Rectangle(10, 145 + (class_menu.menu.Length + class_menu.fu.Length + 2) * 16, 200, 200);
                // Font fontkey_xj = new Font("宋体", 10); //字体           
                e.Graphics.DrawString(table_name, fontkey_sj, colour, square_gj_table_name, geshixia);

            }
        }



        public static int GetLength(string str)
        {
            if (str.Length == 0)
                return 0;
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
            }
            return tempLen;
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
        public string[][] print_cookroom { get; set; }
        public string[][] menu { get; set; }
        public string[][] fu { get; set; }
        public string table_name { get; set; }
        public string time { get; set; }
        public string realpay { get; set; }
    }
    
}
