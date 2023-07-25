namespace DesignPatterns.Observer;

public interface IObserver<T> where T : struct
{
    void Update(T value);
}

public interface IObservable<T> where T : struct
{
    public T Value { get; set; }

    void Subscribe(IObserver<T> observer);
    void Unsubscribe(IObserver<T> observer);
    void Notify();
}

public class ObservableField<T> : IObservable<T> where T : struct
{
    private readonly HashSet<IObserver<T>> _observers = new();

    private T _value = default!;

    public T Value
    {
        get => _value;
        set
        {
            if (Equals(_value, value))
                return;

            _value = value;
            Notify();
        }
    }

    public void Subscribe(IObserver<T> observer)
        => _observers.Add(observer);

    public void Unsubscribe(IObserver<T> observer)
        => _observers.Remove(observer);

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_value);
        }
    }
}

public interface IDamageable
{
    public IObservable<int> Health { get; }
    void AddDamage(int damage);
}

public class Hero : IDamageable
{
    public IObservable<int> Health { get; }

    public Hero(int health)
    {
        Health = new ObservableField<int> { Value = health };
    }

    public void AddDamage(int damage)
        => Health.Value -= damage;
}

public class HealthBar : IObserver<int>
{
    private readonly IDamageable _target;

    public HealthBar(IDamageable target)
    {
        _target = target;
    }

    public void Enable()
    {
        _target.Health.Subscribe(this);

        Update(_target.Health.Value);
    }

    public void Disable()
    {
        _target.Health.Unsubscribe(this);
    }

    public void Update(int value)
    {
        Console.WriteLine($"\tHealthBar: {value}hp");
    }
}

public class Enemy : IObserver<int>, IDisposable
{
    private enum State
    {
        Idle = 0,
        Attack = 1,
    }

    private const int Damage = 25;
    private readonly IDamageable _target;

    public bool CanAttack => _state == State.Attack;

    private State _state;

    public Enemy(IDamageable target)
    {
        _target = target;

        _target.Health.Subscribe(this);

        Update(_target.Health.Value);
    }

    public void Attack()
    {
        _target.AddDamage(Damage);
    }

    public void Update(int value)
    {
        _state = value > 0 ? State.Attack : State.Idle;
    }

    public void Dispose()
    {
        _target.Health.Unsubscribe(this);
    }
}

public static class ObserverSample
{
    public static void Test()
    {
        Console.WriteLine("Observer:");

        var hero = new Hero(100);
        var heroHealthBar = new HealthBar(hero);

        heroHealthBar.Enable();

        using (var enemy = new Enemy(hero))
        {
            while (enemy.CanAttack)
            {
                enemy.Attack();
            }
        }

        heroHealthBar.Disable();
    }
}