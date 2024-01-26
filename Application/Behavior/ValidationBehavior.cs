using Application.Behavior.Messaging;
using Application.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Behavior;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandBase
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context)));

        var errors = validationFailures
            .Where(vr => !vr.IsValid)
            .SelectMany(vr => vr.Errors)
            .Select(vf => new ValidationErrors(vf.PropertyName, vf.ErrorMessage))
            .ToList();

        if (errors.Any())
        {
            throw new Exceptions.ValidationException(errors);
        }
        var response = await next();

        return response;
    }
}
