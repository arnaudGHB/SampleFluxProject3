using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CBS.CheckManagement.Dto;

namespace CBS.CheckManagement.MediatR.PipeLineBehavior
{
    /// <summary>
    /// A pipeline behavior for validating requests using FluentValidation.
    /// This behavior intercepts the request pipeline and validates the request before it's handled by the request handler.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request being handled.</typeparam>
    /// <typeparam name="TResponse">The type of the response from the handler.</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : ServiceResponse, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="validators">The validators for the request type.</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators = null)
        {
            _validators = validators;
        }

        /// <summary>
        /// Handles the request validation and invokes the next handler in the pipeline.
        /// </summary>
        /// <param name="request">The request to validate.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <param name="next">The next handler in the pipeline.</param>
        /// <returns>The response from the next handler or a validation error response.</returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators == null || !_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);
            var validationResults = new List<ValidationResult>();
            
            foreach (var validator in _validators)
            {
                validationResults.Add(await validator.ValidateAsync(context, cancellationToken));
            }

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                var response = new TResponse
                {
                    Status = "VALIDATION_ERROR",
                    Message = "Validation failed",
                    Errors = failures.Select(f => f.ErrorMessage).ToList(),
                    StatusCode = 400
                };

                return response;
            }

            return await next();
        }
    }
}
