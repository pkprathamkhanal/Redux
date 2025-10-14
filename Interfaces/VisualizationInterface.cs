using API.Interfaces.JSON_Objects.Graphs;

namespace API.Interfaces;

interface IVisualization
{
    string visualizationName { get; }
    string visualizationDefinition { get; }
    string source { get; }
    string[] contributors { get; }
    string visualize(string problem);
    string getSolvedVisualization(string problem);
}

interface IVisualization<U> : IVisualization where U : IProblem
{
    string IVisualization.visualize(string problem)
    {
        // Should there be some sort of contraint that assures there is a constructor
        // that matches the signature of a single `string` argument?
        // Perhaps a static `FromInstance(string instance)` method for `IProblem` will work.
        return visualize((U)Activator.CreateInstance(typeof(U), problem));
    }
    string visualize(U problem);

    string IVisualization.getSolvedVisualization(string problem)
    {
        // Should there be some sort of contraint that assures there is a constructor
        // that matches the signature of a single `string` argument?
        // Perhaps a static `FromInstance(string instance)` method for `IProblem` will work.
        return getSolvedVisualization((U)Activator.CreateInstance(typeof(U), problem));
    }
    string getSolvedVisualization(U problem);
}
