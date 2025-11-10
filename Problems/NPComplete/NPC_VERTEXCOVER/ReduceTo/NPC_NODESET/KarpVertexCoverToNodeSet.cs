using API.Interfaces;
using API.Problems.NPComplete.NPC_NODESET;
using API.Problems.NPComplete.NPC_VERTEXCOVER;

namespace API.Problems.NPComplete.NPC_VERTEXCOVER.ReduceTo.NPC_NODESET;

class VertexCoverReduction : IReduction<VERTEXCOVER, NODESET>
{

    // --- Fields ---
    public string reductionName {get;} = "Karp Vertex Cover to Node Set Reduction";
    public string reductionDefinition {get;} = "Karp's Reduction from Vertex Cover to Feedback Node Set";
    public string source {get;} = "This reduction was found by the Algorithms Seminar at the Cornell University Computer Science Department. Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    public string[] contributors {get;} = { "Andrija Sevaljevic" };

    private string _complexity = "";
    private Dictionary<Object, Object> _gadgetMap = new Dictionary<Object, Object>();

    private VERTEXCOVER _reductionFrom;
    private NODESET _reductionTo;


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
    public VERTEXCOVER reductionFrom
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
    public NODESET reductionTo
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
    public VertexCoverReduction(VERTEXCOVER from)
    {
        _reductionFrom = from;
        _reductionTo = reduce();

    }
    public VertexCoverReduction(string instance) : this(new VERTEXCOVER(instance)) { }
    public VertexCoverReduction() : this(new VERTEXCOVER()) { }
    public NODESET reduce()
    {
        VERTEXCOVER VERTEXCOVERInstance = _reductionFrom;
        NODESET reducedNODESET = new NODESET();

        string instance = "(({";
        foreach (var node in reductionFrom.nodes)
        {
            instance += node + ',';
        }

        instance = instance.TrimEnd(',') + "},{(";
        foreach (var node in reductionFrom.nodes)
        {
            foreach (var node2 in reductionFrom.nodes)
            {
                KeyValuePair<string, string> pairCheck1 = new KeyValuePair<string, string>(node, node2);
                KeyValuePair<string, string> pairCheck2 = new KeyValuePair<string, string>(node2, node);
                if ((reductionFrom.edges.Contains(pairCheck1) || reductionFrom.edges.Contains(pairCheck1)) && node != node2)
                {
                    instance += node + ',' + node2 + "),(";
                    instance += node2 + ',' + node + "),(";
                }
            }
        }

        instance = instance.TrimEnd('(').TrimEnd(',') +"})," + reductionFrom.K.ToString() + ')';

        reducedNODESET.K = reductionFrom.K;
        reducedNODESET.nodes = reductionFrom.nodes;
        reducedNODESET.instance = instance;

        reductionTo = reducedNODESET;
        return reducedNODESET;
    }

    public string mapSolutions(string problemFromSolution)
    {
        return "";
    }
}
// return an instance of what you are reducing to