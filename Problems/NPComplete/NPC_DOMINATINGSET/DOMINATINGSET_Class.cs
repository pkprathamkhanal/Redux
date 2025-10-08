using API.Interfaces;
using API.Problems.NPComplete.NPC_DOMINATINGSET.Solvers;
using API.Problems.NPComplete.NPC_DOMINATINGSET.Verifiers;

namespace API.Problems.NPComplete.NPC_DOMINATINGSET;

class DOMINATINGSET : IProblem<DominatingSetSolver, DominatingSetVerifier>
{

    // --- Fields ---
    private string _problemName =  "Dominating Set" ;
    private string _formalDefinition =  "Dominating Set = {<G, k> | G is a graph with a dominating set greater or equal to k}" ;
    private string _problemDefinition = "A dominating set of a graph G is a subset D of the vertices of G such that every vertex v of G is either in the set D or v has at least one neighbour that is in D.";
    private string[] _contributors = { "Quinton Smith" };

    private string _source = "https://webhome.cs.uvic.ca/~wendym/courses/425/14/notes/425_03_dom_alg.pdf";
    private string _defaultInstance = "(({0,1,2,3,4},{{1,0},{0,3},{1,2},{2,4},{1,3},{3,4},{4,1}}),5)";
    private string _instance = string.Empty;


   
    
    private List<string> _nodes = new();
    private List<KeyValuePair<string,string>> _edges = new();
    private int _K ;

    private DominatingSetSolver _defaultSolver = new DominatingSetSolver();
    private DominatingSetVerifier _defaultVerifier = new DominatingSetVerifier();
    private DominatingSetGraph _dominatingSetAsGraph;

    private string _wikiName = "";

    // --- Properties ---
    public string problemName => _problemName;
    public string formalDefinition => _formalDefinition;
    public string problemDefinition => _problemDefinition;
    public string source => _source;
    public string[] contributors => _contributors;

    public string defaultInstance => _defaultInstance;

    public string instance
    {
        get => _instance;
        set => _instance = value;
    }

    public string wikiName => _wikiName;

    public List<string> nodes
    {
        get => _nodes;
        set => _nodes = value;
    }

    public List<KeyValuePair<string, string>> edges
    {
        get => _edges;
        set => _edges = value;
    }

    public int K
    {
        get => _K;
        set => _K = value;
    }

    public DominatingSetGraph dominatingSetAsGraph
    {
        get => _dominatingSetAsGraph;
        set => _dominatingSetAsGraph = value;
    }

    public DominatingSetSolver defaultSolver => _defaultSolver;
    public DominatingSetVerifier defaultVerifier => _defaultVerifier;


    // --- Methods and Constructors ---
    public DOMINATINGSET()
    {
        _instance = _defaultInstance;
        _dominatingSetAsGraph = new DominatingSetGraph(_instance, true);
        _nodes = _dominatingSetAsGraph.nodesStringList;
        _edges = _dominatingSetAsGraph.edgesTuple;
        _K = _dominatingSetAsGraph.K;
    }

    public DOMINATINGSET(string instance)
    {
        _dominatingSetAsGraph = new DominatingSetGraph(instance, true);
        _nodes = _dominatingSetAsGraph.nodesStringList;
        _edges = _dominatingSetAsGraph.edgesTuple;
        _K = _dominatingSetAsGraph.K;

        _instance = _dominatingSetAsGraph.ToString();

    }
}
