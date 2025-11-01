using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Interfaces.JSON_Objects.Graphs;
using API.Interfaces.Graphs.GraphParser;
using API.Problems.NPComplete.NPC_DIRHAMILTONIAN;
using API.Problems.NPComplete.NPC_DIRHAMILTONIAN.Solvers;
using API.Problems.NPComplete.NPC_DIRHAMILTONIAN.Verifiers;


namespace API.Problems.NPComplete.NPC_DIRHAMILTONIAN;

[ApiController]
[Route("[controller]")]
[Tags("Directed Hamiltonian")]

#pragma warning disable CS1591
public class DIRHAMILTONIANGenericController : ControllerBase
{
#pragma warning restore CS1591
///<summary>Returns a graph object used for dynamic solved visualization </summary>
///<param name="problemInstance" example="({1,2,3,4,5},{(2,1),(1,3),(2,3),(3,5),(4,2),(5,4)})">Directed Hamiltonian problem instance string.</param>
///<param name="solution" example="{1,3,5,4,2,1}">Directed Hamiltonian solution instance string.</param>

///<response code="200">Returns graph object</response>

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("solvedVisualization")]
    #pragma warning disable CS1591
    public String solvedVisualization([FromQuery]string problemInstance, string solution){
    #pragma warning restore CS1591
        var options = new JsonSerializerOptions { WriteIndented = true };
        DIRHAMILTONIAN aSet = new DIRHAMILTONIAN(problemInstance);
        DirectedHamiltonianGraph aGraph = aSet.directedHamiltonianAsGraph;
        API_UndirectedGraphJSON apiGraph = new API_UndirectedGraphJSON(aGraph.getNodeList,aGraph.getEdgeList);

        List<string> solutionList = solution.Replace("{", "").Replace("}", "").Split(",").ToList();
        int counter = 0;
        for(int j = 0; j < solutionList.Count - 1; j++)
        {
            for (int i = 0; i < apiGraph.nodes.Count; i++)
            {
                if (apiGraph.nodes[i].name == solutionList[j])
                {
                    apiGraph.nodes[i].attribute1 = i.ToString();
                    apiGraph.nodes[i].attribute2 = true.ToString();
                    apiGraph.nodes[i].attribute3 = (counter * 5000 / apiGraph.nodes.Count).ToString();
                }
            }
 
            counter++;

            for (int i = 0; i < apiGraph.links.Count; i++)
            {
                if (apiGraph.links[i].source == solutionList[j] && apiGraph.links[i].target == solutionList[j+1] )
                {
                    apiGraph.links[i].attribute1 = true.ToString();
                    apiGraph.links[i].attribute2 = (counter * 1000).ToString();
                }
            }
        }

        string jsonString = JsonSerializer.Serialize(apiGraph, options);
        return jsonString;

    }
}
