using System.Linq;
using API.Domain.Common;
using FluentValidation.Results;

namespace API.Business.Implementation
{
    public static class ReponseCreationMethods
    {
        public static Response<TData> CreateValidResponse<TData>(TData data)
        {
            return Response<TData>.Valid(data);
        }

        public static Response<TData> CreateInvalidResponse<TData>(ValidationResult invalidValidationResult)
        {
            return Response<TData>.Invalid(invalidValidationResult.Errors.Select(failure => $"{failure.ErrorCode}: {failure.ErrorMessage}"));
        }
    }
}
