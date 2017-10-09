using System;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Text;


public partial class Form1 : System.Windows.Forms.Form
{
    private System.ComponentModel.Container components;
    private System.Windows.Forms.Button printButton;
    private Font printFont;
    private Button button1;
    private StreamReader streamToPrint;


    public Form1()
    {
        // The Windows Forms Designer requires the following call.
        startprint();
        InitializeComponent();
    }

    // The Click event is raised when the user clicks the Print button.
    private void printButton_Click(object sender, EventArgs e)
    {/*
        string printer = "XP-58";
        try
        {
            streamToPrint = new StreamReader
               ("G:\\123.txt");
            try
            {
                printFont = new Font("Arial", 10);
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler
                   (this.pd_PrintPage);
                pd.PrinterSettings.PrinterName = printer;
                pd.Print();
            }
            finally
            {
                streamToPrint.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
          */

        //实例化打印对象  
        PrintDocument printDocument1 = new PrintDocument();
        //选择打印机
        printDocument1.PrinterSettings.PrinterName = "XP-58";

        //设置打印用的纸张,当设置为Custom的时候，可以自定义纸张的大小  
        printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custum", 200, 500);
        //注册PrintPage事件，打印每一页时会触发该事件 
        printDocument1.PrintPage += new PrintPageEventHandler(this.PrintDocument_PrintPage);

        //初始化打印预览对话框对象  
        PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        //将printDocument1对象赋值给打印预览对话框的Document属性  
        printPreviewDialog1.Document = printDocument1;
        //直接开始打印
        //  printDocument1.Print();//开始打印

        //打开打印预览对话框 
        DialogResult result = printPreviewDialog1.ShowDialog();
        if (result == DialogResult.OK)
            printDocument1.Print();//开始打印  

      


    }
    private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
        //设置打印内容及其字体，颜色和位置  
        // e.Graphics.DrawString(GetPrintSW().ToString(), new Font(new FontFamily("黑体"), 12), System.Drawing.Brushes.Red, 20, 50);
        //  e.Graphics.DrawString(GetPrintSW.ToString(), titleFont, brush, po);   //DrawString方式进行打印。    

        StringFormat geshi = new StringFormat();
        geshi.Alignment = StringAlignment.Center; //居中
                                                  // key.Alignment = StringAlignment.Far; //右对齐
        string content = "12312312313123123";
        Rectangle square = new Rectangle(0, 50, 400, 200); //矩形框
        Rectangle square1 = new Rectangle(0, 250, 400, 200); //矩形框
        Font fontkey = new Font("宋体", 10.5F); //字体
        Brush colour = Brushes.Black;  //颜色
        e.Graphics.DrawString(content, fontkey, colour, square, geshi);
      //  e.Graphics.DrawString(GetPrintSW().ToString(), fontkey, colour, square1, geshi);
    }


    // The Windows Forms Designer requires the following procedure.
    private void InitializeComponent()
    {
        this.printButton = new System.Windows.Forms.Button();
        this.button1 = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // printButton
        // 
        this.printButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.printButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.printButton.Location = new System.Drawing.Point(32, 110);
        this.printButton.Name = "printButton";
        this.printButton.Size = new System.Drawing.Size(136, 40);
        this.printButton.TabIndex = 0;
        this.printButton.Text = "Print the file.";
        this.printButton.Click += new System.EventHandler(this.printButton_Click);
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(311, 110);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 1;
        this.button1.Text = "change";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // Form1
        // 
        this.ClientSize = new System.Drawing.Size(434, 238);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.printButton);
        this.Name = "Form1";
        this.Text = "Print Example";
        this.ResumeLayout(false);

    }




    private void startprint()
    {
        //实例化打印对象  
        PrintDocument printDocument1 = new PrintDocument();
        //选择打印机
        printDocument1.PrinterSettings.PrinterName = "XP-58";

        //设置打印用的纸张,当设置为Custom的时候，可以自定义纸张的大小  
        printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custum", 400, 500);
        //注册PrintPage事件，打印每一页时会触发该事件 
        printDocument1.PrintPage += new PrintPageEventHandler(this.PrintDocument_PrintPage1);

        //初始化打印预览对话框对象  
        PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        //将printDocument1对象赋值给打印预览对话框的Document属性  
        printPreviewDialog1.Document = printDocument1;
        //直接开始打印
        //  printDocument1.Print();//开始打印

        //打开打印预览对话框 
        DialogResult result = printPreviewDialog1.ShowDialog();
        if (result == DialogResult.OK)
            printDocument1.Print();//开始打印  

    }

    private void PrintDocument_PrintPage1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
        //设置打印内容及其字体，颜色和位置  
        // e.Graphics.DrawString(GetPrintSW().ToString(), new Font(new FontFamily("黑体"), 12), System.Drawing.Brushes.Red, 20, 50);
        //  e.Graphics.DrawString(GetPrintSW.ToString(), titleFont, brush, po);   //DrawString方式进行打印。    

        StringFormat geshi = new StringFormat();
        geshi.Alignment = StringAlignment.Center; //居中
                                                  // key.Alignment = StringAlignment.Far; //右对齐
        string content = "12312312313123123";
        Rectangle square = new Rectangle(0, 50, 400, 200); //矩形框
        Rectangle square1 = new Rectangle(0, 250, 400, 200); //矩形框
        Font fontkey = new Font("宋体", 10.5F); //字体
        Brush colour = Brushes.Black;  //颜色
        e.Graphics.DrawString(GetPrintSW().ToString(), fontkey, colour, square, geshi);
        e.Graphics.DrawString(GetPrintSW().ToString(), fontkey, colour, square1, geshi);
    }

    public StringBuilder GetPrintSW()
    {

        StringBuilder sb = new StringBuilder();

        string tou = "测试管理公司名称";

        string address = "河南洛阳";

        string saleID = "2010930233330";    //单号        

        string item = "项目";

        decimal price = 25.00M;

        int count = 5;

        decimal total = 0.00M;

        decimal fukuan = 500.00M;

        sb.AppendLine(" " + tou + " \n");

        sb.AppendLine("-----------------------------------------");

        sb.AppendLine("日期:" + DateTime.Now.ToShortDateString() + " " + "单号:" + saleID);

        sb.AppendLine("-----------------------------------------");

        sb.AppendLine("项目" + "      " + "数量" + "    " + "单价" + "    " + "小计");

        for (int i = 0; i < count; i++)

        {

            decimal xiaoji = (i + 1) * price;

            sb.AppendLine(item + (i + 1) + "      " + (i + 1) + "     " + price + "    " + xiaoji);

            total += xiaoji;

        }

        sb.AppendLine("-----------------------------------------");

        sb.AppendLine("数量:" + count + "  合计: " + total);

        sb.AppendLine("付款:" + fukuan);

        sb.AppendLine("现金找零:" + (fukuan - total));

        sb.AppendLine("-----------------------------------------");

        sb.AppendLine("地址:" + address + "");

        sb.AppendLine("电话:123456789 123456789");

        sb.AppendLine("谢谢惠顾欢迎下次光临 ");

        sb.AppendLine("-----------------------------------------");

        return sb;

    }

    private void button1_Click(object sender, EventArgs e)
    {

    }
}
