using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Creativity feature:
        // The user can choose between multiple scriptures or let the
        // program select a random scripture from the library.
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world that he gave his only begotten Son that whosoever believeth in him should not perish but have everlasting life."
            ),

            new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart and lean not unto thine own understanding. In all thy ways acknowledge him and he shall direct thy paths."
            ),

            new Scripture(
                new Reference("Philippians", 4, 13),
                "I can do all things through Christ which strengtheneth me."
            )
        };

        Console.WriteLine("SCRIPTURE MEMORIZER");
        Console.WriteLine("Choose a scripture:");
        Console.WriteLine("1. John 3:16");
        Console.WriteLine("2. Proverbs 3:5-6");
        Console.WriteLine("3. Philippians 4:13");
        Console.WriteLine("4. Random scripture");

        Console.Write("Enter your choice (1-4): ");
        string choice = Console.ReadLine();

        Random random = new Random();
        Scripture scripture;

        switch (choice)
        {
            case "1":
                scripture = scriptures[0];
                break;

            case "2":
                scripture = scriptures[1];
                break;

            case "3":
                scripture = scriptures[2];
                break;

            case "4":
                scripture = scriptures[random.Next(scriptures.Count)];
                break;

            default:
                Console.WriteLine("Invalid choice. Selecting a random scripture.");
                scripture = scriptures[random.Next(scriptures.Count)];
                break;
        }

        while (true)
        {
            Console.Clear();

            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("All words are hidden! Great job memorizing!");
                break;
            }

            Console.Write("Press Enter to hide words or type 'quit' to exit: ");
            string input = Console.ReadLine();

            if (input != null &&
                input.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            scripture.HideRandomWords(3);
        }
    }
}
