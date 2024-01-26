using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class UserAlreadyExistException : Exception
    {
        public UserAlreadyExistException(string email)
            : base($"User with the email {email} is already exist")
        {
            
        }
    }
}
