
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
using SQLite;

namespace DataPersistence
{
    [Activity(Label = "AddContactActivity")]
    public class AddContactActivity : Activity
    {

        //Globally scoped class variable for db connection
        private SQLiteConnection db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Add);

            FindViewById<Button>(Resource.Id.saveContact).Click += Save_Pushed;
            FindViewById<Button>(Resource.Id.cancelButton).Click += Cancel_Pushed;
        }

        private void Cancel_Pushed(object sender, EventArgs e)
        {
            SetResult(Result.Canceled);
            Finish();
        }

        private void Save_Pushed(object sender, EventArgs e)
        {
            string firstName = FindViewById<EditText>(Resource.Id.firstInput).Text;
            string lastName = FindViewById<EditText>(Resource.Id.lastInput).Text;
            string email = FindViewById<EditText>(Resource.Id.emailInput).Text;
            string phone = FindViewById<EditText>(Resource.Id.phoneInput).Text;

            //Set up db connection
            db = new SQLiteConnection(Globals.dbPath);
            //Connect to table
            var table = db.Table<Contact>();

            //Setup table of type: Contact
            db.CreateTable<Contact>();

            db.Insert(new Contact(firstName, lastName, email, phone));

            var intent = new Intent();

            SetResult(Result.Ok, intent);

            Finish();
        }
    }
}
