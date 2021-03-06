﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Kereta.Dominio.Financeiro.CentroDeCustoAgg;
using Kereta.Dominio.Frota.MarcaAgg;
using Kereta.Dominio.Frota.ModeloAgg;
using Kereta.Dominio.Frota.VeiculoAgg;
using Kereta.Dominio.Manutencao.SistemaAgg;
using Kereta.Dominio.Pessoal.Colaborador;
using Kereta.Infraestrutura.Data;
using Microsoft.Owin.Cors;
using Owin;
using Vvs.Domain.Seedwork;
using Vvs.Domain.Seedwork.Repositorios;
using Vvs.Domain.Seedwork.UnitOfWork;
using Kereta.Dominio.Frota;
using Kereta.Dominio.Frota.ProcessoAgg;
using SimpleInjector;
using SimpleInjector.Integration.WebApi.Extensions;

namespace Kereta.Web.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            if (app == null) throw new ArgumentNullException("app");

            // CORS
            app.UseCors(CorsOptions.AllowAll);

            // Web API
            var httpConfig = new HttpConfiguration();
                        
            WebApiConfig.Register(httpConfig);
            
            var container = CreateContainer(app);

            app.UseSimpleInjector(container, httpConfig);

            httpConfig.EnsureInitialized();
            app.UseWebApi( httpConfig);

#if DEBUG
            app.UseErrorPage();
            app.UseWelcomePage("/");
#endif

        }

        private Container CreateContainer(IAppBuilder app)
        {
            var container = new Container();

            container.RegisterSingle<IAppBuilder>(app);                      

            container.Register<IUnitOfWork, KeretaUnitOfWork>();
            container.Register<IRepository<Modelo>, Repository<Modelo>>();
            container.Register<IRepository<Processo>, Repository<Processo>>();
            container.Register<IRepository<Gravidade>, Repository<Gravidade>>();
            container.Register<IRepository<SubSistema>, Repository<SubSistema>>();
            container.Register<IRepository<Sistema>, Repository<Sistema>>();
            container.Register<IRepository<Marca>, Repository<Marca>>();
            container.Register<IRepository<CentroDeCusto>, Repository<CentroDeCusto>>();
            container.Register<IRepository<FuncaoDoColaborador>, Repository<FuncaoDoColaborador>>();
            container.Register<IRepository<Colaborador>, Repository<Colaborador>>();
            container.Register<IRepository<Veiculo>, Repository<Veiculo>>();
            container.Register<IRepository<Categoria>, Repository<Categoria>>();

            container.Verify();

            return container;
        }
    }
}
