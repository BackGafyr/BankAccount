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

        public bool DeleteUser(Guid userId)
        {
            var user = _fakeDb.GetUserWithId(userId);

            if (user == null)
            {
                return false;
            }

            _fakeDb.DeleteUser(user);
            return true;
        }

        public User GetUserById(Guid userId)
        {
            var user = _fakeDb.GetUserWithId(userId);
            return user;
        }

        public bool ChangePasswordChecking(string password, User user)
        {
            if (password == user.Password)
                return false;
            else
            {
                user.Password = password;
                return true;
            } 
        }

        public void UpdateUser(User _user, string email, string name, string surname)
        {
            _fakeDb.UpdateUser(_user, email, name, surname);
        }

        public bool UpdateEmailChecking(string email, User user)
        {
            if (email == user.Email)
                return false;
            else
            {
                UpdateUser(user, email, user.Name, user.Surname);
                return true;
            }
        }

        public bool UpdateNameChecking(string name, User user)
        {
            if (name == user.Name)
                return false;
            else
            {
                UpdateUser(user, user.Email, name, user.Surname);
                return true;
            }
        }

        public bool UpdateSurnameChecking(string surname, User user)
        {
            if (surname == user.Surname)
                return false;
            else
            {
                UpdateUser(user, user.Email, user.Name, surname);
                return true;
            }
        }

        public string EmailCheck(string email)
        {
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
                    else { return null; }
                }
                else { return null; }
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }

        public string NameCheck(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                if (name.All(Char.IsLetter))
                {
                    return name;
                }
                else
                {
                    return null;
                }
            }
            else { return null; }
        }

        public string SurnameCheck(string surname)
        {
            if (!string.IsNullOrEmpty(surname))
            {
                if (surname.All(Char.IsLetter))
                    return surname;

                else { return null; }
            }
            else { return null; }
        }

        public string PasswordCheck(string password)
        {
            if (!string.IsNullOrEmpty(password))
                return password;

            else { return null; }
        }

        public string PasswordConfirmCheck(string passwordConfirm, string password)
        {
            if (!string.IsNullOrEmpty(passwordConfirm))
            {
                if (passwordConfirm == password)
                {
                    return passwordConfirm;
                }
                else { return null; }
            }
            else { return null; }
        }
    }
}