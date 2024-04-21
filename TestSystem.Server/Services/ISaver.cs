using TestSystem.Core.Models;

namespace TestSystem.Server.Services
{
    public interface ISaver<T> where T : BaseFile
    {
        void SaveInfo(T item, string directory);
        IEnumerable<T> GetList(string directory);
    }
}
