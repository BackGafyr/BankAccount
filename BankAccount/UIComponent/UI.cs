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
                else if (operation == "2"){
                    SignIN();
                } else { Console.WriteLine("Error, Try again...."); }
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

                if (operation == "1") { _bankService.CardToCard(_au._fakeDb, _user); }
                else if (operation == "2") { _bankService.GetBalance(_au._fakeDb, _user.Id); }
                else if (operation == "3") { _au._fakeDb.DeleteUser(_user); HomePage(); }
                else if (operation == "4") { UpdateUserUI(_user); }
                else if (operation == "5") { _au._fakeDb.UserInfo(_user); }
                else if (operation == "6") { _au._fakeDb.ChangePassword(_user, _au); }
                else if (operation == "7") { HomePage(); }
                else { Console.WriteLine("Error, Try again..."); }
                SecondPage(_user);
            }
        }

        public void SignUP(User _user)
        {
            string email = _au.EmailInput();

            int i = 0;

            foreach (var user in _au._fakeDb.FakeDB)
            {
                if (email == user.Email)
                    i++;
            }

            if (i  != 0)
            {
                Console.WriteLine("This email already used..."); SignUP(_user);
            }
            
            _user.Email = email;
            _user.Name = _au.NameInput();
            _user.Surname =_au.SurnameInput();
            _user.Password = _au.PasswordConfirm(_au.PasswordInput());

            _au.SignUp(_user);  

            Console.WriteLine("Sign Up is successfull...");
            Console.WriteLine();

            HomePage();
        }

        public void SignIN()
        {
            string email = _au.EmailInput();
            string password = _au.PasswordInput();

            User user = _au.SignIn(email, password);

            if (user != null)
            {
                Console.WriteLine("Sign In is successful...");
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
                    string email = _au.EmailInput();
                    
                    if (email == _user.Email)
                    {
                        Console.WriteLine("You are already using this email");
                    } else 
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
                    string name = _au.NameInput();

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
                    string surname = _au.SurnameInput();

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
    }
}