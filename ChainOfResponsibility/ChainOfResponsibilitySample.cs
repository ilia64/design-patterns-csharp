// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract

#pragma warning disable CS8603
#pragma warning disable CS8618

namespace DesignPatterns.ChainOfResponsibility;

public class Enemy
{
}

public class Door
{
}

public class Cat
{
}

public interface IHandler
{
    IHandler SetNext(IHandler next);

    string Handle(object request);
}

public abstract class AHandler : IHandler
{
    private IHandler _next;

    public IHandler SetNext(IHandler next)
    {
        _next = next;

        return _next;
    }

    public virtual string Handle(object request)
    {
        if (_next != null)
        {
            return _next.Handle(request);
        }

        return "i don't know";
    }
}

public class EnemyHandler : AHandler
{
    public override string Handle(object request)
    {
        if (request is Enemy)
        {
            return $"Kill {request.GetType().Name}";
        }

        return base.Handle(request);
    }
}

public class DoorHandler : AHandler
{
    public override string Handle(object request)
    {
        if (request is Door)
        {
            return $"Open {request.GetType().Name}";
        }

        return base.Handle(request);
    }
}

public class CatHandler : AHandler
{
    public override string Handle(object request)
    {
        if (request is Cat)
        {
            return $"Feed {request.GetType().Name}";
        }

        return base.Handle(request);
    }
}

public static class ChainOfResponsibilitySample
{
    public static void Test()
    {
        Console.WriteLine("ChainOfResponsibility:");

        var handler = new EnemyHandler()
            .SetNext(new DoorHandler())
            .SetNext(new CatHandler());

        var targets = new List<object>
        {
            new Enemy(),
            new Door(),
            new Cat(),
            "Error",
        };

        foreach (var target in targets)
        {
            Console.WriteLine($"\t Interaction {target.GetType().Name} result: {handler.Handle(target)}");
        }
    }
}