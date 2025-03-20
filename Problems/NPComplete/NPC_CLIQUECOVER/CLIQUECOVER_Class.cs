using API.Interfaces;
using API.Problems.NPComplete.NPC_CLIQUECOVER.Solvers;
using API.Problems.NPComplete.NPC_CLIQUECOVER.Verifiers;

namespace API.Problems.NPComplete.NPC_CLIQUECOVER;

class CLIQUECOVER : IGraphProblem<CliqueCoverBruteForce,CliqueCoverVerifier,CliqueCoverGraph> {

    // --- Fields ---
    public string problemName {get;} = "Clique Cover";
    public string formalDefinition {get;} = "Clique Cover = {<G, k> | G is a graph represnted by k or fewer cliques}";
    public string problemDefinition {get;} = "A clique cover is a partition of the vertices into cliques, subsets of vertices within which every two vertices are adjacent";
    public string source {get;} = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    public string defaultInstance {get;} = "(({1,2,3,4,5,6,7,8},{{2,1},{1,3},{2,3},{3,5},{2,4},{4,5},{6,7},{7,8},{6,8}}),3)";
    public string instance {get;set;} = string.Empty;

    public string wikiName {get;} = "";
    private List<string> _nodes = new List<string>();
    private List<KeyValuePair<string, string>> _edges = new List<KeyValuePair<string, string>>();
    private int _K = 3;
    public CliqueCoverBruteForce defaultSolver {get;} = new CliqueCoverBruteForce();
    public CliqueCoverVerifier defaultVerifier {get;} = new CliqueCoverVerifier();
    private CliqueCoverGraph _cliqueCoverAsGraph;
    public CliqueCoverGraph graph {get => _cliqueCoverAsGraph;}
    public string[] contributors {get;} = { "Andrija Sevaljevic" };

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

    public CliqueCoverGraph cliqueCoverAsGraph {
        get{
            return _cliqueCoverAsGraph;
        }
        set{
            _cliqueCoverAsGraph = value;
        }
    }

    // --- Methods Including Constructors ---
    public CLIQUECOVER() {
        instance = defaultInstance;
        _cliqueCoverAsGraph = new CliqueCoverGraph(instance,true);
        nodes = _cliqueCoverAsGraph.nodesStringList;
        edges = _cliqueCoverAsGraph.edgesKVP;
         _K = _cliqueCoverAsGraph.K;


    }
    public CLIQUECOVER(string GInput) {
        _cliqueCoverAsGraph = new CliqueCoverGraph(GInput, true);
        nodes = _cliqueCoverAsGraph.nodesStringList;
        edges = _cliqueCoverAsGraph.edgesKVP;
        _K = _cliqueCoverAsGraph.K;
        instance = _cliqueCoverAsGraph.ToString();


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