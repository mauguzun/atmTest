using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtmApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtmController : ControllerBase
    {
        private readonly IAtm  _atm;

        public AtmController(IAtm atm)
        {
            _atm = atm;
        }

       
        [HttpPost]
        [Route("insert")]
        public IActionResult InsertCard([FromBody]string card) 
        {
            _atm.InsertCard(card);
            return Ok();
        } 
        
        [HttpGet]
        [Route("return")]
        public IActionResult ReturnCard()
        {
            _atm.ReturnCard();
            return Ok();
        }

        [HttpGet]
        [Route("balance")]
        public IActionResult CheckBalance() => Ok(JsonConvert.SerializeObject(string.Format("{0:F1}", _atm.GetCardBalance())));
  

        [HttpPost]
        [Route("withdraw")]
        public IActionResult WithdrawMoney([FromBody]int amount) => Ok(JsonConvert.SerializeObject(_atm.WithdrawMoney(amount)));

        [HttpGet]
        [Route("fee")]
        public IActionResult Fee() => Ok(JsonConvert.SerializeObject(_atm.RetrieveChargedFees()));




    }
}
