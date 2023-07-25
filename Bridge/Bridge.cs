namespace DesignPatterns.Bridge;

public interface IMoveBehaviour
{
    string Move();
}

public class WalkMoveBehaviour : IMoveBehaviour
{
    public string Move() => "Walk";
}

public class SwimMoveBehaviour : IMoveBehaviour
{
    public string Move() => "Swim";
}

public class FlyMoveBehaviour : IMoveBehaviour
{
    public string Move() => "Fly";
}

public abstract class Animal
{
    protected readonly IMoveBehaviour MoveBehaviour;

    protected Animal(IMoveBehaviour moveBehaviour)
    {
        MoveBehaviour = moveBehaviour;
    }

    public virtual void Move()
    {
        var result = MoveBehaviour.Move();
        Console.WriteLine($"\t{GetType().Name} {result}");
    }
}

public class Cat : Animal
{
    public Cat(IMoveBehaviour moveBehaviour) : base(moveBehaviour)
    {
    }
}

public class Fish : Animal
{
    public Fish(IMoveBehaviour moveBehaviour) : base(moveBehaviour)
    {
    }
}

public class Bird : Animal
{
    private const string Pattern = "\tI a'm little {0} and i can {1}";

    public Bird(IMoveBehaviour moveBehaviour) : base(moveBehaviour)
    {
    }

    public override void Move()
    {
        var result = MoveBehaviour.Move();
        Console.WriteLine(Pattern, GetType().Name, result);
    }
}

public static class BridgeSample
{
    public static void Test()
    {
        Console.WriteLine("Bridge:");

        var list = new List<Animal>
        {
            new Cat(new WalkMoveBehaviour()),
            new Cat(new FlyMoveBehaviour()),
            new Cat(new SwimMoveBehaviour()),
            new Fish(new SwimMoveBehaviour()),
            new Bird(new WalkMoveBehaviour()),
            new Bird(new FlyMoveBehaviour()),
        };

        foreach (var animal in list)
        {
            animal.Move();
        }
    }
}