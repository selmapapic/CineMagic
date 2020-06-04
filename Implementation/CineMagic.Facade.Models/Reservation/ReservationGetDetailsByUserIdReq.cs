using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CineMagic.Facade.Models.Reservation
{
    class ReservationGetDetailsByUserIdReq
    {
        [Required]
        public int UserId { get; set; }
    }
}
