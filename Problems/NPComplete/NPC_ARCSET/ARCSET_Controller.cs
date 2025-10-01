using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_ARCSET;
using API.Problems.NPComplete.NPC_VERTEXCOVER;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using API.Problems.NPComplete.NPC_ARCSET.Verifiers;
using API.Problems.NPComplete.NPC_ARCSET.Solvers;
using API.Problems.NPComplete.NPC_VERTEXCOVER.ReduceTo.NPC_ARCSET;
using API.Interfaces.Graphs;
using API.Interfaces.JSON_Objects.Graphs;

namespace API.Problems.NPComplete.NPC_ARCSET;

[ApiController]
[Route("[controller]")]
[Tags("Feedback Arc Set")]
#pragma warning disable CS1591
public class ARCSETGenericController : ControllerBase {
#pragma warning restore CS1591
///<summary>Returns a graph object used for dynamic solved visualization </summary>
///<param name="problemInstance" example="(({a0,a1,b0,b1,c0,c1,d0,d1,e0,e1},{(a0,a1),(a1,b0),(a1,c0),(a1,e0),(b0,b1),(b1,a0),(b1,e0),(c0,c1),(c1,a0),(c1,d0),(d0,d1),(d1,c0),(e0,e1),(e1,a0),(e1,b0)}),3)">Feedback Arc Set problem instance string.</param>
///<param name="solution" example="{(a0,a1),(b0,b1),(c0,c1)}">Feedback Arc Set solution instance string.</param>

///<response code="200">Returns graph object</response>

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("solvedVisualization")]
    #pragma warning disable CS1591
    public String solvedVisualization([FromQuery]string problemInstance, string solution){
    #pragma warning restore CS1591
        var options = new JsonSerializerOptions { WriteIndented = true };
        ARCSET aSet = new ARCSET(problemInstance);
        ArcsetGraph aGraph = aSet.directedGraph;
        Dictionary<KeyValuePair<string,string>, bool> solutionDict = aSet.defaultSolver.getSolutionDict(problemInstance, solution);
        API_UndirectedGraphJSON apiGraph = new API_UndirectedGraphJSON(aGraph.getNodeList,aGraph.getEdgeList);

        for(int i=0;i<apiGraph.links.Count;i++){
            bool edgeVal = false;
            KeyValuePair<string, string> edge = new KeyValuePair<string, string>(apiGraph.links[i].source, apiGraph.links[i].target);
            solutionDict.TryGetValue(edge, out edgeVal);
            apiGraph.links[i].attribute1 = edgeVal.ToString();
        }

        string jsonString = JsonSerializer.Serialize(apiGraph, options);
        return jsonString;

    }
}
