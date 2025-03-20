using API.Interfaces;
using API.Problems.NPComplete.NPC_PARTITION.Solvers;
using API.Problems.NPComplete.NPC_PARTITION.Verifiers;

namespace API.Problems.NPComplete.NPC_PARTITION;

class PARTITION : IProblem<PartitionBruteForce,PartitionVerifier> {

    // --- Fields ---
    public string problemName {get;} = "Partition";
    public string formalDefinition {get;} = "Partition = <S, I> | S is a set of positive integers and there exists a subset of S, I where the sum of I's elements equals the sum of elements not in set I";
    public string problemDefinition {get;} = "The partition problem is the task of deciding whether a given multiset S of positive integers can be partitioned into two subsets S1 and S2 such that the sum of the numbers in S1 equals the sum of the numbers in S2";
    public string source {get;} = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    public string[] contributors {get;} = {"Andrija Sevaljevic"};



    public string defaultInstance {get;} = "{1,7,12,15,33,12,11,5,6,9,21,18}";
    public string instance {get;set;} = string.Empty;
    private List<string> _S = new List<string>();
    

    public string wikiName {get;} = "";
    public PartitionBruteForce defaultSolver {get;} = new PartitionBruteForce();
    public PartitionVerifier defaultVerifier {get;} = new PartitionVerifier();

    // --- Properties ---
    public List<string> S {
        get {
            return _S;
        }
        set {
            _S = value;
        }
    }
    
    // --- Methods Including Constructors ---
    public PARTITION() {
        instance = defaultInstance;
        S = getIntegers(instance);
    }
    public PARTITION(string instance) {
        this.instance = instance;
        S = getIntegers(instance);
    }
    public List<string> getIntegers(string instance) {

        List<string> allIntegers = new List<string>();
        string strippedInput = instance.Replace("{", "").Replace("}", "").Replace(" ", "");
        
        // [0] is integers,  [1] is T.
        string[] SSsections = strippedInput.Split(':');
        string[] SSintegers = SSsections[0].Split(',');
        
        foreach(string integer in SSintegers) {
            allIntegers.Add(integer);
        }

        return allIntegers;
    }
    


}