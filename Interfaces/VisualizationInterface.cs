using API.Interfaces.JSON_Objects;
using API.Interfaces.JSON_Objects.Graphs;
using API.Tools;

namespace API.Interfaces;

interface IVisualization
{
    string visualizationName { get; }
    string visualizationDefinition { get; }
    string visualizationType { get; }
    string source { get; }
    string[] contributors { get; }
    API_JSON visualize(string problem);
    API_JSON SolvedVisualization(string problem);

    API_JSON StepsVisualization(string problem, Steps steps);
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

    API_JSON IVisualization.SolvedVisualization(string problem)
    {
        // Should there be some sort of contraint that assures there is a constructor
        // that matches the signature of a single `string` argument?
        // Perhaps a static `FromInstance(string instance)` method for `IProblem` will work.
        return SolvedVisualization((U)Activator.CreateInstance(typeof(U), problem));
    }
    API_JSON SolvedVisualization(U problem)
    {
        return new API_empty();
    }

    API_JSON IVisualization.StepsVisualization(string problem, Steps steps)
    {
        if (!steps.Implemented)
            return new API_empty();
        return stepsVisualization((U)Activator.CreateInstance(typeof(U), problem), steps);
    }

    API_JSON stepsVisualization(U problem, Steps steps)
    {
        return new API_empty();
    }
}
