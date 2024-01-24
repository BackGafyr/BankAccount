using BankAccount.Model;
using BankAccount.Services;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

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

        public void ChangePassword(User _user, AuthenticationService _au)
        {
            string newPassword = _au.PasswordInput();

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
        
        public void UserInfo(User _user)
        {
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("             USER INFORMATION             ");
            Console.WriteLine($"Name: {_user.Name}");
            Console.WriteLine($"Surname: {_user.Surname}");
            Console.WriteLine($"Email Adress: {_user.Email}");
            Console.WriteLine($"Password: {_user.Password}");
            Console.WriteLine($"Card ID: {_user.UserCard.CardId}");
            Console.WriteLine($"Balance: {_user.UserCard.Balance}");
            Console.WriteLine($"Card Cvv: {_user.UserCard.Cvv}");
            Console.WriteLine($"Card Creating Date: {_user.UserCard.CardCreatingTime}");
            Console.WriteLine($"Card Ending Date: {_user.UserCard.CardEndingTime}");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine();
        }
    }
}
