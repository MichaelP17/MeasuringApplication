using MeasuringApplication.Interfaces;

namespace MeasuringApplication.Server
{
    /// <summary>
    /// I use this to simulate the possible responses from the server
    /// </summary>
    public enum GetPackageResults
    {
        // Everything successful
        Ok,

        // Path is not valid anymore
        InvalidPath,

        // Server cant be reached
        UnreachableServer,

        // Communication with a package failed
        PackageComFailed,

        // Value within a package has malfunctions
        PackageValueMalfunction,
    }

    public class MockServer : IServer
    {
        private readonly Random mRandom;
        public string Name { get; set; }

        public MockServer(string name)
        {
            mRandom = new Random();
            Name = name;
        }

        public GetPackageResults GetPackage(List<string> package)
        {
            var randomResultCode = mRandom.Next(4);
            var result = (GetPackageResults)randomResultCode;

            return result;
        }
    }
}
