using BankAccount.Model;
using BankAccount.Repository;
using BankAccount.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.UIComponent
{
    public class UI
    {
        private readonly AuthenticationService _au;
        private readonly BankService _bankService;
        public UI()
        {
            _au = new AuthenticationService();
            _bankService = new BankService();
            _bankService.FakeDataBase = _au._fakeDb;

        }
        public void HomePage()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("--             Welcome to Bank              --");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("--                                          --");
            Console.WriteLine("-- Sign Up --> Click 1                      --");
            Console.WriteLine("--                                          --");
            Console.WriteLine("-- Sign In --> Click 2                      --");
            Console.WriteLine("--                                          --");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Sign Up / Sign In?: ");
                string operation = Console.ReadLine();

                if (operation == "1")
                {
                    User _user = new User();
                    _user.UserCard = new Card();
                    SignUP(_user);
                }
                else if (operation == "2")
                {
                    SignIN();
                }
                else { Console.WriteLine("Error, Try again...."); }
            }
        }

        public void SecondPage(User _user)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("--             Operations                   --");
            Console.WriteLine("--                                          --");
            Console.WriteLine("--    1. Card To Card                       --");
            Console.WriteLine("--    2. Get Balance                        --");
            Console.WriteLine("--    3. Delete User                        --");
            Console.WriteLine("--    4. Update User                        --");
            Console.WriteLine("--    5. User Information                   --");
            Console.WriteLine("--    6. Change Password                    --");
            Console.WriteLine("--    7. Log Out                            --");
            Console.WriteLine("--                                          --");
            Console.WriteLine("----------------------------------------------");

            while (true)
            {
                Console.Write("Operation: ");
                string operation = Console.ReadLine();

                if (operation == "1") { IDCheckUI(_user); }
                else if (operation == "2") { GetBalanceUI(_user); }
                else if (operation == "3") { DeleteUserUI(_user); }
                else if (operation == "4") { UpdateUserUI(_user); }
                else if (operation == "5") {  }
                else if (operation == "6") { _au._fakeDb.ChangePassword(_user, Password()); }
                else if (operation == "7") { HomePage(); }
                else { Console.WriteLine("Error, Try again..."); }
                SecondPage(_user);
            }
        }

        public void DeleteUserUI(User user)
        {
            bool result = _au.DeleteUser(user.Id);

            if (result)
            {
                Console.WriteLine();
                Console.WriteLine("----------------------");
                Console.WriteLine("User Deleted.....");
                Console.WriteLine("----------------------");
                Console.WriteLine();
                HomePage();
            }
            else
            {
                Console.WriteLine("Error...");
            }
        }   

        public void GetBalanceUI(User user)
        {
            int result = _bankService.GetBalance(user.Id);
            if (result == -1)
            {
                Console.WriteLine("User not found");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("---------------------");
                Console.WriteLine($"Balance: {result}");
                Console.WriteLine("---------------------");
                Console.WriteLine();
            }
        }

        public void SignUP(User _user)
        {
            string email = Email();

            int i = 0;

            foreach (var user in _au._fakeDb.FakeDB)
            {
                if (email == user.Email)
                    i++;
            }

            if (i != 0)
            {
                Console.WriteLine("This email already used..."); SignUP(_user);
            }

            _user.Email = email;
            _user.Name = Name();
            _user.Surname = Surname();
            _user.Password = PasswordConfirm(Password());

            _au.SignUp(_user);

            Console.WriteLine();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Sign Up is successfull...");
            Console.WriteLine("-------------------------------");
            Console.WriteLine();

            HomePage();
        }

        public void SignIN()
        {
            string email = Email();
            string password = Password();

            User user = _au.SignIn(email, password);

            if (user != null)
            {
                Console.WriteLine();
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Sign In is successful...");
                Console.WriteLine("-------------------------------");
                Console.WriteLine();

                SecondPage(user);
            }
            else { Console.WriteLine("User not found..."); Console.WriteLine(); HomePage(); }
        }

        public void UpdateUserUI(User _user)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("--             Operations                   --");
            Console.WriteLine("--                                          --");
            Console.WriteLine("--   1. Change Email                        --");
            Console.WriteLine("--   2. Change Name                         --");
            Console.WriteLine("--   3. Change Surname                      --");
            Console.WriteLine("--                                          --");
            Console.WriteLine("----------------------------------------------");

            while (true)
            {
                Console.Write("Operation: ");
                string operation = Console.ReadLine();

                if (operation == "1")
                {
                    string email = Email();

                    if (email == _user.Email)
                    {
                        Console.WriteLine("You are already using this email");
                    }
                    else
                    {
                        _user.Email = email;
                        Console.WriteLine();
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("Email Changed Successfully");
                        Console.WriteLine("----------------------------");
                        Console.WriteLine();

                        _au._fakeDb.UpdateUser(_user);
                    }
                }
                else if (operation == "2")
                {
                    string name = Name();

                    if (name == _user.Name)
                    {
                        Console.WriteLine("You are already using this name");
                    }
                    else
                    {
                        _user.Name = name;
                        Console.WriteLine();
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("Name Changed Successfully");
                        Console.WriteLine("----------------------------");
                        Console.WriteLine();

                        _au._fakeDb.UpdateUser(_user);
                    }
                }
                else if (operation == "3")
                {
                    string surname = Surname();

                    if (surname == _user.Surname)
                    {
                        Console.WriteLine("You are already using this surname");
                    }
                    else
                    {
                        _user.Surname = surname;
                        Console.WriteLine();
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("Surname Changed Successfully");
                        Console.WriteLine("----------------------------");
                        Console.WriteLine();

                        _au._fakeDb.UpdateUser(_user);
                    }
                }
                else { Console.WriteLine("Uncorrect Operation..."); }

                Console.Write("Click 1 ---> Exit Update User Page: ");
                string exit = Console.ReadLine();
                if (exit == "1") { break; }
            }
        }

        public void IDCheckUI(User user)
        {
            while (true)
            {
                Console.Write("ID: ");
                string id = Console.ReadLine();

                bool IDchecking = _bankService.IDCheck(user, id);

                if (IDchecking)
                {
                    while (true)
                    {
                        Console.Write("Price of Money: ");
                        string money = Console.ReadLine();

                        if (!string.IsNullOrEmpty(money) && money.All(Char.IsDigit) && Convert.ToInt32(money) >= 0)
                        {
                            _bankService.CardToCard(Convert.ToInt32(money), id);
                            break;
                        } 
                        else
                            Console.WriteLine("Error...");
                    }
                }
                else
                {
                    Console.WriteLine("Error...");
                    Console.Write("Click 1 --> Exit: ");
                    string exit = Console.ReadLine();

                    if (exit == "1") { break; }
                }
            }
        }

        public string Name()
        {
            while (true)
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();

                string Name = _au.NameCheck(name);

                if (Name != null)
                    return Name;
                else
                    Console.WriteLine("Error...");
            }
        }

        public string Email()
        {
            while (true)
            {
                Console.Write("Email: ");
                string email = Console.ReadLine();

                string Email = _au.EmailCheck(email);

                if (Email != null)
                    return Email;
                else
                    Console.WriteLine("Error...");
            }
        }

        public string Surname()
        {
            while (true)
            {
                Console.Write("Surname: ");
                string surname = Console.ReadLine();

                string Surname = _au.SurnameCheck(surname);

                if (Surname != null)
                    return Surname;
                else
                    Console.WriteLine("Error...");
            }
        }

        public string Password()
        {
            while (true)
            {
                Console.Write("Password: ");
                string password = Console.ReadLine();

                string Password = _au.PasswordCheck(password);

                if (Password != null)
                    return Password;
                else
                    Console.WriteLine("Error...");
            }
        }

        public string PasswordConfirm(string password)
        {
            while (true)
            {
                Console.Write("Password Confirm: ");
                string psConfirm = Console.ReadLine();

                string PasswordConfirm = _au.PasswordConfirmCheck(psConfirm, password);

                if (PasswordConfirm != null)
                    return PasswordConfirm;
                else
                    Console.WriteLine("Error...");
            }
        }
    }
}