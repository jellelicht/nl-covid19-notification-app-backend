// Copyright � 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.AgProtocolSettings;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.EfDatabase;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.ExposureKeySets;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.KeysFirstWorkflow.WorkflowAuthorisation;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Manifest;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.RivmAdvice;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Services;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Services.AuthorisationTokens;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Services.Signing;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Workflows;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.RiskCalculationConfig;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.ServerStandAlone
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
            services.AddControllers(options => 
            { 
                options.RespectBrowserAcceptHeader = true; 
            });
                
            services.AddControllers();

            services.AddSingleton<IUtcDateTimeProvider, StandardUtcDateTimeProvider>();
            services.AddSingleton<ILuhnModNConfig, LuhnModNConfig>();
            services.AddSingleton<IAgConfig, AgConfigAppSettings>();
            services.AddSingleton<IPublishingIdCreator>(x => new Sha256PublishingIdCreator(new HardCodedExposureKeySetSigning()));

            services.AddScoped<IDbContextProvider<ExposureContentDbContext>, DbContextProvider<ExposureContentDbContext>>();
            //TODO services.AddScoped<IDbContextBuilder<ExposureContentDbContext>, PostGresMssDbContextBuilder>();
            services.AddScoped<IEfDbConfig>(x => new StandardEfDbConfig(x.GetService<IConfiguration>(), "MSS"));

            services.AddScoped<HttpPostWorkflowCommand, HttpPostWorkflowCommand>();
            services.AddSingleton<IWorkflowValidator, WorkflowValidator>();
            services.AddSingleton<IWorkflowAuthorisationTokenValidator, WorkflowAuthorisationTokenLuhnModNValidator>();
            services.AddSingleton<IWorkflowValidatorConfig, WorkflowValidationAppSettings>();
            services.AddSingleton<ITemporaryExposureKeyValidator, TemporaryExposureKeyValidator>();
            services.AddSingleton<ITemporaryExposureKeyValidatorConfig, TemporaryExposureKeyValidatorConfig>();
            services.AddScoped<IWorkflowWriter, WorkflowInsertDbCommand>();

            services.AddScoped<HttpPostWorkflowAuthoriseCommand, HttpPostWorkflowAuthoriseCommand>();
            services.AddScoped<IWorkflowAuthorisationWriter, WorkflowDbAuthoriseCommand>();
            
            services.AddScoped<HttpGetLatestManifestCommand, HttpGetLatestManifestCommand>();
            services.AddScoped<ManifestBuilder, ManifestBuilder>();
            services.AddScoped<GetActiveExposureKeySetsListCommand, GetActiveExposureKeySetsListCommand>();
            services.AddScoped<GetLatestRiskCalculationConfigCommand, GetLatestRiskCalculationConfigCommand>();
            services.AddScoped<GetLatestRivmAdviceCommand, GetLatestRivmAdviceCommand>();

            services.AddScoped<HttpGetAgExposureKeySetCommand, HttpGetAgExposureKeySetCommand>();
            services.AddScoped<AgExposureKeySetSafeReadCommand, AgExposureKeySetSafeReadCommand>();
            
            services.AddScoped<HttpGetRiskCalculationConfigCommand, HttpGetRiskCalculationConfigCommand>();
            services.AddScoped<SafeGetRiskCalculationConfigDbCommand, SafeGetRiskCalculationConfigDbCommand>();

            services.AddScoped<HttpPostRiskCalculationConfigCommand, HttpPostRiskCalculationConfigCommand>();
            services.AddScoped<RiskCalculationConfigValidator, RiskCalculationConfigValidator>();
            services.AddScoped<RiskCalculationConfigInsertDbCommand, RiskCalculationConfigInsertDbCommand>();

            services.AddScoped<HttpPostRivmAdviceCommand, HttpPostRivmAdviceCommand>();
            services.AddScoped<RivmAdviceInsertDbCommand, RivmAdviceInsertDbCommand>();
            services.AddScoped<RivmAdviceValidator, RivmAdviceValidator>();

            services.AddScoped<HttpGetRivmAdviceCommand, HttpGetRivmAdviceCommand>();
            services.AddScoped<SafeGetRivmAdviceCommand, SafeGetRivmAdviceCommand>();
            
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo { Title = "MSS Stand-Alone Development Server", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "MSS Stand-Alone Development Server V1");
            });
            if(!env.IsDevelopment()) app.UseHttpsRedirection(); //HTTPS redirection not mandatory for development purposes
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
