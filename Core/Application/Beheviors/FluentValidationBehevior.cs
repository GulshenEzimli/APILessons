using FluentValidation;
using MediatR;

namespace Application.Beheviors
{
    public class FluentValidationBehevior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public FluentValidationBehevior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators.Select(v => v.Validate(context)).SelectMany(result => result.Errors)
                                      .GroupBy(e => e.ErrorMessage).Select(e => e.First())
                                      .Where(f => f != null).ToList();
            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return await next();

        }
    }
}
