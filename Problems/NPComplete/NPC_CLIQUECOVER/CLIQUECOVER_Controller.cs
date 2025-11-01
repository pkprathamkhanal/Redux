using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Interfaces.JSON_Objects.Graphs;
using API.Interfaces.Graphs.GraphParser;
using API.Problems.NPComplete.NPC_CLIQUECOVER;
using API.Problems.NPComplete.NPC_CLIQUECOVER.Solvers;
using API.Problems.NPComplete.NPC_CLIQUECOVER.Verifiers;


namespace API.Problems.NPComplete.NPC_CLIQUECOVER;

[ApiController]
[Route("[controller]")]
[Tags("CliqueCover")]

#pragma warning disable CS1591
public class CLIQUECOVERGenericController : ControllerBase {
#pragma warning restore CS1591
///<summary>Returns a graph object used for dynamic solved visualization </summary>
///<param name="problemInstance" example="(({1,2,3,4,5},{{2,1},{1,3},{2,3},{3,5},{2,4},{4,5}}),5)">CliqueCover problem instance string.</param>
///<param name="solution" example="{{4,5},{1,2,3}}">CliqueCover instance string.</param>

///<response code="200">Returns graph object</response>

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("solvedVisualization")]
    #pragma warning disable CS1591
    public String getSolvedVisualization([FromQuery]string problemInstance,string solution) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        CLIQUECOVER sClique = new CLIQUECOVER(problemInstance);
        List<string> solutionList = solution.Replace("{{","").Replace("}}","").Split("},{").ToList();
        API_UndirectedGraphJSON apiGraph = new API_UndirectedGraphJSON(sClique.nodes,sClique.edges);
        for(int i=0;i<apiGraph.nodes.Count;i++){
            int number = 0;
            apiGraph.nodes[i].attribute1 = i.ToString();
            foreach(var j in solutionList) {

            if(j.Split(',').Contains(apiGraph.nodes[i].name)){ //we set the nodes as either having a true or false flag which will indicate to the frontend whether to highlight.
                apiGraph.nodes[i].attribute2 = number.ToString(); 
            }

            number += 1;
            number = number % 8;    
        
            }
        }

        for (int i = 0; i < apiGraph.links.Count; i++)
        {
            int number = 0;
            foreach (var j in solutionList)
            {

                foreach (var source in j.Split(','))
                {
                    foreach (var target in j.Split(','))
                    {
                        if (apiGraph.links[i].source == source && apiGraph.links[i].target == target)
                        {
                            apiGraph.links[i].attribute1 = number.ToString();
                        }
                    }
                }

                number += 1;
                number = number % 8;
            }
        }
    
        string jsonString = JsonSerializer.Serialize(apiGraph, options);
        return jsonString;
        // Console.WriteLine(sClique.clusterNodes.Count.ToString());
        
    }
}
