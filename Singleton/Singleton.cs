namespace DesignPatterns.Singleton;

public sealed class Singleton
{
    private static Singleton _instance = null!;

    public static Singleton GetInstance()
    {
        return _instance ??= new Singleton();
    }

    private Singleton()
    {
    }

    public void DoSomething()
    {
        Console.WriteLine($"Hi i'm {GetType().Name}");
    }
}

public static class SingletonSample
{
    public static void Test()
    {
        var singleton = Singleton.GetInstance();
        singleton.DoSomething();
    }
}