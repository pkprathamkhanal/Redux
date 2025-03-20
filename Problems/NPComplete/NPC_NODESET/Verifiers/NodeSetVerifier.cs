using API.Interfaces;
using API.Interfaces.Graphs.GraphParser;

namespace API.Problems.NPComplete.NPC_NODESET.Verifiers;

class NodeSetVerifier : IVerifier<NODESET> {

    // --- Fields ---
    public string verifierName {get;} = "Node Set Verifier";
    public string verifierDefinition {get;} = "This is a verifier for the Node Set problem";
    public string source {get;} = "";
    public string[] contributors {get;} = {"Andrija Sevaljevic"};


    private string _certificate =  "";

      public string certificate {
        get {
            return _certificate;
        }
    }


    // --- Methods Including Constructors ---
    public NodeSetVerifier() {
        
    }
    
    public string toEdges(string certificate, NODESET problem) {
        List<string> nodes = certificate.Replace("{","").Replace("}","").Split(',').ToList();
        string certificateEdges = "{";
        foreach(var i in problem.edges) {
            if(nodes.Contains(i.Key) || nodes.Contains(i.Value)) {
                certificateEdges += "(" + i.Key + ',' + i.Value + "),";
            }
        }
        return certificateEdges.TrimEnd(',') + '}';
    }

    public bool verify(NODESET problem, string certificate){
        
        NodeSetGraph graph = problem.nodeSetAsGraph; 
        string userInput = toEdges(certificate, problem);

        //Checks if certificate matches k-value;
        graph.processCertificate(userInput);
        bool isACyclical = true;
        for(int i=0; i<graph.getNodeList.Count; i++){
            if(graph.isCyclical(i)){
                isACyclical = false;
            }
        }
        graph.reverseCertificate(userInput);
        //when userInput is removed from graph is it no longer Cyclical? 
        return isACyclical;
    }
}