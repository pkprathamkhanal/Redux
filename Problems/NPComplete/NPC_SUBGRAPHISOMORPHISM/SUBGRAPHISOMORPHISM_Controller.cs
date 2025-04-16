using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_SUBGRAPHISOMORPHISM;
using API.Problems.NPComplete.NPC_SUBGRAPHISOMORPHISM.Verifiers;
using API.Problems.NPComplete.NPC_SUBGRAPHISOMORPHISM.Solvers;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Interfaces.JSON_Objects.Graphs;
using API.Interfaces.Graphs.GraphParser;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Runtime.InteropServices;
using API.Interfaces.Graphs;
using API.Problems.NPComplete.NPC_CLIQUE.ReduceTo.NPC_SubgraphIsomorphism;
using API.Problems.NPComplete.NPC_CLIQUE;
using Newtonsoft.Json.Linq;

namespace API.Problems.NPComplete.NPC_SUBGRAPHISOMORPHISM;

[ApiController]
[Route("[controller]")]
[Tags("SubgraphIsomorphism")]
#pragma warning disable CS1591
public class SUBGRAPHISOMORPHISMGenericController : ControllerBase
{
#pragma warning restore CS1591

    ///<summary>Returns a default Subgraph isomorphism object.</summary>
    [ProducesResponseType(typeof(SUBGRAPHISOMORPHISM), 200)]
    [HttpGet]
    public String getDefault()
    {

        // TODO
        // Obtain target graph, pattern graph and k value from the problemInstance

        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SUBGRAPHISOMORPHISM(), options);
        return jsonString;
    }

    ///<summary>Returns a Subgraph isomorphism object created from a given instance.</summary>
    ///<param name="problemInstance" example="TODO">Subgraph isomorphism problem instance string.</param>
    ///<response code="200">Returns a SUBGRAPHISOMORPHISM problem object.</response>
    [ProducesResponseType(typeof(SUBGRAPHISOMORPHISM), 200)]
    [HttpGet("instance")]
    public String getInstance([FromQuery] string problemInstance)
    {

        // TODO
        // Obtain target graph, pattern graph and k value from the problemInstance

        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SUBGRAPHISOMORPHISM(problemInstance), options);
        return jsonString;
    }

    // TODO: implement visualize routes if Subgraph isomorphism is a graphing problem
    ///<summary>Returns a graph object used for dynamic visualization.</summary>
    ///<param name="problemInstance" example="TODO">Subgraph isomorphism problem instance string.</param>
    ///<response code="200">Returns a graph object.</response>
    ///
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("visualize")]
    public String getVisualization([FromQuery] string problemInstance)
    {
        // TODO
        // Obtain target graph, pattern graph and k value from the problemInstance
        SUBGRAPHISOMORPHISM subgraphIsomorphism = new SUBGRAPHISOMORPHISM(problemInstance);

        // For Target Graph
        // SubgraphIsomorphismGraph targetGraph = subgraphIsomorphism.targetGraphAsGraph;
        // API_UndirectedGraphJSON apiGraphT = new API_UndirectedGraphJSON(targetGraph.getNodeList, targetGraph.getEdgeList);
        API_UndirectedGraphJSON apiGraphT = new API_UndirectedGraphJSON(ListNodesToGraphNodes(subgraphIsomorphism.nodesT), ListEdgesToGraphEdges(subgraphIsomorphism.edgesT));


        // For Pattern Graph
        // SubgraphIsomorphismGraph patternGraph = subgraphIsomorphism.patternGraphAsGraph;
        // API_UndirectedGraphJSON apiGraphP = new API_UndirectedGraphJSON(patternGraph.getNodeList, patternGraph.getEdgeList);
        API_UndirectedGraphJSON apiGraphP = new API_UndirectedGraphJSON(ListNodesToGraphNodes(subgraphIsomorphism.nodesP), ListEdgesToGraphEdges(subgraphIsomorphism.edgesP));


        string jsonString = mergeGraphs(apiGraphT, apiGraphP);
        return jsonString;
    }

    // Helper Function

    private string mergeGraphs(API_UndirectedGraphJSON targetGraph, API_UndirectedGraphJSON patternGraph)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        // Merge nodes ensuring uniqueness
        var mergedNodes = targetGraph._nodes
            .Concat(patternGraph._nodes)
            .GroupBy(n => n.name) // Ensuring unique nodes by name
            .Select(g => g.First())
            .ToList();

        // Merge edges ensuring uniqueness
        var mergedEdges = targetGraph._links
            .Concat(patternGraph._links)
            .GroupBy(e => $"{e.source}-{e.target}") // Ensuring unique edges
            .Select(g => g.First())
            .ToList();

        // Create merged graph
        API_UndirectedGraphJSON mergedGraph = new API_UndirectedGraphJSON
        {
            _nodes = mergedNodes,
            _links = mergedEdges
        };
        string jsonString = JsonSerializer.Serialize(mergedGraph, options);
        return jsonString;
    }
    ///<summary>Returns a graph object used for dynamic solved visualization.</summary>
    ///<param name="problemInstance" example="TODO">Subgraph isomorphism problem instance string.</param>
    ///<param name="solution" example="TODO">Subgraph isomorphism instance string.</param>
    ///<response code="200">Returns a graph object.</response>
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("solvedVisualization")]
    public String getSolvedVisualization([FromQuery] string problemInstance, string solution)
    {
        SUBGRAPHISOMORPHISM subgraphIsomorphism = new SUBGRAPHISOMORPHISM(problemInstance);
        // For Target Graph
        // SubgraphIsomorphismGraph targetGraph = subgraphIsomorphism.targetGraphAsGraph;
        // API_UndirectedGraphJSON apiGraphT = new API_UndirectedGraphJSON(targetGraph.getNodeList, targetGraph.getEdgeList);
        API_UndirectedGraphJSON apiGraphT = new API_UndirectedGraphJSON(ListNodesToGraphNodes(subgraphIsomorphism.nodesT), ListEdgesToGraphEdges(subgraphIsomorphism.edgesT));


        // For Pattern Graph
        // SubgraphIsomorphismGraph patternGraph = subgraphIsomorphism.patternGraphAsGraph;
        API_UndirectedGraphJSON apiGraphP = new API_UndirectedGraphJSON(ListNodesToGraphNodes(subgraphIsomorphism.nodesP), ListEdgesToGraphEdges(subgraphIsomorphism.edgesP));

        // // TODO: implement body of Subgraph isomorphism "solvedVisualization" route

        string jsonString = visualizeSolution(apiGraphT, apiGraphP, solution);
        return jsonString;
    }

    private List<Node> ListNodesToGraphNodes(List<string> nodes)
    {
        List<Node> nodeList = nodes.Select(name => new Node(name)).ToList();
        return nodeList;
    }

    private List<Edge> ListEdgesToGraphEdges(List<KeyValuePair<string, string>> edges)
    {
        List<Edge> edgeList = edges.Select(p => new Edge(new Node(p.Key), new Node(p.Value))).ToList();

        return edgeList;
    }
    private string visualizeSolution(API_UndirectedGraphJSON targetGraph, API_UndirectedGraphJSON patternGraph, string solution)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        if (!string.IsNullOrEmpty(solution))
        {
            Dictionary<string, string> solution_dict = JsonSerializer.Deserialize<Dictionary<string, string>>(solution);
            int number = 0;

            // for pattern graph 
            for (int i = 0; i < patternGraph.nodes.Count; i++)
            {
                patternGraph.nodes[i].attribute1 = i.ToString();
                if (solution_dict.ContainsKey(patternGraph.nodes[i].name.ToString()))
                {
                    patternGraph.nodes[i].attribute3 = solution_dict[patternGraph.nodes[i].name.ToString()];
                    patternGraph.nodes[i].attribute2 = number.ToString();
                    number += 1;
                }
            }
            // for target graph
            for (int i = 0; i < targetGraph.nodes.Count; i++)
            {
                targetGraph.nodes[i].attribute1 = i.ToString();
                if (solution_dict.ContainsValue(targetGraph.nodes[i].name.ToString()))
                {
                    string patternNode = solution_dict.FirstOrDefault(x => x.Value == targetGraph.nodes[i].name.ToString()).Key;
                    targetGraph.nodes[i].attribute3 = patternNode;
                    int nodeIndex = patternGraph.nodes.Select(n => n.name).ToList().IndexOf(patternNode);
                    targetGraph.nodes[i].attribute2 = patternGraph.nodes[nodeIndex].attribute2;
                }
            }

            for (int i = 0; i < patternGraph.links.Count; i++)
            {
                if (solution_dict.ContainsKey(patternGraph.links[i].source) && solution_dict.ContainsKey(patternGraph.links[i].target))
                {
                    patternGraph.links[i].attribute1 = true.ToString();
                }
            }

            for (int i = 0; i < targetGraph.links.Count; i++)
            {
                if (solution_dict.ContainsValue(targetGraph.links[i].source) && solution_dict.ContainsValue(targetGraph.links[i].target))
                {
                    targetGraph.links[i].attribute1 = true.ToString();
                }
            }

            string jsonString = mergeGraphs(targetGraph, patternGraph);
            return jsonString;
        }

        return "";
    }
}

[ApiController]
[Route("[controller]")]
[Tags("SubgraphIsomorphism")]
#pragma warning disable CS1591
public class SubgraphIsomorphismVerifierController : ControllerBase
{
#pragma warning restore CS1591

    ///<summary>Returns information about the Subgraph isomorphism generic verifier.</summary>
    ///<response code="200">Returns SubgraphIsomorphismVerifier object</response>
    [ProducesResponseType(typeof(SubgraphIsomorphismVerifier), 200)]
    [HttpGet("info")]
    public String getGeneric()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SubgraphIsomorphismVerifier verifier = new SubgraphIsomorphismVerifier();
        string jsonString = JsonSerializer.Serialize(verifier, options);
        return jsonString;
    }

    ///<summary>Verifies if a given certificate is a solution to a given Subgraph isomorphism.</summary>
    ///<param name="certificate" example="TODO">Certificate solution to the Subgraph isomorphism problem.</param>
    ///<param name="problemInstance" example="TODO">Subgraph isomorphism problem instance string.</param>
    ///<response code="200">Returns a boolean.</response>
    [ProducesResponseType(typeof(Boolean), 200)]
    [HttpGet("verify")]
    public String solveInstance([FromQuery] string certificate, [FromQuery] string problemInstance)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SUBGRAPHISOMORPHISM SUBGRAPHISOMORPHISM_Problem = new SUBGRAPHISOMORPHISM(problemInstance);
        SubgraphIsomorphismVerifier verifier = new SubgraphIsomorphismVerifier();
        bool response = verifier.verify(SUBGRAPHISOMORPHISM_Problem, certificate);
        // bool response = true;
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
[Tags("SubgraphIsomorphism")]
#pragma warning disable CS1591
public class SubgraphIsomorphismBruteForceController : ControllerBase
{
#pragma warning restore CS1591

    ///<summary>Returns information about the Subgraph isomorphism solver.</summary>
    ///<response code="200">Returns SubgraphIsomorphismSolver solver Object.</response>
    [ProducesResponseType(typeof(SubgraphIsomorphismBruteForce), 200)]
    [HttpGet("info")]
    public String getGeneric()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SubgraphIsomorphismBruteForce solver = new SubgraphIsomorphismBruteForce();
        string jsonString = JsonSerializer.Serialize(solver, options);
        return jsonString;
    }

    ///<summary>Returns a solution to a given Subgraph isomorphism problem instance.</summary>
    ///<param name="problemInstance" example="TODO">Subgraph isomorphism problem instance string.</param>
    ///<response code="200">Returns solution string.</response>
    [ProducesResponseType(typeof(string), 200)]
    [HttpGet("solve")]
    public String verifyInstance([FromQuery] string problemInstance)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SUBGRAPHISOMORPHISM problem = new SUBGRAPHISOMORPHISM(problemInstance);
        string solution = problem.defaultSolver.solve(problem);
        string jsonString = JsonSerializer.Serialize(solution, options);
        return jsonString;
    }
}


[ApiController]
[Route("[controller]")]
[Tags("SubgraphIsomorphism")]
#pragma warning disable CS1591
public class SubgraphIsomorphismUllmannController : ControllerBase
{
#pragma warning restore CS1591

    ///<summary>Returns information about the Subgraph isomorphism solver.</summary>
    ///<response code="200">Returns SubgraphIsomorphismSolver solver Object.</response>
    [ProducesResponseType(typeof(SubgraphIsomorphismUllmann), 200)]
    [HttpGet("info")]
    public String getGeneric()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SubgraphIsomorphismUllmann solver = new SubgraphIsomorphismUllmann();
        string jsonString = JsonSerializer.Serialize(solver, options);
        return jsonString;
    }

    ///<summary>Returns a solution to a given Subgraph isomorphism problem instance.</summary>
    ///<param name="problemInstance" example="TODO">Subgraph isomorphism problem instance string.</param>
    ///<response code="200">Returns solution string.</response>
    [ProducesResponseType(typeof(string), 200)]
    [HttpGet("solve")]
    public String verifyInstance([FromQuery] string problemInstance)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SUBGRAPHISOMORPHISM problem = new SUBGRAPHISOMORPHISM(problemInstance);
        string solution = problem.defaultSolver.solve(problem);
        string jsonString = JsonSerializer.Serialize(solution, options);
        return jsonString;
    }
}

