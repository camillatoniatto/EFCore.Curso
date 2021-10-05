using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Dominio;
using EFCore.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public HeroiContexto _contexto;
        public ValuesController(HeroiContexto contexto)
        {
            _contexto = contexto;
        }

        // GET api/values
        [HttpGet("filtro/{nome}")]
        public ActionResult GetFiltro(string nome) // R => SELECT
        {
            //quando for usar o foreach direto a conexão vai continuar aberta, e pode travar o db
            //é necessário usar o list primeiro!!
            //exemplo criar o vat listHeroi e depois utilizar foreach (var item in listHeroi){...}
            var listHeroi = _contexto.Herois
                           .Where(h => h.Nome.Contains(nome)) //usando o filtro 
                           .ToList(); //link utilizando manager
            //var listHeroi = (from heroi in _contexto.Herois
            //                 where heroi.Nome.Contains(nome) //usando o filtro
            //                 select heroi).ToList(); //link utilizando qwery
            return Ok(listHeroi);
        }


        [HttpGet("atualizar/{nameHero}")]
        public ActionResult Get(string nameHero) //C => INSERT
        {
            //var heroi = new Heroi { Nome = nameHero };
            var heroi = _contexto.Herois
                           .Where(h => h.Id == 3)
                           .FirstOrDefault();
            heroi.Nome = "Thor";
            //_contexto.Herois.Add(heroi); //tipo 1: aqui especifica qual vai adicionar

            _contexto.SaveChanges();            
           
            return Ok();
        }


        [HttpGet("AddRange")]
        public ActionResult GetAddRange()
        {
            _contexto.AddRange(
                new Heroi { Nome = "Capitão América" },
                new Heroi { Nome = "Doutor Estranho" },
                new Heroi { Nome = "Pantera Negra" },
                new Heroi { Nome = "Homem Aranha" },
                new Heroi { Nome = "Hulk" },
                new Heroi { Nome = "Gavião Arqueiro" },
                new Heroi { Nome = "Capitã Marvel" }
            );
            _contexto.SaveChanges();

            return Ok(); 
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpGet("Delete/{id}")]
        public void Delete(int id)
        {
            var heroi = _contexto.Herois
                        .Where(x => x.Id == id)
                        .Single();
            _contexto.Herois.Remove(heroi);
            _contexto.SaveChanges();
        }
    }
}