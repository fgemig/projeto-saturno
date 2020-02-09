using Saturno.Domain.Common.Models;
using System.Threading.Tasks;

namespace Saturno.Domain.Common.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        Task<GenericCommandResult> Handle(T command);
    }
}
