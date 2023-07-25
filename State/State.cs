#pragma warning disable CS8618

namespace DesignPatterns.State;

public abstract class AEnemyState
{
    protected const int AttackDistance = 1;

    protected StateMachine StateMachine;
    protected Hero Hero;

    public void Initialize(StateMachine stateMachine, Hero hero)
    {
        StateMachine = stateMachine;
        Hero = hero;
    }

    public abstract void Update();
}

public class FollowEnemyState : AEnemyState
{
    private const int Speed = 5;

    public override void Update()
    {
        Hero.Distance -= Speed;

        if (Hero.Health <= 0)
        {
            StateMachine.UpdateState(new WinEnemyState());
        }
        else if (Hero.Distance <= AttackDistance)
        {
            StateMachine.UpdateState(new AttackEnemyState());
        }
    }
}

public class AttackEnemyState : AEnemyState
{
    private const int Damage = 5;

    public override void Update()
    {
        Hero.Health -= Damage;

        if (Hero.Health <= 0)
        {
            StateMachine.UpdateState(new WinEnemyState());
        }
        else if (Hero.Distance > AttackDistance)
        {
            StateMachine.UpdateState(new FollowEnemyState());
        }
    }
}

public class WinEnemyState : AEnemyState
{
    public override void Update()
    {
    }
}

public class StateMachine
{
    private readonly Hero _hero;

    public AEnemyState State { get; private set; }

    public StateMachine(Hero hero)
    {
        _hero = hero;

        State = new FollowEnemyState();
        State.Initialize(this, _hero);
    }

    public void UpdateState(AEnemyState state)
    {
        State = state;
        State.Initialize(this, _hero);
    }

    public void Update() => State.Update();
}

public class Hero
{
    public int Health { get; set; } = 10;
    public int Distance { get; set; } = 15;

    public override string ToString()
    {
        return $"Hero: (health: {Health}, distance: {Distance})";
    }
}

public static class StateSample
{
    public static void Test()
    {
        Console.WriteLine("State:");

        var hero = new Hero();
        var enemy = new StateMachine(hero);

        while (enemy.State is not WinEnemyState)
        {
            enemy.Update();

            Console.WriteLine($"\t{hero} - Enemy: {enemy.State.GetType().Name}");
        }
    }
}