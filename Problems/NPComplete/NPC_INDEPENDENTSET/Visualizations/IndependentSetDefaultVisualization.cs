using API.Interfaces;
using API.Interfaces.Graphs.GraphParser;

namespace API.Problems.NPComplete.NPC_INDEPENDENTSET.Verifiers;

class IndependentSetDefaultVisualization : IVisualization<INDEPENDENTSET> {

    // --- Fields ---
    public string visualizationName {get;} = "Independent Set Visualization";
    public string visualizationDefinition {get;} = "This is a default visualization for Independent Set";
    public string source {get;} = " ";
    public string[] contributors {get;} = {"Russell Phillips"};

    // --- Methods Including Constructors ---
    public IndependentSetDefaultVisualization() {
        
    }
    public string visualize(INDEPENDENTSET independentSet)
    {
        return "";
    }
    public string getSolvedVisualization(INDEPENDENTSET independentSet)
    {
        return "";
    }
}