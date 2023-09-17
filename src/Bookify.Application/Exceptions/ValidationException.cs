using Bookify.Application.Abstractions.Behaviors;

namespace Bookify.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationError> errors)
    {
        Errors = errors;
    }

    public IEnumerable<ValidationError> Errors { get; set; }
}