
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataBookStore.Models;

namespace ODataBookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container

            builder.Services.AddDbContext<BookStoreContext>(opt => opt.UseInMemoryDatabase("BookLists"));
            builder.Services.AddControllers();

            builder.Services.AddControllers().AddOData(Option => Option.Select().Filter()
            .Count().OrderBy().Expand().SetMaxTop(100)
            .AddRouteComponents("odata", GetEdmModel()));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); 
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseODataBatching();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.Use(next => context =>
            {
                var endpoint = context.GetEndpoint();
                if (endpoint == null)
                {
                    return next(context);
                }

                IEnumerable<string> templates;
                IODataRoutingMetadata metadata =
                    endpoint.Metadata.GetMetadata<IODataRoutingMetadata>();
                if (metadata != null)
                {
                    templates = metadata.Template.GetTemplates();
                }
                return next(context);
            });

            app.UseAuthorization();

          


            app.MapControllers();

            app.Run();
        }
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder=new ODataConventionModelBuilder();
            builder.EntitySet<Book>("Books");
            builder.EntitySet<Press>("Presses");
            return builder.GetEdmModel();
        }
    }
}
