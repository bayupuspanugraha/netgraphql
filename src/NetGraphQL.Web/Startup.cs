using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NetGraphQL.GraphQL.Extensions;
using NetGraphQL.GraphQL.Schemas;

namespace NetGraphQL.Web
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetGraphQL.Web", Version = "v1" });
            });

            services.AddGraphQLData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetGraphQL.Web v1"));

                app.UseGraphQLPlayground(new PlaygroundOptions
                {
                    GraphQLEndPoint = "/api/user/graphql",
                    SubscriptionsEndPoint = "/api/user/graphql",
                }, "/ui/user/playground");

                app.UseGraphQLPlayground(new PlaygroundOptions
                {
                    GraphQLEndPoint = "/api/app/graphql",
                    SubscriptionsEndPoint = "/api/app/graphql",
                }, "/ui/app/playground");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // GraphQL
            app.UseWebSockets();

            app.UseGraphQLWebSockets<AppSchema>("/api/app/graphql");
            app.UseGraphQL<AppSchema>("/api/app/graphql");

            app.UseGraphQLWebSockets<UserSchema>("/api/user/graphql");
            app.UseGraphQL<UserSchema>("/api/user/graphql");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
