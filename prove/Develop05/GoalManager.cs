
using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine($"You have {_score} points.");
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. Quit");
            Console.Write("Select a choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddGoal();
                    break;

                case "2":
                    DisplayGoals();
                    break;

                case "3":
                    RecordGoalEvent();
                    break;

                case "4":
                    SaveGoals();
                    break;

                case "5":
                    LoadGoals();
                    break;

                case "6":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    public void AddGoal()
    {
        Console.WriteLine();
        Console.WriteLine("The types of goals are:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");

        string choice = Console.ReadLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description? ");
        string description = Console.ReadLine();

        Console.Write("How many points is this goal worth? ");
        int points = int.Parse(Console.ReadLine());

        if (choice == "1")
        {
            _goals.Add(new SimpleGoal(name, description, points));
        }
        else if (choice == "2")
        {
            _goals.Add(new EternalGoal(name, description, points));
        }
        else if (choice == "3")
        {
            Console.Write("How many times must it be completed? ");
            int target = int.Parse(Console.ReadLine());

            Console.Write("How many bonus points are awarded? ");
            int bonus = int.Parse(Console.ReadLine());

            _goals.Add(
                new ChecklistGoal(name, description, points, target, bonus)
            );
        }
        else
        {
            Console.WriteLine("Invalid goal type.");
            return;
        }

        Console.WriteLine("Goal created successfully!");
    }

    public void DisplayGoals()
    {
        Console.WriteLine();
        Console.WriteLine("Your Goals:");

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }

        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet.");
        }
    }

    public void RecordGoalEvent()
    {
        DisplayGoals();

        if (_goals.Count == 0)
        {
            return;
        }

        Console.Write("Which goal did you accomplish? ");
        int choice = int.Parse(Console.ReadLine());

        if (choice >= 1 && choice <= _goals.Count)
        {
            Goal selectedGoal = _goals[choice - 1];

            int earnedPoints = selectedGoal.RecordEvent();

            _score += earnedPoints;

            Console.WriteLine($"You earned {earnedPoints} points!");
            Console.WriteLine($"Your total score is {_score}.");
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Your total score is {_score}.");
    }

    public void SaveGoals()
    {
        List<string> lines = new List<string>();

        lines.Add($"SCORE|{_score}");

        foreach (Goal goal in _goals)
        {
            lines.Add(goal.GetStringRepresentation());
        }

        File.WriteAllLines("goals.txt", lines);

        Console.WriteLine("Goals saved successfully!");
    }

    public void LoadGoals()
    {
        if (!File.Exists("goals.txt"))
        {
            Console.WriteLine("No saved goals found.");
            return;
        }

        _goals.Clear();

        string[] lines = File.ReadAllLines("goals.txt");

        foreach (string line in lines)
        {
            string[] parts = line.Split('|');

            if (parts[0] == "SCORE")
            {
                _score = int.Parse(parts[1]);
            }
            else if (parts[0] == "SimpleGoal")
            {
                SimpleGoal goal = new SimpleGoal(
                    parts[1], parts[2], int.Parse(parts[3])
                );

                if (bool.Parse(parts[4]))
                {
                    goal.RecordEvent();
                }

                _goals.Add(goal);
            }
            else if (parts[0] == "EternalGoal")
            {
                _goals.Add(new EternalGoal(
                    parts[1], parts[2], int.Parse(parts[3])
                ));
            }
            else if (parts[0] == "ChecklistGoal")
            {
                ChecklistGoal goal = new ChecklistGoal(
                    parts[1],
                    parts[2],
                    int.Parse(parts[3]),
                    int.Parse(parts[5]),
                    int.Parse(parts[6])
                );

                int completed = int.Parse(parts[4]);

                for (int i = 0; i < completed; i++)
                {
                    goal.RecordEvent();
                }

                _goals.Add(goal);
            }
        }

        Console.WriteLine("Goals loaded successfully!");
    }
}
