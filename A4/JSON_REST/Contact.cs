using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;


namespace JSON_REST
{
    [Table("Contacts")]
    public class Contact
    {

        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        private string first;
        private string last;
        private string email;
        private string phone;

        /**
         * Public constructor takes four string parameters:
         * first name, last name, email, and phone number
         */
        public Contact(string f, string l, string e, string p)
        {
            this.first = f;
            this.last = l;
            this.email = e;
            this.phone = p;
        }

        public Contact()
        {

        }

        //Getters / setters

        public string FirstName
        {
            get { return first; }
            set { first = value; }
        }

        public string LastName
        {
            get { return last; }
            set { last = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public override string ToString()
        {
            return ($"{first} {last}");
        }
    }
}
