using API.Interfaces;
using API.Interfaces.Graphs;

namespace API.Problems.NPComplete.NPC_ARCSET.Solvers;
class AlexNaiveSolver : ISolver<ARCSET> {

    // --- Fields ---
    public string solverName {get;} = "Alex's Naive Arcset Solver";
    public string solverDefinition {get;} = @" This Solver is a naive solver that does not have a clear origination, although there have been many improvements upon it published. This solver was
                                        sourced from the below Wikipedia page. It works as follows:
                                        Essentially it orders edges into two categories, decending and ascending. Then only returns the bigger set. 
                                        This will guarantee all cycles are broken. This solver specifically makes use of a DFS (Depth First Search), where the graph is ordered into descending edges and back edges.
                                        This allows the removal of all backedges, breaking cycles in a less arbitrary way. 
                                        Note that technically, this will leave one back edge in, because the goal is to return an instance of ARCSET (ie. minimum cyclical graph), as opposed the maximum acyclical subgraph.
                                        This solver has an approximation ratio of 1/2.";
    public string source {get;} = "wikipedia: https://en.wikipedia.org/wiki/Feedback_arc_set";

    public string[] contributors {get;} = { "Alex Diviney"};

    // --- Methods Including Constructors ---
    public AlexNaiveSolver() {

    }


    /**
    * Returns the set of edges that if removed from arcset would turn it acyclic
    */
    public string solve(ARCSET arc){
        string retStr = "";
        List<Edge> backEdges = arc.directedGraph.DFS();
        foreach(Edge be in backEdges){
            retStr =retStr + be.directedString()+",";
        }
        retStr = retStr.TrimEnd(',');
        return retStr;        

    }

}