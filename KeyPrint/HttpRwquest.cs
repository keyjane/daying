using System.Text;
using System.Net;
using System.IO;
using KeyPrint.Properties;
using System.Windows.Forms;

namespace HttpRequest
{
    public class Http
    {
        public string HttpRequest()
        {
            try
            {
                 //请求路径
                 //     MessageBox.Show(postData);
                string url = Settings.Default.url;
                //  string url = "http://diancan1/test";
                //   MessageBox.Show(StaticSettings.Default.url);
                // Settings.Default.PrintTemp = "http://diancan1/test";
                //  Settings.Default.Save();
               // MessageBox.Show(url);

                //定义request并设置request的路径
                WebRequest request = WebRequest.Create(url);

                //定义请求的方式
                request.Method = "POST";

                //初始化request参数
                string postData = Settings.Default.token;
                //   postData += "&title=C#发送后台请求";
                //   postData += "&message=利用C#后台向androidpnserver发送HTTP请求实现客户端的消息推送功能。";
                //MessageBox.Show(postData);

                //设置参数的编码格式，解决中文乱码
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                //设置request的MIME类型及内容长度
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
     

                //打开request字符流
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
          
                //定义response为前面的request响应
                WebResponse response = request.GetResponse();

                //获取相应的状态代码
                //   Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                //定义response字符流
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();//读取所有
                                                               //  Console.WriteLine(responseFromServer);

                //关闭资源
                reader.Close();
                dataStream.Close();
                response.Close();
               // MessageBox.Show(responseFromServer);
                return responseFromServer;

            }
            catch (WebException e)
            {
                //  Console.WriteLine("\r\nWebException Raised. The following error occured : {0}", e.Status);
                //  MessageBox.Show("1");
                return "none";
            }
        }
        public void HttpRequestPost(string url)
        {
            try
            {
                //请求路径
                //     MessageBox.Show(postData);
               // string url = Settings.Default.url;
                //  string url = "http://diancan1/test";
                //   MessageBox.Show(StaticSettings.Default.url);
                // Settings.Default.PrintTemp = "http://diancan1/test";
                //  Settings.Default.Save();

                //定义request并设置request的路径
                WebRequest request = WebRequest.Create(url);

                //定义请求的方式
                request.Method = "POST";

                //初始化request参数
                string postData = Settings.Default.token;
                //   postData += "&title=C#发送后台请求";
                //   postData += "&message=利用C#后台向androidpnserver发送HTTP请求实现客户端的消息推送功能。";


                //设置参数的编码格式，解决中文乱码
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                //设置request的MIME类型及内容长度
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;


                //打开request字符流
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                //定义response为前面的request响应
                WebResponse response = request.GetResponse();

                //获取相应的状态代码
                //   Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                //定义response字符流
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();//读取所有
                                                               //  Console.WriteLine(responseFromServer);

                //关闭资源
                reader.Close();
                dataStream.Close();
                response.Close();
              //  MessageBox.Show(responseFromServer);

                 // return responseFromServer;

            }
            catch (WebException e)
            {
                //  Console.WriteLine("\r\nWebException Raised. The following error occured : {0}", e.Status);
                //   MessageBox.Show(e.Status);
               // return "none";
            }
        }
    }
}
