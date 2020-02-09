using Saturno.Domain.Commands;
using System.Threading.Tasks;

namespace Saturno.Domain.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        Task<GenericCommandResult> Handle(T command);
    }
}
