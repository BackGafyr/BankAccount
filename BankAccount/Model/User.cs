using System;

namespace BankAccount.Model
{
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid Id { get; set; }
        public Card UserCard { get; set; }
    }
}
