 using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_DOMINATINGSET;
using API.Problems.NPComplete.NPC_DOMINATINGSET.Verifiers;
using API.Problems.NPComplete.NPC_DOMINATINGSET.Solvers;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Interfaces.JSON_Objects.Graphs;
using API.Interfaces.Graphs.GraphParser;
using System.Collections.Generic;

namespace API.Problems.NPComplete.NPC_DOMINATINGSET;

[ApiController]
[Route("[controller]")]
[Tags("Dominating Set")]
#pragma warning disable CS1591


public class DOMINATINGSETGenericController : ControllerBase
{
#pragma warning restore CS1591

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("visualization")]
    #pragma warning disable CS1591
    public string getVisualization([FromQuery] string problemInstance){
    #pragma warning restore CS1591
        var options = new JsonSerializerOptions { WriteIndented = true };
        DOMINATINGSET dominatingset = new DOMINATINGSET(problemInstance);
        DominatingSetGraph dsgraph = dominatingset.graph;
        API_UndirectedGraphJSON apiGraph = new API_UndirectedGraphJSON(dsgraph.getNodeList, dsgraph.getEdgeList);

        for (int i = 0; i < apiGraph.nodes.Count; i++)
        {
            apiGraph.nodes[i].attribute1 = i.ToString();
            apiGraph.nodes[i].attribute2 = bool.FalseString;
        }

        return JsonSerializer.Serialize(apiGraph, options);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("solvedVisualization")]
    public string GetSolvedVisualization([FromQuery] string problemInstance, string solution)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        DOMINATINGSET dominatingset = new DOMINATINGSET(problemInstance);
        DominatingSetGraph dsgraph = dominatingset.graph;
        API_UndirectedGraphJSON apiGraph = new API_UndirectedGraphJSON(dsgraph.getNodeList, dsgraph.getEdgeList);

        List<string> solutionNodes = GraphParser.parseNodeListWithStringFunctions(solution);
        for (int i = 0; i < apiGraph.nodes.Count; i++)
        {
            apiGraph.nodes[i].attribute1 = i.ToString();
            apiGraph.nodes[i].attribute2 = solutionNodes.Contains(apiGraph.nodes[i].name) ? bool.TrueString : bool.FalseString;
        }

        return JsonSerializer.Serialize(apiGraph, options);
    }

}

    
   


