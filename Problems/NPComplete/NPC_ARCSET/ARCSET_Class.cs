/*
* Arcset_Class.cs
* @author Alex Diviney
*/

using API.Interfaces;
using API.Interfaces.JSON_Objects;
using API.Problems.NPComplete.NPC_ARCSET.Solvers;
using API.Problems.NPComplete.NPC_ARCSET.Verifiers;
using API.Problems.NPComplete.NPC_ARCSET.Visualizers;
using SPADE;

namespace API.Problems.NPComplete.NPC_ARCSET;

class ARCSET : IGraphProblem<ArcSetBruteForce, ArcSetVerifier, ArcSetDefaultVisualization, UtilCollectionGraph>
{
    // --- Fields ---
    public string problemName { get; } = "Feedback Arc Set";
    public string problemLink { get; } = "https://en.wikipedia.org/wiki/Feedback_arc_set";

    public string formalDefinition { get; } =
        "ARCSET = {<G, k> | G is a directed graph that can be rendered acyclic by removal of at most k edges}";
    public string problemDefinition { get; } =
        "The Feedback Arc Set problem asks whether removing at most k directed edges from graph G eliminates all cycles, making the graph acyclic.";
    public string source { get; } =
        "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, 1972.";
    public string sourceLink { get; } =
        "https://en.wikipedia.org/wiki/Feedback_arc_set";
    // NEW: Contributor hyperlink
    public string[] contributors { get; } = { "Alex Diviney" };
    public string contributorsLink { get; } = "https://redux.portneuf.cose.isu.edu/aboutus";
    // --- NEW ANNOTATIONS (for info box) ---
    public string complexityClass { get; } = "NP-Complete";
    // Brute-force tries subsets of edges → O(2^m)
    public string solutionComplexity { get; } = "O(2^m)";
    // Reductions to ARCSET are linear or quadratic
    public string reductionComplexity { get; } = "O(n + m)";
    // Verifier uses DFS to check acyclicity
    public string verifierComplexity { get; } = "O(n + m)";
    // --- Default Instance ---
    private static string _defaultInstance =
        "(({1,2,3,4},{(1,2),(2,4),(3,2),(4,1),(4,3)}),1)";
    public string defaultInstance { get; } = _defaultInstance;
    public string instance { get; set; } = string.Empty;
    public string wikiName { get; } = "";
    public UtilCollectionGraph graph { get; set; }
    public ArcSetBruteForce defaultSolver { get; } = new ArcSetBruteForce();
    public ArcSetVerifier defaultVerifier { get; } = new ArcSetVerifier();
    public ArcSetDefaultVisualization defaultVisualization { get; } = new ArcSetDefaultVisualization();
    public int K;
    // --- Constructors ---
    public ARCSET() : this(_defaultInstance) {}
    public ARCSET(string arcInput)
    {
        instance = arcInput;
        StringParser arcset =
            new("{((N,E),K) | N is set, E subset N cross N, K is int}");
        arcset.parse(arcInput);
        graph = new UtilCollectionGraph(arcset["N"], arcset["E"]);
        K = int.Parse(arcset["K"].ToString());
    }
}
