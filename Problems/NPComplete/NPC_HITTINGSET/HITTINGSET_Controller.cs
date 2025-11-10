
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using API.Problems.NPComplete.NPC_HITTINGSET.ReduceTo.NPC_EXACTCOVER;
using API.Problems.NPComplete.NPC_EXACTCOVER;

namespace API.Problems.NPComplete.NPC_HITTINGSET;

[ApiController]
[Route("[controller]")]
[Tags("Hitting Set")]
#pragma warning disable CS1591
public class reduceToEXACTCOVERController : ControllerBase {


///<summary>Returns a reduction object with info for reduction to clique </summary>
///<response code="200">Returns sipserReduction Object</response>

    [ProducesResponseType(typeof(reduceToEXACTCOVER), 200)]
    [HttpGet("info")]

    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        HITTINGSET defaultHITTINGSET = new HITTINGSET();
        //SipserReduction reduction = new SipserReduction(defaultSAT3);
        reduceToEXACTCOVER reduction = new reduceToEXACTCOVER(defaultHITTINGSET);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

///<summary>Returns a reduction from Independent Set to Clique based on the given Independent Set instance  </summary>
///<param name="problemInstance" example="({1,2,3,4},{{4,1},{1,2},{4,3},{3,2},{2,4}})">Independent Set problem instance string.</param>
///<response code="200">Returns Independent Set to CliqueReduction object</response>

    [ProducesResponseType(typeof(EXACTCOVER), 200)]
    [HttpPost("reduce")]
    public String getReduce([FromBody]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        HITTINGSET defaultHITTINGSET = new HITTINGSET(problemInstance);
        reduceToEXACTCOVER reduction = new reduceToEXACTCOVER(defaultHITTINGSET);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

}