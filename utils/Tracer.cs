using GitMigrater.utils.que;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMigrater.utils
{
   internal class Tracer
   {
      private static string fileDateStr = DateTime.Now.ToString("yyyyMMdd");
      private MessageQueueService mqservice = new MessageQueueService();
      private static Tracer tracerInstance = new Tracer();
      private Tracer()
      {
         mqservice.OnMessage += new MessageQueueService.DelegateOnMessage(writeToFile);
         mqservice.StartService();
      }
      /// <summary>
      /// 静态方法：写日志
      /// </summary>
      /// <param name="msg"></param>
      public static void trace(string msg)
      {
         tracerInstance.mqservice.sendMessage(msg);
      }
      /// <summary>
      /// 往当前程序根目录下边打印内容
      /// 日志格式为:DebugMsg20141203.txt
      /// </summary>
      /// <param name="msg">要打印的内容</param>
      private void writeToFile(object msg)
      {
         try
         {
            using (StreamWriter sw = new StreamWriter("./DebugMsg" + fileDateStr + ".txt", true, Encoding.UTF8, 1024))
            {
               sw.WriteLine(msg.ToString());
               sw.Flush();
            }
         }
         catch (Exception excp) { }
      }
   }
}
