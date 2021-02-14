using System.Collections.Generic;
using Newtonsoft.Json;

namespace HseAr.Integration.SceneExport.Responses
{
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }
}