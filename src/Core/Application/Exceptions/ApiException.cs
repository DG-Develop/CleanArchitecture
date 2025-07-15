using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Exceptions
{
    /// <summary>
    /// custom class to handle API-specific exceptions.
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the class  <see cref="ApiException"/> without a message.
        /// </summary>
        public ApiException() : base() { }

        /// <summary>
        /// Initializes a new instance of the class <see cref="ApiException"/> with a message specific.
        /// </summary>
        /// <param name="mensaje">The message describe error.</param>
        public ApiException(string mensaje) : base(mensaje) { }


    }
}
