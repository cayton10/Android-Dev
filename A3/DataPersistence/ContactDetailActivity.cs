
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


            
        }
    }
}
