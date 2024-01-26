using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(Guid Id)
            : base($"The User with the id {Id} is not exist")
        {

        }
    }
}
