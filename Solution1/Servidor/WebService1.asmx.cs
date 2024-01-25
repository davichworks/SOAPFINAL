using System;
using System.Web.Services;

namespace Servidor
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class WebService1 : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "¡Hola, mundo!";
        }

        [WebMethod]
        public int sumar(int n1,int n2)
        {
            Console.WriteLine("El servidor recibio" + n1 + n2);
            return n1 + n2;
        }
    }
}
