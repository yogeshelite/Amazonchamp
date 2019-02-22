﻿using AmazonwebApi.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Persistance.Repositary
{

    public abstract class GenericRepository<C>: IGenericRepository<C> where C : amazonchampEntities1, new()
    {
        public C Context { get; set; } = new C();

    }

    public interface IGenericRepository<C> where C : amazonchampEntities1, new()
    {
    }
}
