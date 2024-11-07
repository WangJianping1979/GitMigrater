using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMigrater.utils.que
{
   internal class MessageItemCache
   {
      Queue<MessageItem> queue = new Queue<MessageItem>();
      public void add(MessageItem messageItem)
      {
         queue.Enqueue(messageItem);
      }
      public MessageItem getOne()
      {
         return queue.Dequeue();
      }
      public int count
      {
         get { return queue.Count; }
      }
   }
}
