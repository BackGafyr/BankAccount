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
        private readonly User _user;
        private readonly BankService _bankService;
        public UI()
        {
            _au = new AuthenticationService();
            _user = new User();
            _user.UserCard = new Card();
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

        public void SecondPage()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("--             Operations                   --");
            Console.WriteLine("--                                          --");
            Console.WriteLine("--    1. Card To Card                       --");
            Console.WriteLine("--    2. Take money from Card               --");
            Console.WriteLine("--    3. Pay money to Card                  --");
            Console.WriteLine("--    4. Get Balance                        --");
            Console.WriteLine("--    5. Delete User                        --");
            Console.WriteLine("--    6. Update User                        --");
            Console.WriteLine("--    7. User Information                   --");
            Console.WriteLine("--    8. Exit                               --");
            Console.WriteLine("--                                          --");
            Console.WriteLine("----------------------------------------------");

            while (true)
            {
                Console.Write("Operation: ");
                string operation = Console.ReadLine();

                if (operation == "1") { _bankService.CardToCard(_au._fakeDb, _user); }
                else if (operation == "2") { _bankService.TakeFromCard(); }
                else if (operation == "3") { _bankService.PayToCard(); }
                else if (operation == "4") { _bankService.GetBalance(); }
                else if (operation == "5") { _au._fakeDb.DeleteUser(); }
                else if (operation == "6") { _au._fakeDb.UpdateUser(); }
                else if (operation == "7") { _au._fakeDb.UserInfo(); }
                else if (operation == "8") { HomePage(); }
                else { Console.WriteLine("Error, Try again..."); }
                SecondPage();
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

            bool signInChecking = _au.SignIn(email, password);

            if (signInChecking)
            {
                Console.WriteLine("Sign In is successful...");
                SecondPage();
            }
            else { Console.WriteLine("User not found..."); Console.WriteLine(); HomePage(); }
        }
    }
}
