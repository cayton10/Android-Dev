
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

namespace DataPersistence
{
    [Activity(Label = "ContactDetailActivity")]
    public class ContactDetailActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ContactDetail);

            int position = Intent.GetIntExtra("ContactPos", -1);

            //Grab contact details from list index
            var contact = MainActivity.Contacts[position];

            //Load that contact info into appropriate data fields
            FindViewById<TextView>(Resource.Id.nameField).Text = $"{contact.FirstName} {contact.LastName}";
            FindViewById<TextView>(Resource.Id.phoneField).Text = $"{contact.Phone}";
            FindViewById<TextView>(Resource.Id.emailField).Text = $"{contact.Email}";

            //Bind methods for buttons
            FindViewById<Button>(Resource.Id.callContact).Click += CallButtonPushed;
            FindViewById<Button>(Resource.Id.emailContact).Click += EmailButtonPushed;
            //FindViewById<Button>(Resource.Id.editContact).Click += EditButtonPushed;
            
        }

        private void CallButtonPushed(object sender, EventArgs e)
        {
            var intent = new Intent();

            //Grab the textview w/ the Contact's number
            TextView contactNum = FindViewById<TextView>(Resource.Id.phoneField);
            String phoneNum = contactNum.Text;

            //Set the intent for calling
            intent.SetAction(Intent.ActionDial);
            intent.SetData(Android.Net.Uri.Parse("tel:" + phoneNum));

            if(intent.ResolveActivity(PackageManager) != null)
            {
                StartActivity(intent);
            }
        }

        private void EmailButtonPushed(object sender, EventArgs e)
        {
            TextView email = FindViewById<TextView>(Resource.Id.emailField);
            String contactEmail = email.Text;

            var intent = new Intent();
            intent.SetAction(Intent.ActionSendto);

            intent.SetData(Android.Net.Uri.Parse($"mailto:{contactEmail}"));

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
