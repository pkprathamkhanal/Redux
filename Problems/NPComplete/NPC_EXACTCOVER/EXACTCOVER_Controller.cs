using Microsoft.AspNetCore.Mvc;

using System.Text.Json;
using API.Problems.NPComplete.NPC_EXACTCOVER.ReduceTo.NPC_SUBSETSUM;

namespace API.Problems.NPComplete.NPC_EXACTCOVER;

[ApiController]
[Route("[controller]")]
[Tags("Exact Cover")]


#pragma warning disable CS1591

public class KarpExactCoverToSubsetSumController : ControllerBase {
#pragma warning restore CS1591

  
///<summary>Returns a reduction object with info for Graph Coloring to CliqueCover Reduction </summary>
///<response code="200">Returns CliqueCoverReduction object</response>

    [ProducesResponseType(typeof(SubsetSumReduction), 200)]
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        EXACTCOVER defaultExactCover = new EXACTCOVER();
        SubsetSumReduction reduction = new SubsetSumReduction(defaultExactCover);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

///<summary>Returns a reduction from Graph Coloring to CliqueCover based on the given Graph Coloring instance  </summary>
///<param name="problemInstance" example="{{1,7,12,15} : 28}">Graph Coloring problem instance string.</param>
///<response code="200">Returns Fengs's Graph Coloring to CliqueCover object</response>

    [ProducesResponseType(typeof(SubsetSumReduction), 200)]
    [HttpPost("reduce")]
    public String getReduce([FromBody]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        EXACTCOVER defaultExactCover = new EXACTCOVER(problemInstance);
        SubsetSumReduction reduction = new SubsetSumReduction(defaultExactCover);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

}