namespace DesignPatterns.Strategy;

public interface IMoveBehaviour
{
    void Move(object data);
}

public class WalkMoveBehaviour : IMoveBehaviour
{
    public void Move(object target) => Console.WriteLine($"\t{target.GetType().Name} Walk");
}

public class SwimMoveBehaviour : IMoveBehaviour
{
    public void Move(object target) => Console.WriteLine($"\t{target.GetType().Name} Swim");
}

public class FlyMoveBehaviour : IMoveBehaviour
{
    public void Move(object target) => Console.WriteLine($"\t{target.GetType().Name} Fly");
}

public class Hero
{
    private IMoveBehaviour _moveBehaviour;

    public Hero(IMoveBehaviour moveBehaviour)
    {
        _moveBehaviour = moveBehaviour;
    }

    public void SetMoveBehaviour(IMoveBehaviour moveBehaviour)
    {
        _moveBehaviour = moveBehaviour;
    }

    public void FixedUpdate()
    {
        _moveBehaviour.Move(this);
    }
}

public static class StrategySample
{
    public static void Test()
    {
        Console.WriteLine("Strategy:");

        var list = new List<IMoveBehaviour>()
        {
            new WalkMoveBehaviour(),
            new SwimMoveBehaviour(),
            new FlyMoveBehaviour(),
        };

        var hero = new Hero(list.First());
        foreach (var moveBehaviour in list)
        {
            hero.SetMoveBehaviour(moveBehaviour);
            hero.FixedUpdate();
        }
    }
}