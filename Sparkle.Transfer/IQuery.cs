using MediatR;

namespace Sparkle.Transfer
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
