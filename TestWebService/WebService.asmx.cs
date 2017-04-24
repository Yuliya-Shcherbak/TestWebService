using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace TestWebService
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "TestWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod (Description = "Getting list of rates for period")]
        public string  TestWebService(DateTime DateStart, DateTime DateEnd, bool mode)
        {            
            if (!mode)
            {
                return CalculateService.CalculateMode1(DateStart, DateEnd);
            }
            else
            {
                return CalculateService.CalculateMode2(DateStart, DateEnd);
            }
        }
    }
}
