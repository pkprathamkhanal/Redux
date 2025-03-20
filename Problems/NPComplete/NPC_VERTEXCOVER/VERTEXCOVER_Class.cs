using API.Interfaces;
using API.Problems.NPComplete.NPC_VERTEXCOVER.Solvers;
using API.Problems.NPComplete.NPC_VERTEXCOVER.Verifiers;
using API.Interfaces.Graphs;

namespace API.Problems.NPComplete.NPC_VERTEXCOVER;

class VERTEXCOVER : IGraphProblem<VertexCoverBruteForce,VCVerifier,VertexCoverGraph> {

    // --- Fields ---
    public string problemName {get;} = "Vertex Cover";
    public string formalDefinition {get;} = "VERTEXCOVER = {<G, k> | G in an undirected graph that has a k-node vertex cover}";
    public string problemDefinition {get;} = "A vertex cover is a subset of nodes S, such that every edge in the graph, G, touches a node in S.";
    public string source {get;} = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    //public string defaultInstance {get;} = "{{a,b,c,d,e,f,g} : {{a,b} & {a,c} & {c,d} & {c,e} & {d,f} & {e,f} & {e,g}} : 3}";
    public string defaultInstance {get;} = "(({a,b,c,d,e},{{a,b},{a,c},{a,e},{b,e},{c,d}}),3)";
    public string instance {get;set;} = string.Empty;
    private List<string> _nodes = new List<string>();
    private List<KeyValuePair<string, string>> _edges = new List<KeyValuePair<string, string>>();
    private int _K = 3;
    public string wikiName {get;} = "";
    public VertexCoverBruteForce defaultSolver {get;} = new VertexCoverBruteForce();
    public VCVerifier defaultVerifier {get;} = new VCVerifier();

    private VertexCoverGraph _VCAsGraph;
    public VertexCoverGraph graph {get => _VCAsGraph;}
    private string _vertexCover = string.Empty;

    public string[] contributors {get;} = { "Janita Aamir", "Alex Diviney" };


    // --- Properties ---
    public List<string> nodes {
        get {
            return _nodes;
        }
        set {
            _nodes = value;
        }
    }
    public List<KeyValuePair<string, string>> edges {
        get {
            return _edges;
        }
        set {
            _edges = value;
        }
    }

    public int K {
        get {
            return _K;
        }
        set {
            _K = value;
        }
    }
    public VertexCoverGraph VCAsGraph{
        get{
            return _VCAsGraph;
        }
    }

    // --- Methods Including Constructors ---
    public VERTEXCOVER() {
        // string VCDefaultString = _defaultInstance;
        // _VCAsGraph = new UndirectedGraph();
        // _vertexCover = _vertexCover.ToString();
        instance = defaultInstance;
        _VCAsGraph = new VertexCoverGraph(instance,true);
        nodes = _VCAsGraph.nodesStringList;
        edges = _VCAsGraph.edgesKVP;
        K = _VCAsGraph.K;

    }
    public VERTEXCOVER(string instanceInput) {
        // _VCAsGraph = new UndirectedGraph(GkInput);
        // _vertexCover = _VCAsGraph.ToString();
        instance = instanceInput;
        _VCAsGraph = new VertexCoverGraph(instance,true);
        nodes = _VCAsGraph.nodesStringList;
        edges = _VCAsGraph.edgesKVP;
        K = _VCAsGraph.K;
    }

    public List<string> getNodes(string Ginput) {
        List<string> allGNodes = new List<string>();
        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");
        
        // [0] is nodes,  [1] is edges,  [2] is k.
        string[] Gsections = strippedInput.Split(':');
        string[] Gnodes = Gsections[0].Split(',');
        
        foreach(string node in Gnodes) {
            allGNodes.Add(node);
        }

        return allGNodes;
    }

    public List<KeyValuePair<string, string>> getEdges(string Ginput) {

        List<KeyValuePair<string, string>> allGEdges = new List<KeyValuePair<string, string>>();

        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");
        
        // [0] is nodes,  [1] is edges,  [2] is k.
        string[] Gsections = strippedInput.Split(':');
        string[] Gedges = Gsections[1].Split('&');
        
        foreach (string edge in Gedges) {
            string[] fromTo = edge.Split(',');
            string nodeFrom = fromTo[0];
            string nodeTo = fromTo[1];
            
            KeyValuePair<string,string> fullEdge = new KeyValuePair<string,string>(nodeFrom, nodeTo);
            allGEdges.Add(fullEdge);
        }

        return allGEdges;
    }

    public int getK(string Ginput) {
        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");
        
        // [0] is nodes,  [1] is edges,  [2] is k.
        string[] Gsections = strippedInput.Split(':');
        return Int32.Parse(Gsections[2]);
    }
}

