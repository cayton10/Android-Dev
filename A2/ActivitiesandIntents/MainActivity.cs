using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
//Namespace for creating explicit intent
using Android.Content;

namespace ActivitiesandIntents
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //Construct Dr. Mauro for this application
        Contact drMauro = new Contact();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Main);

            FindViewById<Button>(Resource.Id.callSegue).Click += MainCallButton_Push;
            FindViewById<Button>(Resource.Id.openEmail).Click += OpenEmail_Push;
            FindViewById<Button>(Resource.Id.openMap).Click += OpenMap_Push;

        }

        private void OpenMap_Push(object sender, EventArgs e)
        {
            //Get the location from our dr mauro object
            String location = drMauro.Location.ToString();

            var intent = new Intent();
            intent.SetAction(Intent.ActionView);

            intent.SetData(Android.Net.Uri.Parse($"geo:0,0?q={location}"));

            //Lick stamp and send IF we have an application that can accomplish
            if (intent.ResolveActivity(PackageManager) != null)
            {
                StartActivity(intent);
            }
        }
        
        private void OpenEmail_Push(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        //Method to "segue" between activities / views for calling
        private void MainCallButton_Push(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CallActivity));

            StartActivity(intent);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
