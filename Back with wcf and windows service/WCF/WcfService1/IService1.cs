using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WcfService1.Util;

namespace WcfService1
{

    [ServiceContract]
    public interface IContactService
    {
        [OperationContract]
        void CreateContact(BackData data);


        [OperationContract]
        DataWithTotalCount GetContact(string SortName, bool Descending, int Page, int PageSize);


        [OperationContract]
        FileData Download(int fileId);
    }
}