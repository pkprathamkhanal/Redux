using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using API.Interfaces.JSON_Objects.Graphs;
using API.Interfaces.Graphs;

namespace API.Problems.NPComplete.NPC_WEIGHTEDCUT;

[ApiController]
[Route("[controller]")]
[Tags("Weighted Cut")]

#pragma warning disable CS1591
public class WEIGHTEDCUTGenericController : ControllerBase {
#pragma warning restore CS1591
///<summary>Returns a graph object used for dynamic solved visualization </summary>
///<param name="problemInstance" example="{{1,2,3,4,5},{{2,1},{1,3},{2,3},{3,5},{2,4},{4,5}},5}">Cut problem instance string.</param>
///<param name="solution" example="{{2,1},{2,3},{2,4},{5,3},{5,4}}">Cut instance string.</param>

///<response code="200">Returns graph object</response>

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("solvedVisualization")]
    public String solvedVisualization([FromQuery]string problemInstance, string solution){
        var options = new JsonSerializerOptions { WriteIndented = true };
        WEIGHTEDCUT aSet = new WEIGHTEDCUT(problemInstance);
        WeightedCutGraph aGraph = aSet.weightedCutAsGraph;
        List<Edge> edgesWithoutWeight = new List<Edge>();
        foreach(var edgeTuple in aGraph.getEdgeList) {
            Node n1 = edgeTuple.source;
            Node n2 = edgeTuple.target;
            edgesWithoutWeight.Add(new Edge(n1,n2));
        }
        API_UndirectedGraphJSON apiGraph = new API_UndirectedGraphJSON(aGraph.getNodeList,edgesWithoutWeight);

        List<string> parsedS = solution.Replace("{","").Replace("}","").Split(',').ToList();
        List<string> setS = new List<string>();

        for (int i = 0; i < apiGraph.nodes.Count; i++) {
            apiGraph.nodes[i].attribute1 = i.ToString();
            bool found = parsedS.Where((value, index) => index % 3 == 0 && value == apiGraph.nodes[i].name).Any();
            if(found) {
                apiGraph.nodes[i].attribute2 = true.ToString();
                setS.Add(apiGraph.nodes[i].name);
            }
        }

        for(int i=0;i<apiGraph.links.Count;i++){
            if(setS.Contains(apiGraph.links[i].source) && setS.Contains(apiGraph.links[i].target)) {
                apiGraph.links[i].attribute1 = true.ToString();
            }
            else if(setS.Contains(apiGraph.links[i].source) || setS.Contains(apiGraph.links[i].target)) {
                apiGraph.links[i].attribute1 = "Cut";
            }
            else {
                apiGraph.links[i].attribute1 = false.ToString();
            }
        }        

        string jsonString = JsonSerializer.Serialize(apiGraph, options);
        return jsonString;

    }

}
