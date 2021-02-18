using System;
namespace ActivitiesandIntents
{
    public class Contact
    {
        private String location;
        private String email;
        //Just construct w/ default for Dr. Mauro
        public Contact()
        {
            this.location = "Morrow+Library+Huntington+WV";
            this.email = "maurod@marshall.edu";
        }

        public String Location
        {
            get { return location; }
            set { location = value; }
        }
    }
}
