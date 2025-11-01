using API.Interfaces;

namespace API.Problems.NPComplete.NPC_SUBGRAPHISOMORPHISM.Verifiers;

class SubgraphIsomorphismVerifier : IVerifier<SUBGRAPHISOMORPHISM>
{

    // --- Fields ---
    private string _verifierName = "Subgraph Isomorphism Verifier";
    private string _verifierDefinition = "This is a verifier for the NP-Complete Subgraph Isomorphism problem";
    private string _source = " ";
    private string[] _contributers = { "Sabal Subedi" };
    private string _certificate = "W : b, X : c, Y : e, Z : d";

    // --- Properties ---
    public string verifierName
    {
        get
        {
            return _verifierName;
        }
    }
    public string verifierDefinition
    {
        get
        {
            return _verifierDefinition;
        }
    }
    public string source
    {
        get
        {
            return _source;
        }
    }
    public string[] contributors
    {
        get
        {
            return _contributers;
        }
    }
    public string certificate
    {
        get
        {
            return _certificate;
        }
    }

    // public string[] contributors => throw new NotImplementedException();

    // --- Methods Including Constructors ---

    public SubgraphIsomorphismVerifier()
    {

    }

    public bool verify(SUBGRAPHISOMORPHISM problem, string certificate)
    {
        // Parse the certificate into a dictionary: pattern graph -> target graph. e.g. "P1:Ta, P2:Tb" => { "P1": "Ta", "P2": "Tb" }
        Dictionary<string, string> mapping = new Dictionary<string, string>();
        var mappings = certificate.Split(",", StringSplitOptions.RemoveEmptyEntries);

        foreach (var item in mappings)
        {
            var pair = item.Split(":", StringSplitOptions.RemoveEmptyEntries);

            if (pair.Length != 2)
            {
                return false;
            }

            string hNode = pair[0].Trim().Trim('"', '\'');
            string gNode = pair[1].Trim().Trim('"', '\'');

            if (!problem.nodesP.Contains(hNode))
            {
                return false;
            }

            if (!problem.nodesT.Contains(gNode))
            {
                return false;
            }

            if (mapping.ContainsKey(hNode))
            {
                return false;
            }

            mapping[hNode] = gNode;
        }

        if (mapping.Count != problem.nodesP.Count)
        {
            return false;
        }

        var mappedGNodes = mapping.Values.ToList();
        if (mappedGNodes.Count != mappedGNodes.Distinct().Count())
        {
            return false;
        }

        // Verify that every edge in Pattern Graph is mapped to an edge in Target Graph.
        var edgeSetG = new HashSet<(string, string)>(
            problem.edgesT.Select(e => (e.Key, e.Value))
        );
        // If undirected, also include reversed edges in the set
        edgeSetG.UnionWith(problem.edgesT.Select(e => (e.Value, e.Key)));

        foreach (var edgeH in problem.edgesP)
        {
            string mappedNode1 = mapping[edgeH.Key];
            string mappedNode2 = mapping[edgeH.Value];

            // Check forward or backward in Target Graph's edge set
            var forward = (mappedNode1, mappedNode2);
            var backward = (mappedNode2, mappedNode1);

            if (!edgeSetG.Contains(forward) && !edgeSetG.Contains(backward))
            {
                return false;
            }
        }

        // Certificate is a valid subgraph isomorphism.
        return true;
    }

}
