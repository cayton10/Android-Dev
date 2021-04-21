using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace FibService
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        FibReceiver receiver;
        TextView fibResult;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);


            //Declare function headers for click events here
            FindViewById<Button>(Resource.Id.calculateNumber).Click += Calculate_Fib_Number;
            FindViewById<Button>(Resource.Id.stopCalc).Click += Stop_Calculation;
            fibResult = FindViewById<TextView>(Resource.Id.fibCalc);
        }

        //Handler for stopping service
        public void Stop_Calculation(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(FibService));
            StopService(intent);
        }


        /**
         * Method grabs the value from the EditText field on main activity
         * and starts the service to compute the accompanying fibonacci 
         * number. Cancels if no number is entered.
         */
        public void Calculate_Fib_Number(object sender, System.EventArgs e)
        {

            //Reset the textview containing previous number if not empty
            if(fibResult.Text != "")
            {
                fibResult.SetText("", TextView.BufferType.Normal);
            }
            //Grab the text value entered by user
            var initNumber = FindViewById<EditText>(Resource.Id.numberEntry).Text;
            long result;
            try
            {
                //Parse string from edit text
                //result = Long.Parse(initNumber);
                result = long.Parse(initNumber, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign);

                var intent = new Intent(this, typeof(FibService));
                intent.PutExtra("number", result);

                StartService(intent);
            }
            catch (FormatException)
            {
                //Show appropriate message if fails
                Toast.MakeText(this, "Enter a valid number to compute", ToastLength.Short).Show();
            }
        }


        //Process the broadcastreceiver
        protected override void OnResume()
        {
            base.OnResume();

            var filter = new IntentFilter("CalcCompleteFilter");
            filter.AddAction("CalcComplete");


            //Instantiate receiver
            receiver = new FibReceiver();
            receiver.CalcComplete += Receiver_CalcComplete;

            RegisterReceiver(receiver, filter);
        }

        protected override void OnPause()
        {
            base.OnPause();
            if(receiver != null)
            {
                receiver.CalcComplete -= Receiver_CalcComplete;
                UnregisterReceiver(receiver);
            }
        }

        private void Receiver_CalcComplete(object sender, EventArgs e)
        {
            string result = receiver.num.ToString();
            Console.WriteLine(result);
            fibResult.SetText(result, TextView.BufferType.Normal);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
