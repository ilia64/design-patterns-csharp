namespace DesignPatterns.Visitor;

public interface IVisitorAcceptable
{
    void Accept(IVisitor visitor);
}

public class Enemy : IVisitorAcceptable
{
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public string Kill() => $"{GetType().Name} {nameof(Kill)}";
    public string Anger() => $"{GetType().Name} {nameof(Anger)}";
}

public class Chest : IVisitorAcceptable
{
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public string Open() => $"{GetType().Name} {nameof(Open)}";
    public string Broke() => $"{GetType().Name} {nameof(Broke)}";
}

public interface IVisitor
{
    void Visit(Enemy target);
    void Visit(Chest target);
}

public class Weapon : IVisitor
{
    public void Visit(Enemy target)
    {
        var result = target.Kill();
        Console.WriteLine($"\t{result} by {GetType().Name}");
    }

    public void Visit(Chest target)
    {
        var result = target.Broke();
        Console.WriteLine($"\t{result} by {GetType().Name}");
    }
}

public class Key : IVisitor
{
    public void Visit(Enemy target)
    {
        var result = target.Anger();
        Console.WriteLine($"\t{result} by {GetType().Name}");
    }

    public void Visit(Chest target)
    {
        var result = target.Open();
        Console.WriteLine($"\t{result} by {GetType().Name}");
    }
}

public static class VisitorSample
{
    public static void Test()
    {
        Console.WriteLine("Visitor play quest:");
        
        var visitors = new List<IVisitor>
        {
            new Weapon(),
            new Key(),
        };

        var targets = new List<IVisitorAcceptable>
        {
            new Enemy(),
            new Chest(),
        };

        foreach (var visitor in visitors)
        {
            foreach (var target in targets)
            {
                target.Accept(visitor);
            }
        }
    }
}