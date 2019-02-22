using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Helper
{
    public class EnumClass
    {
        public enum  UploadDocumentType{

            IdendityCard,
            CommercialRegisterExtract,
            Other
        }
        public enum DocUploadCalledFrom { BankUserProfile, LenderUserProfile }


    }
}