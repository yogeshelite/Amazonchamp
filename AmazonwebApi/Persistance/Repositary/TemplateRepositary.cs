using AmazonwebApi.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Persistance.Repositary
{
    public class TemplateRepositary : GenericRepository<amazonchampEntities1>, ITemplateRepositary
    {
        public List<GetTemplates_Result> GetTemplates()
        {
            return Context.GetTemplates().ToList();
        }
    }
    public interface ITemplateRepositary : IGenericRepository<amazonchampEntities1>
    {
        List<GetTemplates_Result> GetTemplates();

    }
}