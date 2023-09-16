using HolidayCabins;

DateTime nw = DateTime.Now;
Console.WriteLine($"Holiday bookings for {DateOnly.FromDateTime(nw)}");

List<CabinData> cabindata = JsonServices.ReadAllCabinsFromFile();
DateOnly todaysdate = DateOnly.FromDateTime(nw);
foreach (CabinData cb in cabindata)
{
    Cabin.RegisterCabin(new Cabin(cb), todaysdate);
}

List<PartyData> partydata = JsonServices.ReadProspectiveGuestsFromFile();
foreach (PartyData pd in partydata)
{
    Party.RegisterParty(new Party(pd));
}

//New party appears!
Party iverson = new Party("Iverson", 7);
Party.RegisterParty(iverson);

//The Shahs go home
Party party = Party.FindRegisteredParty("Shah");
if (party != null)
{
    Party.UnregisterParty(party);
}


//If Shahs left, lets close Dunroamin
Cabin cabin = Cabin.FindRegisteredCabin(4);
if (cabin != null)
{
    Cabin.UnregisterCabin(cabin);
}

//Confirm state of the cabins.
Cabin.ReportRegisteredCabins();