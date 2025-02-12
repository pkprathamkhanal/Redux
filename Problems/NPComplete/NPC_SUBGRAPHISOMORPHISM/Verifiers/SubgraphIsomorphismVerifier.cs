using API.Interfaces;

namespace API.Problems.NPComplete.NPC_SUBGRAPHISOMORPHISM.Verifiers;

class SubgraphIsomorphismVerifier : IVerifier {

    // --- Fields ---
    private string _verifierName = "Subgraph Isomorphism Verifier";
    private string _verifierDefinition = "This is a verifier for the NP-Complete Subgraph Isomorphism problem";
    private string _source = " ";
    private string[] _contributers = { "TODO" };
    private string _certificate =  "";

    // --- Properties ---
    public string verifierName {
        get {
            return _verifierName;
        }
    }
    public string verifierDefinition {
        get {
            return _verifierDefinition;
        }
    }
    public string source {
        get {
            return _source;
        }
    }
    public string[] contributors{
        get {
            return _contributers;
        }
    }
    public string certificate {
        get {
            return _certificate;
        }
    }

    // public string[] contributors => throw new NotImplementedException();

    // --- Methods Including Constructors ---

    public SubgraphIsomorphismVerifier() {
        
    }

    public bool verify(SUBGRAPHISOMORPHISM problem, string certificate){
        // TODO: implement Subgraph isomorphism Verifier for Subgraph isomorphism
        return true;
    }
}
