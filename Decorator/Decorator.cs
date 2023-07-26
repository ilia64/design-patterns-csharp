namespace DesignPatterns.Decorator;

public struct Stats
{
    public float Health;
    public float Damage;

    public static Stats operator +(Stats a, Stats b) => new()
    {
        Health = a.Health + b.Health,
        Damage = a.Damage + b.Damage,
    };

    public static Stats operator *(Stats a, float multiplier) => new()
    {
        Health = a.Health * multiplier,
        Damage = a.Damage * multiplier,
    };

    public override string ToString()
    {
        return $"{nameof(Health)}: {Health}, {nameof(Damage)}: {Damage}";
    }
}

public interface IStatsProvider
{
    Stats GetStats();
}

public class EntityStats : IStatsProvider
{
    private readonly Stats _stats;

    public EntityStats(Stats stats)
    {
        _stats = stats;
    }

    public Stats GetStats() => _stats;
}

public abstract class AStatsDecorator : IStatsProvider
{
    protected readonly IStatsProvider Parent;

    protected AStatsDecorator(IStatsProvider parent)
    {
        Parent = parent;
    }

    public abstract Stats GetStats();
}

public class Academy
{
    public readonly float StatsMultiplier;

    public Academy(float statsMultiplier)
    {
        StatsMultiplier = statsMultiplier;
    }
}

public class AcademyStats : AStatsDecorator
{
    private readonly Academy _academy;

    public AcademyStats(IStatsProvider parent, Academy academy) : base(parent)
    {
        _academy = academy;
    }

    public override Stats GetStats() => Parent.GetStats() * _academy.StatsMultiplier;
}

public class AmmoStats : AStatsDecorator
{
    private readonly Stats _weapon;
    private readonly Stats _armor;

    public AmmoStats(IStatsProvider parent, Stats weapon, Stats armor) : base(parent)
    {
        _weapon = weapon;
        _armor = armor;
    }

    public override Stats GetStats()
    {
        return Parent.GetStats() + _weapon + _armor;
    }
}

public static class DecoratorSample
{
    public static void Test()
    {
        Console.WriteLine("Decorator:");

        var academy = new Academy(1.2f);

        IStatsProvider heroStatsProvider = new EntityStats(new Stats { Health = 10, Damage = 2 });
        heroStatsProvider = new AcademyStats(heroStatsProvider, academy);
        heroStatsProvider = new AmmoStats(heroStatsProvider,
            weapon: new Stats { Health = 0, Damage = 4 },
            armor: new Stats { Health = 3, Damage = 0 }
        );

        var stats = heroStatsProvider.GetStats();

        Console.WriteLine($"\tHeroStats: {stats}");
    }
}