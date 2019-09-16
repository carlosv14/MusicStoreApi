using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MusicStore.Database.Models;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using MusicStore.Database.Repositories;
using MusicStore.Core.Album;
using System.Reflection;
using MusicStore.Core.Artist;
using Swashbuckle.AspNetCore.Swagger;

namespace MusicStore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "MusicStoreApi",
                    Description = "Testing"  
                 });
            });

            var builder = new ContainerBuilder();
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<MusicStoreContext>().UseSqlServer(Configuration.GetConnectionString("MusicStoreDatabase"));
            builder.Populate(services);
            builder.RegisterType<MusicStoreContext>()
                .WithParameter("options", dbContextOptionsBuilder.Options)
                .AsSelf()
                .InstancePerLifetimeScope();
            builder.RegisterType<AlbumRepository>().As<IRepository<Album>>().InstancePerLifetimeScope();
            builder.RegisterType<ArtistRepository>().As<IRepository<Artist>>().InstancePerLifetimeScope();

            builder.RegisterType<AlbumService>().As<IAlbumService>().InstancePerLifetimeScope();
            builder.RegisterType<ArtistService>().As<IArtistService>().InstancePerLifetimeScope();

            builder.RegisterModule(new AutoMapperModule(Assembly.GetExecutingAssembly()));
            this.ApplicationContainer = builder.Build();


            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json" , "MusicStoreApi");
            });
        }
    }
}
