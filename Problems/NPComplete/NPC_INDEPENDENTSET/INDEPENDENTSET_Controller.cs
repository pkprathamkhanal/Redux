using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_INDEPENDENTSET;
using API.Problems.NPComplete.NPC_INDEPENDENTSET.Solvers;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Interfaces.JSON_Objects.Graphs;
using API.Problems.NPComplete.NPC_INDEPENDENTSET.Verifiers;
using API.Interfaces.Graphs.GraphParser;
using API.Problems.NPComplete.NPC_INDEPENDENTSET.ReduceTo.NPC_CLIQUE;

namespace API.Problems.NPComplete.NPC_INDEPENDENTSET;

[ApiController]
[Route("[controller]")]
[Tags("Independent Set")]
#pragma warning disable CS1591
public class INDEPENDENTSETGenericController : ControllerBase {
#pragma warning restore CS1591
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("solvedVisualization")]
    public String getSolvedVisualization([FromQuery]string problemInstance,string solution) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        List<string> solutionList = GraphParser.parseNodeListWithStringFunctions(solution); //Note, this is just a convenience string to list function.
        INDEPENDENTSET independentSet = new INDEPENDENTSET(problemInstance);
        API_UndirectedGraphJSON apiGraph = independentSet.graph.ToAPIGraph();
        for(int i=0;i<apiGraph.nodes.Count;i++){
            apiGraph.nodes[i].attribute1 = i.ToString();
            if(solutionList.Contains(apiGraph.nodes[i].name)){ //we set the nodes as either having a true or false flag which will indicate to the frontend whether to highlight.
                apiGraph.nodes[i].attribute2 = true.ToString(); 
            }
            else{apiGraph.nodes[i].attribute2 = false.ToString();}
        }
        string jsonString = JsonSerializer.Serialize(apiGraph, options);
        return jsonString;
    }
}

[ApiController]
[Route("[controller]")]
[Tags("Independent Set")]
#pragma warning disable CS1591
public class reduceToCLIQUEController : ControllerBase {


///<summary>Returns a reduction object with info for reduction to clique </summary>
///<response code="200">Returns sipserReduction Object</response>

    [ProducesResponseType(typeof(CliqueReduction), 200)]
    [HttpGet("info")]

    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        INDEPENDENTSET defaultINDEPENDENTSET = new INDEPENDENTSET();
        //SipserReduction reduction = new SipserReduction(defaultSAT3);
        CliqueReduction reduction = new CliqueReduction(defaultINDEPENDENTSET);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

///<summary>Returns a reduction from Independent Set to Clique based on the given Independent Set instance  </summary>
///<param name="problemInstance" example="(({1,2,3,4},{{4,1},{1,2},{4,3},{3,2},{2,4}}),2)">Independent Set problem instance string.</param>
///<response code="200">Returns Independent Set to CliqueReduction object</response>

    [ProducesResponseType(typeof(CliqueReduction), 200)]
    [HttpPost("reduce")]
    public String getReduce([FromBody]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        INDEPENDENTSET defaultINDEPENDENTSET = new INDEPENDENTSET(problemInstance);
        CliqueReduction reduction = new CliqueReduction(defaultINDEPENDENTSET);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

}
