using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebTestApp.Services.Validations.Attributes
{
    public class SupportedFormatsAttribute : ValidationAttribute
    {
        private readonly string[] _supportedFormats;

        public SupportedFormatsAttribute(params string[] supportedFormats)
        {
            _supportedFormats = supportedFormats;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                if(!_supportedFormats.Contains(extension))
                {
                    return new ValidationResult("Unknown format");
                }
            }

            return ValidationResult.Success;
        }
    }
}
