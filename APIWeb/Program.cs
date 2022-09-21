using APIWeb.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region services

builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwagger();
builder.Services.AddAuthorizationPolicies();

#endregion


#region Build

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

#endregion