using System.Text.Json;
using API.Interfaces.Graphs;
using API.Interfaces.JSON_Objects.Graphs;

namespace API.Interfaces;

abstract class Graph
{
    public abstract List<Node> nodes { get; }
    public abstract List<Edge> edges { get; }

    public List<Node> getNodeList { get => nodes; }
    public List<Edge> getEdgeList { get => edges; }

    public virtual API_UndirectedGraphJSON ToAPIGraph(string filename)
    {
        return new API_UndirectedGraphJSON(nodes, edges, filename);
    }
}
