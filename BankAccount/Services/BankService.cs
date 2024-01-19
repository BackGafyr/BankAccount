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
        public void CardToCard(FakeDataBase _db, User _user)
        {
            while (true)
            {
                int count = 0;
                Console.Write("Card ID: ");
                string id = Console.ReadLine();

                if (id.Length == 16 && id.All(Char.IsDigit))
                {
                    if (id == _user.UserCard.CardId)
                    {
                        Console.WriteLine("Error...");
                    }
                    else
                    {
                        foreach (var user in _db.FakeDB)
                        {
                            if (user.UserCard.CardId == id)
                            {
                                count++;
                                Console.Write("Amount Of Money: ");
                                int money = Convert.ToInt32(Console.ReadLine());
                                user.UserCard.Balance += money;
                            }
                        }
                        if (count == 0) { Console.WriteLine("User not found..."); }
                    }
                }else { Console.WriteLine("The syntax of ID is uncorrect..."); }

                Console.Write("Click 1 --> Exit: ");
                string exit = Console.ReadLine();
                if (exit == "1") { break; }
            }
        }

        public void GetBalance(FakeDataBase _db, Guid ID)
        {
            int count = 0;

            foreach (var user in _db.FakeDB)
            {
                if (ID == user.Id)
                {
                    count++;
                    Console.WriteLine();
                    Console.WriteLine("---------------------");
                    Console.WriteLine($"Balance: {user.UserCard.Balance}$");
                    Console.WriteLine("---------------------");
                    Console.WriteLine();
                }
            }
            if (count == 0) { Console.WriteLine("User not found"); }
        }
    }
}