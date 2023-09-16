using System;
namespace HolidayCabins
{
    public struct PartyData
    {
        public string? PartyName { get; set; }
        public short Size { get; set; }
    }

    public class Party
    {
        private static List<Party> registeredParties = new List<Party>();

        private Cabin cabin;
        private PartyData data;

        public Party(PartyData record)
        {
            data = record;
        }

        public string GetPartyName() => data.PartyName;
           
        public Party(string partyname, short number)
        {
            data = new PartyData();
            data.Size = number;
            data.PartyName = partyname;
        }

        public static void RegisterParty(Party party)
        {
            if (registeredParties.Exists(py => party.data.PartyName == py.data.PartyName))
            {
                Console.WriteLine($"{party.data.PartyName} is already registered");
                return;
            }

            Cabin cabin = Cabin.FindSuitableCabin(party.data.Size);
            if (cabin != null)
            {
                party.cabin = cabin;
                cabin.SetGuestParty(party);
                registeredParties.Add(party);
                Console.WriteLine($"\"{party.data.PartyName}\" party registered in {cabin}. Happy Holidays!");           
            }
            else Console.WriteLine($"No available cabins suitable for the \"{party.data.PartyName}\" party");
        }

        public static void UnregisterParty(Party party)
        {
            party.cabin.SetGuestParty(null);
            registeredParties.Remove(party);

            Console.WriteLine($"Party \"{party.data.PartyName}\" unregistered.");
        }

        public static Party FindRegisteredParty(string name)
        {
            return registeredParties.Find(party => party.data.PartyName == name);
        }
    }
}

