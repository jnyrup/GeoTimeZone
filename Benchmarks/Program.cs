using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using GeoTimeZone;

BenchmarkRunner.Run<MyClass>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net48)]
[SimpleJob(RuntimeMoniker.Net60)]
public class MyClass
{
    public IEnumerable<Data> Coordinates()
    {
        yield return new Data(0d, 0d, "0 timezones");
        yield return new Data(-31.55, 159.0833, "1 timezone");
        yield return new Data(47.589983, 7.587417, "3 timezones");
    }

    [Benchmark]
    [ArgumentsSource(nameof(Coordinates))]
    public TimeZoneResult GetTimeZone(Data data) => TimeZoneLookup.GetTimeZone(data.Lat, data.Lon);
}

public sealed class Data
{
    private readonly string name;

    public Data(double lat, double lon, string name)
    {
        Lat = lat;
        Lon = lon;
        this.name = name;
    }

    public double Lat { get; }

    public double Lon { get; }

    public override string ToString() => name;
}
