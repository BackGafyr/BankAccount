using System;

namespace BankAccount.Model
{
    public class Card
    {
        public string CardId { get; set; }
        public int Cvv { get; set; }
        public int Balance { get; set; }
        public DateTime CardCreatingTime { get; set; }
        public DateTime CardEndingTime { get; set; }
    }
}
