
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
using Xamarin.Android;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace JSON_REST
{
    [Activity(Label = "ContactDetailActivity")]
    public class ContactDetailActivity : Activity
    {
        //Global class variable to keep contact id at arm's length
        private Contact contact;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ContactDetail);

            int position = Intent.GetIntExtra("ContactPos", -1);

            //Grab contact details from list index
            contact = MainActivity.Contacts[position];

            //Load that contact info into appropriate data fields
            FindViewById<TextView>(Resource.Id.nameField).Text = $"{contact.FirstName} {contact.LastName}";
            FindViewById<TextView>(Resource.Id.phoneField).Text = $"{contact.Phone}";
            FindViewById<TextView>(Resource.Id.emailField).Text = $"{contact.Email}";

            //Bind methods for buttons
            FindViewById<Button>(Resource.Id.callContact).Click += CallButtonPushed;
            FindViewById<Button>(Resource.Id.emailContact).Click += EmailButtonPushed;
            FindViewById<Button>(Resource.Id.editContact).Click += EditButtonPushed;
            FindViewById<Button>(Resource.Id.viewGravatar).Click += ViewGravatarPushed;
            
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

        //Method for sending user to edit activity w/ correct contact id
        private void EditButtonPushed(object sender, EventArgs e)
        {
            //Send the contact's id in the intent
            var bundle = new Bundle();
            bundle.PutInt("ContactID", contact.Id);

            var intent = new Intent(this, typeof(EditContactActivity));
            intent.PutExtras(bundle);

            StartActivityForResult(intent, 100);
        }

        //Send to view Gravatar
        private async void ViewGravatarPushed(object sender, EventArgs e)
        {
            const string baseURL = "https://www.gravatar.com/";
            const string extension = ".json";

            TextView email = FindViewById<TextView>(Resource.Id.emailField);
            String contactEmail = email.Text;

            string emailHash = CreateMD5(contactEmail);

            //Full gravatar url (hopefully it exists)
            string url = baseURL + emailHash + extension;


            GravatarProfiles gravatar = new GravatarProfiles();
            //Send out our calls to API and get returned data

            try
            {
                gravatar = await GetGravatar(url);

                /*foreach (var item in gravatar)
                {
                    Console.WriteLine(item);
                }*/
            }
            catch (Exception)
            {
                Console.WriteLine("No user information");
            }


        }

        //Create header request task
        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<GravatarProfiles> GetGravatar(string str)
        {

            try
            {
                HttpClient client = await GetClient();
                HttpResponseMessage response = await client.GetAsync(str);
                var result = await client.GetStringAsync(str);
                return JsonConvert.DeserializeObject<GravatarProfiles>(result);
            }
            catch(Exception)
            {
                throw;
            }
        }

        //Static function to create hash
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            //base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 100 && resultCode == Result.Ok)
            {
                string firstName = data.GetStringExtra("FirstName");
                string lastName = data.GetStringExtra("LastName");
                string email = data.GetStringExtra("Email");
                string phone = data.GetStringExtra("Phone");

                //Load that contact info into appropriate data fields
                FindViewById<TextView>(Resource.Id.nameField).Text = $"{firstName} {lastName}";
                FindViewById<TextView>(Resource.Id.phoneField).Text = $"{phone}";
                FindViewById<TextView>(Resource.Id.emailField).Text = $"{email}";

            }
        }




        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
