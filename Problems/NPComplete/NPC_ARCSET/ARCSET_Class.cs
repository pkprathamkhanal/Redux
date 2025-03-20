/*
* Arcset_Class.cs
* @author Alex Diviney
*/



using API.Interfaces;
using API.Problems.NPComplete.NPC_ARCSET.Solvers;
using API.Problems.NPComplete.NPC_ARCSET.Verifiers;
namespace API.Problems.NPComplete.NPC_ARCSET;

class ARCSET : IGraphProblem<ArcSetBruteForce,ArcSetVerifier,ArcsetGraph>{

    // --- Fields ---
    public string problemName {get;} = "Feedback Arc Set";
    public string formalDefinition {get;} = "ARCSET = {<G,k> | G is a directed graph that can be rendered acyclic by removal of at most k edges}";
    public string problemDefinition {get;} = "ARCSET, or the Feedback Arc Set satisfiability problem, is an NP-complete problem that can be described like the following. Given a directed graph, does removing a given set of edges render the graph acyclical? That is, does removing the edges break every cycle in the graph?";

    public string source {get;} = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";

    //ALEX NOTE: The standard mathematical form for a DIGRAPH is A = {x,y,z} r = {(x,y),(y,z),(z,x)} where A is a set of nodes and r is a set of pairs of edges. (r stands for relation)
    //           Since Arcset also has a k value needed, we put the digraph and k in as a ordered pair. (({x,y,z},{(x,y),(y,z),(z,x)}),1) where the first item of the pair is the graph,
    //           the second is the last number that is related to the solution value for the NP-Complete problem, in this case: 
    //           the question is "how many edges would you have to remove to break this graph's cycle?" and the answer is 1. 
    // G = {A,r} 
    //Mathmatical notation of the following default instance: "A = {1,2,3,4} r = {(4,1),(1,2),(4,3),(3,2),(2,4)} k = 1"; 
    public string defaultInstance {get;} = "(({1,2,3,4},{(1,2),(2,4),(3,2),(4,1),(4,3)}),1)"; //final formal version. This is standard mathmatical digraph notation with a K element appended. 
    public string instance {get;set;} = string.Empty;

    public string wikiName {get;} ="";
    private ArcsetGraph _arcsetAsGraph;
    public ArcsetGraph graph {get => _arcsetAsGraph;}
    public ArcSetBruteForce defaultSolver {get;} = new ArcSetBruteForce();
    public ArcSetVerifier defaultVerifier {get;} = new ArcSetVerifier(); //Verifier implements a Depth First Search. 
    
    public string[] contributors {get;} = { "Alex Diviney" };


    // --- Properties ---
    public ArcsetGraph directedGraph{
        get{
            return _arcsetAsGraph;
        }
    }

    // --- Methods Including Constructors ---
    public ARCSET() {
        //Should be a graph object.
        _arcsetAsGraph = new ArcsetGraph(defaultInstance,true); //first value is the string input, second is a dummy boolean to utilize overloading.
        instance = _arcsetAsGraph.ToString(); 
    }
    public ARCSET(string arcInput) {
        _arcsetAsGraph = new ArcsetGraph(arcInput,true); //first value is the string input, second is a dummy boolean to utilize overloading.
        instance = _arcsetAsGraph.ToString();
    }
}