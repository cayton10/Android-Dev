
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
    [Activity(Label = "EditContactActivity")]
    public class EditContactActivity : Activity
    {
        //Globally scoped class variable for db connection
        private SQLiteConnection db;
        private Contact contactToEdit;
        private int contactId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Edit);

            //Get the contact id and query data table for contact info
            contactId = Intent.GetIntExtra("ContactID", -1);

            //Set up db connection
            db = new SQLiteConnection(Globals.dbPath);

            //Connect to table
            var table = db.Table<Contact>();
            var contactToEdit = db.Query<Contact>("SELECT * FROM Contacts WHERE Id = ?", contactId);

            //Populate the information for contact we want to edit / delete
            FindViewById<EditText>(Resource.Id.firstInput).Text = contactToEdit[0].FirstName;
            FindViewById<EditText>(Resource.Id.lastInput).Text = contactToEdit[0].LastName;
            FindViewById<EditText>(Resource.Id.phoneInput).Text = contactToEdit[0].Phone;
            FindViewById<EditText>(Resource.Id.emailInput).Text = contactToEdit[0].Email;


            FindViewById<Button>(Resource.Id.deleteContact).Click += DeletePushed;
            FindViewById<Button>(Resource.Id.cancelButton).Click += CancelPushed;
            FindViewById<Button>(Resource.Id.saveContact).Click += SavePushed;
        }

        //Method for sending user to edit activity w/ correct contact id
        private void DeletePushed(object sender, EventArgs e)
        {
            //Construct an alert prompt to make sure we're deleting with prejudice
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Delete Contact");
            alert.SetMessage("Are you sure?");
            alert.SetButton("Delete", (c, ev) =>
            {
                // Ok button click task
                var rowcount = db.Delete<Contact>(contactId);

                if(rowcount > 0)
                {
                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                }
                
            });
            alert.SetButton2("Cancel", (c, ev) =>
            {
                //Cancel button click task
                alert.Hide();
            });
            alert.Show();
        }

        private void SavePushed(object sender, EventArgs e)
        {
            string firstName = FindViewById<EditText>(Resource.Id.firstInput).Text;
            string lastName = FindViewById<EditText>(Resource.Id.lastInput).Text;
            string email = FindViewById<EditText>(Resource.Id.emailInput).Text;
            string phone = FindViewById<EditText>(Resource.Id.phoneInput).Text;

            try
            {
                //Make connection to Contacts table
                var data = db.Table<Contact>();
                //Grab the record containing the contactId in question w/ LINQ
                var record = (from values in data where values.Id == contactId select values).Single();

                record.FirstName = firstName;
                record.LastName = lastName;
                record.Email = email;
                record.Phone = phone;

                db.Update(record);

                Toast.MakeText(this, "Updated Successfully", ToastLength.Short).Show();

            }
            catch(Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
                Finish();
            }

            //Pass the updated fields back to contact details
            var intent = new Intent();

            intent.PutExtra("FirstName", firstName);
            intent.PutExtra("LastName", lastName);
            intent.PutExtra("Email", email);
            intent.PutExtra("Phone", phone);

            SetResult(Result.Ok, intent);

            Finish();

        }

        private void CancelPushed(object sender, EventArgs e)
        {
            Finish();
        }
    }
}
