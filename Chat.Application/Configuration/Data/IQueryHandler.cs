using MediatR;

namespace Chat.Application.Configuration.Data
{
    public interface IQueryHandler<in TQuery, TResult> :
         IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {

    }
}
