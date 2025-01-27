using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveLetters.Service.DTO
{
    public class InvitesDTO
    {
        public int id { get; set; }
        public bool inviteAccepted { get; set; }
        public bool inviteRejected { get; set; }
        public DateTime? inviteUpdateDate { get; set; }
        public DateTime inviteDate { get; set; }
        public string guidInvited { get; set; }
        public string guidInviter { get; set; }
    }
}
