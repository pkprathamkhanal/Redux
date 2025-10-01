using API.Interfaces;
using API.Problems.NPComplete.NPC_DIRHAMILTONIAN.Solvers;
using API.Problems.NPComplete.NPC_DIRHAMILTONIAN.Verifiers;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SPADE;

namespace API.Problems.NPComplete.NPC_DIRHAMILTONIAN;

class DIRHAMILTONIAN : IGraphProblem<DirectedHamiltonianBruteForce,DirectedHamiltonianVerifier,DirectedHamiltonianGraph> {

    // --- Fields ---
    public string problemName {get;} = "Directed Hamiltonian";
    public string formalDefinition {get;} = "Directed Hamiltonian = {<G> | G has a cycle which covers every node exactly once}";
    public string problemDefinition {get;} = "Directed Hamiltonian is the problem of determining whether a Hamiltonian cycle (a path in an undirected or directed graph that visits each vertex exactly once).";
    public string source {get;} = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private static string _defaultInstance = "({1,2,3,4,5},{(2,1),(1,3),(2,3),(3,5),(4,2),(5,4)})";
    public string defaultInstance { get; } = _defaultInstance;
    public string instance {get;set;} = string.Empty;

    public string wikiName {get;} = "";
    private List<string> _nodes = new List<string>();
    private List<KeyValuePair<string, string>> _edges = new List<KeyValuePair<string, string>>();
    public DirectedHamiltonianBruteForce defaultSolver {get;} = new DirectedHamiltonianBruteForce();
    public DirectedHamiltonianVerifier defaultVerifier {get;} = new DirectedHamiltonianVerifier();
    private DirectedHamiltonianGraph _directedHamiltonianAsGraph;
    public DirectedHamiltonianGraph graph {get => _directedHamiltonianAsGraph;}
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

    public DirectedHamiltonianGraph directedHamiltonianAsGraph {
        get{
            return _directedHamiltonianAsGraph;
        }
        set{
            _directedHamiltonianAsGraph = value;
        }
    }

    // --- Methods Including Constructors ---
    public DIRHAMILTONIAN() : this(_defaultInstance) {

    }
    public DIRHAMILTONIAN(string GInput)
    {
        instance = GInput;

        StringParser dirhamiltonianparser = new("{(N,E) | N is set, E subset N cross N}");
        dirhamiltonianparser.parse(GInput);
        nodes = dirhamiltonianparser["N"].ToList().Select(node => node.ToString()).ToList();
        edges = dirhamiltonianparser["E"].ToList().Select(edge =>
        {
            List<UtilCollection> cast = edge.ToList();
            return new KeyValuePair<string, string>(cast[0].ToString(), cast[1].ToString());
        }).ToList();

        _directedHamiltonianAsGraph = new DirectedHamiltonianGraph(nodes, edges);
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