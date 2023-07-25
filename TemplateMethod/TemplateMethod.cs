namespace DesignPatterns.TemplateMethod;

public abstract class Bootstrap
{
    protected readonly string Name;

    protected Bootstrap()
    {
        Name = GetType().Name;
    }

    public void Run()
    {
        InitGame();
        InitPermissions();
        InitStore();
    }

    private void InitGame()
    {
        Console.WriteLine($"\t{Name}: Init game");
    }

    protected virtual void InitPermissions()
    {
        Console.WriteLine($"\t{Name}: Init permissions");
    }

    protected abstract void InitStore();
}

public class AndroidBootstrap : Bootstrap
{
    protected override void InitStore()
    {
        Console.WriteLine($"\t{Name}: Init GooglePlay");
    }
}

public class IosBootstrap : Bootstrap
{
    protected override void InitStore()
    {
        Console.WriteLine($"\t{Name}: Init AppStore");
    }

    protected override void InitPermissions()
    {
        base.InitPermissions();
        Console.WriteLine($"\t{Name}: Init IOS permissions");
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
        iosBootstrap.Run();
    }
}