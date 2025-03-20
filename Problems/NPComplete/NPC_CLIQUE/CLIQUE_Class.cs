using API.Interfaces;
using API.Problems.NPComplete.NPC_CLIQUE.Solvers;
using API.Problems.NPComplete.NPC_CLIQUE.Verifiers;

namespace API.Problems.NPComplete.NPC_CLIQUE;

class CLIQUE : IGraphProblem<CliqueBruteForce,CliqueVerifier,CliqueGraph> {

    // --- Fields ---
    public string problemName {get;} = "Clique";
    public string formalDefinition {get;} = "Clique = {<G, k> | G is an graph that has a set of k mutually adjacent nodes}";
    public string problemDefinition {get;} = "A clique is the problem of uncovering a subset of vertices in an undirected graph G = (V, E) such that every two distinct vertices are adjacent";
    public string source {get;} = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    public string defaultInstance {get;} = "(({1,2,3,4},{{4,1},{1,2},{4,3},{3,2},{2,4}}),3)";
    public string instance {get;set;} = string.Empty;
    public string wikiName {get;} = "";
    private List<string> _nodes = new List<string>();
    private List<KeyValuePair<string, string>> _edges = new List<KeyValuePair<string, string>>();
    private int _K ;
    public CliqueBruteForce defaultSolver {get;} = new CliqueBruteForce();
    public CliqueVerifier defaultVerifier {get;} = new CliqueVerifier();
    private CliqueGraph _cliqueAsGraph;
    public CliqueGraph graph {get => _cliqueAsGraph;}
    public string[] contributors {get;} = { "Kaden Marchetti", "Alex Diviney" };

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

    public CliqueGraph cliqueAsGraph {
        get{
            return _cliqueAsGraph;
        }
        set{
            _cliqueAsGraph = value;
        }
    }

    // --- Methods Including Constructors ---
    public CLIQUE() {
        instance = defaultInstance;
        _cliqueAsGraph = new CliqueGraph(instance,true);
        nodes = _cliqueAsGraph.nodesStringList;
        edges = _cliqueAsGraph.edgesKVP;
         _K = _cliqueAsGraph.K;
    }
    public CLIQUE(string GInput) {
        _cliqueAsGraph = new CliqueGraph(GInput, true);
        nodes = _cliqueAsGraph.nodesStringList;
        edges = _cliqueAsGraph.edgesKVP;
        _K = _cliqueAsGraph.K;
        instance = _cliqueAsGraph.ToString();
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