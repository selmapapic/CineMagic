using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CineMagic.Facade.Models.Reservation
{
    public class ReservationGetDetailsByUserIdReq
    {
        [Required]
        public string UserId { get; set; }
    }
}
