using API.Interfaces;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace API.Problems.NPComplete.NPC_SUBGRAPHISOMORPHISM.Solvers;
class SubgraphIsomorphismUllmann : ISolver
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




        int m = adjacentMatrixPattern.GetLength(0);
        int n = adjacentMatrixTarget.GetLength(0);
        bool[,] candidateMatrix = new bool[m, n];

        // initializing the candidate matrix
        candidateMatrix = initializeCandidateMatrix(m, n);

        // Console.WriteLine("index of candidateMatrix "+ candidateMatrix.GetLength(0) + " , "+  candidateMatrix.GetLength(1));
        // Console.WriteLine("****************************************************");
        Console.WriteLine("Candidate matrix");
        displayBoolMatrix(candidateMatrix);
        Console.WriteLine("****************************************************");

        while (Refine(candidateMatrix, adjacentMatrixTarget, adjacentMatrixPattern)) ;

        bool result = Backtrack(0, candidateMatrix, adjacentMatrixTarget, adjacentMatrixPattern);
        Dictionary<int, int> mapping = result ? GetMapping(candidateMatrix) : null;

        if (result)
        {
            Console.WriteLine("The pattern graph is a subgraph of the target graph.");
            Console.WriteLine("Mapping from pattern graph vertices to target graph vertices:");
            foreach (var kvp in mapping)
            {
                Console.WriteLine($"Pattern vertex {kvp.Key} -> Target vertex {kvp.Value}");
            }

            Console.WriteLine("\nDetailed adjacency information:");
            foreach (var kvp in mapping)
            {
                Console.WriteLine($"Pattern vertex V{kvp.Key}:");
                for (int i = 0; i < adjacentMatrixPattern.GetLength(0); i++)
                {
                    if (adjacentMatrixPattern[kvp.Key, i] == 1)
                    {
                        Console.WriteLine($"  - Connected to V{i} in pattern, which maps to V{mapping[i]} in target");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("The pattern graph is not a subgraph of the target graph.");
        }
        // return result;
        return "{}";
    }

    private bool Refine(bool[,] M, int[,] targetGraph, int[,] patternGraph)
    // int [,] M, int [,] targetGraph, int [,] patternGraph, int m, int n
    {
        int m = patternGraph.GetLength(0);
        int n = targetGraph.GetLength(0);

        bool changed = false;

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (M[i, j])
                {
                    for (int k = 0; k < m; k++)
                    {
                        if (patternGraph[i, k] == 1)
                        {
                            int rowSum = Enumerable.Range(0, n).Sum(l => M[k, l] ? targetGraph[j, l] : 0);
                            if (rowSum == 0)
                            {
                                M[i, j] = false;
                                changed = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        Console.WriteLine("Inside refine procedure : M ");
        displayBoolMatrix(M);
        return changed;

    }

    private bool Backtrack(int depth, bool[,] M, int[,] targetGraph, int[,] patternGraph)
    // int [,] M, int [,] targetGraph, int [,] patternGraph, int m, int n,
    {
        int m = patternGraph.GetLength(0);
        int n = targetGraph.GetLength(0);

        if (depth == m)
        {
            return IsIsomorphic(M, targetGraph, patternGraph);
            // M, targetGraph, patternGraph, m, n
        }
        for (int i = 0; i < n; i++)
        {
            if (M[depth, i])
            {
                bool[] tempRow = new bool[n];
                for (int j = 0; j < n; j++)
                {
                    tempRow[j] = M[depth, j];
                    M[depth, j] = false;
                }
                M[depth, i] = true;

                if (Backtrack(depth + 1, M, targetGraph, patternGraph))
                {
                    return true;
                }

                for (int j = 0; j < n; j++)
                {
                    M[depth, j] = tempRow[j];
                }
            }
        }

        return false;
    }

    private bool IsIsomorphic(bool[,] M, int[,] targetGraph, int[,] patternGraph)
    //  int [,] M, int [,] targetGraph, int [,] patternGraph, int m, int n
    {
        int m = patternGraph.GetLength(0);
        int n = targetGraph.GetLength(0);
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (patternGraph[i, j] == 1)
                {
                    bool found = false;
                    for (int k = 0; k < n && !found; k++)
                    {
                        if (M[i, k])
                        {
                            for (int l = 0; l < n; l++)
                            {
                                if (M[j, l] && targetGraph[k, l] == 1)
                                {
                                    found = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!found) return false;
                }
            }
        }
        return true;
    }

    // private int[] GetRow(int[,] matrix, int row)
    // {
    //     return Enumerable.Range(0, matrix.GetLength(1)).Select(i => matrix[row, i]).ToArray();
    // }

    private Dictionary<int, int> GetMapping(bool[,] M)
    // int [,] M, int m, int n
    {
        int m = M.GetLength(0);
        int n = M.GetLength(1);

        var mapping = new Dictionary<int, int>();
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (M[i, j])
                {
                    mapping[i] = j;
                    break;
                }
            }
        }
        return mapping;
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

    // Helper function
    public bool[,] initializeCandidateMatrix(int m, int n)
    {
        bool[,] M = new bool[m, n];
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                M[i, j] = true;  // Initially allow all mappings
            }
        }
        return M;
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

    public static void displayBoolMatrix(bool[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                // Display '1' for True and '0' for False
                Console.Write(matrix[i, j] ? "1 " : "0 ");
            }
            Console.WriteLine();  // Move to the next row
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

    public UllmannAlgorithm(int[,] targetGraph, int[,] patternGraph)
    {
        this.target = targetGraph;
        this.pattern = patternGraph;
        this.targetSize = targetGraph.GetLength(0);
        this.patternSize = patternGraph.GetLength(0);
        this.M = new int[targetSize, patternSize];

        InitializeMappingMatrix();
    }

    // Initialize the mapping matrix based on node degrees
    private void InitializeMappingMatrix()
    {
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
        int degree = 0;
        for (int i = 0; i < matrix.GetLength(1); i++)
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
            return IsIsomorphism();
        }

        for (int j = 0; j < targetSize; j++)
        {
            if (M[depth, j] == 1)
            {
                int[,] savedM = (int[,])M.Clone(); // Save current state
                AssignMapping(depth, j);
                mapping[depth] = j;

                if (Ullmann(depth + 1))
                    return true; // If isomorphism found, return true

                M = savedM; // Restore state for backtracking
                mapping.Remove(depth); // Remove mapping on backtracking
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
    private void PrintMapping()
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
}