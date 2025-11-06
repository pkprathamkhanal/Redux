using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Interfaces.JSON_Objects.Graphs;
using API.Interfaces.Graphs.GraphParser;
using API.Problems.NPComplete.NPC_PARTITION;
using API.Problems.NPComplete.NPC_PARTITION.Solvers;
using API.Problems.NPComplete.NPC_PARTITION.Verifiers;
using API.Problems.NPComplete.NPC_PARTITION.ReduceTo.NPC_WEIGHTEDCUT;


namespace API.Problems.NPComplete.NPC_PARTITION;

[ApiController]
[Route("[controller]")]
[Tags("Partition")]


#pragma warning disable CS1591

public class KarpPartitionToCutController : ControllerBase {
#pragma warning restore CS1591

  
///<summary>Returns a reduction object with info for Graph Coloring to CliqueCover Reduction </summary>
///<response code="200">Returns CliqueCoverReduction object</response>

    [ProducesResponseType(typeof(WEIGHTEDCUTReduction), 200)]
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        PARTITION defaultGRAPHCOLORING = new PARTITION();
        //SipserReduction reduction = new SipserReduction(defaultSAT3);
        WEIGHTEDCUTReduction reduction = new WEIGHTEDCUTReduction(defaultGRAPHCOLORING);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

///<summary>Returns a reduction from Graph Coloring to CliqueCover based on the given Graph Coloring instance  </summary>
///<param name="problemInstance" example="{{1,7,12,15} : 28}">Graph Coloring problem instance string.</param>
///<response code="200">Returns Fengs's Graph Coloring to CliqueCover object</response>

    [ProducesResponseType(typeof(WEIGHTEDCUTReduction), 200)]
    [HttpPost("reduce")]
    public String getReduce([FromBody]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        PARTITION defaultGRAPHCOLORING = new PARTITION(problemInstance);
        WEIGHTEDCUTReduction reduction = new WEIGHTEDCUTReduction(defaultGRAPHCOLORING);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

}
