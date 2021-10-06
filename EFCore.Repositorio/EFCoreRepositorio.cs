using EFCore.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Repositorio
{
    public class EFCoreRepositorio : IEFCoreRepositorio
    {
        private readonly HeroiContexto _contexto;

        public object Batalhas => throw new NotImplementedException();

        //CLASSE QUE IMPLEMENTA A INTERFACE!!
        public EFCoreRepositorio(HeroiContexto contexto)
        {
            _contexto = contexto;
        }

        public void Add<T>(T entity) where T : class
        {
            _contexto.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _contexto.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _contexto.Remove(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            //espera até que o savechanges tenha ocorrido e analisa se é > 0
            return (await _contexto.SaveChangesAsync()) > 0; 
        }

        public async Task<Heroi[]> GetAllHerois(bool incluirBatalha = false)
        {
            //aqui faz um join entre herois/identidadesecreta e herois/armas
            IQueryable<Heroi> query = _contexto.Herois //montando a query para fazer essa execução
                .Include(h => h.Identidade)
                .Include(h => h.Armas);

            if (incluirBatalha) //só inclui a batalha se é passado true, para n ocorrer loop infinito
            {
             //aqui inclui os heroisbatalhas-batalhas 
             query = query.Include(h => h.HeroisBatalhas).ThenInclude(heroib => heroib.Batalha);
            }
            
            query = query.AsNoTracking().OrderBy(h => h.Id); //consulta utilizando asnotracking e ordenando pelo id
            
            return await query.ToArrayAsync();
        }

        public async Task<Heroi> GetHeroiById(int id, bool incluirBatalha = false)
        {
            IQueryable<Heroi> query = _contexto.Herois 
               .Include(h => h.Identidade)
               .Include(h => h.Armas);

            if (incluirBatalha) 
            {                
                query = query.Include(h => h.HeroisBatalhas).ThenInclude(heroib => heroib.Batalha);
            }

            query = query.AsNoTracking().OrderBy(h => h.Id); 

            return await query.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Heroi[]> GetHeroisByNome(string nome, bool incluirBatalha = false)
        {
            IQueryable<Heroi> query = _contexto.Herois
               .Include(h => h.Identidade)
               .Include(h => h.Armas);

            if (incluirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas).ThenInclude(heroib => heroib.Batalha);
            }

            query = query.AsNoTracking()
                .Where(h => h.Nome.Contains(nome)) //retorna todos aqueles que contem x coisa no nome
                .OrderBy(h => h.Id);

            return await query.ToArrayAsync();                
        }

        public async Task<Batalha[]> GetAllBatalhas(bool incluirHerois = false)
        {
            IQueryable<Batalha> query = _contexto.Batalhas; //montando a query para fazer essa execução
               
            if (incluirHerois) //só inclui a heroi se é passado true, para n ocorrer loop infinito
            {
                //aqui inclui os heroisbatalhas-herois 
                query = query.Include(h => h.HeroisBatalhas).ThenInclude(heroib => heroib.Heroi);
            }

            query = query.AsNoTracking().OrderBy(h => h.Id); //consulta utilizando asnotracking e ordenando pelo id

            return await query.ToArrayAsync();
        }

        public async Task<Batalha> GetBatalhaById(int id, bool incluirHerois = false)
        {
            IQueryable<Batalha> query = _contexto.Batalhas; 

            if (incluirHerois)
            {                
                query = query.Include(h => h.HeroisBatalhas).ThenInclude(heroib => heroib.Heroi);
            }

            query = query.AsNoTracking().OrderBy(h => h.Id); 

            return await query.FirstOrDefaultAsync();
        }
    }
}
