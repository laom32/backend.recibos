using Arch.EntityFrameworkCore.UnitOfWork;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using backend.recibos.Config;
using Infraestructura;
using Infraestructura.Data;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.recibos;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public ILifetimeScope AutofacContainer { get; private set; }
    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });
        services.AddApiVersioning(o => {
            o.ReportApiVersions = true;
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = new ApiVersion(1, 0);
        });
        var con = Configuration["ConnectionStrings:DefaultConnection"];
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AutomapperConfig());
        });
        //services.AddInvoicingClient(Configuration["AWS:AccessKey"], Configuration["AWS:SecretKey"]);
        services.AddHttpClient();
        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
        services.AddDbContext<BackDbContext>(opt => opt.UseSqlServer(con))
        .AddUnitOfWork<BackDbContext>();
        services.AddMvc().AddControllersAsServices();
        services.AddOptions();
        services.Configure<FormOptions>(options =>
        {
            // Set the limit to 256 MB
            options.MultipartBodyLengthLimit = 268435456;
        });
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new InfraestructuraModule());
        builder.RegisterMediatR(AppDomain.CurrentDomain.GetAssemblies());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

        app.UseHttpsRedirection();

        app.UseRouting();

        //app.UseAuthorization();
        app.UseCors("AllowOrigin");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}