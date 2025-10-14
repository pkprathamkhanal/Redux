using Microsoft.AspNetCore.Mvc;

namespace API.Problems.NPComplete.NPC_JOBSEQ;

[ApiController]
[Route("[controller]")]
[Tags("Job Sequencing")]
#pragma warning disable CS1591
public class JOBSEQGenericController : ControllerBase {
#pragma warning restore CS1591
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("solvedVisualization")]
    public String getSolvedVisualization([FromQuery]string problemInstance,string solution) {
        throw new NotImplementedException();
       // List<string> solutionList = GraphParser.parseNodeListWithStringFunctions(solution); //Note, this is just a convenience string to list function.
       // JOBSEQ independentSet = new JOBSEQ(problemInstance);
       // IndependentSetGraph cGraph = independentSet.independentSetAsGraph;
       // API_UndirectedGraphJSON apiGraph = new API_UndirectedGraphJSON(cGraph.getNodeList,cGraph.getEdgeList);
       // for(int i=0;i<apiGraph.nodes.Count;i++){
       //     apiGraph.nodes[i].attribute1 = i.ToString();
       //     if(solutionList.Contains(apiGraph.nodes[i].name)){ //we set the nodes as either having a true or false flag which will indicate to the frontend whether to highlight.
       //         apiGraph.nodes[i].attribute2 = true.ToString(); 
       //     }
       //     else{apiGraph.nodes[i].attribute2 = false.ToString();}
       // }
       // string jsonString = JsonSerializer.Serialize(apiGraph, options);
       // return jsonString;
    }
}
