using API.Interfaces;
using API.Tools.UtilCollection;

namespace API.Problems.NPComplete.NPC_HITTINGSET.Verifiers;

class HittingSetVerifier : IVerifier<HITTINGSET> {

    // --- Fields ---
    public string verifierName {get;} = "Hitting Set Verifier";
    public string verifierDefinition {get;} = "This is a verifier for Hitting Set";
    public string source {get;} = " ";
    public string[] contributors {get;} = {"Russell Phillips"};


    private string _certificate =  "";

      public string certificate {
        get {
            return _certificate;
        }
    }


    // --- Methods Including Constructors ---
    public HittingSetVerifier() {
        
    }

    public IEnumerable<List<int>> possibleSolutions(int len)
    {
        for (int i = 0; i < Math.Pow(2, len); i++) 
        {
            List<int> solution = new List<int>(); 
            for (int solBin = i + (int)Math.Pow(2, len); solBin != 1; solBin >>= 1)
            {
                if ((solBin & 1) == 0)
                    solution.Add(0);
                else
                    solution.Add(1);
            }
            yield return solution;
        }
    }

    private List<string> parseCertificate(string certificate)
    {
        throw new NotImplementedException();
    }

    public bool verify(HITTINGSET problem, string certificate)
    {
        UtilCollection choosenSet = new UtilCollection(certificate);
        foreach (UtilCollection set in problem.subSets)
        {
            if (set.Intersect(choosenSet).Count() != 1)
                return false;
        }
        return true;
    }
}