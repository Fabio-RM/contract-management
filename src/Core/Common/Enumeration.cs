using System.Reflection;

namespace Core.Common;

public abstract class Enumeration : IComparable
{
    public int Id { get; }
    public string DisplayName { get; }

    protected Enumeration(int id, string displayName)
    {
        Id = id;
        DisplayName = displayName;
    }

    public override string ToString() => DisplayName;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        var fields = typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        return fields
            .Select(f => f.GetValue(null))
            .Cast<T>();
    }

    public static T FromId<T>(int id) where T : Enumeration
    {
        var matchingItem = GetAll<T>()
            .FirstOrDefault(item => item.Id == id);

        if (matchingItem is null)
            throw new InvalidOperationException(
                $"'{id}' is not a valid id for {typeof(T).Name}");

        return matchingItem;
    }

    public static T FromDisplayName<T>(string displayName) where T : Enumeration
    {
        var matchingItem = GetAll<T>()
            .FirstOrDefault(item => item.DisplayName == displayName);

        if (matchingItem is null)
            throw new InvalidOperationException(
                $"'{displayName}' is not a valid display name for {typeof(T).Name}");

        return matchingItem;
    }

    public int CompareTo(object? other) =>
        Id.CompareTo(((Enumeration)other!).Id);

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherValue)
            return false;

        var typeMatches = GetType() == obj.GetType();
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode() =>
        HashCode.Combine(GetType(), Id);

    public static bool operator ==(Enumeration left, Enumeration right) =>
        left.Equals(right);

    public static bool operator !=(Enumeration left, Enumeration right) =>
        !(left == right);
}