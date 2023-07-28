namespace DesignPatterns.Adapter;

public interface IAnalyticsClientAdapter
{
    void SendEvent(string type, string data);
}

public class YaAnalytics
{
    public void ReportEvent(string key, object parameters)
    {
        Console.WriteLine($"\t{GetType().Name} report event {key} with {parameters}");
    }
}

public class FireAnalytics
{
    public void LogEvent(string analyticsEvent, params string[] parameters)
    {
        Console.WriteLine($"\t{GetType().Name} log event {analyticsEvent} with {string.Join(", ", parameters)}");
    }
}

public class YaAnalyticsClientAdapter : IAnalyticsClientAdapter
{
    private readonly YaAnalytics _analytics;

    public YaAnalyticsClientAdapter(YaAnalytics analytics)
    {
        _analytics = analytics;
    }

    public void SendEvent(string type, string data)
    {
        _analytics.ReportEvent(type, data);
    }
}

public class FireAnalyticsClientAdapter : IAnalyticsClientAdapter
{
    private readonly FireAnalytics _analytics;

    public FireAnalyticsClientAdapter(FireAnalytics analytics)
    {
        _analytics = analytics;
    }

    public void SendEvent(string type, string data)
    {
        _analytics.LogEvent(type, data);
    }
}

public class AnalyticsClient
{
    private readonly List<IAnalyticsClientAdapter> _list = new();

    public void Initialize()
    {
        _list.Add(new FireAnalyticsClientAdapter(new FireAnalytics()));
        _list.Add(new YaAnalyticsClientAdapter(new YaAnalytics()));
    }

    public void SendEvent(string type, string data)
    {
        foreach (var client in _list)
        {
            client.SendEvent(type, data);
        }
    }
}

public static class AdapterSample
{
    public static void Test()
    {
        Console.WriteLine("Adapter:");

        var analytics = new AnalyticsClient();
        analytics.Initialize();
        analytics.SendEvent("GameInit", "ios:17");
    }
}