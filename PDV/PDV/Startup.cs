﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PDV.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AppPDVContext = PDV.Models.AppPDVContext;
using PDV.Services;
using PDV.Repository;

namespace PDV
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            string connection = Configuration["ConexaoDB:ConnectionString"];
            //Configuração a conexão da base de dados 
            services.AddDbContext<AppPDVContext>(op => op.UseSqlServer(connection));

            services.AddTransient<UsuarioRepository, UsuarioRepository>();
            services.AddTransient<ItemRepository, ItemRepository>();
            services.AddTransient<PedidoRepository, PedidoRepository>();

            services.AddTransient<UsuarioService, UsuarioService>();
            services.AddTransient<ItemService, ItemService>();
            services.AddTransient<PedidoService, PedidoService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
