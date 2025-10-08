using SPADE;
using API.Interfaces.JSON_Objects.Graphs;
using API.Interfaces.Graphs;

namespace API.Interfaces;

/// <summary>
/// Help class from graph operations, and for converting to API ready graph for visualizations on the front end
/// </summary>
/// Expects graphs in the following formats:
/// unweighted undirected: (N,E) where N is set of nodes, E is a set of edges represented as {a,b}
/// unweighted directed  : (N,E) where N is set of nodes, E is a set of edges represented as (a,b)
/// weighted undirected  : (N,E) where N is set of nodes, E is a set of edges represented as ({a,b}, w) where w is the weight (this needs to be looked at)
/// weighted directed    : (N,E) where N is set of nodes, E is a set of edges represented as ((a,b), w) where w is the weight
/// Child of Graph class to fix some typing issues while codebase is converted. Expected to be removed
class UtilCollectionGraph : Graph
{
    public UtilCollection Nodes;

    public UtilCollection Edges;

    bool IsDirected;
    bool IsWeighted;

    public UtilCollectionGraph(UtilCollection n, UtilCollection e, bool isDirected, bool isWeighted)
    {
        Nodes = n;
        Edges = e;

        IsDirected = isDirected;
        IsWeighted = isWeighted;
    }

    public override List<Node> nodes => null;

    public override List<Edge> edges => null;

    public override API_UndirectedGraphJSON ToAPIGraph()
    {
        //nodes are always the same
        List<string> nodes = Nodes.ToList().Select(node => node.ToString()).ToList(); 
        //edges need special handling based on case
        List<KeyValuePair<string, string>> edges;
        List<UtilCollection> EdgeList = Edges.ToList();

        API_UndirectedGraphJSON graph;

        if (IsDirected)
        {
            if (IsWeighted)
            {
                edges = EdgeList.Select(edge =>
                {
                    return new KeyValuePair<string, string>(edge[0][0].ToString(), edge[0][1].ToString());
                }).ToList();

                graph = new API_UndirectedGraphJSON(nodes, edges);

                for (int i = 0; i < graph.links.Count; i++)
                {
                    graph.links[i].weight = EdgeList[i][1].ToString();
                }

            }
            else 
            {
                edges = EdgeList.Select(edge =>
                {
                    return new KeyValuePair<string, string>(edge[0].ToString(), edge[1].ToString());
                }).ToList();

                graph = new API_UndirectedGraphJSON(nodes, edges);
            }
        }
        else
        {
            if (IsWeighted)
            {
                edges = EdgeList.Select(edge =>
                {
                    List<UtilCollection> cast = edge[0].ToList();
                    return new KeyValuePair<string, string>(cast[0].ToString(), cast[1].ToString());
                }).ToList();

                graph = new API_UndirectedGraphJSON(nodes, edges);

                for (int i = 0; i < graph.links.Count; i++)
                {
                    graph.links[i].weight = EdgeList[i][1].ToString();
                }

            }
            else //default case
            {

                edges = EdgeList.Select(edge =>
                {
                    List<UtilCollection> cast = edge.ToList();
                    return new KeyValuePair<string, string>(cast[0].ToString(), cast[1].ToString());
                }).ToList();

                graph = new API_UndirectedGraphJSON(nodes, edges);
            }
        }
        return graph;
    }

}