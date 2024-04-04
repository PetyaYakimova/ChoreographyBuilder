using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddApplicationIdentity(builder.Configuration);

builder.Services.AddControllersWithViews(options=>
{
	options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddMappingServices();

builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	//TODO: Check why when user tries to access amdin pages or when admin tries to access user pages they see general error page and not my custom views
	//TODO: Uncomment the following line before submitting
	//app.UseDeveloperExceptionPage();
	//Use the following 2 line only to see the error pages
	app.UseExceptionHandler("/Home/Error/500");
	app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error/500");
	app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
			name: "areas",
			pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
		  );
	endpoints.MapDefaultControllerRoute();
	endpoints.MapRazorPages();
});

app.Run();
