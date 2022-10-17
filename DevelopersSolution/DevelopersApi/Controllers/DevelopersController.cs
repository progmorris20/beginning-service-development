using DevelopersApi.Models;
using Microsoft.AspNetCore.Mvc;
using DevelopersApi.Adapters;
using DevelopersApi.Domain;

namespace DevelopersApi.Controllers;
public class DevelopersController : ControllerBase
{
    private readonly MongoDevelopersAdapter _mongoAdapter;
    public DevelopersController(MongoDevelopersAdapter mongoAdapter)
    {
        _mongoAdapter = mongoAdapter;
    }
    //GET On-call developer
    [HttpGet("/on-call-developer")]
    public ActionResult GetOnCallDeveloper()
    {
        var response = new DeveloperDetailsModel("1", "Jeff", "Gonzalez", "555-1212", "jeff@hypertheory.com");
        return Ok(response); //200
    }

    [HttpGet("/developers")]
    public ActionResult GetAllDevelopers()
    {
        var response = new CollectionResponse<DeveloperSummaryModel>();
        response.Data = new List<DeveloperSummaryModel>()
        {
            new DeveloperSummaryModel("1", "Jeff", "Gonzalez", "jeff@hypertheory.com"),
            new DeveloperSummaryModel("2", "Sue", "Jones", "sue@aol.com")
        };
        return Ok(response);
    }

    [HttpPost("/developers")]
    public async Task<ActionResult> AddADeveloper([FromBody] DeveloperCreateModel request)
    {
        var developerToAdd = new DeveloperEntity {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email  = request.Email,
            Phone = request.Phone,
            IsOnCall = true
        };
        await _mongoAdapter.Developers.InsertOneAsync(developerToAdd);
        //var response = new DeveloperDetailsModel(Guid.NewGuid().ToString(), request.FirstName, request.LastName, request.Email, request.Phone);
        return StatusCode(201); // "Good. Ok. I created this.
    }
}


