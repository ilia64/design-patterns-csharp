namespace DesignPatterns.TemplateMethod;

public abstract class Bootstrap
{
    public void Run()
    {
        InitGame();
        InitPermissions();
        InitStore();
    }

    private void InitGame()
    {
        Console.WriteLine("\tInit game");
    }

    protected virtual void InitPermissions()
    {
        Console.WriteLine("\tInit permissions");
    }

    protected abstract void InitStore();
}

public class AndroidBootstrap : Bootstrap
{
    protected override void InitStore()
    {
        Console.WriteLine("\tInit GooglePlay");
    }
}

public class IosBootstrap : Bootstrap
{
    protected override void InitStore()
    {
        Console.WriteLine("\tInit AppStore");
    }

    protected override void InitPermissions()
    {
        base.InitPermissions();
        Console.WriteLine("\tInit IOS permissions");
    }
}

public static class TemplateMethodSample
{
    public static void Test()
    {
        Console.WriteLine("TemplateMethod:");

        var androidBootstrap = new AndroidBootstrap();
        var iosBootstrap = new IosBootstrap();

        androidBootstrap.Run();
        Console.WriteLine("\t---");
        iosBootstrap.Run();
    }
}