using LoveLetters.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveLetters.Service.Services.Interfaces
{
    public interface IDomainService
    {
        Task<List<DomainsDTO>> GetDomain(string cod_domain);
        Task<DomainsDTO> GetSubDomain(string cod_domain, string cod_sub_domain);
    }
}
