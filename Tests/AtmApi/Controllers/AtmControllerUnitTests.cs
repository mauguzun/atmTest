using AtmApi.Controllers;
using Data.Contracts;
using Data.Exceptions;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Tests.AtmApi.Controllers
{
    class AtmControllerUnitTests
    {

        [Test]
        [TestCase("112")]
        public void ValidData_InsertCard_View(string serial)
        {
            //Arange
            var atm = new Mock<IAtm>();
            var target = new AtmController(atm.Object);
            atm.Setup(x => x.InsertCard(serial));
            // Act
            var result = target.InsertCard(serial);
            // Assert
            atm.Verify(x => x.InsertCard(serial), Times.Once);
        }

        [Test]
    
        public void ValidData_Return_View()
        {
            //Arange
            var atm = new Mock<IAtm>();
            var target = new AtmController(atm.Object);
            atm.Setup(x => x.ReturnCard()).Throws(new InvalidCardReaderException());

            // Act
            Assert.Throws<InvalidCardReaderException>(() => target.ReturnCard());
            // Assert
            atm.Verify(x => x.ReturnCard(), Times.Once);
        }



        [Test]
        [TestCase(123.1)]
        [TestCase(-1.1)]
        [TestCase(-0)]
        public void ValidData_CheckBalance_View(decimal balance)
        {
            //Arange
            var atm = new Mock<IAtm>();
            var target = new AtmController(atm.Object);
            atm.Setup(x => x.GetCardBalance()).Returns(balance);
            // Act
            var result = target.CheckBalance();
            var json = ((OkObjectResult)result).Value;
            // Assert
            atm.Verify(x => x.GetCardBalance(), Times.Once);
            Assert.AreEqual(string.Format("{0:F1}", balance), json);
        }

        [Test]
        [TestCaseSource("_money")]
        public void ValidData_WithdrawMoney_View(Money money)
        {
            //Arange
            int ammount = 25;
            var atm = new Mock<IAtm>();
            var target = new AtmController(atm.Object);
            atm.Setup(x => x.WithdrawMoney(ammount)).Returns(money);
            // Act
            var result = target.WithdrawMoney(ammount);
            var json = ((OkObjectResult)result).Value;
            // Assert
            atm.Verify(x => x.WithdrawMoney(ammount), Times.Once);
            Assert.AreEqual(Newtonsoft.Json.JsonConvert.SerializeObject(money), json);
        }

        [Test]
        [TestCaseSource("_fees")]
        public void ValidData_Fee_View(List<Fee> fees)
        {
            //Arange
            var atm = new Mock<IAtm>();
            var target = new AtmController(atm.Object);
            atm.Setup(x => x.RetrieveChargedFees()).Returns(fees);
            // Act
            var result = target.Fee();
            var json = ((OkObjectResult)result).Value;
            // Assert
            atm.Verify(x => x.RetrieveChargedFees(), Times.Once);
            Assert.AreEqual(Newtonsoft.Json.JsonConvert.SerializeObject(fees), json);
        }

        private static readonly object[] _money =
        {
              new object[] 
              {
                  new Money()
                  {
                             Amount = 25,
                             Notes = new Dictionary<PaperNote, int>
                             {
                                 { PaperNote.NoteFive,3},
                                 { PaperNote.NoteTen, 1 },

                             }
                  }
             },
        }; 

        private static readonly object[] _fees =
        {
             new object[]
             {
                 new List<Fee>
                 {
                     new Fee { CardNumber = "112", WithdrawalDate = DateTime.Now , WithdrawalFeeAmount = 2.1M },
                     new Fee { CardNumber = "112", WithdrawalDate = DateTime.Now , WithdrawalFeeAmount = 2.2M }
                 }
             }
        };

    }
}
