using System;

namespace managing_humanitarian_collections_api.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
