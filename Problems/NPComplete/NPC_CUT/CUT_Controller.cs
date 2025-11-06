// using Microsoft.AspNetCore.Mvc;
// using System.Text.Json;
// using System.Text.Json.Serialization;
// using API.Interfaces.JSON_Objects.Graphs;
// using API.Interfaces.Graphs.GraphParser;
// using API.Problems.NPComplete.NPC_CUT;
// using API.Problems.NPComplete.NPC_CUT.Solvers;
// using API.Problems.NPComplete.NPC_CUT.Verifiers;


// namespace API.Problems.NPComplete.NPC_CUT;

// [ApiController]
// [Route("[controller]")]
// [Tags("Cut")]

// #pragma warning disable CS1591
// public class CUTGenericController : ControllerBase {
// #pragma warning restore CS1591
// ///<summary>Returns a graph object used for dynamic solved visualization </summary>
// ///<param name="problemInstance" example="{{1,2,3,4,5},{{2,1},{1,3},{2,3},{3,5},{2,4},{4,5}},5}">Cut problem instance string.</param>
// ///<param name="solution" example="{{2,1},{2,3},{2,4},{5,3},{5,4}}">Cut instance string.</param>

// ///<response code="200">Returns graph object</response>

//     [ApiExplorerSettings(IgnoreApi = true)]
//     [HttpGet("solvedVisualization")]
//     #pragma warning disable CS1591
//     public String solvedVisualization([FromQuery]string problemInstance, string solution){
//     #pragma warning restore CS1591
//         var options = new JsonSerializerOptions { WriteIndented = true };
//         CUT aSet = new CUT(problemInstance);
//         CutGraph aGraph = aSet.cutAsGraph;
//         Dictionary<KeyValuePair<string,string>, bool> solutionDict = aSet.defaultSolver.getSolutionDict(problemInstance, solution);
//         API_UndirectedGraphJSON apiGraph = new API_UndirectedGraphJSON(aGraph.getNodeList,aGraph.getEdgeList);

//         for(int i=0;i<apiGraph.links.Count;i++){
//             bool edgeVal = false;
//             KeyValuePair<string, string> edge = new KeyValuePair<string, string>(apiGraph.links[i].source, apiGraph.links[i].target);
//             solutionDict.TryGetValue(edge, out edgeVal);
//             apiGraph.links[i].attribute1 = edgeVal.ToString();
//         }

//         List<string> parsedS = solution.TrimEnd().TrimStart().Replace("{","").Replace("}","").Split(',').ToList();

//         for (int i = 0; i < apiGraph.nodes.Count; i++) {
//             apiGraph.nodes[i].attribute1 = i.ToString();
//             if(parsedS.IndexOf(apiGraph.nodes[i].name) % 2 == 0) {
//                 apiGraph.nodes[i].attribute2 = true.ToString();
//             }
//         }

//         string jsonString = JsonSerializer.Serialize(apiGraph, options);
//         return jsonString;

//     }
// }
