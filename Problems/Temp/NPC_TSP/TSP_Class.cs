using API.Interfaces;
using API.Problems.NPComplete.NPC_TSP.Solvers;
using API.Problems.NPComplete.NPC_TSP.Verifiers;

namespace API.Problems.NPComplete.NPC_TSP;

class TSP : IGraphProblem<TSPBruteForce, TSPVerifier, TSPGraph> {

    // --- Fields ---
    public string problemName {get;} = "Traveling Sales Person";
    public string formalDefinition {get;} = "TSP = {<G,k> | G is a weighted graph where there is a path through every vertex whose weights add up to less than k}";
    public string problemDefinition {get;} = "";
    public string[] contributors {get;} = {"Andrija Sevaljevic"};
    
    public string source {get;} = "";
    public string defaultInstance {get;} = "(({Narnia,Atlantis,Wakanda,Pocatello,Neverland},{{Narnia,Atlantis,30},{Narnia,Wakanda,55},{Narnia,Pocatello,80},{Narnia,Neverland,45},{Atlantis,Wakanda,65},{Atlantis,Pocatello,15},{Atlantis,Neverland,30},{Wakanda,Pocatello,40},{Wakanda,Neverland,90},{Pocatello,Neverland,25}}),200)";
                                      
    public string instance {get;set;} = string.Empty;
    
    private List<string> _nodes = new List<string>();
    private List<(string source, string target, int weight)> _edges = new List<(string source, string destination, int weight)>();
    private int _K;
    public TSPBruteForce defaultSolver {get;} = new TSPBruteForce();
    public TSPVerifier defaultVerifier {get;} = new TSPVerifier();
    private TSPGraph _tspAsGraph;
    public TSPGraph graph {get => _tspAsGraph;}
    
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
    public List<(string source, string target, int weight)> edges {
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

    public TSPGraph tspAsGraph {
        get{
            return _tspAsGraph;
        }
    }

    // --- Methods Including Constructors ---
    public TSP() {
        instance = defaultInstance;
        _tspAsGraph = new TSPGraph(instance,true);
        nodes = _tspAsGraph.nodesStringList;
        edges = _tspAsGraph.edgesTuple;
         _K = _tspAsGraph.K;


    }
    public TSP(string GInput) {
        _tspAsGraph = new TSPGraph(GInput, true);
        nodes = _tspAsGraph.nodesStringList;
        edges = _tspAsGraph.edgesTuple;
        _K = _tspAsGraph.K;
        instance = _tspAsGraph.ToString();
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