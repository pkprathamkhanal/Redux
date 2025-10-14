using API.Interfaces;
using API.Problems.NPComplete.NPC_JOBSEQ.Solvers;
using API.Problems.NPComplete.NPC_JOBSEQ.Verifiers;

namespace API.Problems.NPComplete.NPC_JOBSEQ;

class JOBSEQ : IProblem<JobSeqBruteForce,JobSeqVerifier> {

    // --- Fields ---
    public string problemName {get;} = "Job Sequencing";
    public string formalDefinition {get;} = "JobSeq = <T, D, P, K> is a vecter T of execution times, vector D of deadlines, vector P of penalties, and integer k where there exists a permutation pi of {1,2,3...,p} such that the sum of the penalties of every job that was not finished before the deadline is less than equal to k.";
    public string problemDefinition {get;} = "Job sequencing is the task of deciding in what order to do a series of jobs. Each job has a length of time it takes, a deadline, and a penalty that is applied if the deadline is missed. The task is to find an ordering of the jobs that results in a penalty that is less than k.";
    public string source {get;} = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    public string[] contributors {get;} = {"Russell Phillips"};



    public string defaultInstance {get;} = "((4,2,5,9,4,3),(9,13,2,17,21,16),(1,4,3,2,5,8),4)";

    public string instance {get;set;} = string.Empty;
    private List<int> _T = new List<int>();
    private List<int> _D = new List<int>();
    private List<int> _P = new List<int>();
    private int _K;

    

    public string wikiName {get;} = "";
    public JobSeqBruteForce defaultSolver {get;} = new JobSeqBruteForce();
    public JobSeqVerifier defaultVerifier {get;} = new JobSeqVerifier();

    // --- Properties ---
    public List<int> T {
        get {
            return _T;
        }
        set {
            _T = value;
        }
    }

    public List<int> D {
        get {
            return _D;
        }
        set {
            _D = value;
        }
    }

    public List<int> P {
        get {
            return _P;
        }
        set {
            _P = value;
        }
    }

    public int K {
        get {
            return _K;
        }
        set {
            _K = value;
        }
    }

    // --- Methods Including Constructors ---
    public JOBSEQ() {
        instance = defaultInstance;
        T = getT(instance);
        D = getD(instance);
        P = getP(instance);
        K = getK(instance);
    }
    public JOBSEQ(string instance) {
        instance = instance;
        T = getT(instance);
        D = getD(instance);
        P = getP(instance);
        K = getK(instance);
    }
    private List<int> getT(string instance)
    {
        return instance.TrimStart('(')
                            .TrimStart('(')
                            .Split("),(")[0]
                            .Split(',')
                            .Select(int.Parse)
                            .ToList();
        
    }

    private List<int> getD(string instance)
    {
        return instance.TrimStart('(')
                            .TrimStart('(')
                            .Split("),(")[1]
                            .Split(',')
                            .Select(int.Parse)
                            .ToList();
    }

    private List<int> getP(string instance)
    {
        return instance.TrimStart('(')
                            .TrimStart('(')
                            .Split("),(")[2]
                            .Split("),")[0]
                            .Split(',')
                            .Select(int.Parse)
                            .ToList();
    }

    private int getK(string instance) {
        return Int32.Parse(instance.TrimStart('(')
                            .TrimStart('(')
                            .Split("),(")[2]
                            .Split("),")[1]
                            .TrimEnd(')'));
        
    }

}