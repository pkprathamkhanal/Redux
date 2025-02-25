using API.Interfaces.JSON_Objects.Graphs;

namespace API.Interfaces;

interface IProblem {
    string problemName{get;}

    string formalDefinition{get;}
    string problemDefinition{get;}
    string source {get;}
    string wikiName {get;}
    string defaultInstance{get;}

    string[] contributors{ get; }

    ISolver defaultSolver {get;}
    IVerifier defaultVerifier {get;}
}

interface IProblem<T,U> : IProblem where T : ISolver where U : IVerifier {
    new T defaultSolver{get;}
    ISolver IProblem.defaultSolver {get => defaultSolver;}
    new U defaultVerifier{get;}
    IVerifier IProblem.defaultVerifier {get => defaultVerifier;}
}

interface IGraphProblem : IProblem {
    Graph graph {get;}
    API_UndirectedGraphJSON visualize() {
        return new API_UndirectedGraphJSON(graph.nodes, graph.edges);
    }
}

interface IGraphProblem<T,U,V> : IProblem<T,U>, IGraphProblem where T : ISolver where U : IVerifier where V : Graph {
    new V graph {get;}
    Graph IGraphProblem.graph {get => graph;}
}
