using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GitMigrater.utils
{
   internal class WebAccessor
   {
      static int 尝试次数 = 3;
      static int 当前出错次数 = 0;

      string lasturl = "";
      private CookieContainer _cookie = new CookieContainer();
      public void refreshWeb()
      {
         this._cookie = new CookieContainer();
         lasturl = "";
      }
      /// <summary>
      /// 保证单个实例
      /// </summary>
      private static WebAccessor _instance;
      private WebAccessor() { }
      public static WebAccessor Instance
      {
         get
         {
            if (_instance == null) { _instance = new WebAccessor(); }
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            return WebAccessor._instance;
         }

      }
      public enum RequestMethod
      {
         GetMethod,
         PostMethod,
         DeleteMethod

      }
      /// <summary>
      /// 页面访问请求
      /// </summary>
      /// <param name="url">请求的地址</param>
      /// <param name="queryData">请求的数据</param>
      /// <returns>返回的页面数据</returns>
      public string PostRequest(string url, string queryData)
      {
         return Request(url, queryData, RequestMethod.PostMethod);
      }
      public string GetRequest(string url, string queryData)
      {
         return Request(url, queryData, RequestMethod.GetMethod);
      }
      public Image GetImage(string url, string queryData)
      {
         //Image image = Image.FromStream();
         int i = 0;
         while (true)
         {
            try
            {
               string querystring = "";
               if (!string.IsNullOrEmpty(queryData)) querystring = "?" + queryData;
               HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url + querystring);

               req.CookieContainer = this._cookie;
               SetRequestCommonHeaders(req);
               req.Method = "GET";

               Stream result = RecvRespStream(req);
               Image image = Image.FromStream(result);
               return image;
            }
            catch (Exception e)
            {
               i++;
               if (i == 尝试次数) throw e;
            }
         }

      }
      public string SimpleRequest(string url)
      {
         return Request(url, "", RequestMethod.GetMethod);
      }
      /// <summary>
      /// 页面访问请求
      /// </summary>
      /// <param name="url">请求地址</param>
      /// <param name="queryData">请求的数据</param>
      /// <param name="method">期望的请求方法 ，post或者get</param>
      /// <returns>返回页面的数据</returns>
      public string Request(string url, string queryData, RequestMethod method)
      {
         int i = 0;
         while (true)
         {
            try
            {
               if (method == RequestMethod.GetMethod)
               {
                  return RequestWithGetMethod(url, queryData);
               }
               else if(method == RequestMethod.PostMethod)
               {
                  return RequestWithPostMethod(url, queryData);
               }else if(method == RequestMethod.DeleteMethod)
               {
                  return RequestWithDelMethod(url, queryData);
               }
            }
            catch (Exception e)
            {
               i++;
               Tracer.trace("[错误信息]:请求失败，休息5秒钟");
               Thread.Sleep(5000);
               Tracer.trace(String.Format("[错误信息]：url={0}，queryData={1}，method={2}", url, queryData, method));
               Tracer.trace(string.Format("[错误信息]：尝试了{0}次", i.ToString()));
               if (i == 尝试次数) throw e;
            }
         }

      }
      public HttpWebResponse GetRequestResp(string url, string queryData)
      {
         int i = 0;
         while (true)
         {
            try
            {
               string querystring = "";
               if (!string.IsNullOrEmpty(queryData)) querystring = "?" + queryData;
               HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url + querystring);
               req.CookieContainer = this._cookie;
               SetRequestCommonHeaders(req);
               req.Method = "GET";
               req.Referer = lasturl;
               lasturl = url + querystring;
               return RecvResp(req);
            }
            catch (Exception e)
            {
               i++;
               Tracer.trace("[错误信息]:请求失败，休息5秒钟");
               Thread.Sleep(5000);
               Tracer.trace(String.Format("[错误信息]：url={0}，queryData={1}，method={2}", url, queryData, "GetRequestResp"));
               Tracer.trace(string.Format("[错误信息]：尝试了{0}次", i.ToString()));
               if (i == 尝试次数) throw e;
            }
         }
      }
      private string RequestWithDelMethod(string url, string queryData)
      {

         string querystring = "";
         if (!string.IsNullOrEmpty(queryData)) querystring = "?" + queryData;
         HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url + querystring);
         req.CookieContainer = this._cookie;
         SetRequestCommonHeaders(req);
         req.Method = "DELETE";
         req.Referer = lasturl;
         lasturl = url + querystring;

         Tracer.trace("[数据访问debug] DELETE\r\n" + url + querystring);

         string result = RecvRespData(req);
         return result;
      }
      private string RequestWithPostMethod(string url, string queryData)
      {

         HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
         req.CookieContainer = this._cookie;
         SetRequestCommonHeaders(req);
         req.Method = "POST";
         req.Referer = lasturl;
         lasturl = url;

         Tracer.trace("[数据访问debug] POST\r\n" + url);
         Tracer.trace("[数据访问debug] Params\r\n" + queryData);

         Byte[] postDate = Encoding.Default.GetBytes(queryData);
         req.ContentType = "application/x-www-form-urlencoded";
         req.ContentLength = postDate.Length;
         Stream sw = req.GetRequestStream();
         sw.Write(postDate, 0, postDate.Length);
         sw.Close();
         string result = RecvRespData(req);
         return result;
      }

      private string RequestWithGetMethod(string url, string queryData)
      {
         string querystring = "";
         if (!string.IsNullOrEmpty(queryData)) querystring = "?" + queryData;
         HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url + querystring);
         req.CookieContainer = this._cookie;
         SetRequestCommonHeaders(req);
         req.Method = "GET";
         req.Referer = lasturl;
         lasturl = url + querystring;

         Tracer.trace("[数据访问debug] GET\r\n" + url + querystring);

         string result = RecvRespData(req);
         return result;
      }
      /// <summary>
      /// 取得返回的数据
      /// </summary>
      /// <param name="req">请求对象</param>
      /// <returns>返回的数据</returns>
      private Stream RecvRespStream(HttpWebRequest req)
      {
         HttpWebResponse resp = RecvResp(req);
         Stream stream = GetRespStreamByEncoding(resp);
         return stream;
      }
      /// <summary>
      /// 取得返回的数据
      /// </summary>
      /// <param name="req">请求对象</param>
      /// <returns>返回的数据</returns>
      private string RecvRespData(HttpWebRequest req)
      {

         HttpWebResponse resp = RecvResp(req);


         Stream stream = GetRespStreamByEncoding(resp);
         StreamReader sr = new StreamReader(stream);
         string result = sr.ReadToEnd();
         sr.Close();

         Tracer.trace("[数据访问debug] RESP\r\n" + result);

         return result;
      }
      /// <summary>
      /// 直接取得resp对象
      /// </summary>
      /// <param name="req"></param>
      /// <returns></returns>
      private HttpWebResponse RecvResp(HttpWebRequest req)
      {
         HttpWebResponse resp = null;
         req.Timeout = 30000;
         while (true)
         {
            try
            {
               resp = (HttpWebResponse)req.GetResponse();
               当前出错次数 = 0;
               break;
            }
            catch (Exception e)
            {
               Tracer.trace("[数据访问错误]" + e.Message);
               当前出错次数++;
               if (当前出错次数 > 尝试次数) throw e;
            }
         }

         return resp;


      }
      /// <summary>
      /// 根据Resp的编码类型得到不同的stream
      /// </summary>
      /// <param name="resp">相应对象</param>
      /// <returns>相应对象的流</returns>
      private Stream GetRespStreamByEncoding(HttpWebResponse resp)
      {
         Stream respstream = resp.GetResponseStream();
         if (resp.ContentEncoding == "gzip")
         {
            return new GZipStream(respstream, CompressionMode.Decompress);
         }
         else if (resp.ContentEncoding == "deflate")
         {
            return new DeflateStream(respstream, CompressionMode.Decompress);
         }
         else
         {
            return respstream;
         }
      }

      private void SetRequestCommonHeaders(HttpWebRequest req)
      {
         req.Accept = "Content-Type:text/plain,text/html, application/xml;q=0.9, application/xhtml+xml, image/png, image/jpeg, image/gif, image/x-xbitmap, */*;q=0.1";
         req.Headers["Accept-Charset"] = "iso-8859-1, utf-8, utf-16, *;q=0.1";
         req.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.9,en;q=0.8");
         req.UserAgent = "Opera/9.50 (Windows NT 6.1; U; en)";
         //req.Headers.Add(HttpRequestHeader.AcceptEncoding, "deflate, gzip, x-gzip, identity, *;q=0");
         req.Timeout = 10000;
         req.KeepAlive = true;
      }
      private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
      {
         return true; //总是接受  
      }
   }
}
