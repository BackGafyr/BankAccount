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
                    SignUP(); break;
                }
                else if (operation == "2"){
                    SignIN(); break;
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
            Console.WriteLine("--    4. Delete User                        --");
            Console.WriteLine("--    5. Update User                        --");
            Console.WriteLine("--                                          --");
            Console.WriteLine("----------------------------------------------");

            while (true)
            {
                Console.WriteLine("Operation: ");
                string operation = Console.ReadLine();

                if (operation == "1") { _bankService.CardToCard(); }
                else if (operation == "2") { _bankService.TakeFromCard(); }
                else if (operation == "3") { _bankService.PayToCard(); }
                else if (operation == "4") { _au._fakeDb.DeleteUser(); }
                else if (operation == "5") { _au._fakeDb.UpdateUser(); }
                else { Console.WriteLine("Error, Try again..."); }
            }
        }

        public void SignUP()
        {
            _user.Email = _au.EmailInput();
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
            SecondPage();
        }
    }
}
