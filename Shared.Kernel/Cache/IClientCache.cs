using Shared.Kernel.Models;

namespace Shared.Kernel.Cache;

public interface IClientCache
{
    public Task<Client> GetClientById(Guid id);
}