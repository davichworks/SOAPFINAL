using System;
using System.IO;
using System.Net;
using System.Text;

class Program
{
    static void Main()
    {
        
        sumar(2,3);
        Console.ReadLine();
    }

    // Método para obtener el código de estado HTTP de una excepción WebException
    private static int GetHttpStatusCode(WebException webEx)
    {
        if (webEx.Response is HttpWebResponse httpResponse)
        {
            return (int)httpResponse.StatusCode;
        }
        return 0;
    }

    private static void sumar(int n1,int n2)
    {
        try
        {
            // URL del servicio ASMX
            string serviceUrl = "https://localhost:44334/WebService1.asmx";

            // Crear la solicitud HTTP
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.Method = "POST"; // Configurar el método de solicitud como POST
            request.ContentType = "text/xml"; // Configurar el tipo de contenido
            request.Headers.Add("SOAPAction", "\"http://tempuri.org/sumar\""); // Agregar el encabezado SOAPAction

            string soapBody = $@"<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                            <soap:Body>
                                <sumar xmlns=""http://tempuri.org/"">
                                    <n1>{n1}</n1>
                                    <n2>{n2}</n2>
                                </sumar>
                            </soap:Body>
                        </soap:Envelope>";
            // Configurar la longitud del contenido
            byte[] data = Encoding.UTF8.GetBytes(soapBody); // Puedes enviar datos aquí si es necesario
            request.ContentLength = data.Length;


            string soapRequestBody = Encoding.UTF8.GetString(data);

            // Mostrar la solicitud SOAP en la consola
            Console.WriteLine("Solicitud SOAP:");
            Console.WriteLine(soapRequestBody);

            // Escribir el contenido en el cuerpo de la solicitud (si es necesario)
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }

            // Obtener la respuesta del servidor
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
            {
                string result = reader.ReadToEnd();
                Console.WriteLine(result);
            }
        }
        catch (WebException webEx)
        {
            Console.WriteLine("Error de red:");
            Console.WriteLine($"Status Code: {GetHttpStatusCode(webEx)}");

            if (webEx.Response != null)
            {
                using (var stream = webEx.Response.GetResponseStream())
                using (var reader = new System.IO.StreamReader(stream))
                {
                    Console.WriteLine($"Respuesta del servidor: {reader.ReadToEnd()}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.ReadLine();
    }
    

    private static void conectar()
    {
        try
        {
            // URL del servicio ASMX
            string serviceUrl = "https://localhost:44334/WebService1.asmx/HelloWorld";

            // Crear la solicitud HTTP
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.Method = "POST"; // Configurar el método de solicitud como POST

            // Configurar la longitud del contenido
            byte[] data = Encoding.UTF8.GetBytes(""); // Puedes enviar datos aquí si es necesario
            request.ContentLength = data.Length;

            // Escribir el contenido en el cuerpo de la solicitud (si es necesario)
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }

            // Obtener la respuesta del servidor
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
            {
                string result = reader.ReadToEnd();
                Console.WriteLine(result);
            }
        }
        catch (WebException webEx)
        {
            Console.WriteLine("Error de red:");
            Console.WriteLine($"Status Code: {GetHttpStatusCode(webEx)}");

            if (webEx.Response != null)
            {
                using (var stream = webEx.Response.GetResponseStream())
                using (var reader = new System.IO.StreamReader(stream))
                {
                    Console.WriteLine($"Respuesta del servidor: {reader.ReadToEnd()}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.ReadLine();
    }
}
