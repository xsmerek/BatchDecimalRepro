using System.Linq;

using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;

namespace BatchDecimalRepro
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddOData();

            //services.AddDbContext<AppDbContext>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ODataConventionModelBuilder(app.ApplicationServices);
            builder.EntitySet<Example>("Examples");
            IEdmModel model = builder.GetEdmModel();

            app.UseODataBatching();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Expand().Filter().OrderBy().Count();

                routeBuilder.MapODataServiceRoute("ODataRoute", "odata", model, new DefaultODataBatchHandler());
            });
        }
    }
}
