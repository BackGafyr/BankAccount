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

        public User GetUserWithId(Guid ID)
        {
            foreach (var user in FakeDB)
            {
                if (ID == user.Id)
                    return user;
            }
            return null;
        }

        public User UpdateUser(User _user, string email, string name, string surname)
        {
            foreach (var user in FakeDB)
            {
                if (_user == user)
                {
                    user.Email = email;
                    user.Name = name;
                    user.Surname = surname;
                    return user;
                }
            }
            return null;
        }
    }
}