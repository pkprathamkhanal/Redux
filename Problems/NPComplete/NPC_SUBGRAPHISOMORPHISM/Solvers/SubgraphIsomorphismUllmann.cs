using API.Interfaces;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;


namespace API.Problems.NPComplete.NPC_SUBGRAPHISOMORPHISM.Solvers;
class SubgraphIsomorphismUllmann : ISolver<SUBGRAPHISOMORPHISM>
{

    // --- Fields ---
    private string _solverName = "Subgraph  Isomorphism Solver Based On Ullmann's Algorithm";
    private string _solverDefinition = "This is a solver for the NP-Complete Subgraph Isomorphism problem using Ullmann's algorithm";
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

    public SubgraphIsomorphismUllmann() { }

    public string solve(SUBGRAPHISOMORPHISM subgraph)
    {

        // TODO: implement Subgraph isomorphism Solver for Subgraph isomorphism
        // Given TGraph(N,E) PGraph(N,E) _k

        //computing the adjacent matrix of target graph
        int[,] adjacentMatrixTarget = getAdjacentMatrix(subgraph.nodesT, subgraph.edgesT);
        displayMatrix(adjacentMatrixTarget);

        // computing the adjacent matrix for pattern matrix 
        int[,] adjacentMatrixPattern = getAdjacentMatrix(subgraph.nodesP, subgraph.edgesP);
        displayMatrix(adjacentMatrixPattern);


        // call Ullmann Algorithm class here
        UllmannAlgorithm ull = new UllmannAlgorithm(adjacentMatrixTarget, adjacentMatrixPattern);

        bool result = ull.Ullmann(0);

        if (result)
        {
            Dictionary<int, int> mapping = ull.getMapping();
            if (mapping != null && mapping.Count > 0)
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                Dictionary<string, string> mapped_pair = new Dictionary<string, string>();
                foreach (KeyValuePair<int, int> pair in mapping)
                {
                    string patternNode = subgraph.nodesP[pair.Key];
                    string targetNode = subgraph.nodesT[pair.Value];
                    mapped_pair[patternNode] = targetNode;
                }
                string jsonString = JsonSerializer.Serialize(mapped_pair, options);
                return jsonString;
            }
        }
        return "{}";
    }

    public int[,] getAdjacentMatrix(List<string> nodes, List<KeyValuePair<string, string>> edges)
    {
        int[,] adjacencyMatrix = new int[nodes.Count, nodes.Count];

        // Fill the adjacency matrix based on the edges
        foreach (var edge in edges)
        {
            int i = nodes.IndexOf(edge.Key);   // Get index of first vertex in the edge
            int j = nodes.IndexOf(edge.Value); // Get index of second vertex in the edge

            adjacencyMatrix[i, j] = 1;  // Mark edge between i and j
            adjacencyMatrix[j, i] = 1;  // Since the graph is undirected, mark j and i as well
        }
        return adjacencyMatrix;
    }

    public static void displayMatrix(int[,] matrix)
    {
        int row = matrix.GetLength(0);
        int col = matrix.GetLength(1);
        // Print each row of the matrix
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}

class UllmannAlgorithm
{
    private int[,] target;
    private int[,] pattern;

    private int[,] M;
    private int targetSize;
    private int patternSize;

    private Dictionary<int, int> mapping;

    private HashSet<int> usedNodes;

    public UllmannAlgorithm(int[,] targetGraph, int[,] patternGraph)
    {
        this.target = targetGraph;
        this.pattern = patternGraph;
        this.targetSize = targetGraph.GetLength(0);
        this.patternSize = patternGraph.GetLength(0);
        this.M = new int[patternSize, targetSize];
        this.mapping = new Dictionary<int, int>();
        this.usedNodes = new HashSet<int>();

        InitializeMappingMatrix();
    }

    // Initialize the mapping matrix based on node degrees
    private void InitializeMappingMatrix()
    {
        if (M == null)
            throw new NullReferenceException("Mapping matrix M is not initialized.");

        for (int i = 0; i < patternSize; i++)
        {
            for (int j = 0; j < targetSize; j++)
            {
                // A node in pattern can only be mapped to a node in graph if it has enough neighbors
                if (Degree(pattern, i) <= Degree(target, j))
                    M[i, j] = 1;
                else
                    M[i, j] = 0;
            }
        }
    }

    // Function to compute the degree of a node
    private int Degree(int[,] matrix, int node)
    {
        if (matrix == null)
            throw new NullReferenceException("Degree function received a null matrix.");

        int degree = 0;
        // for (int i = 0; i < matrix.GetLength(1); i++)
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            if (matrix[node, i] == 1)
                degree++;
        }
        return degree;
    }

    // Recursive backtracking to find isomorphism
    public bool Ullmann(int depth)
    {
        if (depth == patternSize) // If all pattern nodes are assigned, check isomorphism
        {
            // return IsIsomorphism();
            if (IsIsomorphism())
            {
                PrintMapping();
                return true;
            }
            return false;
        }

        for (int j = 0; j < targetSize; j++)
        {
            if (M[depth, j] == 1 && !usedNodes.Contains(j)) // Ensure unique mapping
            {
                int[,] savedM = (int[,])M.Clone(); // Save current state
                AssignMapping(depth, j);
                mapping[depth] = j;
                usedNodes.Add(j); // Mark node as used

                if (Ullmann(depth + 1))
                    return true; // If isomorphism found, return true

                M = savedM; // Restore state for backtracking
                mapping.Remove(depth); // Remove mapping on backtracking
                usedNodes.Remove(j); // Unmark for backtracking
            }
        }
        return false;
    }

    // Assign mapping of pattern node to a graph node
    private void AssignMapping(int row, int col)
    {
        for (int j = 0; j < targetSize; j++)
            M[row, j] = 0; // Clear previous assignments

        M[row, col] = 1; // Assign new mapping
    }

    // Validate if current mapping is an isomorphism
    private bool IsIsomorphism()
    {
        for (int i = 0; i < patternSize; i++)
        {
            for (int j = 0; j < patternSize; j++)
            {
                if (pattern[i, j] == 1)
                {
                    int mappedI = GetMappedNode(i);
                    int mappedJ = GetMappedNode(j);

                    if (mappedI == -1 || mappedJ == -1 || target[mappedI, mappedJ] == 0)
                        return false;
                }
            }
        }
        return true;
    }

    // Get the mapped node in the graph for a pattern node
    private int GetMappedNode(int row)
    {
        for (int j = 0; j < targetSize; j++)
        {
            if (M[row, j] == 1)
                return j;
        }
        return -1;
    }

    // Print the mapping when an isomorphism is found
    public void PrintMapping()
    {
        Console.WriteLine("Subgraph isomorphism found!");
        Console.WriteLine("Mapped Nodes (Pattern -> Graph):");
        foreach (var pair in mapping)
        {
            Console.WriteLine($"Pattern Node {pair.Key} -> Graph Node {pair.Value}");
        }

        Console.WriteLine("Mapped Edges:");
        for (int i = 0; i < patternSize; i++)
        {
            for (int j = i + 1; j < patternSize; j++)
            {
                if (pattern[i, j] == 1)
                {
                    int mappedI = mapping[i];
                    int mappedJ = mapping[j];
                    Console.WriteLine($"({mappedI}, {mappedJ}) in Graph");
                }
            }
        }
    }

    public Dictionary<int, int> getMapping()
    {
        return this.mapping;
    }
}

