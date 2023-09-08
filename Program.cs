using Microsoft.AspNetCore.Rewrite;
using Google.Cloud.Firestore;
using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.ENV;
using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.SINGLE;

USE_ENVIROMENT.GOOGLE_CREDENTIALS();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMemoryCache();

var app = builder.Build();

ENV_THIS.ENVIROMENT(app);

app.UseHttpsRedirection();

app.MapControllers();


app.Run();