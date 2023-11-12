using MeasuringApplication.Server;

namespace MeasuringApplication.Interfaces
{
    public interface IServer
    {
        string Name { get; set; }

        GetPackageResults GetPackage(List<string> package);
    }
}
