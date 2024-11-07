using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitMigrater.utils.que
{
   internal class MessageItem
   {
      private object _Item;

      public object Item
      {
         get { return _Item; }
         set { _Item = value; }
      }
   }
}
