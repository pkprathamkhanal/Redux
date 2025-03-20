using API.Interfaces;
using API.Problems.NPComplete.NPC_HAMILTONIAN.Solvers;
using API.Problems.NPComplete.NPC_HAMILTONIAN.Verifiers;

namespace API.Problems.NPComplete.NPC_HAMILTONIAN;

class HAMILTONIAN : IGraphProblem<HamiltonianBruteForce,HamiltonianVerifier,HamiltonianGraph> {

    // --- Fields ---
    public string problemName {get;} = "Hamiltonian";
    public string formalDefinition {get;} = "Hamiltonian = {<G> | G has a cycle which covers every node exactly once}";
    public string problemDefinition {get;} = "Hamiltonian is the problem of determining whether a Hamiltonian cycle (a path in an undirected or directed graph that visits each vertex exactly once).";
    public string source {get;} = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    public string defaultInstance {get;} = "({1,2,3,4,5},{{2,1},{1,3},{2,3},{3,5},{2,4},{4,5}})";
    public string instance {get;set;} = string.Empty;

    public string wikiName {get;} = "";
    private List<string> _nodes = new List<string>();
    private List<KeyValuePair<string, string>> _edges = new List<KeyValuePair<string, string>>();
    public HamiltonianBruteForce defaultSolver {get;} = new HamiltonianBruteForce();
    public HamiltonianVerifier defaultVerifier {get;} = new HamiltonianVerifier();
    private HamiltonianGraph _hamiltonianAsGraph;
    public HamiltonianGraph graph {get => _hamiltonianAsGraph;}
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

    public HamiltonianGraph hamiltonianAsGraph {
        get{
            return _hamiltonianAsGraph;
        }
        set{
            _hamiltonianAsGraph = value;
        }
    }

    // --- Methods Including Constructors ---
    public HAMILTONIAN() {
        instance = defaultInstance;
        _hamiltonianAsGraph = new HamiltonianGraph(instance,true);
        nodes = _hamiltonianAsGraph.nodesStringList;
        edges = _hamiltonianAsGraph.edgesKVP;

    }
    public HAMILTONIAN(string GInput) {
        _hamiltonianAsGraph = new HamiltonianGraph(GInput, true);
        nodes = _hamiltonianAsGraph.nodesStringList;
        edges = _hamiltonianAsGraph.edgesKVP;
        instance = _hamiltonianAsGraph.ToString();

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




}