using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceReference;

namespace Correios
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {

                    var client = new AtendeClienteClient();
                    var resposta = client.consultaCEPAsync("00000000");

                    var unidadesPostagem = resposta.Result.@return.unidadesPostagem;
                    var bairro = resposta.Result.@return.bairro;
                    var cidade = resposta.Result.@return.cidade;
                    var complemento2 = resposta.Result.@return.complemento2;
                    var end = resposta.Result.@return.end;
                    var uf = resposta.Result.@return.uf;

                    await context.Response
                            .WriteAsync($"Endereco: {end}, {unidadesPostagem}, {complemento2}\nBairro: {bairro}\n{cidade}-{uf}");
                });
            });
        }
    }
}
