using API.Interfaces;
using API.Problems.NPComplete.NPC_STEINERTREE.Solvers;
using API.Problems.NPComplete.NPC_STEINERTREE.Verifiers;

namespace API.Problems.NPComplete.NPC_STEINERTREE;

class STEINERTREE : IGraphProblem<SteinerTreeBruteForce, SteinerTreeVerifier, SteinerGraph> {

    // --- Fields ---
    public string problemName {get;} = "Steiner Tree";
    public string formalDefinition {get;} = "Steiner = {<G,R,k> | G has a subtree of weight <= k containing the set of nodes in R}";
    public string problemDefinition {get;} = "Steiner tree problem in graphs requires a tree of minimum weight that contains all terminals";
    public string source {get;} = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    public string defaultInstance {get;} = "(({1,2,3,4,5,6,7,8},{{2,1},{1,3},{2,3},{3,5},{2,4},{4,5},{6,7},{7,8},{6,8},{6,1}}),{5,2,8},6)";
    public string instance {get;set;} = string.Empty;

    public string wikiName {get;} = "";
    private int _K;
    private List<string> _nodes = new List<string>();
    private List<string> _terminals = new List<string>();
    private List<KeyValuePair<string, string>> _edges = new List<KeyValuePair<string, string>>();
    public SteinerTreeBruteForce defaultSolver {get;} = new SteinerTreeBruteForce();
    public SteinerTreeVerifier defaultVerifier {get;} = new SteinerTreeVerifier();
    private SteinerGraph _steinerAsGraph;
    public SteinerGraph graph {get => _steinerAsGraph;}
    public string[] contributors {get;} = { "Andrija Sevaljevic" };

    // --- Properties ---
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
    public List<string> terminals
    {
        get
        {
            return _terminals;
        }
        set
        {
            _terminals = value;
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

    public SteinerGraph steinerAsGraph
    {
        get
        {
            return _steinerAsGraph;
        }
        set
        {
            _steinerAsGraph = value;
        }
    }

    // --- Methods Including Constructors ---
    public STEINERTREE()
    {
        instance = defaultInstance;
        _steinerAsGraph = new SteinerGraph(instance,true);
        nodes = _steinerAsGraph.nodesStringList;
        terminals = getTerminals(instance);
        edges = _steinerAsGraph.edgesKVP;
        K = _steinerAsGraph.K;

    }
    public STEINERTREE(string GInput)
    {
        _steinerAsGraph = new SteinerGraph(GInput, true);
        nodes = _steinerAsGraph.nodesStringList;
        edges = _steinerAsGraph.edgesKVP;
        K = _steinerAsGraph.K;
        instance = _steinerAsGraph.ToString();
        terminals = getTerminals(GInput);

    }
    public List<string> getTerminals(string Ginput)
    {

        List<string> seperate = Ginput.Split("}}),{").ToList();
        List<string> allGNodes = seperate[1].Split("},").ToList();
        allGNodes = allGNodes[0].Split(",").ToList();

        return allGNodes;
    }


}