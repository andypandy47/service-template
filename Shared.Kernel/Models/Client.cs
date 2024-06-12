using Shared.Kernel.Enums;

namespace Shared.Kernel.Models;

public record Client
{
    Guid Id { get; }
    string Name { get; }
    IEnumerable<Module> Modules { get; }
}