using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace JSON_REST
{
    
    public class Gravatar
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string RequestHash { get; set; }
        public string ProfileUrl { get; set; }
        public string PreferredUsername { get; set; }
        public string ThumbnailUrl { get; set; }


    }

}
