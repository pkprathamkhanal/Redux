using API.Interfaces;
using API.Problems.NPComplete.NPC_NODESET.Solvers;
using API.Problems.NPComplete.NPC_NODESET.Verifiers;
using API.Problems.NPComplete.NPC_NODESET.Visualizations;
using SPADE;

namespace API.Problems.NPComplete.NPC_NODESET;

class NODESET : IGraphProblem<NodeSetBruteForce, NodeSetVerifier, NodeSetDefaultVisualization, UtilCollectionGraph>
{

    // --- Fields ---
    public string problemName { get; } = "Feedback Node Set";
    public string formalDefinition { get; } = "Feedback Node Set = ";
    public string problemDefinition { get; } = "Node Set is the problem";
    public string source { get; } = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private static string _defaultInstance = "(({1,2,3,4},{(1,2),(2,4),(3,2),(4,1),(4,3)}),1)";
    public string defaultInstance { get; } = _defaultInstance;
    public string instance { get; set; } = string.Empty;
    private int _K;

    public string wikiName { get; } = "";
    private List<string> _nodes = new List<string>();
    private List<KeyValuePair<string, string>> _edges = new List<KeyValuePair<string, string>>();
    public NodeSetBruteForce defaultSolver { get; } = new NodeSetBruteForce();
    public NodeSetVerifier defaultVerifier { get; } = new NodeSetVerifier();
    public NodeSetDefaultVisualization defaultVisualization { get; } = new NodeSetDefaultVisualization();
    public UtilCollectionGraph graph { get; set; }
    public string[] contributors { get; } = { "Andrija Sevaljevic" };

    // --- Properties ---
    public int K
    {
        get
        {
            return _K;
        }
        set
        {
            _K = value;
        }
    }
    public List<string> nodes
    {
        get
        {
            return _nodes;
        }
        set
        {
            _nodes = value;
        }
    }
    public List<KeyValuePair<string, string>> edges
    {
        get
        {
            return _edges;
        }
        set
        {
            _edges = value;
        }
    }

    // --- Methods Including Constructors ---
    public NODESET() : this(_defaultInstance)
    {

    }
    public NODESET(string GInput)
    {
        instance = GInput;

        StringParser nodeSet = new("{((N,E),K) | N is set, E subset N unorderedcross N, K is int}");
        nodeSet.parse(GInput);
        nodes = nodeSet["N"].ToList().Select(node => node.ToString()).ToList();
        edges = nodeSet["E"].ToList().Select(edge =>
        {
            List<UtilCollection> cast = edge.ToList();
            return new KeyValuePair<string, string>(cast[0].ToString(), cast[1].ToString());
        }).ToList();
        _K = int.Parse(nodeSet["K"].ToString());

        graph = new UtilCollectionGraph(nodeSet["N"], nodeSet["E"]);
    }


    public List<string> getNodes(string Ginput)
    {

        List<string> allGNodes = new List<string>();
        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")", "");

        // [0] is nodes,  [1] is edges,  [2] is k.
        string[] Gsections = strippedInput.Split(':');
        string[] Gnodes = Gsections[0].Split(',');

        foreach (string node in Gnodes)
        {
            allGNodes.Add(node);
        }

        return allGNodes;
    }
    public List<KeyValuePair<string, string>> getEdges(string Ginput)
    {

        List<KeyValuePair<string, string>> allGEdges = new List<KeyValuePair<string, string>>();

        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")", "");

        string[] Gsections = strippedInput.Split(':');
        string[] Gedges = Gsections[1].Split('&');

        foreach (string edge in Gedges)
        {
            string[] fromTo = edge.Split(',');
            string nodeFrom = fromTo[0];
            string nodeTo = fromTo[1];

            KeyValuePair<string, string> fullEdge = new KeyValuePair<string, string>(nodeFrom, nodeTo);
            allGEdges.Add(fullEdge);
        }

        return allGEdges;
    }
}