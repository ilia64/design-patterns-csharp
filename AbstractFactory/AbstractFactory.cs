namespace DesignPatterns.AbstractFactory;

public interface ICastle
{
    string GetTax(IBuilding building);
}

public interface IBuilding
{
    string GetBuildingName();
}

public abstract class ACastle : ICastle
{
    protected abstract string GetName();

    public string GetTax(IBuilding building)
    {
        return $"\"{GetName()}\" get tax from \"{building.GetBuildingName()}\".";
    }
}

public class WinterCastle : ACastle
{
    protected override string GetName() => "Castle of snow";
}

public class WinterBuilding : IBuilding
{
    public string GetBuildingName() => "Cold building";
}

public class SummerCastle : ACastle
{
    protected override string GetName() => "Castle at the sun";
}

public class SummerBuilding : IBuilding
{
    public string GetBuildingName() => "Building with flowers";
}

public interface ICityFactory
{
    ICastle CreateCastle();
    IBuilding CreateBuilding();
}

public class WinterCityFactory : ICityFactory
{
    public ICastle CreateCastle() => new WinterCastle();
    public IBuilding CreateBuilding() => new WinterBuilding();
}

public class SummerCityFactory : ICityFactory
{
    public ICastle CreateCastle() => new SummerCastle();
    public IBuilding CreateBuilding() => new SummerBuilding();
}

public class City
{
    private ICityFactory _factory;

    public City(ICityFactory factory)
    {
        _factory = factory;
    }

    public void ChangeFactory(ICityFactory factory)
    {
        _factory = factory;
    }

    public void Run()
    {
        var building = _factory.CreateBuilding();
        var castle = _factory.CreateCastle();

        var taxOrder = castle.GetTax(building);

        Console.WriteLine($"\t{taxOrder}");
    }
}

public static class AbstractFactorySample
{
    public static void Test()
    {
        Console.WriteLine("AbstractFactory:");

        var city = new City(new SummerCityFactory());
        city.Run();

        city.ChangeFactory(new WinterCityFactory());
        city.Run();
    }
}