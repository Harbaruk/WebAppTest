using System;
using System.Collections.Generic;
using System.Text;

namespace WebTestApp.Services.FileReaders.Models
{
    public class FileProcessingResult<T>
    {
        public bool IsValid { get; set; }
        public IList<T> ValidItems { get; set; }
        public IList<object> InvalidItems { get; set; }
    }
}
