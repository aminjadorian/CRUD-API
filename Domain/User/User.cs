using Azure.Core;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User
{
    public sealed class User : Entity
    {
        private User(Guid id, Name name, string email)
            : base(id)
        {
            Name = name;
            Email = email;
        }
        private User(Guid id, string email) //declare this Constructor for EF Core because we dont want to include ValueObject in it
            : base(id)
        {
            Email = email;
        }
        public Name Name { get; private set; }
        public string Email { get; private set; }

        public static User Create(
            Name name,
            string email)
        {
            var user = new User(Guid.NewGuid(), name, email);
            user.Raise(new UserCreatedDomainEvent(Guid.NewGuid(),user.Id)); 
            return user;
        }

        public void Update(
            Name name,
            string email)
        {
            Email = email;
            Name = name;
        }
    }

}
