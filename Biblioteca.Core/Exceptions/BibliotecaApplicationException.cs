using System;
using System.Runtime.Serialization;

namespace Biblioteca.Core.Exceptions
{
    [Serializable]
    public class BibliotecaApplicationException : Exception
    {
        public BibliotecaApplicationException() : base()
        {           
        }

        public BibliotecaApplicationException(string message) : base(message)
        {            
        }

        public BibliotecaApplicationException(string message, Exception innerException) : base(message, innerException)
        {            
        }

        protected BibliotecaApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
