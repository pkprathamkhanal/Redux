using API.Interfaces;
using System.Text.Json;
using API.Interfaces.Graphs.GraphParser;
using API.Interfaces.JSON_Objects.Graphs;
using API.Interfaces.JSON_Objects;

namespace API.Problems.NPComplete.NPC_WEIGHTEDCUT.Visualizations;

class WeightedCutDefaultVisualization : IVisualization<WEIGHTEDCUT>
{

    // --- Fields ---
    public string visualizationName { get; } = "Weighted Cut Visualization";
    public string visualizationDefinition { get; } = "This is a default visualization for Cut";
    public string source { get; } = "";
    public string[] contributors { get; } = { "Andrija Sevaljevic" };
    public UtilCollectionGraph graph { get; set; }
    public string visualizationType { get; } = "Graph D3";

    // --- Methods Including Constructors ---
    public WeightedCutDefaultVisualization()
    {

    }
    public API_JSON visualize(WEIGHTEDCUT weightedCut)
    {
        return weightedCut.graph.ToAPIGraph();
    }

    public API_JSON SolvedVisualization(WEIGHTEDCUT weightedCut)
    {
        string solution = weightedCut.defaultSolver.solve(weightedCut);
        List<KeyValuePair<string, string>> solutionEdges = GraphParser.parseUndirectedEdgeListWithStringFunctions(solution);
        // removing duplicate edges since visualization cares about first edge only
        for (int i = solutionEdges.Count - 1; i >= 0; i--)
            if (i % 2 == 1) solutionEdges.RemoveAt(i);

        API_GraphJSON apiGraph = weightedCut.graph.ToAPIGraph();

        foreach (var edge in solutionEdges)
        {
            var link = apiGraph.links.FirstOrDefault(l =>
                (l.source == edge.Key && l.target == edge.Value) || (l.source == edge.Value && l.target == edge.Key)
            );

            var node = apiGraph.nodes.FirstOrDefault(n => n.name == edge.Key);

            if (link != null)
            {
                link.color = "Solution";
                link.dashed = "True";
            }

            if (node != null)
            {
                node.color = "Solution";
            }
        }

        foreach (var link in apiGraph.links)
        {
            var node1 = apiGraph.nodes.FirstOrDefault(n => n.name == link.source);
            var node2 = apiGraph.nodes.FirstOrDefault(n => n.name == link.target);
            if (node1 != null && node2 != null && node1.color == "Solution" && node2.color == "Solution")
                link.color = "Solution";
        }
        return apiGraph;
    }
}