using System;
using System.Collections.Generic;
using System.Text;

namespace Clouder.Server.Api.Exceptions
{
   public  class ClouderValidationException : Exception
    {
        public List<string> ValidationErrors { get; set; }

        public ClouderValidationException(List<string> validationErrors)
        {
            this.ValidationErrors = validationErrors;
        }
    }
}
