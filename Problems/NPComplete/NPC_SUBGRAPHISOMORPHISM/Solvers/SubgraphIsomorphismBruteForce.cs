using API.Interfaces;
using API.Interfaces.Graphs;
using System.Data;
using System.Diagnostics;
using System.Text.Json;

namespace API.Problems.NPComplete.NPC_SUBGRAPHISOMORPHISM.Solvers;
class SubgraphIsomorphismBruteForce : ISolver
{

    // --- Fields ---
    private string _solverName = "Subgraph Isomorphism Brute Force Solver";
    private string _solverDefinition = "This is a brute force solver for the NP-Complete Subgraph Isomorphism problem";
    private string _source = "TODO";
    private string[] _contributers = { "TODO" };

    // --- Properties ---
    public string solverName
    {
        get
        {
            return _solverName;
        }
    }
    public string solverDefinition
    {
        get
        {
            return _solverDefinition;
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

    // public string[] contributors => throw new NotImplementedException();

    // --- Methods Including Constructors ---

    public SubgraphIsomorphismBruteForce() { }

    public string solve(SUBGRAPHISOMORPHISM subgraph)
    {
        // TODO: implement Subgraph isomorphism Solver for Subgraph isomorphism
        Dictionary<int, int> mapping = new Dictionary<int, int>();
        mapping = FindSubgraphIsomorphism(subgraph.nodesP, subgraph.edgesP, subgraph.nodesT, subgraph.edgesT);
        if(mapping != null && mapping.Count > 0){
            var options = new JsonSerializerOptions { WriteIndented = true };
            Dictionary <string, string> mapped_pair = new Dictionary<string, string>();
            foreach (KeyValuePair<int, int> pair in mapping){
                string patternNode = subgraph.nodesP[pair.Key];
                string targetNode = subgraph.nodesT[pair.Value];
                mapped_pair[patternNode] = targetNode;
            }
            string jsonString = JsonSerializer.Serialize(mapped_pair, options);
            return jsonString;
        }
        return "{}";        
    }
    public static Dictionary<int, int> FindSubgraphIsomorphism(
            List<string> nodes1,
            List<KeyValuePair<string, string>> edges1,
            List<string> nodes2,
            List<KeyValuePair<string, string>> edges2)
            // nodes1 -> nodes of pattern graph
            // nodes2 -> nodes of target graph
            // edges1 -> nodes of pattern edges
            // edges2 -> nodes of target edges
    {
        int n1 = nodes1.Count;  // Number of vertices in graph1
        int n2 = nodes2.Count;  // Number of vertices in graph2

        if (n1 > n2)
        {
            return null;  // pattern cannot be a subgraph if it's larger than target graph
        }

        // Helper function to check if a mapping is valid
        bool IsMappingValid(Dictionary<int, int> mapping)
        {
            foreach (var edge in edges1)
            {
                int source1 = nodes1.IndexOf(edge.Key);
                int target1 = nodes1.IndexOf(edge.Value);

                if (!mapping.ContainsKey(source1) || !mapping.ContainsKey(target1))
                    return false;

                int source2 = mapping[source1];
                int target2 = mapping[target1];

                // Check if the corresponding edge exists in graph2
                if (!edges2.Any(e =>
                    (e.Key == nodes2[source2] && e.Value == nodes2[target2]) ||
                    (e.Key == nodes2[target2] && e.Value == nodes2[source2])))
                {
                    return false;  // Edge mismatch
                }
            }
            return true;
        }

        // Generate all permutations of n2 taken n1 at a time
        var permutations = Permutations(Enumerable.Range(0, n2), n1);

        foreach (var permutation in permutations)
        {
            var mapping = Enumerable.Range(0, n1).ToDictionary(i => i, i => permutation.ElementAt(i));
            if (IsMappingValid(mapping))
            {
                return mapping;  // Found a valid isomorphism - return the mapping
            }
        }

        return null;  // No valid isomorphism found
    }

    // Helper function to generate permutations
    private static IEnumerable<IEnumerable<int>> Permutations(IEnumerable<int> list, int length)
    {
        if (length == 1) return list.Select(t => new[] { t });

        return Permutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                        (t1, t2) => t1.Concat(new[] { t2 }));
    }
}