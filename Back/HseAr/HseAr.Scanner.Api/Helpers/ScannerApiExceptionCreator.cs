using System;
using HseAr.Scanner.Api.Exceptions;

namespace HseAr.Scanner.Api.Helpers
{
    internal static class ScannerApiExceptionCreator
    {
        internal static ScannerApiException GetScannerApiException(Exception exception)
            => exception switch
            {
                ScannerApiException apiException => apiException,
                _ => new ScannerApiException(ScannerApiErrorCode.UnspecifiedError,
            "К сожалению, система в данный момент недоступна")
            };
    }
}