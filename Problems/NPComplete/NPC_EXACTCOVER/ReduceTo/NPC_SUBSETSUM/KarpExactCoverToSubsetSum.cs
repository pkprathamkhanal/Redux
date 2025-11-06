using API.Interfaces;
using API.Problems.NPComplete.NPC_EXACTCOVER;
using API.Problems.NPComplete.NPC_SUBSETSUM;

namespace API.Problems.NPComplete.NPC_EXACTCOVER.ReduceTo.NPC_SUBSETSUM;

class SubsetSumReduction : IReduction<EXACTCOVER, SUBSETSUM>
{

    // --- Fields ---
    public string reductionName {get;} = "Karp's Subset Sum Reduction";
    public string reductionDefinition {get;} = "Karp's Reduction from Exact Cover to Subset Sum";
    public string source {get;} = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    public string[] contributors {get;} = { "Andrija Sevaljevic" };

    private string _complexity = "";
    private Dictionary<Object, Object> _gadgetMap = new Dictionary<Object, Object>();

    private EXACTCOVER _reductionFrom;
    private SUBSETSUM _reductionTo;


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
    public EXACTCOVER reductionFrom
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
    public SUBSETSUM reductionTo
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
    public SubsetSumReduction(EXACTCOVER from)
    {
        _reductionFrom = from;
        _reductionTo = reduce();

    }
    public SUBSETSUM reduce()
    {
        EXACTCOVER ExactCoverInstance = _reductionFrom;
        SUBSETSUM reducedSUBSETSUM = new SUBSETSUM();

        int r = reductionFrom.S.Count;
        int d = r + 1;
        int[,] e = new int[reductionFrom.S.Count,reductionFrom.X.Count];

        for(int j = 0; j < reductionFrom.S.Count; j++) {
            for(int i = 0; i < reductionFrom.X.Count; i++) {
                if(reductionFrom.S[j].Contains(reductionFrom.X[i])) e[j,i] = 1;
                else e[j,i] = 0;
            }
        }

        string instance = "{{";
        double sum = 0;
        for(int j = 0; j < reductionFrom.S.Count; j++) {
            for(int i = 0; i < reductionFrom.X.Count; i++) {
                sum += e[j,i] * Math.Pow(d,i);
            }
            instance += sum.ToString() + ',';
            sum = 0;
        }

        instance = instance.TrimEnd(',') + "} : ";
        string K = ((Math.Pow(d,reductionFrom.X.Count) - 1) / (d - 1)).ToString();
        instance += K + '}';

        reducedSUBSETSUM.S = instance.Replace("{","").Replace("}","").Split(',').ToList();
        reducedSUBSETSUM.T = Int32.Parse(K);
        reducedSUBSETSUM.instance = instance;

        reductionTo = reducedSUBSETSUM;
        return reducedSUBSETSUM;
    }

    public string mapSolutions(EXACTCOVER reductionFrom, SUBSETSUM problemTo, string reductionFromSolution)
    {
        if (!reductionFrom.defaultVerifier.verify(reductionFrom, reductionFromSolution))
        {
            return "Solution is incorect";
        }

        return false.ToString();




    }

}
// return an instance of what you are reducing to