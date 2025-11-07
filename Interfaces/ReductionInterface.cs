namespace API.Interfaces;

interface IReduction
{
    string reductionName { get; }
    string reductionDefinition { get; }
    string source { get; }
    string[] contributors { get; }
    IVisualization visualization { get; }
    Dictionary<Object, Object> gadgetMap { get; }
    IProblem reductionFrom { get; }
    IProblem reductionTo { get; }
    IProblem reduce();
    string mapSolutions(IProblem problemFrom, IProblem problemTo, string problemFromSolution);
}

interface IReduction<T, U> : IReduction where T : IProblem where U : IProblem
{
    IVisualization IReduction.visualization
    {
        get
        {
            return reductionFrom.defaultVisualization;
        }
    }

    IProblem IReduction.reductionFrom { 
        get
        {
            return reductionFrom;
        }
    }
    new T reductionFrom { get; }
    IProblem IReduction.reductionTo {
        get
        {
            return reductionTo;
        } 
    }
    new U reductionTo { get; }

    IProblem IReduction.reduce()
    {
        return reduce();
    }
    new U reduce();

    string IReduction.mapSolutions(IProblem problemFrom, IProblem problemTo, string problemFromSolution)
    {
        return mapSolutions(problemFrom, problemTo, problemFromSolution);
    }
    string mapSolutions(T problemFrom, U problemTo, string problemFromSolution);
}
