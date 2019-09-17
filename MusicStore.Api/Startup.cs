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
using MusicStore.Core.Song;

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


        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "MusicStoreApi",
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
            builder.RegisterType<SongRepository>().As<IRepository<Song>>().InstancePerLifetimeScope();

            builder.RegisterType<SongService>().As<ISongService>().InstancePerLifetimeScope();
            builder.RegisterType<AlbumService>().As<IAlbumService>().InstancePerLifetimeScope();
            builder.RegisterType<ArtistService>().As<IArtistService>().InstancePerLifetimeScope();

            builder.RegisterModule(new AutoMapperModule(Assembly.GetExecutingAssembly()));
            this.ApplicationContainer = builder.Build();


            return new AutofacServiceProvider(this.ApplicationContainer);
        }

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
