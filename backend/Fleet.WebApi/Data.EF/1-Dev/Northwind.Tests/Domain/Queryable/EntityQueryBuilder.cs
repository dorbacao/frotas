﻿using System.Data.Entity;
using System.Linq;
using Vvs.Domain.Seedwork.Repositorios.Queryable;

namespace Northwind.Tests.Domain.Queryable
{
    public class EntityQueryBuilder<TEntidade> : DefaultQueryBuilder<TEntidade> where TEntidade : class
    {

        /// <summary>
        /// Método com a especificação de como o Entity Framework realiza o Join em queries (Include Properties)
        /// </summary>
        public override IQueryable<TEntidade> JoinQueryWith(IQueryable<TEntidade> query, string[] joinWith)
        {
            ValidaArrayDeNavigationProperties(joinWith);
            return joinWith.Aggregate(query, (x, prop) => x.Include(prop));
        }

    }
}
