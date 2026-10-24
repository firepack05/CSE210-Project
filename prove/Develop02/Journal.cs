
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("Your journal is empty.");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                string date = Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(entry.GetDate()));
                string prompt = Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(entry.GetPrompt()));
                string response = Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(entry.GetResponse()));

                writer.WriteLine($"{date}|{prompt}|{response}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        string[] lines = File.ReadAllLines(filename);
        List<Entry> loadedEntries = new List<Entry>();

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = line.Split('|');

            if (parts.Length != 3)
            {
                throw new FormatException("Invalid journal entry format.");
            }

            string date = Encoding.UTF8.GetString(
                Convert.FromBase64String(parts[0]));
            string prompt = Encoding.UTF8.GetString(
                Convert.FromBase64String(parts[1]));
            string response = Encoding.UTF8.GetString(
                Convert.FromBase64String(parts[2]));

            loadedEntries.Add(new Entry(date, prompt, response));
        }

        // Loading replaces the current journal with the saved entries.
        _entries = loadedEntries;
    }

    // Extra feature: Find previous entries by searching response keywords.
    public void SearchEntries(string keyword)
    {
        int matches = 0;

        foreach (Entry entry in _entries)
        {
            if (entry.GetResponse().Contains(
                keyword, StringComparison.OrdinalIgnoreCase))
            {
                entry.Display();
                matches++;
            }
        }

        if (matches == 0)
        {
            Console.WriteLine("No matching entries found.");
        }
        else
        {
            Console.WriteLine($"Found {matches} matching entr(y/ies).");
        }
    }
}
