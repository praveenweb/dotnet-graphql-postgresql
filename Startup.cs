using dotnet_graphql_postgres.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


namespace dotnet_graphql_postgres
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
      _ = services.AddDbContext<postgresContext>();


      _ = services.AddControllers();
      _ = services.AddSwaggerGen(c =>
        {
          c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnet_graphql_postgres", Version = "v1" });
        });

      _ = services.AddGraphQLServer()
          .AddQueryType<Query>()
          .AddProjections()
          .AddFiltering()
          .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        _ = app.UseDeveloperExceptionPage();
        _ = app.UseSwagger();
        _ = app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnet_graphql_postgres v1"));
      }

      //   _ = app.UseHttpsRedirection();

      _ = app.UseRouting();

      _ = app.UseAuthorization();

      _ = app.UseEndpoints(endpoints =>
        {
          _ = endpoints.MapControllers();
          _ = endpoints.MapGraphQL();
        });
    }
  }
}
