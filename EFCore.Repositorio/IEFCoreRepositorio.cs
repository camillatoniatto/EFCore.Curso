using EFCore.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Repositorio
{
    public interface IEFCoreRepositorio
    {
        //INTERFACE!!
        //metodos genericos aceitando qualquer variavel com tipo qualquer
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangeAsync();

        
        Task<Heroi[]> GetAllHerois(bool incluirBatalha = false); //retornar uma listagem de herois
        Task<Heroi> GetHeroiById(int id, bool incluirBatalha = false); //retorna apenas um heroi pelo id
        Task<Heroi[]> GetHeroisByNome(string nome, bool incluirBatalha = false); //retorna apenas um heroi pelo nome
        
        Task<Batalha[]> GetAllBatalhas(bool incluirHerois = false); //retornar uma listagem de batalhas
        Task<Batalha> GetBatalhaById(int id, bool incluirHerois = false); //retorna apenas uma batalha pelo id
    }
}
