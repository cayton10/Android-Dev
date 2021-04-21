using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FibService
{
    //Tag the service
    [Service (Label="CalcFibNumber")]
    public class FibService : Service
    {
        //Declare media player class object
        MediaPlayer player;
        const string tag = "CalcFibNumber";

        //Volatile variables for stumping threads
        volatile bool isCancelled;
        volatile bool calcFinished;

        public override IBinder OnBind(Intent intent)
        {
            Log.Debug(tag, "On Bind Called");
            throw new NotImplementedException();
        }

        public override void OnCreate()
        {
            base.OnCreate();
            //Instantiate the media player object
            player = MediaPlayer.Create(this, Resource.Raw.fart);
            //Play sounds
            player.Start();
            Log.Debug(tag, "Service Created");
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            isCancelled = true;
            if(calcFinished)
            {
                player.Stop();
                Toast.MakeText(this, "Computation Complete", ToastLength.Short).Show();
            }
            else
            {
                player.Stop();
                Toast.MakeText(this, "Computation Canceled", ToastLength.Short).Show();
            }
            Log.Debug(tag, "Service Destroyed");
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            //Run 
            Log.Debug(tag, "On start command called");


            //Set flags
            isCancelled = false;
            calcFinished = false;
            var userNumber = intent.GetLongExtra("number", -1);
            long fibNumber;
            //User Lambda to run the fibonacci function
            Task.Run(() => {
                fibNumber = Fibonacci(userNumber);

                if(!isCancelled)
                {
                    calcFinished = true;
                    
                    Console.WriteLine($"Calculated Fibonacci number: '{fibNumber}'");
                    //Stop the service
                    StopSelf();

                    Intent broadcast = new Intent();
                    broadcast.SetAction("CalcCompleteFilter");
                    broadcast.PutExtra("CalcComplete", true);
                    broadcast.PutExtra("FibNumber", fibNumber);
                    SendBroadcast(broadcast);
                }
            });

            
            return base.OnStartCommand(intent, flags, startId);
        }

        //Fib calc as required
        public long Fibonacci(long n)
        {
            player.Start();
            if (isCancelled == false)
            {
                if (n == 0 || n == 1)
                {
                    return n;
                }
                else
                {
                    return Fibonacci(n - 1) + Fibonacci(n - 2);
                }
            }
            else
                return -1;
            
        }
    }
}
