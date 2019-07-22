using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FH.Core
{
    [Serializable]
    public class FhException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="FhException"/> object.
        /// </summary>
        public FhException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="FhException"/> object.
        /// </summary>
        public FhException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public FhException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public FhException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
