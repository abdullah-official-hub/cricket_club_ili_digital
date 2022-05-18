using System.Threading.Tasks;

namespace Framework.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}