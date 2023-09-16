using System;
using System.Security.Cryptography;

namespace HolidayCabins
{
    public struct CabinData
    {
        public short Number { get; set; }
        public string? Name { get; set; }
        public short Capacity { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
    }

    public class Cabin
    {
        private static List<Cabin> registeredCabins = new List<Cabin>();

        private Party? guestParty;
        private CabinData data;
        private DateOnly from;
        private DateOnly to;

        public Cabin(CabinData record)
        {
            data = record;
            if (DateOnly.TryParse(data.From, out DateOnly result))
            {
                from = result;
            }
            else Console.WriteLine($"Not a valid date {data.From}");

            if (DateOnly.TryParse(data.To, out result))
            {
                to = result;
            }
            else Console.WriteLine($"Not a valid date {data.To}");
        }

        public override string ToString()
        {
            return data.Name;
        }

        public void SetGuestParty(Party party) => guestParty = party;

        public static void RegisterCabin(Cabin cabin, DateOnly date)
        {
            if (registeredCabins.Exists(cb => cabin.data.Number == cb.data.Number))
            {
                Console.WriteLine($"{cabin.data.Name} is already registered");
                return;
            }


            if (cabin.from <= date && date <= cabin.to)
            {
                registeredCabins.Add(cabin);
                Console.WriteLine($"Cabin \"{cabin.data.Name}\" registered");
            }
            else Console.WriteLine($"(Cabin \"{cabin.data.Name}\" not available at the moment)");
        }

        public static void UnregisterCabin(Cabin cabin)
        {
            if (cabin.guestParty != null)
                throw new Exception($"Cannot unregister \"{cabin.data.Name}\" as it is not vacant");

            registeredCabins.Remove(cabin);

            Console.WriteLine($"Cabin \"{cabin.data.Name}\" unregistered.");
        }

        public static Cabin FindRegisteredCabin(short number)
        {
            return registeredCabins.Find(cabin => cabin.data.Number == number);
        }

        public static Cabin FindSuitableCabin(short partysize)
        {
            Cabin cabin = registeredCabins.Find(cb => cb.guestParty == null && cb.data.Capacity >= partysize);

            return cabin;
        }

        public static void ReportRegisteredCabins()
        {
            foreach (Cabin cabin in registeredCabins)
            {
                if (cabin.guestParty == null)
                    Console.WriteLine($"Cabin \"{cabin.data.Name}\" is vacant");
                else
                    Console.WriteLine($"The \"{cabin.guestParty.GetPartyName()}\" party are staying in \"{cabin.data.Name}\" ");
            }
        }   
    }
}

