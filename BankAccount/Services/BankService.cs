using BankAccount.Model;
using BankAccount.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Services
{
    public class BankService
    {
        public void CardToCard(FakeDataBase _db, User _user)  // Oz kartina pul attiqda error vermelidir... Bunu id-e gore User tapdiqdan sonra etmek olar.
        {
            while (true)
            {
                int count = 0;

                Console.Write("Card ID: ");
                string id = Console.ReadLine();
                if ((id.Length == 16) && id.All(Char.IsDigit))
                {
                    foreach (var user in _db.FakeDB)
                    {
                        if (user.UserCard.CardId == _user.UserCard.CardId)
                        {
                            Console.WriteLine("Error...");
                        }
                        else if (user.UserCard.CardId == id)
                        {
                            count++;
                            Console.Write("Price of Money: ");
                            int money = Convert.ToInt32(Console.ReadLine());
                            user.UserCard.Balance += money; break;
                        }
                    }
                }

                if (count == 0) 
                { 
                    Console.WriteLine("User not found...");
                }

                Console.Write("Click 1 --> Exit: ");
                string exit = Console.ReadLine();
                if (exit == "1") { break; }
            }
        }

        public void TakeFromCard()
        {

        }
        
        public void PayToCard()
        {

        }

        public void GetBalance()
        {

        }
    }
}
