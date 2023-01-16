#pragma warning disable ASP0014

var builder = WebApplication.CreateBuilder();

builder.Host.UseSerilog();

builder.Services.AddResponseCompression();
builder.Services.AddSwaggerDefault();
builder.Services.AddSpaStaticFiles("Frontend");
builder.Services.AddAuthenticationJwtBearer();
builder.Services.AddContext<Context>(options => options.UseSqlServer(builder.Services.GetConnectionString(nameof(Context))));
builder.Services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();
builder.Services.AddClassesMatchingInterfaces(nameof(Architecture));
builder.Services.AddMediator(nameof(Architecture));
builder.Services.AddControllers().AddJsonOptions().AddAuthorizationPolicy();

var application = builder.Build();

application.UseException();
application.UseHttps();
application.UseLocalization("en", "pt");
application.UseResponseCompression();
application.UseSwagger().UseSwaggerUI();
application.UseRouting();
application.UseAuthentication().UseAuthorization();
application.UseEndpoints(endpoints => endpoints.MapControllers());
application.UseSpaAngular("Frontend", "start", "http://localhost:4200");

application.Run();
