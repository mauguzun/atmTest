using Data.Contracts;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastucture.Implementation
{
    public class Atm : IAtm
    {
        public string Manufacturer { get; private set; }
        public string SerialNumber { get; private set; }


        public Atm(string serialnumber, string manufacturer)
        {
            this.SerialNumber = serialnumber;
            this.Manufacturer = manufacturer;
        }
     

        public decimal GetCardBalance()
        {
            throw new NotImplementedException();
        }

        public void InsertCard(string cardNumber)
        {
            throw new NotImplementedException();
        }

        public void LoadMoney(Money money)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Fee> RetrieveChargedFees()
        {
            throw new NotImplementedException();
        }

        public void ReturnCard()
        {
            throw new NotImplementedException();
        }

        public Money WithdrawMoney(int amount)
        {
            throw new NotImplementedException();
        }
    }
}
