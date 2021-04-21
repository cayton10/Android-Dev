using System;
using System.IO;


namespace JSON_REST
{
    static class Globals
    {
        //Stored database path here so we can reference app wide instead of repeating code everywhere
        public static string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "contactDB.db3");
    }
}
