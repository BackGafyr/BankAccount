using BankAccount.Model;
using BankAccount.Services;
using System;
using System.Collections.Generic;

namespace BankAccount.Repository
{
    public class FakeDataBase
    {
        public readonly List<User> FakeDB;
        public FakeDataBase()
        {
            FakeDB = new List<User>();
        }

        public string DeleteUser(User user)
        {
            string resposeId = user.Id.ToString();
            FakeDB.Remove(user);
            return resposeId;
        }

        public User UpdateUser(User _user)
        {
            return _user;
        }

        public void ChangePassword(User _user, string password)
        {
            string newPassword = password;

            if (newPassword == _user.Password) 
            {
                Console.WriteLine("You are already using this password");
            } else
            {
                _user.Password = newPassword;

                Console.WriteLine();
                Console.WriteLine("----------------------------");
                Console.WriteLine("Password Changed Successfully");
                Console.WriteLine("----------------------------");
                Console.WriteLine();
            }
        }

        public User GetUserWithId(Guid ID)
        {
            foreach (var user in FakeDB)
            {
                if (ID == user.Id)
                    return user;
            }
            return null;
        }
    }
}