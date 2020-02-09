using Saturno.Domain.Models;
using System.Threading.Tasks;

namespace Saturno.Domain.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        Task<GenericCommandResult> Handle(T command);
    }
}
