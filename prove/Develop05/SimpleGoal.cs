
using System;

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string shortName, string description, int points)
        : base(shortName, description, points)
    {
        _isComplete = false;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return base.RecordEvent();
        }

        return 0;
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetDetailsString()
    {
        string status = _isComplete ? "X" : " ";
        return $"[{status}] {base.GetDetailsString()}";
    }

    public override string GetStringRepresentation()
    {
        return $"{base.GetStringRepresentation()}|{_isComplete}";
    }
}
