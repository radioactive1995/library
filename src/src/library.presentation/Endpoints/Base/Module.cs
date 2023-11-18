using Microsoft.AspNetCore.Http;
using ErrorOr;

namespace library.presentation.Endpoints.Base;
public abstract class Module
{
    protected IResult Problem(List<Error> errors)
    {
        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }
        return Problem(errors, errors[0]);
    }

    private IResult Problem(List<Error> errors, Error mainError)
    {
        var httpStatus = mainError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status422UnprocessableEntity,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status400BadRequest,
            ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
            _ => throw new ArgumentOutOfRangeException(nameof(mainError)),
        };

        var description = mainError.Description;
        var title = mainError.Code;
        var dictionary = new Dictionary<string, object?>();
        dictionary["errorCodes"] = errors.Select(e => e.Code);

        return Results.Problem(detail: description, title: title, statusCode: httpStatus, extensions: dictionary);
    }

    private IResult ValidationProblem(List<Error> errors)
    {
        var dictionary = new Dictionary<string, string[]>();


        foreach (var error in errors)
        {
            dictionary[error.Code] = new[] { error.Description };
        }

        return Results.ValidationProblem(errors: dictionary, statusCode: StatusCodes.Status422UnprocessableEntity);
    }
}
