using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CineMagic.Facade.Models.CinemaCreditCard
{
    public class AddFundsModel
    {
        [Required]
        public double Balance { get; set; }
        [Required]
        public long CardNumber { get; set; }
    }
}
