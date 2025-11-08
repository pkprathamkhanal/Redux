using API.Interfaces;
using API.Problems.NPComplete.NPC_CLIQUECOVER;

namespace API.Problems.NPComplete.NPC_GRAPHCOLORING.ReduceTo.NPC_CLIQUECOVER;

class CliqueCoverReduction : IReduction<GRAPHCOLORING, CLIQUECOVER>
{

    // --- Fields ---
    public string reductionName {get;} = "Clique Cover Reduction";
    public string reductionDefinition {get;} = "Karp's Reduction from Graph Coloring to Clique Cover";
    public string source {get;} = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    public string sourceLink { get; } = "https://cgi.di.uoa.gr/~sgk/teaching/grad/handouts/karp.pdf";
    public string[] contributors {get;} = { "Andrija Sevaljevic" };

    private string _complexity = "";
    private Dictionary<Object, Object> _gadgetMap = new Dictionary<Object, Object>();

    private GRAPHCOLORING _reductionFrom;
    private CLIQUECOVER _reductionTo;


    // --- Properties ---
    public Dictionary<Object, Object> gadgetMap
    {
        get
        {
            return _gadgetMap;
        }
        set
        {
            _gadgetMap = value;
        }
    }
    public GRAPHCOLORING reductionFrom
    {
        get
        {
            return _reductionFrom;
        }
        set
        {
            _reductionFrom = value;
        }
    }
    public CLIQUECOVER reductionTo
    {
        get
        {
            return _reductionTo;
        }
        set
        {
            _reductionTo = value;
        }
    }



    // --- Methods Including Constructors ---
    public CliqueCoverReduction(GRAPHCOLORING from)
    {
        _reductionFrom = from;
        _reductionTo = reduce();

    }
    public CliqueCoverReduction(string instance) : this(new GRAPHCOLORING(instance)) { }
    public CLIQUECOVER reduce()
    {
        GRAPHCOLORING GRAPHCOLORINGInstance = _reductionFrom;
        CLIQUECOVER reducedCLIQUECOVER = new CLIQUECOVER();

        string instance = "(({";
        foreach (var node in reductionFrom.nodes)
        {
            instance += node + ',';
        }

        instance = instance.TrimEnd(',') + "},{{";
        foreach (var node in reductionFrom.nodes)
        {
            foreach (var node2 in reductionFrom.nodes)
            {
                KeyValuePair<string, string> pairCheck1 = new KeyValuePair<string, string>(node, node2);
                KeyValuePair<string, string> pairCheck2 = new KeyValuePair<string, string>(node2, node);
                if (!(reductionFrom.edges.Contains(pairCheck1) || reductionFrom.edges.Contains(pairCheck1)) && node != node2)
                {
                    instance += node + ',' + node2 + "},{";
                }
            }
        }

        instance = instance.TrimEnd('{').TrimEnd(',') +"})," + reductionFrom.K.ToString() + ')';

        reducedCLIQUECOVER.K = reductionFrom.K;
        reducedCLIQUECOVER.nodes = reductionFrom.nodes;
        reducedCLIQUECOVER.instance = instance;

        reductionTo = reducedCLIQUECOVER;
        return reducedCLIQUECOVER;
    }

    public string mapSolutions(string problemFromSolution)
    {
        return "";
    }
}
// return an instance of what you are reducing to