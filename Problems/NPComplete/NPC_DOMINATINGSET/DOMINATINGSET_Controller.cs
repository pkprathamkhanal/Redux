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

    [ProducesResponseType(typeof(DOMINATINGSET), 200)]
    [HttpGet]
    public string getDefault()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(new DOMINATINGSET(), options);
    }

    [ProducesResponseType(typeof(DOMINATINGSET), 200)]
    [HttpPost("instance")]
    public string getInstance([FromBody] string problemInstance)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(new DOMINATINGSET(problemInstance), options);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("visualize")]
    public string getVisualization([FromQuery] string problemInstance)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        DOMINATINGSET dominatingSet = new DOMINATINGSET(problemInstance);
        DominatingSetGraph dsGraph = dominatingSet.dominatingSetAsGraph;
        API_UndirectedGraphJSON apiGraph = new API_UndirectedGraphJSON(dsGraph.getNodeList, dsGraph.getEdgeList);

        for (int i = 0; i < apiGraph.nodes.Count; i++)
        {
            apiGraph.nodes[i].attribute1 = i.ToString();
            apiGraph.nodes[i].attribute2 = bool.FalseString;
        }

        return JsonSerializer.Serialize(apiGraph, options);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("solvedVisualization")]
    public string getSolvedVisualization([FromQuery] string problemInstance, string solution)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        DOMINATINGSET dominatingSet = new DOMINATINGSET(problemInstance);
        DominatingSetGraph dsGraph = dominatingSet.dominatingSetAsGraph;
        API_UndirectedGraphJSON apiGraph = new API_UndirectedGraphJSON(dsGraph.getNodeList, dsGraph.getEdgeList);

        List<string> solutionNodes = GraphParser.parseNodeListWithStringFunctions(solution);
        for (int i = 0; i < apiGraph.nodes.Count; i++)
        {
            apiGraph.nodes[i].attribute1 = i.ToString();
            apiGraph.nodes[i].attribute2 = solutionNodes.Contains(apiGraph.nodes[i].name)
                ? bool.TrueString
                : bool.FalseString;
        }

        return JsonSerializer.Serialize(apiGraph, options);
    }
}

[ApiController]
[Route("[controller]")]
[Tags("Dominating Set")]
#pragma warning disable CS1591
public class DominatingSetVerifierController : ControllerBase
{
#pragma warning restore CS1591

    [ProducesResponseType(typeof(DominatingSetVerifier), 200)]
    [HttpGet("info")]
    public string getGeneric()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(new DominatingSetVerifier(), options);
    }

    [ProducesResponseType(typeof(bool), 200)]
    [HttpPost("verify")]
    public string verifyInstance([FromBody] Tools.ApiParameters.Verify verify)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        DOMINATINGSET problem = new DOMINATINGSET(verify.ProblemInstance);
        DominatingSetVerifier verifier = new DominatingSetVerifier();
        bool response = verifier.verify(problem, verify.Certificate);
        return JsonSerializer.Serialize(response, options);
    }
}

[ApiController]
[Route("[controller]")]
[Tags("Dominating Set")]
#pragma warning disable CS1591
public class DominatingSetSolverController : ControllerBase
{
#pragma warning restore CS1591

    [ProducesResponseType(typeof(DominatingSetSolver), 200)]
    [HttpGet("info")]
    public string getGeneric()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(new DominatingSetSolver(), options);
    }

    [ProducesResponseType(typeof(string), 200)]
    [HttpPost("solve")]
    public string solveInstance([FromBody] string problemInstance)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        DOMINATINGSET problem = new DOMINATINGSET(problemInstance);
        string solution = problem.defaultSolver.solve(problem);
        return JsonSerializer.Serialize(solution, options);
    }
}
