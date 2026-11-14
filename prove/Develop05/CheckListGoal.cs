
using System;

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(
        string shortName,
        string description,
        int points,
        int target,
        int bonus)
        : base(shortName, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
    }

    public override int RecordEvent()
    {
        if (!IsComplete())
        {
            _amountCompleted++;

            int earnedPoints = base.RecordEvent();

            if (_amountCompleted == _target)
            {
                earnedPoints += _bonus;
            }

            return earnedPoints;
        }

        return 0;
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        string status = IsComplete() ? "X" : " ";

        return $"[{status}] {base.GetDetailsString()} " +
               $"-- Completed {_amountCompleted}/{_target} times";
    }

    public override string GetStringRepresentation()
    {
        return $"{base.GetStringRepresentation()}|" +
               $"{_amountCompleted}|{_target}|{_bonus}";
    }
}
