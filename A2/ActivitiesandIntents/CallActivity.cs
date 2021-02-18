
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;

namespace ActivitiesandIntents
{
    [Activity(Label = "CallActivity")]
    public class CallActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Call);

            FindViewById<Button>(Resource.Id.placeCall).Click += PlaceCallButtonPushed;
            FindViewById<Button>(Resource.Id.cancelCall).Click += CancelCallButtonPushed;
        }

        private void CancelCallButtonPushed(object sender, EventArgs e)
        {
            Finish();
        }

        //Make the phone call
        //Method to "segue" between activities / views for calling
        private void PlaceCallButtonPushed(object sender, EventArgs e)
        {
            //Gonna use that PHONE DIALER, BROOOOOOO!!!
            var intent = new Intent();

            //Grab the textview w/ contact number
            TextView contact = FindViewById<TextView>(Resource.Id.phoneNumber);
            String phoneNum = contact.Text;
            //Remove number separators
            phoneNum = phoneNum.Replace(".", "");

            intent.SetAction(Intent.ActionDial);
            intent.SetData(Android.Net.Uri.Parse("tel:" + phoneNum));

            if(intent.ResolveActivity(PackageManager) != null)
            {
                StartActivity(intent);
            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


    }


}
