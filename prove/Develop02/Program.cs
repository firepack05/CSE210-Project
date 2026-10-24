
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();

        string[] prompts =
        {
            "What was the best part of my day?",
            "Who did I help today, and how?",
            "How did I see the hand of the Lord in my life today?",
            "What is something new I learned today?",
            "What made me smile today?",
            "What am I grateful for today?",
            "What would I do differently if I could repeat today?"
        };

        Random random = new Random();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nJournal Menu");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal");
            Console.WriteLine("4. Load the journal");
            Console.WriteLine("5. Search entries");
            Console.WriteLine("6. Quit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    string prompt = prompts[random.Next(prompts.Length)];

                    Console.WriteLine($"\nPrompt: {prompt}");
                    Console.Write("> ");

                    string response = Console.ReadLine() ?? "";
                    string date = DateTime.Now.ToShortDateString();

                    journal.AddEntry(new Entry(date, prompt, response));
                    Console.WriteLine("Entry added!");
                    break;

                case "2":
                    journal.DisplayAll();
                    break;

                case "3":
                    Console.Write("Filename to save (for example, journal.txt): ");
                    string saveFile = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(saveFile))
                    {
                        Console.WriteLine("Filename cannot be empty.");
                        break;
                    }

                    try
                    {
                        journal.SaveToFile(saveFile);
                        Console.WriteLine("Journal saved!");
                    }
                    catch (Exception ex) when (
                        ex is IOException || ex is UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Save failed: {ex.Message}");
                    }
                    break;

                case "4":
                    Console.Write("Filename to load: ");
                    string loadFile = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(loadFile))
                    {
                        Console.WriteLine("Filename cannot be empty.");
                        break;
                    }

                    try
                    {
                        journal.LoadFromFile(loadFile);
                        Console.WriteLine("Journal loaded!");
                    }
                    catch (Exception ex) when (
                        ex is IOException ||
                        ex is UnauthorizedAccessException ||
                        ex is FormatException ||
                        ex is ArgumentException)
                    {
                        Console.WriteLine($"Load failed: {ex.Message}");
                    }
                    break;

                case "5":
                    Console.Write("Keyword to search: ");
                    string keyword = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(keyword))
                    {
                        Console.WriteLine("Please enter a keyword.");
                        break;
                    }

                    journal.SearchEntries(keyword);
                    break;

                case "6":
                    running = false;
                    Console.WriteLine("Goodbye! Keep journaling.");
                    break;

                default:
                    Console.WriteLine("Please choose a number from 1 to 6.");
                    break;
            }
        }
    }
}

/*
 * Creativity and Exceeding Requirements:
 * I added a keyword search feature so users can find journal entries
 * without displaying the entire journal.
 * I also encode saved fields so punctuation and separators in responses
 * do not break the file format.
 */
