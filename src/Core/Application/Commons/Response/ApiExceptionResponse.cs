using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Commons.Response
{
    public class ApiExceptionResponse<T>
    {
        /// <summary>
        /// flag to know if the process was successful
        /// </summary>
        public bool IsSucceeded { get; set; }

        /// <summary>
        /// Object result of the request
        /// </summary>
        public T? Result { get; set; }

        /// <summary>
        /// Message information 
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
