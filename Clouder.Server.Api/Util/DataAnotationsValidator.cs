using System;
using System.Collections.Generic;
using System.Text;
using Clouder.Server.Api.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Clouder.Server.Api.Util
{
    public class DataAnotationsValidator
    {
        public static void TryValidate(object @object)
        {
            var context = new ValidationContext(@object, serviceProvider: null, items: null);
            List<ValidationResult> results = new List<ValidationResult>();
            var validation =  Validator.TryValidateObject(@object, context, results, validateAllProperties: true);
            if (!validation)
            {
                List<string> errorMessages = new List<string>();

                foreach(ValidationResult validationResult in results)
                {
                    
                    string errorMessage = validationResult.ErrorMessage;
                    errorMessages.Add(errorMessage);
                }
                throw new ClouderValidationException(errorMessages);
            }
        }
    }
}
