using API.Interfaces.JSON_Objects;
using API.Interfaces.JSON_Objects.Graphs;

namespace API.Interfaces;

interface IVisualization
{
    string visualizationName { get; }
    string visualizationDefinition { get; }
    string visualizationType { get; }
    string source { get; }
    string[] contributors { get; }
    API_JSON visualize(string problem);
    API_JSON getSolvedVisualization(string problem);
}

interface IVisualization<U> : IVisualization where U : IProblem
{
    API_JSON IVisualization.visualize(string problem)
    {
        // Should there be some sort of contraint that assures there is a constructor
        // that matches the signature of a single `string` argument?
        // Perhaps a static `FromInstance(string instance)` method for `IProblem` will work.
        return visualize((U)Activator.CreateInstance(typeof(U), problem));
    }
    API_JSON visualize(U problem);

    API_JSON IVisualization.getSolvedVisualization(string problem)
    {
        // Should there be some sort of contraint that assures there is a constructor
        // that matches the signature of a single `string` argument?
        // Perhaps a static `FromInstance(string instance)` method for `IProblem` will work.
        return getSolvedVisualization((U)Activator.CreateInstance(typeof(U), problem));
    }
    API_JSON getSolvedVisualization(U problem);
}
