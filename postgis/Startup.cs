using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Converters;
using postgis.Infrastructures.Context;

namespace postgis
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
            services.AddMvc();
            services.AddDbContext<ApplicationDBContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("CnPostGis"), o => o.UseNetTopologySuite()));

            services.AddScoped<IApplicationDbContext, ApplicationDBContext>();

            services.AddSwaggerGen(services =>
            {
                services.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "PostGis",
                    Version = "v1",
                    Description = "Documentation API",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Name = "Mohammad Adi Fadilah",
                        Email = "adhi.development@gmail.com"
                    }
                });
            });

            services.AddControllers()
  .         AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new NetTopologySuite.IO.Converters.GeoJsonConverterFactory());
            });            //.AddJsonOptions(configure =>
            //{
            //    var geometryFactoryEx = new GeometryFactoryEx(new PrecisionModel(), 4326)
            //    {
            //        OrientationOfExteriorRing = LinearRingOrientation.CounterClockwise,
            //    };

            //    configure.JsonSerializerOptions.Converters.Add(new GeoJsonConverterFactory(geometryFactoryEx));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
