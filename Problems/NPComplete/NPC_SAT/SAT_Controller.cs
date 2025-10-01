using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using API.Problems.NPComplete.NPC_SAT.ReduceTo.NPC_SAT3;
namespace API.Problems.NPComplete.NPC_SAT;

[ApiController]
[Route("[controller]")]
[Tags("Exact Cover")]


#pragma warning disable CS1591

public class KarpSATToSAT3Controller : ControllerBase
{
#pragma warning restore CS1591


    ///<summary>Returns a reduction object with info for Graph Coloring to CliqueCover Reduction </summary>
    ///<response code="200">Returns CliqueCoverReduction object</response>

    [ProducesResponseType(typeof(SATReduction), 200)]
    [HttpGet("info")]
    public String getInfo()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT defaultSAT = new SAT();
        SATReduction reduction = new SATReduction(defaultSAT);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

    ///<summary>Returns a reduction from Graph Coloring to CliqueCover based on the given Graph Coloring instance  </summary>
    ///<param name="problemInstance" example="{{1,7,12,15} : 28}">Graph Coloring problem instance string.</param>
    ///<response code="200">Returns Fengs's Graph Coloring to CliqueCover object</response>

    [ProducesResponseType(typeof(SATReduction), 200)]
    [HttpPost("reduce")]
    public String getReduce([FromBody]string problemInstance)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT defaultSAT = new SAT(problemInstance);
        SATReduction reduction = new SATReduction(defaultSAT);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }
}
