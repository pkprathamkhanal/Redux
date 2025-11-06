using System.Runtime.ConstrainedExecution;
using API.Interfaces;
using API.Interfaces.Graphs;
using API.Interfaces.Graphs.GraphParser;
using SPADE;

namespace API.Problems.NPComplete.NPC_ARCSET.Solvers;
class ArcSetBruteForce : ISolver<ARCSET> {

    // --- Fields ---
    public string solverName {get;} = "Arc Set Brute Force Solver";
    public string solverDefinition {get;} = @" This Solver is a brute force solver, which checks all combinations of k edges until a solution is found or its determined there is no solution";
    public string source {get;} = "wikipedia: https://en.wikipedia.org/wiki/Feedback_arc_set";

    public string[] contributors {get;} = { "Alex Diviney","Caleb Eardley"};

    // --- Methods Including Constructors ---
    public ArcSetBruteForce() {

    }
    private long factorial(long x){
        long y = 1;
        for(long i=1; i<=x; i++){
            y *= i;
        }
        return y;
    }
    private string indexListToCertificate(List<int> indecies, List<Edge> edges){
        string certificate = "";
        foreach(int i in indecies){
            certificate += ","+edges[i].directedString();
        }
        return "{" + certificate.Substring(1) + "}";
    }
    private List<int> nextComb(List<int> combination, int size){
        for(int i=combination.Count-1; i>=0; i--){
            if(combination[i]+1 <= (i + size - combination.Count)){
                combination[i] += 1;
                for(int j = i+1; j < combination.Count; j++){
                    combination[j] = combination[j-1]+1;
                }
                return combination;
            }
        }
        return combination;
    }

    /**
    * Returns the set of edges that if removed from arcset would turn it acyclic
    */

    public string solve(ARCSET arc){
        UtilCollectionGraph graph = arc.graph;

        foreach (UtilCollection potentialSolution in graph.Edges.ChooseUpTo(arc.K))
        {
            string certificate = potentialSolution.ToString();
            if (arc.defaultVerifier.verify(arc, certificate)) return certificate;
        }
        return "{}";
    }
}