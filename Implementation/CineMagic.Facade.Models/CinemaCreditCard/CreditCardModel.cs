using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CineMagic.Facade.Models.CinemaCreditCard
{
    public class CreditCardModel
    {
        [Required]
        public double Balance { get; set; }
        [Required]
        public string UserMail { get; set; }
    }
}
