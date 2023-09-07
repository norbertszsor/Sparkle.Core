using MediatR;

namespace Sparkle.Transfer
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
