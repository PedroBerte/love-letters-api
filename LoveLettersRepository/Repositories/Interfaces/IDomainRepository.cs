using LoveLetters.Repository.Context;

namespace LoveLetters.Repository.Repositories.Interfaces
{
    public interface IDomainRepository
    {
        Task<Domains> GetSubDomain(string cod_domain, string cod_sub_domain);
        Task<List<Domains>> GetDomain(string cod_domain);
    }
}
