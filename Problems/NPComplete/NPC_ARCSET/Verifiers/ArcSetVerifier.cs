using API.Interfaces;
using API.Interfaces.Graphs;
namespace API.Problems.NPComplete.NPC_ARCSET.Verifiers;


class ArcSetVerifier : IVerifier<ARCSET> {
    public string verifierDefinition {get;} =  @"This Verifier takes in an arcset problem and a list of edges to remove from that problem. It removes those edges and then checks if the problem is still an instance of ARCSET
                                            ie. Does this input graph no longer have cycles after these input edges are removed? Returns true or false ";
    
    public string source {get;} = "This verifier is essentially common knowledge, as it utilizes a widely recognized algorithm in computer science: The Depth First Search.";

    private string _certificate = "{(2,4)}"; //The certificate should be in the form of a set of directed edges
    public string[] contributors {get;} = {"Alex Diviney","Caleb Eardley"};

    // --- Properties ---
    public string verifierName { get; } = "Arc Set Verifier";
      public string certificate {
        get {
            return _certificate;
        }
    }



    public ArcSetVerifier(){

    }

    /**
    * This method should take in an arcset problem and a list of edges to remove from that problem. It removes those edges and then checks if the problem is still an instance of ARCSET
    * ie. Does this input graph continue to have cycles after these input edges are removed? 
    **/
    public bool verify(ARCSET problem, string certificate){

        ArcsetGraph graph = problem.directedGraph; 

        //Checks if certificate matches k-value;
        if(graph.cerfitficateLength(certificate) > graph.K){
            return false;
        }
        graph.processCertificate(certificate);
        bool isACyclical = true;
        for(int i=0; i<graph.getNodeList.Count; i++){
            if(graph.isCyclical(i)){
                isACyclical = false;
            }
        }
        graph.reverseCertificate(certificate);
        //when certificate is removed from graph is it no longer Cyclical? 
        return isACyclical;
    }
}