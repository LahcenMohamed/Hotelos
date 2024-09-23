using Hangfire;
using Hotelos.Application.Custom;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Hotelos;

[DependsOn(
    typeof(HotelosDomainModule),
    typeof(HotelosApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpBackgroundJobsHangfireModule)
    )]
public class HotelosApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetAbpHostEnvironment();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<HotelosApplicationModule>();
        });

        context.Services.AddTransient<IUserClaimsPrincipalFactory<Volo.Abp.Identity.IdentityUser>, CustomUserClaimsPrincipalFactory>();
        context.Services.AddScoped<SignInManager<Volo.Abp.Identity.IdentityUser>, CustomSignInManager>();

        //Configure<AbpDistributedCacheOptions>(options =>
        //{
        //    options.KeyPrefix = "Hotelos_";
        //    options.GlobalCacheEntryOptions.AbsoluteExpiration = DateTimeOffset.Now.AddHours(1);
        //});

        ConfigureHangfire(context, configuration);
        Configure<AbpBackgroundJobWorkerOptions>(options =>
        {
            options.DefaultTimeout = 1; //10 days (as seconds)
        });
    }

    private void ConfigureHangfire(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddHangfire(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
            config.UseSimpleAssemblyNameTypeSerializer();
            config.UseRecommendedSerializerSettings();
            config.UseSqlServerStorage(configuration.GetConnectionString("HangFire"));
        });
    }
}
