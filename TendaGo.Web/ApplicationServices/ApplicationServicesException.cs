using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TendaGo.Web.Models;

namespace TendaGo.Web.ApplicationServices
{
    public class ApplicationServicesException : Exception
    {
        private readonly ApiErrorResponse _apiErrorResponse;


        public ApiErrorResponse ApiErrorResponse => _apiErrorResponse;

        public ApplicationServicesException(string errorMessage) : base(errorMessage)
        {
            _apiErrorResponse = new ApiErrorResponse
            {
                TechnicalMessage = errorMessage,
                UserMessage = errorMessage
            };
        }

        public ApplicationServicesException(ApiErrorResponse apiErrorResponse) : base(apiErrorResponse.TechnicalMessage)
        {
            _apiErrorResponse = apiErrorResponse;
        }

        public ApplicationServicesException(Exception ex) : base(ex.Message, ex)
        {
            _apiErrorResponse = new ApiErrorResponse
            {
                TechnicalMessage = ex.ToString(),
                UserMessage = $"{ex.Message} {ex.InnerException?.Message}"
            };
        }
    }
}