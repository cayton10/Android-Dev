
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

        }

        //Make the phone call
        //Method to "segue" between activities / views for calling
        private void PlaceCallButtonPushed(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CallActivity));

            StartActivity(intent);
        }
    }


}
