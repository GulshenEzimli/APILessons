using FluentValidation;
using MediatR;

namespace Application.Behaviors
{
	public class FluentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> validators;

		public FluentValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
		{
			this.validators = validators;
		}
		public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			var context = new ValidationContext<TRequest>(request);
			var failures = validators.Select(v => v.Validate(context)).SelectMany(result => result.Errors)
									.GroupBy(error => error.ErrorMessage).Select(g => g.First())
									.Where(f => f != null).ToList();

			if (failures.Any())
				throw new ValidationException(failures);

			return next();
		}
	}
}
