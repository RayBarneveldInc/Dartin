using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dartin.Controls
{
    public class ScrollingListView : ListView
    {
        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (Items.Count > 0)
            {
                ScrollIntoView(Items[Items.Count - 1]);
            }

            base.OnItemsChanged(e);
        }
    }
}
