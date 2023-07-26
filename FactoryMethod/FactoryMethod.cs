namespace DesignPatterns.FactoryMethod;

public interface ICell
{
    bool CanMove();
}

public class IsoCell : ICell
{
    public bool CanMove() => true;
}

public class HexagonalCell : ICell
{
    public bool CanMove() => true;
}

public abstract class AMaze
{
    protected abstract ICell CreateCell();

    public void Build()
    {
        var cell = CreateCell();

        Console.WriteLine($"\t{GetType().Name} create {cell.GetType().Name}");
    }
}

public class IsoMaze : AMaze
{
    protected override ICell CreateCell() => new IsoCell();
}

public class HexagonalMaze : AMaze
{
    protected override ICell CreateCell() => new HexagonalCell();
}

public static class FactoryMethodSample
{
    public static void Test()
    {
        Console.WriteLine("FactoryMethod:");

        AMaze? maze;

        maze = new IsoMaze();
        maze.Build();

        maze = new HexagonalMaze();
        maze.Build();
    }
}