using API.Interfaces;
using API.Problems.NPComplete.NPC_INDEPENDENTSET.Solvers;
using API.Problems.NPComplete.NPC_INDEPENDENTSET.Verifiers;
using SPADE;

namespace API.Problems.NPComplete.NPC_INDEPENDENTSET;
class INDEPENDENTSET : IGraphProblem<IndependentSetBruteForce,IndependentSetVerifier,IndependentSetDefaultVisualization,UtilCollectionGraph> {

    // --- Fields ---
    public string problemName {get;} = "Independent Set";
    public string formalDefinition {get;} = "In a graph G = (V, E), an independent set is a subset X of vertices no two of which are adjacent";
    public string problemDefinition {get;} = "An Independent Set is a set of nodes in a graph G, where no node is connected to another node in the set";
    public string source {get;} = "Golumbic, M. C. (2004). Algorithmic graph theory and perfect graphs. Elsevier.";
    private static string _defaultInstance = "(({a,b,c,d,e,f,g,h,i},{{a,b},{b,a},{b,c},{c,a},{a,c},{c,b},{a,d},{d,a},{d,e},{e,a},{a,e},{e,d},{a,f},{f,a},{f,g},{g,a},{a,g},{g,f},{a,h},{h,a},{h,i},{i,a},{a,i},{i,h}}),4)";
    public string defaultInstance { get; } = _defaultInstance;
    public string instance {get;set;} = string.Empty;
    public string wikiName {get;} = "";
    private List<string> _nodes = new List<string>();
    private List<KeyValuePair<string, string>> _edges = new List<KeyValuePair<string, string>>();
    private int _K ;
    public IndependentSetBruteForce defaultSolver {get;} = new IndependentSetBruteForce();
    public IndependentSetVerifier defaultVerifier {get;} = new IndependentSetVerifier();
    public IndependentSetDefaultVisualization defaultVisualization { get; } = new IndependentSetDefaultVisualization();
    public UtilCollectionGraph graph { get; set; }
    public string[] contributors {get;} = { "Russell Phillips" };

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

    // --- Methods Including Constructors ---
    public INDEPENDENTSET() : this(_defaultInstance) {
        
    }
    public INDEPENDENTSET(string GInput)
    {
        instance = GInput;

        StringParser independentset = new("{((N,E),K) | N is set, E subset N unorderedcross N, K is int}");
        independentset.parse(GInput);
        nodes = independentset["N"].ToList().Select(node => node.ToString()).ToList();
        edges = independentset["E"].ToList().Select(edge =>
        {
            List<UtilCollection> cast = edge.ToList();
            return new KeyValuePair<string, string>(cast[0].ToString(), cast[1].ToString());
        }).ToList();
        _K = int.Parse(independentset["K"].ToString());

        graph = new UtilCollectionGraph(independentset["N"], independentset["E"]);
    }

}