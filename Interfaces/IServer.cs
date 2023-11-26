using MeasuringApplication.Models;
using MeasuringApplication.Server;

namespace MeasuringApplication.Interfaces
{
    public interface IServer
    {
        string Name { get; set; }

        GetPackageResults GetPackage(Package package);
    }
}
