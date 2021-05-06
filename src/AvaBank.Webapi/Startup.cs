using Liquid.Cache.Redis;
using Liquid.Core.DependencyInjection;
using Liquid.Messaging.Extensions;
using Liquid.WebApi.Http.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using AvaBank.Domain.Config;
using AvaBank.Domain.DI;
using AvaBank.Domain.Queries;
using AvaBank.Domain.Service;
using AvaBank.Infra.MessageBroker;
using AvaBank.Infra.ServiceClient;
using AvaBank.Infra.ServiceClient.DI;

namespace AvaBank.Webapi
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services
                .Configure<GzipCompressionProviderOptions>(options =>
                    options.Level = System.IO.Compression.CompressionLevel.Optimal)
                .AddResponseCompression(options =>
                {
                    options.Providers.Add<GzipCompressionProvider>();
                    options.EnableForHttps = true;
                });

            services
                .AddOptions();

            services
               .Configure<CacheConfig>(_configuration.GetSection(nameof(CacheConfig)));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAccountServiceClient, AccountServiceHttpClient>();
            services.AddAutoMapper(typeof(ListAccountsQuery).Assembly);

            services.ConfigureLiquidHttp();
            services.AddLiquidSwagger();
            services.AddLightRedisCache();
            //services.AddLightMemoryCache();
            services.RegisterDomainConfigs();
            services.RegisterHttpService();

            services.RegisterMessage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseLiquidSwagger();
            app.ConfigureApplication();

            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.ApplicationServices.StartProducersConsumers();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}