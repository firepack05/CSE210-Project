
using System;

class Program
{
    static void Main(string[] args)
    {
        int breathingCount = 0;
        int reflectionCount = 0;
        int listingCount = 0;

        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. View session statistics");
            Console.WriteLine("  5. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BreathingActivity breathing = new BreathingActivity();
                    breathing.Run();
                    breathingCount++;
                    break;

                case "2":
                    ReflectionActivity reflection = new ReflectionActivity();
                    reflection.Run();
                    reflectionCount++;
                    break;

                case "3":
                    ListingActivity listing = new ListingActivity();
                    listing.Run();
                    listingCount++;
                    break;

                case "4":
                    DisplayStatistics(
                        breathingCount,
                        reflectionCount,
                        listingCount
                    );
                    break;

                case "5":
                    Console.WriteLine();
                    Console.WriteLine("Thank you for using the Mindfulness Program!");

                    DisplayStatistics(
                        breathingCount,
                        reflectionCount,
                        listingCount
                    );

                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select 1-5.");
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    // Creativity feature:
    // Tracks how many times each activity has been completed
    // during the current program session and displays the results.
    // This exceeds the basic assignment requirements.

    static void DisplayStatistics(
        int breathing,
        int reflection,
        int listing
    )
    {
        Console.WriteLine();
        Console.WriteLine("===== Session Statistics =====");
        Console.WriteLine($"Breathing activities: {breathing}");
        Console.WriteLine($"Reflection activities: {reflection}");
        Console.WriteLine($"Listing activities: {listing}");

        int total = breathing + reflection + listing;

        Console.WriteLine($"Total activities completed: {total}");
        Console.WriteLine("==============================");

        Console.WriteLine();
        Console.WriteLine("Press Enter to return to the menu.");
        Console.ReadLine();
    }
}
