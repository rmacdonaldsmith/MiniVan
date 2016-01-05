using System;
using MiniVan.Bus;

namespace MiniVan.Consumers
{
	public class DelegatingQueryHandler<Rq, Rs> : IHandleQueries<Rq, Rs>
		where Rq : class, IRequest<Rs>
		where Rs : class, IMessage
	{
		private readonly Func<Rq, Rs> _delegate;

		public DelegatingQueryHandler(Func<Rq, Rs> delegatingFunction)
		{
			Ensure.NotNull(delegatingFunction, "delegatingFunction");
			_delegate = delegatingFunction;
		}

		public Rs Handle(Rq request)
		{
			return _delegate(request);
		}
	}
}
