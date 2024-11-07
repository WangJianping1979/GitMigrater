using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMigrater.utils.que
{
   internal class MessageQueueService
   {
      public delegate void DelegateOnMessage(object o);
      /// <summary>
      /// 限速
      /// </summary>
      private int _sleepMilisecond = 0;

      public int SleepMilisecond
      {
         get { return _sleepMilisecond; }
         set { _sleepMilisecond = value; }
      }
      public DelegateOnMessage OnMessage;
      /// <summary>
      /// 队列消息持有者
      /// </summary>
      private MessageItemCache messageItemCache = new MessageItemCache();
      /// <summary>
      /// 线程通知事件
      /// </summary>
      private AutoResetEvent _recieverwait = new AutoResetEvent(false);
      private AutoResetEvent _senderwait = new AutoResetEvent(false);
      private bool running = false;
      /// <summary>
      /// 队列中允许存储的最大消息个数
      /// </summary>
      private int maxCount = 100;

      public int MaxCount
      {
         get { return maxCount; }
         set { maxCount = value; }
      }

      private void NotifyRevicer()
      {
         _recieverwait.Set();
      }
      private void NotifySender()
      {
         _senderwait.Set();
      }
      public void sendMessage(Object o)
      {
         if (!running) return;
         if (messageItemCache.count >= maxCount) _senderwait.WaitOne();
         MessageItem mi = new MessageItem();
         mi.Item = o;
         Monitor.Enter(messageItemCache);
         messageItemCache.add(mi);
         Monitor.Exit(messageItemCache);
         Thread.Sleep(_sleepMilisecond);
         NotifyRevicer();
      }
      private void checkMessageThread()
      {

         while (running)
         {
            if (messageItemCache.count == 0) _recieverwait.WaitOne();
            if (messageItemCache.count == 0) continue;
            Monitor.Enter(messageItemCache);
            MessageItem mi = messageItemCache.getOne();
            Monitor.Exit(messageItemCache);
            if (OnMessage != null) OnMessage(mi.Item);// ThreadPool.QueueUserWorkItem(new WaitCallback(OnMessage), mi.Item);
                                                      //通知监听者
            NotifySender();
         }
         while (messageItemCache.count > 0)
         {
            if (OnMessage != null)
            {
               OnMessage(messageItemCache.getOne().Item);
            }
         }
      }
      public void StartService()
      {
         running = true;
         Thread startThread = new Thread(new ThreadStart(checkMessageThread));
         startThread.Start();
      }
      public void stopService()
      {
         running = false;
         NotifyRevicer();
         NotifySender();
      }
   }
}
