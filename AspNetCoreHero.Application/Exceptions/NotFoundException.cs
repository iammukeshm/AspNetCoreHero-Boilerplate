using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entity, object entityId) : base($"{entity} with id {entityId} not found.")
        {
        }

        protected NotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
