using Dapper;
using LoveLetters.Repository.Context;
using LoveLetters.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveLetters.Repository.Repositories
{
    public class DomainRepository : IDomainRepository
    {
        private readonly LoveLettersContext loveLettersContext;
        public DomainRepository(LoveLettersContext loveLettersContext) 
        {
            this.loveLettersContext = loveLettersContext;
        }

        public async Task<Domains> GetSubDomain(string cod_domain, string cod_sub_domain)
        {
            //dapper for perfomance
            using(var connection = new MySqlConnection(loveLettersContext.Database.GetConnectionString()))
            {
                var response = await connection.QueryAsync<Domains>(
                    "SELECT * FROM Domains WHERE cod_domain = @cod_domain AND cod_sub_domain = @cod_sub_domain",
                    new { cod_domain, cod_sub_domain });

                return response.FirstOrDefault();
            }
        }

        public async Task<List<Domains>> GetDomain(string cod_domain)
        {
            //dapper for perfomance
            using (var connection = new MySqlConnection(loveLettersContext.Database.GetConnectionString()))
            {
                var response = await connection.QueryAsync<Domains>(
                    "SELECT * FROM Domains WHERE cod_domain = @cod_domain",
                    new { cod_domain });

                return response.ToList();
            }
        }
    }
}
