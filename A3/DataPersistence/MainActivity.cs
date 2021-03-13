using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;

namespace DataPersistence
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public static List<Contact> Contacts = new List<Contact>();

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);


            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));
            Contacts.Add(new Contact("Benjamin", "Cayton", "cayton10@live.marshall.edu", "3046389603"));

            var lv = FindViewById<ListView>(Resource.Id.contactListView);

            lv.Adapter = new ArrayAdapter<Contact>(this, Android.Resource.Layout.SimpleListItem1, Contacts);

            var addContactButton = FindViewById<Button>(Resource.Id.addContact);


        }


        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            //base.OnActivityResult(requestCode, resultCode, data);

            if(requestCode == 100 && resultCode == Result.Ok)
            {
                string name = data.GetStringExtra("FirstName");
            }
        }


        /*private void Contacts_Push(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ContactsActivity));
            StartActivity(intent);
        }*/

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
