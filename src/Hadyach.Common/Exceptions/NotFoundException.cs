using System;

namespace Hadyach.Common.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException()
        {

        }

        public NotFoundException(int id)
            : base(String.Format("Not found. Id: {0}", id))
        {

        }
    }
}
