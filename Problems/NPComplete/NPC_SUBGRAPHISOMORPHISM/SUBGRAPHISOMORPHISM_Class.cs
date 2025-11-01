using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using API.Interfaces;
using API.Problems.NPComplete.NPC_SUBGRAPHISOMORPHISM.Solvers;
using API.Problems.NPComplete.NPC_SUBGRAPHISOMORPHISM.Verifiers;

namespace API.Problems.NPComplete.NPC_SUBGRAPHISOMORPHISM;
using SPADE;
using API.Interfaces.Graphs.GraphParser;
using Microsoft.AspNetCore.Components;
using Xunit;

class SUBGRAPHISOMORPHISM : IProblem<SubgraphIsomorphismBruteForce, SubgraphIsomorphismVerifier>
{
    // class SUBGRAPHISOMORPHISM : IProblem<SubgraphIsomorphismUllmann, SubgraphIsomorphismVerifier> {


    // knapsack look into it


    // --- Fields ---
    private string _problemName = "Subgraph Isomorphism";
    private string _formalDefinition = "SUBGRAPHISOMORPHISM = { <G,H> | G is a graph that contains a subgraph isomorphic to H }";
    private string _problemDefinition = "Given two graphs G and H, the subgraph isomorphism problem is to check if a smaller graph H can be found within a larger graph G by mapping the vertices and edges of H to a part of G so that the edges between the vertices in H are preserved in G.";
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    // private string _defaultInstance = "(({1,2,3,4,5,6,7,8},{{2,1},{1,3},{2,3},{3,5},{2,4},{4,5},{6,7},{7,8},{6,8}}),3)";
    private string _defaultInstance = "(({a,b,c,d,e,f},{{a,b},{a,c},{b,c},{c,e},{b,d},{d,e},{e,f}}),({W,X,Y,Z},{{W,X},{W,Z},{X,Y},{Y,Z}}))";

    private string _instance = string.Empty;

    private string _instanceT = string.Empty;
    private string _instanceP = string.Empty;

    private string _wikiName = "";
    // private SubgraphIsomorphismUllmann _defaultSolver = new SubgraphIsomorphismUllmann();
    private SubgraphIsomorphismBruteForce _defaultSolver = new SubgraphIsomorphismBruteForce();

    private SubgraphIsomorphismVerifier _defaultVerifier = new SubgraphIsomorphismVerifier();
    private string[] _contributers = { "Sabal Subedi" };

    // TODO: implement properties if Subgraph isomorphism is a graphing problem
    // For Target Graph
    private List<string> _nodesT = new List<string>();
    // private List<Node> _nodesT = new List<Node>();
    private List<KeyValuePair<string, string>> _edgesT = new List<KeyValuePair<string, string>>();
    // private List<Edge> _edgesT = new List<Edge>();


    // For Pattern Graph
    private List<string> _nodesP = new List<string>();
    private List<KeyValuePair<string, string>> _edgesP = new List<KeyValuePair<string, string>>();
    // private List<Edge> _edgesP = new List<Edge>();

    private int _K;
    private SubgraphIsomorphismGraph _targetGraphAsGraph;

    private SubgraphIsomorphismGraph _patternGraphAsGraph;


    // --- Properties ---
    public string problemName
    {
        get
        {
            return _problemName;
        }
    }
    public string formalDefinition
    {
        get
        {
            return _formalDefinition;
        }
    }
    public string problemDefinition
    {
        get
        {
            return _problemDefinition;
        }
    }

    public string source
    {
        get
        {
            return _source;
        }
    }

    public string[] contributors
    {
        get
        {
            return _contributers;
        }
    }
    public string defaultInstance
    {
        get
        {
            return _defaultInstance;
        }
    }
    public string instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    // For Target Graph Instance
    public string instanceT
    {
        get
        {
            return _instanceT;
        }
        set
        {
            _instanceT = value;
        }
    }

    // For Pattern Graph Instance
    public string instanceP
    {
        get
        {
            return _instanceP;
        }
        set
        {
            _instanceP = value;
        }
    }
    public string wikiName
    {
        get
        {
            return _wikiName;
        }
    }

    // TODO: implement properties if Subgraph isomorphism is a graphing problem
    public List<string> nodesT
    {
        get
        {
            return _nodesT;
        }
        set
        {
            _nodesT = value;
        }
    }
    public List<KeyValuePair<string, string>> edgesT
    {
        get
        {
            return _edgesT;
        }
        set
        {
            _edgesT = value;
        }
    }

    public List<string> nodesP
    {
        get
        {
            return _nodesP;
        }
        set
        {
            _nodesP = value;
        }
    }
    public List<KeyValuePair<string, string>> edgesP
    {
        get
        {
            return _edgesP;
        }
        set
        {
            _edgesP = value;
        }
    }

    // public List<string> nodesP {
    //     get {
    //         return _nodesP;
    //     }
    //     set {
    //         _nodesP = value;
    //     }
    // }
    // public List<KeyValuePair<string, string>> edgesP {
    //     get {
    //         return _edgesP;
    //     }
    //     set {
    //         _edgesP = value;
    //     }
    // }
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
    public SubgraphIsomorphismGraph targetGraphAsGraph
    {
        get
        {
            return _targetGraphAsGraph;
        }
        set
        {
            _targetGraphAsGraph = value;
        }
    }

    public SubgraphIsomorphismGraph patternGraphAsGraph
    {
        get
        {
            return _patternGraphAsGraph;
        }
        set
        {
            _patternGraphAsGraph = value;
        }
    }

    public SubgraphIsomorphismBruteForce defaultSolver
    {
        get
        {
            return _defaultSolver;
        }
    }
    public SubgraphIsomorphismVerifier defaultVerifier
    {
        get
        {
            return _defaultVerifier;
        }
    }

    // public string[] contributors => throw new NotImplementedException();

    // --- Methods and Constructors ---

    // public SUBGRAPHISOMORPHISM() : this(_defaultInstance) {

    // }
    public SUBGRAPHISOMORPHISM()
    {
        _instance = defaultInstance;

        StringParser isograph = new("{((n1,e1),(n2,e2)) | n1 is set, e1 subset n1 unorderedcross n1, n2 is set, e2 subset n2 unorderedcross n2}");
        isograph.parse(_instance);

        GraphParser gp = new GraphParser();
        // For Target Graph
        string targetInstance = "(" + isograph["n1"].ToString() + "," + isograph["e1"].ToString() + ")";
        // _targetGraphAsGraph = new SubgraphIsomorphismGraph(targetInstance,true);
        nodesT = gp.getNodesFromNodeListString(isograph["n1"].ToString());
        edgesT = GraphParser.parseUndirectedEdgeListWithStringFunctions(isograph["e1"].ToString());
        _instanceT = targetInstance;

        // For Pattern Graph
        string patternInstance = "(" + isograph["n2"].ToString() + "," + isograph["e2"].ToString() + ")";
        // _targetGraphAsGraph = new SubgraphIsomorphismGraph(targetInstance,true);
        nodesP = gp.getNodesFromNodeListString(isograph["n2"].ToString());
        edgesP = GraphParser.parseUndirectedEdgeListWithStringFunctions(isograph["e2"].ToString());
        _instanceP = patternInstance;

        // // For Target Graph
        // string targetInstance = "((" + isograph["n1"] + "," + isograph["e1"] + "),3)";
        // _targetGraphAsGraph = new SubgraphIsomorphismGraph(targetInstance, true);
        // nodesT = _targetGraphAsGraph.nodesStringList;
        // edgesT = _targetGraphAsGraph.edgesKVP;
        // _K = _targetGraphAsGraph.K;
        // _instanceT = _targetGraphAsGraph.ToString();

        // // For Pattern Graph
        // string patternInstance = "((" + isograph["n2"] + "," + isograph["e2"] + "),3)";
        // _patternGraphAsGraph = new SubgraphIsomorphismGraph(patternInstance, true);
        // nodesP = _patternGraphAsGraph.nodesStringList;
        // edgesP = _patternGraphAsGraph.edgesKVP;
        // _K = _patternGraphAsGraph.K;
        // _instanceP = _patternGraphAsGraph.ToString();
    }

    public SUBGRAPHISOMORPHISM(string GInput)
    {
        StringParser isograph = new("{((n1,e1),(n2,e2)) | n1 is set, e1 subset n1 unorderedcross n1, n2 is set, e2 subset n2 unorderedcross n2}");
        isograph.parse(GInput);

        GraphParser gp = new GraphParser();

        // For Target Graph
        string targetInstance = "(" + isograph["n1"].ToString() + "," + isograph["e1"].ToString() + ")";
        // _targetGraphAsGraph = new SubgraphIsomorphismGraph(targetInstance,true);
        nodesT = gp.getNodesFromNodeListString(isograph["n1"].ToString());
        edgesT = GraphParser.parseUndirectedEdgeListWithStringFunctions(isograph["e1"].ToString());
        _instanceT = targetInstance;

        // For Pattern Graph
        string patternInstance = "(" + isograph["n2"].ToString() + "," + isograph["e2"].ToString() + ")";
        // _targetGraphAsGraph = new SubgraphIsomorphismGraph(targetInstance,true);
        nodesP = gp.getNodesFromNodeListString(isograph["n2"].ToString());
        edgesP = GraphParser.parseUndirectedEdgeListWithStringFunctions(isograph["e2"].ToString());
        _instanceP = patternInstance;
    }

}