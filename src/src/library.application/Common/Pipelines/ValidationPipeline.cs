using ErrorOr;
using FluentValidation;
using MediatR;

namespace library.application.Common.Pipelines;

public class ValidationPipeline<TRequst, TResponse> : IPipelineBehavior<TRequst, TResponse>
    where TRequst : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequst>? _validator;
    public ValidationPipeline(IValidator<TRequst>? validator = null)
    {
        _validator = validator;
    }
    public async Task<TResponse> Handle(TRequst request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
            return await next();

        var validationResult = await _validator.ValidateAsync(request);

        if (validationResult.IsValid is true)
            return await next();

        var errors = validationResult.Errors.ConvertAll(
            error => Error.Validation(error.PropertyName, error.ErrorMessage));

        return (dynamic)errors;
    }
}
