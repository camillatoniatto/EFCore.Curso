﻿using EFCore.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Repositorio
{
    public class HeroiContexto : DbContext
    {
        public HeroiContexto(DbContextOptions<HeroiContexto> options) : base(options) { } //passa options para startup.cs

        public DbSet<Heroi> Herois { get; set; }
        public DbSet<Batalha> Batalhas { get; set; }
        public DbSet<Arma> Armas { get; set; }
        public DbSet<HeroiBatalha> HeroisBatalhas { get; set; }
        public DbSet<IdentidadeSecreta> IdentidadeSecretas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeroiBatalha>(entity => //CHAVE COMPOSTA
            {
                entity.HasKey(e => new { e.BatalhaId, e.HeroiId }); //a chave é composta de e.BatalhaId e e.HeroiId
            });
        }
    }
}