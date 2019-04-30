using Microsoft.AspNetCore.Mvc.ModelBinding;
using SqsLibraries.Common.Utilities.ResponseObjects;
using System.Collections.Generic;
using System.Linq;

namespace SmartData.Api.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static List<ResponseMessage> ToResponseMessages(this ModelStateDictionary modelState)
        {
            return modelState.Keys.Where(k => modelState[k].Errors.Count > 0).ToList().Select(key =>
                                    ResponseMessage.ToError(key, modelState[key].Errors.Select(e => e.ErrorMessage).FirstOrDefault())).ToList();
        }
    }
}
