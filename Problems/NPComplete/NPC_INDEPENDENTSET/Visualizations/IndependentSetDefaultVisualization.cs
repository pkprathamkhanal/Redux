using API.Interfaces;
using System.Text.Json;
using API.Interfaces.Graphs.GraphParser;
using API.Interfaces.JSON_Objects.Graphs;

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
        var options = new JsonSerializerOptions { WriteIndented = true };

        API_UndirectedGraphJSON apiGraph = independentSet.graph.ToAPIGraph();

        string jsonString = JsonSerializer.Serialize(apiGraph, options);
        return jsonString;
    }
    
    public string getSolvedVisualization(INDEPENDENTSET independentSet)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };

        string solution = independentSet.defaultSolver.solve(independentSet);
        List<string> solutionNodes = GraphParser.parseNodeListWithStringFunctions(solution);

        API_UndirectedGraphJSON apiGraph = independentSet.graph.ToAPIGraph();
        for(int i=0;i<apiGraph.nodes.Count;i++){
            apiGraph.nodes[i].attribute1 = i.ToString();
            if(solutionNodes.Contains(apiGraph.nodes[i].name)){ //we set the nodes as either having a true or false flag which will indicate to the frontend whether to highlight.
                apiGraph.nodes[i].attribute2 = true.ToString(); 
            }
            else{apiGraph.nodes[i].attribute2 = false.ToString();}
        }
        string jsonString = JsonSerializer.Serialize(apiGraph, options);
        return jsonString;
    }
}