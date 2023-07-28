namespace DesignPatterns.Builder;

public class HouseBuilder
{
    private List<string> _product = new();

    public HouseBuilder WithFacade()
    {
        _product.Add("Facade");

        return this;
    }

    public HouseBuilder WithShop(string name)
    {
        _product.Add(name + " shop");

        return this;
    }

    public HouseBuilder WithFloor(int flats)
    {
        _product.Add($"Leaving floor (flats:{flats})");

        return this;
    }

    public HouseBuilder WithRoof()
    {
        _product.Add("Roof");

        return this;
    }

    public string Build()
    {
        var result = string.Join(", ", _product);

        Reset();

        return result;
    }

    private void Reset() => _product = new List<string>();
}

public class HouseBuildDirector
{
    private readonly HouseBuilder _builder;

    public HouseBuildDirector(HouseBuilder builder)
    {
        _builder = builder;
    }

    public string BuildCountryHouse()
    {
        return _builder
            .WithFacade()
            .WithFloor(1)
            .WithRoof()
            .Build();
    }

    public string BuildHouseFromData(int floorCount, int flatPerFloor, string shopName)
    {
        _builder
            .WithFacade()
            .WithShop(shopName);

        for (var i = 0; i < floorCount; i++)
        {
            _builder.WithFloor(flatPerFloor);
        }

        return _builder
            .WithRoof()
            .Build();
    }
}

public static class BuilderSample
{
    public static void Test()
    {
        Console.WriteLine("Builder:");

        var builder = new HouseBuilder();
        var director = new HouseBuildDirector(builder);
        var countryHouse = director.BuildCountryHouse();
        var cityHouse = director.BuildHouseFromData(3, 2, "Sneaker");

        Console.WriteLine($"\t Build Country House with: {countryHouse}");
        Console.WriteLine($"\t Build City House with: {cityHouse}");
    }
}