using Data.Contracts;
using Data.Exceptions;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastucture.Implementation
{
    public class Atm : IAtm
    {
        public string Manufacturer { get; private set; }
        public string SerialNumber { get; private set; }

        // private , let`s imagine its loaded from source
        public List<CreditCard> Creditcards { get;  set; }
        public List<Fee> Fees { get; set; } = new List<Fee>();

        private CreditCard _insertedCard;
        private Money _atmBalance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serialnumber"></param>
        /// <param name="manufacturer"></param>
        /// <param name="atmBalance"></param>
        public Atm(string serialnumber, string manufacturer, Money atmBalance )
        {
            SerialNumber = serialnumber;
            Manufacturer = manufacturer;
            // 
            _atmBalance = atmBalance;
        }
        /// <summary>
        /// Get card balance 
        /// </summary>
        /// <returns></returns>
        public decimal GetCardBalance()
        {
            if (_insertedCard == null)
                throw new InvalidCardReaderException("No card inserted.");

            return _insertedCard.Summ;
        }
        /// <summary>
        /// Set card to ATM
        /// </summary>
        /// <param name="cardNumber"></param>
        public void InsertCard(string cardNumber)
        {
            if (_insertedCard != null)
                throw new InvalidCardReaderException("Card already inserted.");

            _insertedCard = Creditcards
                .Where(x => x.CardNumber == cardNumber)
                .FirstOrDefault();

            if (_insertedCard == null)
                throw new InvalidCardException("Invalide Card inserted.");
        }
        /// <summary>
        /// Add money to account
        /// </summary>
        /// <param name="money"></param>
        public void LoadMoney(Money money)
        {
            if (_insertedCard == null)
                throw new InvalidCardReaderException("No card inserted.");

            _insertedCard.Summ += money.Amount;
            _atmBalance.Amount += money.Amount;

            foreach (var key in money.Notes.Keys.ToList()) 
            { 
                if (!_atmBalance.Notes.ContainsKey(key))
                    _atmBalance.Notes[key] = money.Notes[key];
                else
                    _atmBalance.Notes[key] += money.Notes[key];
            }
            Fees.Add(new Fee() { CardNumber = _insertedCard.CardNumber, WithdrawalDate = DateTime.Now, WithdrawalFeeAmount = 0 });
        }
        /// <summary>
        /// Show fee for user
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Fee> RetrieveChargedFees()
        {
            if (_insertedCard == null)
                throw new InvalidCardReaderException("No card inserted.");

            return Fees
                 .Where(x => x.CardNumber == _insertedCard.CardNumber)
                 .OrderBy(x => x.WithdrawalDate)
                 .ToList();
        }
        /// <summary>
        /// Return card
        /// </summary>
        public void ReturnCard()
        {
            if (_insertedCard == null)
                throw new InvalidCardReaderException("No card inserted.");

            _insertedCard = null;
        }
        /// <summary>
        /// WithdrawMoney
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public Money WithdrawMoney(int amount)
        {
            if (_insertedCard == null)
                throw new InvalidCardReaderException("No card inserted.");
            if ((amount % 5) > 0)
                throw new InvalidAmountException("Invalid Amount Exception requested.");

            // 1% fee
            decimal fee = ((decimal)amount / 100);
            var amountwithfee = fee + amount;

            if (_atmBalance.Amount < amount)
                throw new InfucientATMMoneyException("Not enough money in ATM Machine.");
            if (_insertedCard.Summ < amountwithfee)
                throw new InfucientCardMoneyException("Not enough money.");

            // Calculate available ATM notes
            var cashBack = new Money() { Amount = 0, Notes = new Dictionary<PaperNote, int>() }; ;
            foreach (var item in _atmBalance.Notes.Where(x => x.Value > 0).OrderByDescending(x => x.Key))
            {
                int noteAmount = (int)item.Key;
                if (amount >= noteAmount)
                {
                    int passibleAmount = item.Value * noteAmount > amount ? amount : item.Value * noteAmount;
                    int totalNotes = passibleAmount / noteAmount;
                    cashBack.Notes[item.Key] = totalNotes;
                    amount -= totalNotes * noteAmount;
                    cashBack.Amount += amount;
                }
            };

            if (amount > 0)
                throw new InfucientATMMoneyException("Not enough money (notes) in ATM Machine.");

            // Renew Card Balance
            _insertedCard.Summ -= amountwithfee;
            // Renew ATM Balance
            _atmBalance.Amount -= cashBack.Amount;
            // Rewnew ATM Notes
            foreach (var key in cashBack.Notes.Keys.ToList())
                _atmBalance.Notes[key] -= cashBack.Notes[key];

            Fees.Add(new Fee { CardNumber = _insertedCard.CardNumber, WithdrawalDate = DateTime.Now, WithdrawalFeeAmount = fee });
            return cashBack;
        }
    }
}
