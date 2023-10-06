using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using QuizzWebApi.Models;

namespace QuizzWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShitController : ControllerBase
{
    [HttpGet(Name = "Shit")]
    public string Get()
    {
        return @"
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⡛⠛⠷⣶⣤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣿⡇⠀⠀⠀⠙⠻⣷⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣾⠟⠀⠀⠀⠀⠀⠀⠈⣹⣿⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⡇⠀⠀⠀⠀⠀⣠⡴⠞⠉⠈⠻⣷⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣿⡧⠤⠤⠶⠖⠋⠉⠀⠀⠀⠀⠀⢹⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⠟⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⣿⣦⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣾⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⠞⠉⠀⠉⠙⠻⣶⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⠀⠀⠀⣀⣀⣀⠀⠀⠀⠀⢀⣠⡴⠞⠉⠀⢀⣀⣀⣀⠀⠀⠘⣿⡄⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⢈⣿⣆⣴⠟⠉⠉⠉⠛⢶⡖⠛⠉⠁⠀⠀⢠⡾⠋⠉⠈⠉⠻⣦⣰⣿⣀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⢀⣤⣾⠟⠋⡿⠁⢀⣾⣿⣷⣄⠈⢿⡀⠀⠀⠀⢠⡟⠀⢠⣾⣿⣷⡄⠘⣿⠉⠛⢿⣦⡀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⢠⣿⠏⠀⠀⢸⡇⠀⢸⣿⣿⣿⣿⠀⢸⡇⠀⠀⠀⢸⡇⠀⣿⣿⣿⣿⣧⠀⢹⡇⠀⠀⠙⣿⡆⠀⠀⠀⠀
⠀⠀⠀⠀⠀⢸⡏⠀⠀⠀⠘⣧⠀⠘⣿⣿⣿⡟⠀⣸⠇⠀⣀⣤⢾⣇⠀⠹⣿⣿⣿⠇⠀⣾⠁⠀⠀⠀⢸⣿⠀⠀⠀⠀
⠀⠀⠀⠀⠀⢸⣷⠀⠀⠀⠀⠹⣧⡀⠈⠉⠁⢀⣴⠿⠞⠋⠉⠀⠀⠻⣦⡀⠈⠉⠁⣠⡾⠃⠀⠀⠀⠀⣾⡏⠀⠀⠀⠀
⠀⠀⢀⣠⣶⠿⠛⠛⠛⠛⠛⠛⠉⠙⠛⠒⠛⠋⠁⠀⠀⠀⠀⠀⠀⠀⠈⠙⠛⠚⠛⠉⠀⠀⠀⠀⢀⡼⠿⢷⣦⣄⠀⠀
⠀⣠⡿⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣟⠛⠛⠒⠶⠶⠶⠶⠶⠶⠶⠖⠚⠛⢛⡷⠀⠀⠀⣀⡴⠋⠀⠀⠀⠈⠻⣷⡄
⢰⣿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢷⣄⡀⠀⠀⠀⠀⠀⠀⠀⢀⣠⡶⠛⢁⣠⡴⠞⠋⠀⠀⠀⠀⠀⠀⠀⠘⣿
⢸⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠓⠶⠶⠦⠶⠶⣚⣫⡥⠶⠚⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿
⠈⢿⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣀⣤⡤⠴⠖⠚⠛⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⡿⠃
⠀⠈⠛⣷⣦⣤⣤⣤⣤⣤⣤⣶⡶⠾⠿⠟⠿⠿⠿⠶⣶⣶⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣶⠶⠿⠛⠉⠀⠀";
    }

    [HttpPost()]
    public string Post([FromBody] JsonElement json)
    {
        string c = json.ToString();
        return @"{""isAuth"": ""true""}";
    }
}