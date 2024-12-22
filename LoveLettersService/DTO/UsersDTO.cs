using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveLetters.Service.DTO
{
    public class UsersDTO
    {
        public string guid { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string? profilePhoto { get; set; }
        public string? partnerGuid { get; set; }
        public string? partnerName { get; set; }
        public bool havePartner { get; set; }
        public string jwtToken { get; set; }    
    }
}
