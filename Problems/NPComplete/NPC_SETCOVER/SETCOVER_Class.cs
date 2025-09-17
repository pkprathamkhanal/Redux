using API.Interfaces;
using API.Problems.NPComplete.NPC_SETCOVER.Solvers;
using API.Problems.NPComplete.NPC_SETCOVER.Verifiers;
using Microsoft.AspNetCore.Http.Features;
using SPADE;

namespace API.Problems.NPComplete.NPC_SETCOVER;

class SETCOVER : IProblem<SetCoverBruteForce,SetCoverVerifier> {

    // --- Fields ---
    public string problemName {get;} = "Set Cover";
    public string formalDefinition {get;} = "Sub Cover = {<S,T,k> | S is a set of elements, and there exists a grouping of k T subsetse equal to S}";
    public string problemDefinition {get;} = "Given a set of elements and a collection S of m sets whose union equals the universe, the set cover problem is to identify the smallest sub-collection of S whose union equals the universe";
    public string source {get;} = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";

    private static string _defaultInstance = "{{1,2,3,4,5},{{1,2,3},{2,4},{3,4},{4,5}},3}";
    public string defaultInstance { get; } = _defaultInstance;
    public string instance {get;set;} = string.Empty;
    private List<string> _universal = new List<string>();
    private List<List<string>> _subsets = new List<List<string>>();

    public string wikiName {get;} = "";
    private int _K = 3;
    public SetCoverBruteForce defaultSolver {get;} = new SetCoverBruteForce();
    public SetCoverVerifier defaultVerifier {get;} = new SetCoverVerifier();
    public string[] contributors {get;} = { "Andrija Sevaljevic" };

    // --- Properties ---
    public List<string> universal {
        get {
            return _universal;
        }
        set {
            _universal = value;
        }
    }

    public List<List<string>> subsets {
        get {
            return _subsets;
        }
        set {
            _subsets = value;
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
    public SETCOVER() : this(_defaultInstance) {
    }
    public SETCOVER(string GInput) {

        StringParser cliqueGraph = new("{(U,S,K) | U is set S subset {a | a subset U}}");
        cliqueGraph.parse(GInput);
        universal = cliqueGraph["N"].ToList().Select(node => node.ToString()).ToList();
        subsets = cliqueGraph["S"].ToList().Select(subset => subset.ToList().Select(item => item.ToString()).ToList()).ToList();
        _K = int.Parse(cliqueGraph["K"].ToString());


    }


    public List<string> getUniversalSet(string Ginput) {

        List<string> allElements = new List<string>();
        List<string> seperation = Ginput.Split("},{{").ToList();
        string sections = seperation[0];
        allElements = sections.Replace("{","").Split(',').ToList();

        return allElements;
    }
    public List<List<string>> getSubsets(string Ginput) {
        List<string> allSets = new List<string>();
        List<string> seperation = Ginput.Split("},{{").ToList();
        string sections = seperation[1];
        List<string> seperation2 = sections.Split("}},").ToList();
        string sections2 = seperation2[0];
        allSets = sections2.Split("},{").ToList();

        List<List<string>> subsets = new List<List<string>>();
        foreach(var i in allSets) {
            subsets.Add(i.Split(',').ToList());
        }

        return subsets;      
    }

    public int getK(string Ginput) {
        List<string> sections = Ginput.Split("},{{").ToList();
        sections = sections[1].Split("}},").ToList();
        return Int32.Parse(sections[1].Replace("}",""));
    }


}