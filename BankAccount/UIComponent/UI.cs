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
                else if (operation == "5") { UserInfo(_user); }
                else if (operation == "6") { ChangePassword(_user); }
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
            else
            {
                Console.WriteLine();
                Console.WriteLine("-----------------------");
                Console.WriteLine("User not found...");
                Console.WriteLine("-----------------------");
                Console.WriteLine(); 
                HomePage(); 
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

        public void UserInfo(User user)
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("--           Information");
            Console.WriteLine("--");
            Console.WriteLine($"--  Name: {user.Name}");
            Console.WriteLine($"--  Surname: {user.Surname}");
            Console.WriteLine($"--  Email: {user.Email}");
            Console.WriteLine($"--  Password: {user.Password}");
            Console.WriteLine($"--  Card ID: {user.UserCard.CardId}");
            Console.WriteLine($"--  Card Cvv: {user.UserCard.Cvv}");
            Console.WriteLine($"--  Card Creating Date: {user.UserCard.CardCreatingTime}");
            Console.WriteLine($"--  Card Ending Date: {user.UserCard.CardEndingTime}");
            Console.WriteLine("--");
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine();
        }

        public void UpdateUserUI(User user)
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

                if (operation == "1") { UpdateEmail(user); }
                else if (operation == "2") { UpdateName(user); }
                else if (operation == "3") { UpdateSurname(user); }
                else { Console.WriteLine("Error..."); }
            }
        }

        public void UpdateEmail(User user)
        {
            string email = Email();

            bool checkEmail = _au.UpdateEmailChecking(email, user);

            if (checkEmail)
            {
                Console.WriteLine();
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Email changed...");
                Console.WriteLine("-------------------------------");
                Console.WriteLine();
                SecondPage(user);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("You are already use this email");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine();
                SecondPage(user); 
            }
        }

        public void UpdateName(User user)
        {
            string name = Name();

            bool checkName = _au.UpdateNameChecking(name, user);

            if (checkName)
            {
                Console.WriteLine();
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Name changed...");
                Console.WriteLine("-------------------------------");
                Console.WriteLine();
                SecondPage(user);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("You are already use this Name");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine();
                SecondPage(user);
            }
        }

        public void UpdateSurname(User user)
        {
            string surname = Surname(); 

            bool checkSurname = _au.UpdateSurnameChecking(surname, user);

            if (checkSurname)
            {
                Console.WriteLine();
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Surname changed...");
                Console.WriteLine("-------------------------------");
                Console.WriteLine();
                SecondPage(user);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("You are already use this Surname");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine();
                SecondPage(user);
            }
        }

        public void ChangePassword(User user)
        {
            while (true)
            {
                string password = Password();

                bool passCheck = _au.ChangePasswordChecking(password, user);

                if (passCheck)
                {
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("Password changed...");
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine();
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("You are already use this Password");
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine();
                    break;
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