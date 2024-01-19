using BankAccount.Model;
using BankAccount.Repository;
using System;
using System.Linq;

namespace BankAccount.Services
{
    public class AuthenticationService
    {
        public FakeDataBase _fakeDb;
        public AuthenticationService()
        {
            _fakeDb = new FakeDataBase();
        }

        public void SignUp(User user)
        {
            Random rndm = new Random();

            user.Id = Guid.NewGuid();
            user.UserCard.Balance = 0;
            user.UserCard.CardCreatingTime = DateTime.Now;
            user.UserCard.CardEndingTime = user.UserCard.CardCreatingTime.AddYears(3);
            user.UserCard.Cvv = rndm.Next(100, 1000);
            user.UserCard.CardId = rndm.Next(1000, 10000).ToString() + rndm.Next(1000, 10000).ToString()
                + rndm.Next(1000, 10000).ToString() + rndm.Next(1000, 10000).ToString();

            _fakeDb.FakeDB.Add(user);
        }

        public User SignIn(string email, string password)
        {
            foreach (var user in _fakeDb.FakeDB)
            {
                if (user.Email == email && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }

        public bool DeleteUser(string userId)
        {
            var user = _fakeDb.GetUserWithId(Guid.Parse(userId));

            if (user == null)
            {
                return false;
            }

            _fakeDb.DeleteUser(user);
            return true;
        }

        public string EmailInput()
        {
            while (true)
            {
                Console.Write("Email: ");
                string email = Console.ReadLine();

                int count = 0;

                try
                {
                    for (int i = 0; i < email.Length; i++)
                    {
                        if (email[i] == '@') { count = i; break; }
                    }

                    if (((email.Substring(count, 12) == "@outlook.com") || (email.Substring(count, 12) == "@hotmail.com")) && email.Length > 12)
                    {
                        if ((email.Substring(0, email.Length - 12).All(Char.IsLetter)))
                        {
                            return email;
                        }
                        else { Console.WriteLine("Error, Try again..."); }
                    }
                    else { Console.WriteLine("Error, Try again..."); }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Error, Try again...");
                }
            }
        }

        public string NameInput()
        {
            while (true)
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();

                if (!string.IsNullOrEmpty(name))
                {
                    if (name.All(Char.IsLetter))
                        return name;
                    else 
                    { Console.WriteLine("Error, Try again..."); }
                }
                else { Console.WriteLine("Error, Try again..."); }
            }
        }

        public string SurnameInput()
        {
            while (true)
            {
                Console.Write("Surname: ");
                string surname = Console.ReadLine();

                if (!string.IsNullOrEmpty(surname))
                {
                    if (surname.All(Char.IsLetter))
                        return surname;

                    else { Console.WriteLine("Error, Try again..."); }
                }
                else { Console.WriteLine("Error, Try again..."); }
            }
        }

        public string PasswordInput()
        {
            while (true)
            {
                Console.Write("Password: ");
                string password = Console.ReadLine();

                if (!string.IsNullOrEmpty(password))
                    return password;

                else { Console.WriteLine("Error, Try again..."); }
            }
        }

        public string PasswordConfirm(string password)
        {
            while (true)
            {
                Console.Write("Confirm password: ");
                string passwordConfirm = Console.ReadLine();
                Console.WriteLine();

                if (!string.IsNullOrEmpty(passwordConfirm))
                {
                    if (passwordConfirm == password)
                    {
                        return passwordConfirm;
                    }
                    else { Console.WriteLine("Error, Try again..."); }
                }
                else { Console.WriteLine("Error, Try again..."); }
            }
        }
    }
}
