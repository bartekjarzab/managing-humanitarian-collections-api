using System;

namespace managing_humanitarian_collections_api.Exceptions
{
    public class ForbidException : Exception
    {
        public ForbidException(string message) : base(message)
        {

        }

    }
}
