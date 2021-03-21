using System;

namespace HseAr.Scanner.Api.Exceptions
{
    public class ScannerApiException : Exception
    {
        public ScannerApiException(ScannerApiErrorCode code, string message)
            : base(message ?? string.Empty)
        {
            Code = code;
        }

        public ScannerApiErrorCode Code { get; set; }
        
    }
}