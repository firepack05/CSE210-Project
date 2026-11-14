
using System;

public abstract class Goal
{
    private string _shortName;
    private string _description;
    private int _points;

    public Goal(string shortName, string description, int points)
    {
        _shortName = shortName;
        _description = description;
        _points = points;
    }

    public virtual int RecordEvent()
    {
        return _points;
    }

    public abstract bool IsComplete();

    public virtual string GetDetailsString()
    {
        return $"{_shortName}: {_description}";
    }

    public virtual string GetStringRepresentation()
    {
        return $"{GetType().Name}|{_shortName}|{_description}|{_points}";
    }

    protected int GetPoints()
    {
        return _points;
    }

    protected string GetShortName()
    {
        return _shortName;
    }

    protected string GetDescription()
    {
        return _description;
    }
}
