using Saturno.Domain.Commands;
using Saturno.Domain.Commands.Contracts;
using System.Threading.Tasks;

namespace Saturno.Domain.Handlers.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        Task<GenericCommandResult> Handle(T command);
    }
}
