using MediatR;

namespace Sparkle.Transfer.Interfaces
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
