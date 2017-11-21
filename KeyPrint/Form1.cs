using HttpRequest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using KeyPrint.Properties;

namespace KeyPrint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            //  Application.DoEvents();
            // this.Load += new EventHandler(Form1_Load);
            //  Circulate ci = new Circulate();
            //  ci.Forcirculate();
            //后台线程
            Thread worker = new Thread(delegate () { send_to_box(); });
            worker.IsBackground = true;
            worker.Start();
           
            //给token赋值
            textBox1.AppendText(Settings.Default.token);

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           // string str = textBox1.Text;
            // this.changeTextValue(textBox1, "test");.
            string str1 = textBox1.Text + "\n" + textBox2.Text;
            int i = Encoding.Default.GetBytes(str1).Length;
            // string str2 = str1.Substring(0, 100);
            //  textBox2.Text = str1;
            /*
            if (i > 200)
            {
              //  textBox2.Text = "";
                textBox2.Text=textBox1.Text;
               // MessageBox.Show(Settings.Default.token);
            }
            else {
                textBox2.AppendText(str1);
            }*/


            if (MessageBox.Show("是否保存？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Settings.Default.token = textBox1.Text;
                Settings.Default.Save();
                textBox2.AppendText(textBox1.Text);
            }
   
            // textBox2.Pre(str1);
            //  MessageBox.Show(textBox1.Text);
            // string str1 = str.Substring(0, i);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public void send_to_box() {
            try
            {
                while (true)
                {
                    Http hp = new Http();
                    // Product m = JsonConvert.DeserializeObject<Product>(hp.HttpRequest());
                  //  MessageBox.Show(Settings.Default.url);
                  // MessageBox.Show(hp.HttpRequest());
                    if (hp.HttpRequest() != "none")
                    {
                        //传值打印机
                        DoPrint DoP = new DoPrint();
                        DoP.startprint(hp.HttpRequest());
                       // textBox2.AppendText("程序正在备打印\r\n");
                    }
                    testc_length();
                    textBox2.AppendText("程序正准备打印\r\n");                  
                    Thread.Sleep(5000);
                }
            }
            catch (Exception e)
            {
              //  return "返回数据有问题";
            }
        }
        private void testc_length() {
            string str1 = textBox2.Text;
            int i = Encoding.Default.GetBytes(str1).Length;
            if (i > 150)
            {
                textBox2.Text = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
