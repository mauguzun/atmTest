using Data.Exceptions;
using Data.Models;
using Infrastucture.Implementation;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Infrastucture.Implementation
{
    class AtmUnitTests
    {
        Atm atm;
        [SetUp]
        public void SetUp()
        {
            atm = new Atm
                (
                    "12",
                    "iddqd",
                     new Money()
                     {
                         Amount = 5 * 3 + 10 + 50,
                         Notes = new Dictionary<PaperNote, int>
                         {
                             { PaperNote.NoteFive,3},
                             { PaperNote.NoteTen, 1 },
                             { PaperNote.NoteFifty, 1 }
                         }
                     }
                )
            {
                Creditcards = new List<CreditCard>()
                    {
                       new CreditCard() { CardNumber = "112", Summ = 20},
                       new CreditCard() { CardNumber = "113", Summ = 2 }
                  }
            };
        }

        [Test]
        public void TestInsertIncorectCard()
        {
            Assert.Throws<InvalidCardException>(() => atm.InsertCard("iddqd"), "Unregistered card accepted.");
        }
        [Test]
        public void TestInsertCardToNotEmptyCardReader()
        {
            Assert.DoesNotThrow(() => atm.InsertCard("112"), "Registered card not accepted.");
            Assert.Throws<InvalidCardReaderException>(() => atm.InsertCard("112"), "Card already inserted.");
            Assert.DoesNotThrow(() => atm.ReturnCard(), "No card inserted.");
        }

        [Test]
        public void TestInsertCorrectCard()
        {
            Assert.DoesNotThrow(() => atm.InsertCard("112"), "Registered card not accepted.");
            Assert.DoesNotThrow(() => atm.ReturnCard(), "No card inserted.");
        }

        [Test]
        public void TestLoadMoneyCard()
        {
            Assert.DoesNotThrow(() => atm.InsertCard("112"), "Registered card not accepted.");
            var oldbalance = atm.GetCardBalance();
            var money = new Money()
            {
                Amount = 5 + 5 * 5,
                Notes = new Dictionary<PaperNote, int>()
                {
                    { PaperNote.NoteFive, 1 },
                    { PaperNote.NoteTen, 5 },
                    { PaperNote.NoteTwenty, 0 },
                    { PaperNote.NoteFifty, 0 }
                }
            };

            Assert.DoesNotThrow(() => atm.LoadMoney(money), "Problem with loading money to card");
            var newbalance = atm.GetCardBalance();
            Assert.AreNotEqual(oldbalance, newbalance, "Card loading failed.");
            Assert.DoesNotThrow(() => atm.ReturnCard(), "No card inserted.");
        }


        [Test]
        public void TestWithdrawal()
        {
            Assert.DoesNotThrow(() => atm.InsertCard("112"), "Registered card not accepted.");
            var oldbalance = atm.GetCardBalance();

            var money = atm.WithdrawMoney(10);
            var newbalance = atm.GetCardBalance();
            decimal fee = ((decimal)10 / 100);
            Assert.AreEqual(oldbalance - 10 - fee, newbalance, "Card withdraw failed.");
            Assert.DoesNotThrow(() => atm.ReturnCard(), "No card inserted.");

        }

        [Test]
        public void TestWithdrawalIncorectAmount()
        {
            Assert.DoesNotThrow(() => atm.InsertCard("112"), "Registered card not accepted.");

            // Try get incorect amount
            Assert.Throws<InvalidAmountException>(() => atm.WithdrawMoney(99), "Allow withdraw invalide amount");
            Assert.DoesNotThrow(() => atm.ReturnCard(), "No card inserted.");
        }


        [Test]
        public void TestWithdrawalCardNotAvailableAmount()
        {
            Assert.DoesNotThrow(() => atm.InsertCard("112"), "Registered card not accepted.");
            Assert.Throws<InfucientATMMoneyException>(() => atm.WithdrawMoney(100000), "Not enough money in ATM Machine");
            Assert.DoesNotThrow(() => atm.ReturnCard(), "No card inserted.");
        }

        [Test]
        public void TestClientFees()
        {
            Assert.DoesNotThrow(() => atm.InsertCard("112"), "Registered card not accepted.");
            var oldbalance = atm.GetCardBalance();

            atm.WithdrawMoney(5);
            decimal fee = ((decimal)(5) / 100);
            var newbalance = atm.GetCardBalance();

            List<Fee> storedfees = atm.RetrieveChargedFees().ToList();

            Assert.AreEqual(oldbalance - 5 - fee, newbalance, "Card withdraws failed.");
            Assert.AreEqual(storedfees[0].WithdrawalFeeAmount , fee , "Card fees not valid .");
        
        }


      
    }
}
