using System;
using System.Linq;

namespace RhythmsGonnaGetYou
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to your band database");
            var context = new RecordLabelDatabaseContext();

            bool runProgram = true;
            while (runProgram)
            {
                Console.WriteLine("Enter an option you would like to do: Update Bands, View Bands, Update Albums, View Albums, Add song, or Quit.");
                string menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {
                    case "quit":
                        runProgram = false;
                        break;
                    case "update bands":
                        Console.WriteLine("How would you like to update? Add, Sign, or Unsign?");
                        string updateBandChoice = Console.ReadLine().ToLower();

                        switch (updateBandChoice)
                        {
                            case "add":
                                Console.Write("Name: ");
                                string bandName = Console.ReadLine();
                                Console.Write("Country of origin: ");
                                string countryOfOrigin = Console.ReadLine();
                                Console.Write("Number of members: ");
                                int numOfMembers = int.Parse(Console.ReadLine());
                                Console.Write("Website: ");
                                string bandWebsite = Console.ReadLine();
                                Console.Write("Style: ");
                                string bandStyle = Console.ReadLine();
                                Console.Write("Signed (Y/N): ");
                                string signedStatus = Console.ReadLine().ToLower();
                                bool isSigned;
                                if (signedStatus == "y")
                                    isSigned = true;
                                else if (signedStatus == "n")
                                    isSigned = false;
                                else
                                {
                                    Console.WriteLine("Invalid signed status.");
                                    break;
                                }
                                Console.Write("Contact Name: ");
                                string contactName = Console.ReadLine();
                                Console.Write("Contact phone number: ");
                                string contactPhoneNumber = Console.ReadLine();

                                Band bandToAdd = new Band()
                                {
                                    Name = bandName,
                                    CountryOfOrigin = countryOfOrigin,
                                    NumberOfMembers = numOfMembers,
                                    Website = bandWebsite,
                                    Style = bandStyle,
                                    IsSigned = isSigned,
                                    ContactName = contactName,
                                    ContactPhoneNumber = contactPhoneNumber
                                };

                                context.Bands.Add(bandToAdd);
                                context.SaveChanges();
                                break;
                            case "sign":

                                break;
                            case "unsign":
                                Console.WriteLine("What band would you like to unsign?");
                                string unsignName = Console.ReadLine().ToLower();

                                var existingBand = context.Bands.FirstOrDefault(band => band.Name.ToLower() == unsignName);
                                if (existingBand != null)
                                {
                                    existingBand.IsSigned = false;
                                    context.SaveChanges();
                                }

                                break;
                        }
                        break;
                    case "view bands":
                        break;
                    case "update albums":
                        break;
                    case "view albums":
                        break;
                    case "add song":
                        break;
                    default:
                        Console.WriteLine("Only enter the given options spelled correctly.");
                        break;
                }
            }
        }
    }
}
