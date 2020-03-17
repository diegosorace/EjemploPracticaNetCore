using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EjemploPracticaNetCore.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EjemploPracticaNetCore.MW
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleware
    {  
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddleware> _Logger;

        public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> Logger)
        {
            _next = next;
            _Logger = Logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                // originBody se inicia como un body de la respuesta vacío.
                var originBody = httpContext.Response.Body;

                //newBody inicializa un nuevo espacio en memoria para la transferencia de datos en bytes (stream)
                var newBody = new MemoryStream();

                // Ahora nuestro body response se convierte en el espacio en memoria
                httpContext.Response.Body = newBody;

                var dateDateOfAdmission = DateTime.Now;

                await _next.Invoke(httpContext);

                //Le asignamos el espacio en memoria a los datos traídos del body response.
                httpContext.Response.Body = originBody;

                httpContext.Response.ContentType = "application/json";

                //Nos posicionamos desde el comienzo para poder imprimir los datos guardados.
                newBody.Seek(0, SeekOrigin.Begin);

                if (httpContext.Response.StatusCode == StatusCodes.Status200OK && httpContext.Request.Method == "GET")
                {
                    string json = await new StreamReader(newBody).ReadToEndAsync();

                    var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

                    List<ProductMw> productMws = new List<ProductMw>();
                    foreach (var item in products)
                    {
                        var data = new ProductMw
                        {
                            DateOfAdmission = dateDateOfAdmission,
                            DateOfEgress = DateTime.Now,
                            Product = item
                        };
                        productMws.Add(data);
                    }

                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(productMws));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
