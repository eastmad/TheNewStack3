using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Principal;

namespace HolidayCabins
{
    public class JsonServices
    {
        const string BASEDIRECTORY = "/Users/eastmad/Projects/TheNewStack/HolidayCabins/HolidayCabins/";
        const string ALLCABINS = "allcabins.json";
        const string PROSPECTIVEPARTIES = "prospectiveparties.json";

        public static List<CabinData> ReadAllCabinsFromFile()
        {
            return ReadFromFile<CabinData>(ALLCABINS);
        }

        public static List<PartyData> ReadProspectiveGuestsFromFile()
        {
            return ReadFromFile<PartyData>(PROSPECTIVEPARTIES);
        }

        private static List<T> ReadFromFile<T>(string filename)
        {
            List<T>? list = null;

            filename = Path.Combine(BASEDIRECTORY, filename);

            if (File.Exists(filename))
            {
                string jsonString = File.ReadAllText(filename);
                list = JsonSerializer.Deserialize<List<T>>(jsonString);
            }
            else Console.WriteLine("No existing file found for " + filename);

            return list ?? new List<T>();
        }
    }
}

