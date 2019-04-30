using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartData.Api.Extensions;
using SmartData.Common.Utilities;
using SqsLibraries.Common.Utilities.ResponseObjects;
using System.Collections.Generic;

namespace SmartData.Api.ActionResultHelpers
{
    public class ValidationActionResult : ObjectResult
    {
        public ValidationActionResult(ModelStateDictionary modelState) : base(modelState.ToResponseMessages())
        {
            StatusCode = Constants.VALIDATION_HTTP_STATUS_CODE;
        }

        public ValidationActionResult(List<ResponseMessage> validationMessages) : base(validationMessages)
        {
            StatusCode = Constants.VALIDATION_HTTP_STATUS_CODE;
        }
    }
}
