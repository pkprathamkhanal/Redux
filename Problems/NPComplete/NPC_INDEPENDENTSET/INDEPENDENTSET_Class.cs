using API.Interfaces;
using API.Problems.NPComplete.NPC_INDEPENDENTSET.Solvers;
using API.Problems.NPComplete.NPC_INDEPENDENTSET.Verifiers;
using SPADE;

namespace API.Problems.NPComplete.NPC_INDEPENDENTSET;
class INDEPENDENTSET : IGraphProblem<IndependentSetBruteForce,IndependentSetVerifier,IndependentSetGraph> {

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
    private IndependentSetGraph _independentSetAsGraph;
    public IndependentSetGraph graph {get => _independentSetAsGraph;}
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

    public IndependentSetGraph independentSetAsGraph {
        get{
            return _independentSetAsGraph;
        }
        set{
            _independentSetAsGraph = value;
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

        _independentSetAsGraph = new IndependentSetGraph(nodes, edges, _K);
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