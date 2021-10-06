using EFCore.Dominio;
using EFCore.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.Curso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhaController : ControllerBase
    {
        private readonly IEFCoreRepositorio _repositorio;

        public BatalhaController(IEFCoreRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        // GET: api/<BatalhaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var herois = await _repositorio.GetAllBatalhas(true);

                return Ok(herois);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET api/<BatalhaController>/5
        [HttpGet("{id}", Name = "GetBatalha")] //está dando problema de looping!!
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var herois = await _repositorio.GetBatalhaById(id, true);

                return Ok(herois);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<BatalhaController>
        [HttpPost] //para adicionar não coloca id
        public async Task<IActionResult> Post(Batalha model) //está trabalhando com async
        {
            try
            {
                _repositorio.Add(model); 
                if (await _repositorio.SaveChangeAsync()) //sempre utilizar um await, se utiliza o async
                {
                    return Ok("Adicionado!");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            
            return BadRequest("Não foi adicionado");
        }

        // PUT api/<BatalhaController>/5
        [HttpPut("{id}")] //para atualizar precisa do id
        public async Task<IActionResult> Put(int id, Batalha model)
        {
            try
            {
                var heroi = await _repositorio.GetBatalhaById(id);
                if (heroi != null)
                {
                    _repositorio.Update(model);
                    if (await _repositorio.SaveChangeAsync()) 
                    {
                        return Ok("Editado!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não foi editado");
        }

        // DELETE api/<BatalhaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var heroi = await _repositorio.GetBatalhaById(id);
                if (heroi != null)
                {
                    _repositorio.Delete(heroi);
                    if (await _repositorio.SaveChangeAsync()) //sempre utilizar um await, se utiliza o async
                    {
                        return Ok("Editado!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não foi editado");
        }
    }
}
