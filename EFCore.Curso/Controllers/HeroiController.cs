using EFCore.Dominio;
using EFCore.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Curso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroiController : ControllerBase
    {
        private readonly HeroiContexto _contexto;
        public HeroiController(HeroiContexto contexto)
        {
            _contexto = contexto;
        }

        // GET: api/<HeroiController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(new Heroi());
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET api/<HeroiController>/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        {
            return Ok("value");
        }

        // POST api/<HeroiController>
        [HttpPost]
        public ActionResult Post(Heroi model)
        {
            try
            {                
                _contexto.Herois.Add(model);
                _contexto.SaveChanges();

                return Ok("Adicionado!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // PUT api/<HeroiController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Heroi model)
        {
            try
            {
                if (_contexto.Herois.Find(id) != null)
                {
                    _contexto.Update(model); //_contexto.Herois.Update(heroi); é mais especifico
                    _contexto.SaveChanges();

                    return Ok("Editado!");
                }

                return Ok("Não Encontrado!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // DELETE api/<HeroiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
