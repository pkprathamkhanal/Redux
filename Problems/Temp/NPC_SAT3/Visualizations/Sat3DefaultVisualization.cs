

using API.Interfaces;
using API.Interfaces.Graphs.GraphParser;
using API.Interfaces.JSON_Objects;
using API.Problems.NPComplete.NPC_CLIQUE.Inherited;
using API.Problems.NPComplete.NPC_SAT3;
using API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_CLIQUE;
using API.Problems.NPComplete.NPC_SAT3.Solvers;

class Sat3DefaultVisualization : IVisualization<SAT3>
{
    public string visualizationName { get; } = "3 SAT visualization";
    public string visualizationDefinition { get; } = "This is a default visualization for 3 SAT";
    public string source { get; } = "";
    public string[] contributors { get; } = { "Kaden Marchetti" };
    public string visualizationType { get; } = "Boolean Satisfiability";

    // --- Methods Including Constructors ---
    public Sat3DefaultVisualization()
    {

    }
    public API_JSON visualize(SAT3 instance)
    {
        throw new NotImplementedException();
    }

    public API_JSON SolvedVizualization(SAT3 instance)
    {
        Sat3BacktrackingSolver solver = instance.defaultSolver;
        string solution = solver.solve(instance);
        SipserReduction reduction = new SipserReduction(instance);
        SipserClique reducedClique = reduction.reduce();
        //Turn string into solution dictionary
        List<string> solutionList = GraphParser.parseNodeListWithStringFunctions(solution);
        SipserClique sClique = reduction.solutionMappedToClusterNodes(reducedClique, solutionList);
        return new API_SAT3(sClique.clusterNodes);
    }
}
