using API.Interfaces;
using API.Problems.NPComplete.NPC_CUT.Solvers;
using API.Problems.NPComplete.NPC_CUT.Verifiers;
using SPADE;

namespace API.Problems.NPComplete.NPC_CUT;

class CUT : IGraphProblem<CutBruteForce, CutVerifier, CutGraph> {

    // --- Fields ---
    public string problemName {get;} = "Cut";
    public string formalDefinition {get;} = "Cut = {<G, k> | G is a graph with cut of size k}";
    public string problemDefinition {get;} = "A cut in an undirected graph is a partition of the graph's vertices into two complementary sets S and T, and the size of the cut is the number of edges between S and T.";
    public string[] contributors {get;} = {"Andrija Sevaljevic"};
    
    public string source {get;} = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private static string _defaultInstance = "(({1,2,3,4,5},{{2,1},{1,3},{2,3},{3,5},{2,4},{4,5}}),5)";
    public string defaultInstance { get; } = _defaultInstance;
    public string instance {get;set;} = string.Empty;
    
    private List<string> _nodes = new List<string>();
    private List<KeyValuePair<string, string>> _edges = new List<KeyValuePair<string, string>>();
    private int _K;
    public CutBruteForce defaultSolver {get;} = new CutBruteForce();
    public CutVerifier defaultVerifier {get;} = new CutVerifier();
    private CutGraph _cutAsGraph;
    public CutGraph graph {get => _cutAsGraph;}
    
    public string wikiName {get;} = "";
  

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

    public CutGraph cutAsGraph {
        get{
            return _cutAsGraph;
        }
    }

    // --- Methods Including Constructors ---
    public CUT() : this(_defaultInstance) {

    }
    public CUT(string GInput) {
        instance = GInput;

        StringParser cliqueGraph = new("{((N,E),K) | N is set, E subset N unorderedcross N, K is int}");
        cliqueGraph.parse(GInput);
        nodes = cliqueGraph["N"].ToList().Select(node => node.ToString()).ToList();
        edges = cliqueGraph["E"].ToList().Select(edge =>
        {
            List<UtilCollection> cast = edge.ToList();
            return new KeyValuePair<string, string>(cast[0].ToString(), cast[1].ToString());
        }).ToList();
        _K = int.Parse(cliqueGraph["K"].ToString());

        _cutAsGraph = new CutGraph(nodes, edges, _K);
    }


}