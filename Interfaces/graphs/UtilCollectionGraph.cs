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
/// weighted undirected  : (N,E) where N is set of nodes, E is a set of edges represented as {a,b, w} where w is the weight (this needs to be looked at)
/// weighted directed    : (N,E) where N is set of nodes, E is a set of edges represented as (a,b, w) where w is the weight
class UtilCollectionGraph
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

    public API_UndirectedGraphJSON toAPIGraph()
    {
        if (IsDirected)
        {
            if (IsWeighted) //same as default case, but also needs to keep track of weights
            {
                List<string> nodes = Nodes.ToList().Select(node => node.ToString()).ToList();
                List<UtilCollection> EdgeList = Edges.ToList();
                List<KeyValuePair<string, string>> edges = EdgeList.Select(edge =>
                {
                    List<UtilCollection> cast = edge.ToList();
                    return new KeyValuePair<string, string>(cast[0].ToString(), cast[1].ToString());
                }).ToList();
                API_UndirectedGraphJSON graph = new API_UndirectedGraphJSON(nodes, edges);

                for (int i = 0; i < graph.links.Count; i++)
                {
                    graph.links[i].weight = EdgeList[i][2].ToString();
                }

                return graph;
            }
            else // same as default case
            {
                List<string> nodes = Nodes.ToList().Select(node => node.ToString()).ToList();
                List<KeyValuePair<string, string>> edges = Edges.ToList().Select(edge =>
                {
                    List<UtilCollection> cast = edge.ToList();
                    return new KeyValuePair<string, string>(cast[0].ToString(), cast[1].ToString());
                }).ToList();

            }
        }
        else
        {
            if (IsWeighted)
            {

            }
            else //default case
            {

                List<string> nodes = Nodes.ToList().Select(node => node.ToString()).ToList();
                List<KeyValuePair<string, string>> edges = Edges.ToList().Select(edge =>
                {
                    List<UtilCollection> cast = edge.ToList();
                    return new KeyValuePair<string, string>(cast[0].ToString(), cast[1].ToString());
                }).ToList();
                return new API_UndirectedGraphJSON(nodes, edges);
            }
        }
        return new API_UndirectedGraphJSON();
    }

}