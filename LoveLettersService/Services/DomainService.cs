using AutoMapper;
using LoveLetters.Repository.Repositories.Interfaces;
using LoveLetters.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveLetters.Service.Services
{
    public class DomainService
    {
        private readonly IMapper mapper;
        private readonly IDomainRepository repository;
        public DomainService(IMapper mapper, IDomainRepository repository) 
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<List<DomainsDTO>> GetDomain(string cod_domain)
        {
            try
            {
                var entities = await repository.GetDomain(cod_domain);
                return mapper.Map<List<DomainsDTO>>(entities);
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task<DomainsDTO> GetSubDomain(string cod_domain, string cod_sub_domain)
        {
            try
            {
                var entities = await repository.GetSubDomain(cod_domain, cod_sub_domain);
                return mapper.Map<DomainsDTO>(entities);
            }
            catch (ApplicationException)
            {
                throw;
            }
        }
    }
}
