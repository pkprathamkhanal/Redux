using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using API.Interfaces.JSON_Objects.Graphs;

namespace API.Problems.NPComplete.NPC_STEINERTREE;

[ApiController]
[Route("[controller]")]
[Tags("Steiner Tree")]

#pragma warning disable CS1591
public class STEINERTREEGenericController : ControllerBase
{
#pragma warning restore CS1591
    ///<summary>Returns a graph object used for dynamic solved visualization </summary>
    ///<param name="problemInstance" example="{{1,2,3,4,5},{{2,1},{1,3},{2,3},{3,5},{2,4},{4,5}},5}">Steiner problem instance string.</param>
    ///<param name="solution" example="{{2,1},{2,3},{2,4},{5,3},{5,4}}">Steiner instance string.</param>

    ///<response code="200">Returns graph object</response>

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("solvedVisualization")]
#pragma warning disable CS1591
    public String getSolvedVisualization([FromQuery] string problemInstance, string solution)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        STEINERTREE steiner = new STEINERTREE(problemInstance);
        List<string> solutionListNodes = solution.Replace("{", "").Replace("}", "").Split(",").ToList();
        List<string> solutionListEdges = solution.Replace("{{", "").Replace("}}", "").Split("},{").ToList();
        SteinerGraph hGraph = steiner.steinerAsGraph;
        API_UndirectedGraphJSON apiGraph = new API_UndirectedGraphJSON(hGraph.getNodeList, hGraph.getEdgeList);
        if(solution != "{}") {
        for(int j = 0; j < solutionListNodes.Count; j++)
        {
            for (int i = 0; i < apiGraph.nodes.Count; i++)
            {
                if (solutionListNodes.Contains(apiGraph.nodes[i].name))
                {
                    apiGraph.nodes[i].attribute1 = i.ToString();
                    apiGraph.nodes[i].attribute2 = true.ToString();
                    if(steiner.terminals.Contains(apiGraph.nodes[i].name)) {
                        apiGraph.nodes[i].attribute3 = true.ToString();
                    }
                }
            }
        }

        for(int j = 0; j < solutionListEdges.Count; j++) {
            List<string> edgeValues = solutionListEdges[j].Split(',').ToList();
            string target = edgeValues[1];
            string source = edgeValues[0];
            for (int i = 0; i < apiGraph.links.Count; i++)
            {
                if ((apiGraph.links[i].target == target && apiGraph.links[i].source == source) ||
                (apiGraph.links[i].source == target && apiGraph.links[i].target == source) )
                {
                    apiGraph.links[i].attribute1 = true.ToString();
                }
            }
        }
        }
        string jsonString = JsonSerializer.Serialize(apiGraph, options);
        return jsonString;

    }
}
