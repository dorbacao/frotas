﻿using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Kereta.Dominio.Manutencao.SistemaAgg;
using Kereta.Infraestrutura.Data.Frota;
using Kereta.Infraestrutura.Data.Migrations;
using Vvs.Infraestrutura.Data.EF;
using Kereta.Infraestrutura.Data.Documentacao;

namespace Kereta.Infraestrutura.Data
{
    
    public class KeretaUnitOfWork : UnitOfWork
    {
        public KeretaUnitOfWork()
            : base()
        {

            Database.SetInitializer(new CreateDatabaseIfNotExistsInitializer());
            
        }

        internal KeretaUnitOfWork(DbConnection connection)
            : base(connection, true)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<KeretaUnitOfWork, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Frota
            modelBuilder.Configurations.Add(new MarcaDbMapping());
            modelBuilder.Configurations.Add(new GravidadeDbMapping());
            modelBuilder.Configurations.Add(new ProcessoDbMapping());
            modelBuilder.Configurations.Add(new ModeloDbMapping());
            modelBuilder.Configurations.Add(new SubSistemaDbMapping());
            modelBuilder.Configurations.Add(new SistemaDbMapping());
            modelBuilder.Configurations.Add(new VeiculoDbMapping());
            modelBuilder.Configurations.Add(new CategoriaDbMapping());
            

            //Financeiro
            modelBuilder.Configurations.Add(new CentroDeCustoDbMapping());

            //Pessoal
            modelBuilder.Configurations.Add(new FuncaoDoColaboradorDbMapping());
            modelBuilder.Configurations.Add(new ColaboradorDbMapping());


        }
    }
}
