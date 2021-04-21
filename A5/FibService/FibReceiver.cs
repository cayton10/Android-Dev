using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FibService
{
    [BroadcastReceiver]
    public class FibReceiver : BroadcastReceiver
    {
        public long num;
        public event EventHandler<EventArgs> CalcComplete;
        public override void OnReceive(Context context, Intent intent)
        {
            //Store the resultant number from the Intent extra
            num = intent.GetLongExtra("FibNumber", -1);

            if(intent.GetBooleanExtra("CalcComplete", false))
            {
                CalcComplete?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
