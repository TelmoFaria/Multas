using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class MultasDb : DbContext {

        public MultasDb() : base("name=MultasDBConnectionString") { }

        //descrever o nome das tabelas na base de dados
        public virtual DbSet<Multas> Multas { get; set; } //tabela multas
        public virtual DbSet<Condutores> Condutores { get; set; } //tabela condutores
        public virtual DbSet<Agentes> Agentes { get; set; } //tabela agentes
        public virtual DbSet<Viaturas> Viaturas { get; set; } //tabela voiotures


    }
}