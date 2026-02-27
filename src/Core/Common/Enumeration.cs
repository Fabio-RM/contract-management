using System.Reflection;

namespace Core.Common;

public abstract class Enumeration : IComparable
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    protected Enumeration(int id, string name) => (Id, Name) = (id, name);

    public override string ToString() => Name;
    
    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))
            .Cast<T>();
    
    public override bool Equals(object obj)
    {
        if (obj is not Enumeration other) return false;
        
        var typesMatch = GetType() == obj.GetType();
        var valuesMatch = Id == other.Id;
        
        return typesMatch && valuesMatch;
    }
    
    public override int GetHashCode() => Id.GetHashCode();

    public int CompareTo(object obj) => Id.CompareTo(((Enumeration)obj).Id);
}