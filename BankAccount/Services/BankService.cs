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
        public FakeDataBase FakeDataBase { get; set; }

        public bool IDCheck(User _user, string id)
        {
            if (id.Length == 16 && id.All(Char.IsDigit))
            {
                foreach (var user in FakeDataBase.FakeDB)
                {
                    if ((user != _user) && (user.UserCard.CardId == id))
                        return true;
                }
                return false;
            }
            else
                return false;
        }

        public void CardToCard(int money, string id)
        {
            foreach (var user in FakeDataBase.FakeDB)
            {
                if (user.UserCard.CardId == id)
                    user.UserCard.Balance += money;
            }
        }

        public int GetBalance(Guid ID)
        {
            int count = 0;

            foreach (var user in FakeDataBase.FakeDB)
            {
                if (ID == user.Id)
                {
                    count++;
                    return user.UserCard.Balance;
                }
            }
            if (count == 0) { return -1; }
            return -1;
        }
    }
}