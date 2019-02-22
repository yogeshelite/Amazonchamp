using AmazonwebApi.Models;
using AmazonwebApi.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Persistance.Repositary
{
    public class AboutRepositary : GenericRepository<amazonchampEntities1>, IAboutRepositary
    {
        public IEnumerable<GetAbout_Result> GetAbout()
        {
            return Context.GetAbout();
        }

        public GetUserAbout_Result GetUserAbout(AboutModel aboutModel)
        {
            return Context.GetUserAbout(aboutModel.UserId).FirstOrDefault();
        }

        public SaveUserAbout_Result SaveAbout(AboutModel aboutModel)
        {
            return Context.SaveUserAbout(aboutModel.AboutTitle, aboutModel.AboutSummary, aboutModel.UserId,aboutModel.Address,aboutModel.PhoneNo,aboutModel.Twitter,aboutModel.Facebook,aboutModel.Linkedin,aboutModel.AttachmentLogoName).FirstOrDefault();
        }
    }
    public interface IAboutRepositary : IGenericRepository<amazonchampEntities1>
    {
        SaveUserAbout_Result SaveAbout(AboutModel aboutModel);
        GetUserAbout_Result GetUserAbout(AboutModel aboutModel);
        IEnumerable<GetAbout_Result> GetAbout();
    }
}