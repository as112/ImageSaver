using System.Threading.Tasks;

namespace TestSystem.Client.Commands.Base
{
    internal delegate Task ActionAsync();

    internal delegate Task ActionAsync<in T>(T parameter);
}
