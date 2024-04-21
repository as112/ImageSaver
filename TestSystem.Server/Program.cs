using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using TestSystem.Core.Models;
using TestSystem.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ISaver<ImageWithText>, Saver<ImageWithText>>();

var app = builder.Build();

app.UseHttpsRedirection();

var imgPath = Path.Combine(Environment.CurrentDirectory, "img");
var listPath = Path.Combine(Environment.CurrentDirectory, "list");

app.MapGet("/all", (ISaver<ImageWithText> saver) => Results.Ok(saver.GetList(listPath)));

app.MapGet("/img/{id}", async (string id) => 
{
    var imagePath = Path.Combine(imgPath, id);

    if (!File.Exists(imagePath))
    {
        return Results.NotFound();
    }

    try
    {
        var imageBytes = await File.ReadAllBytesAsync(imagePath);
        return Results.File(imageBytes, "image/jpeg", id);
        
    }
    catch
    {
        return Results.StatusCode(500);
    }
});

app.MapPost("/img", async (HttpContext context, ISaver<ImageWithText> saver) =>
{
    var baseUrl = $"{context.Request.Scheme}://{context.Request.Host}";
    var multipartRequest = context.Request;
    var json = multipartRequest.Form["json"];
    var image = multipartRequest.Form.Files["image"];

    if (!Directory.Exists(imgPath))
    {
        Directory.CreateDirectory(imgPath);
    }
    if (!Directory.Exists(listPath))
    {
        Directory.CreateDirectory(listPath);
    }
    var imageWithText = JsonSerializer.Deserialize<ImageWithText>(json);
    var tempFileName = $"{imageWithText.Id}_{image.FileName}";
    var tempFile = Path.Combine(imgPath, tempFileName);
    using var stream = File.OpenWrite(tempFile);
    await image.CopyToAsync(stream);
    imageWithText.FilePath = $"{baseUrl}/img/{tempFileName}";
    saver.SaveInfo(imageWithText, listPath);
    return Results.Ok(imageWithText);
});

app.Run();
