using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using API.Interfaces.JSON_Objects.Graphs;
using API.Interfaces.Graphs;

namespace API.Problems.NPComplete.NPC_TSP;

[ApiController]
[Route("[controller]")]
[Tags("TSP")]

#pragma warning disable CS1591
public class TSPGenericController : ControllerBase
{
#pragma warning restore CS1591
    ///<summary>Returns a graph object used for dynamic solved visualization </summary>
    ///<param name="problemInstance" example="{{1,2,3,4,5},{{2,1},{1,3},{2,3},{3,5},{2,4},{4,5}},5}">Cut problem instance string.</param>
    ///<param name="solution" example="{{2,1},{2,3},{2,4},{5,3},{5,4}}">Cut instance string.</param>

    ///<response code="200">Returns graph object</response>

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("solvedVisualization")]
#pragma warning disable CS1591
    public String solvedVisualization([FromQuery] string problemInstance, string solution)
    {
#pragma warning restore CS1591
        var options = new JsonSerializerOptions { WriteIndented = true };
        TSP aSet = new TSP(problemInstance);
        TSPGraph aGraph = aSet.tspAsGraph;
        List<Edge> edgesWithoutWeight = new List<Edge>();
        foreach (var edgeTuple in aGraph.getEdgeList)
        {
            Node n1 = edgeTuple.source;
            Node n2 = edgeTuple.target;
            edgesWithoutWeight.Add(new Edge(n1, n2));
        }
        API_UndirectedGraphJSON apiGraph = new API_UndirectedGraphJSON(aGraph.getNodeList, edgesWithoutWeight);

        List<string> parsedS = solution.Replace("{", "").Replace("}", "").Split(',').ToList();

        for (int i = 0; i < apiGraph.nodes.Count; i++)
        {
            apiGraph.nodes[i].attribute1 = i.ToString();
            if(parsedS.Contains(apiGraph.nodes[i].name))
                apiGraph.nodes[i].attribute2 = true.ToString();
            else 
                apiGraph.nodes[i].attribute2 = false.ToString();
        }

        for (int j = 0; j < apiGraph.links.Count; j++)
        {
            for (int i = 0; i < parsedS.Count - 1; i++)
            {
                if ((parsedS[i] == apiGraph.links[j].source && parsedS[i+1] == apiGraph.links[j].target) || (parsedS[i] == apiGraph.links[j].target && parsedS[i+1] == apiGraph.links[j].source)) {
                    apiGraph.links[j].attribute1 = true.ToString();
                    break;
                }
                else
                    apiGraph.links[j].attribute1 = false.ToString();
            }
        }

        string jsonString = JsonSerializer.Serialize(apiGraph, options);
        return jsonString;

    }

}
