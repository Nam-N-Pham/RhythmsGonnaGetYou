using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
                        string updateBandsChoice = Console.ReadLine().ToLower();

                        switch (updateBandsChoice)
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
                                Console.WriteLine("What band would you like to sign?");
                                string signName = Console.ReadLine().ToLower();

                                Band bandToSign = context.Bands.FirstOrDefault(band => band.Name.ToLower() == signName);
                                if (bandToSign != null)
                                {
                                    bandToSign.IsSigned = true;
                                    context.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine("Band not found.");
                                }

                                break;
                            case "unsign":
                                Console.WriteLine("What band would you like to unsign?");
                                string unsignName = Console.ReadLine().ToLower();

                                Band bandToUnsign = context.Bands.FirstOrDefault(band => band.Name.ToLower() == unsignName);
                                if (bandToUnsign != null)
                                {
                                    bandToUnsign.IsSigned = false;
                                    context.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine("Band not found.");
                                }

                                break;
                        }
                        break;
                    case "view bands":
                        Console.WriteLine("What would you like to view? All bands, signed bands, or unsigned bands");
                        string viewBandsChoice = Console.ReadLine().ToLower();

                        switch (viewBandsChoice)
                        {
                            case "all bands":
                                foreach (Band band in context.Bands)
                                {
                                    Console.WriteLine(band.Name);
                                }
                                break;
                            case "signed bands":
                                foreach (Band band in context.Bands)
                                {
                                    if (band.IsSigned == true)
                                        Console.WriteLine(band.Name);
                                }
                                break;
                            case "unsigned bands":
                                foreach (Band band in context.Bands)
                                {
                                    if (band.IsSigned == false)
                                        Console.WriteLine(band.Name);
                                }
                                break;
                        }
                        break;
                    case "update albums":
                        Console.WriteLine("Enter the name of the band you would like to add an album for.");
                        string bandNameToAddAlbum = Console.ReadLine().ToLower();
                        int bandId;
                        Band bandToAddAlbum = context.Bands.FirstOrDefault(band => band.Name.ToLower() == bandNameToAddAlbum);
                        if (bandToAddAlbum != null)
                        {
                            bandId = bandToAddAlbum.Id;
                        }
                        else
                        {
                            Console.WriteLine("Band does not exist.");
                            break;
                        }

                        Console.Write("Title: ");
                        string albumTitle = Console.ReadLine().ToLower();
                        Console.Write("Explicit (Y/N): ");
                        string explicitStatus = Console.ReadLine().ToLower();
                        bool isExplicit;
                        if (explicitStatus == "y")
                            isExplicit = true;
                        else if (explicitStatus == "n")
                            isExplicit = false;
                        else
                        {
                            Console.WriteLine("Invalid explicit status.");
                            break;
                        }
                        Console.Write("Release date (YYYY-MM-DD): ");
                        DateTime releaseDate = DateTime.Parse(Console.ReadLine());

                        Album albumToAdd = new Album()
                        {
                            Title = albumTitle,
                            IsExplicit = isExplicit,
                            ReleaseDate = releaseDate,
                            BandId = bandId
                        };
                        context.Albums.Add(albumToAdd);
                        context.SaveChanges();

                        break;
                    case "view albums":
                        Console.WriteLine("What would you like to view? All albums or band albums?");
                        string viewAlbumsChoice = Console.ReadLine().ToLower();

                        switch (viewAlbumsChoice)
                        {
                            case "all albums":
                                var orderedByReleaseDateAlbums = context.Albums.OrderBy(album => album.ReleaseDate);
                                foreach (Album album in orderedByReleaseDateAlbums)
                                {
                                    Console.WriteLine(album.Title);
                                }
                                break;
                            case "band albums":
                                Console.WriteLine("What band would you like to see albums of?");
                                string viewAlbumByBandName = Console.ReadLine().ToLower();

                                var albumsWithBands = context.Albums.Include(album => album.Band);
                                foreach (Album album in albumsWithBands)
                                {
                                    if (album.Band.Name.ToLower() == viewAlbumByBandName)
                                        Console.WriteLine(album.Title);
                                }
                                break;
                        }

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
