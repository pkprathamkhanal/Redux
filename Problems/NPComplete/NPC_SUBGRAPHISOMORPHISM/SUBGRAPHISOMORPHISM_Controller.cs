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

