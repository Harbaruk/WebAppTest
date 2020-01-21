using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using WebTestApp.Services.Validations.Attributes;

namespace WebTestApp.Services.FileProcessors.Models
{
    public class FileUploadModel
    {
        [MaxFileSize(1 * 1024 * 1024)] // 1 Mb
        [SupportedFormats(".csv", ".xml")]
        public IFormFile file { get; set; }
    }
}
