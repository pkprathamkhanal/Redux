using System;
using System.Collections.Generic;
using System.Linq;
using API.Interfaces;
using API.Interfaces.Graphs.GraphParser;
using API.Problems.NPComplete.NPC_SUBGRAPHISOMORPHISM;

namespace API.Problems.NPComplete.NPC_CLIQUE.ReduceTo.NPC_SubgraphIsomorphism
{
    // class name should be same as the filename
    class GreeksForGreeksReduceToSGI : IReduction<CLIQUE, SUBGRAPHISOMORPHISM>
    {
        // --- Fields ---
        private string _reductionName = "Clique to Subgraph Isomorphism Reduction";
        private string _reductionDefinition = @"This reduction converts the Clique problem into a Subgraph Isomorphism problem. 
                                                We construct a complete graph G1 with k vertices and check if it is isomorphic 
                                                to a subgraph of G2 (the original input graph).";
        private string _source = "Reduction inspired by computational complexity theory.";
        private string[] _contributors = { };

        private Dictionary<object, object> _gadgetMap = new Dictionary<object, object>();
        private CLIQUE _reductionFrom;
        private SUBGRAPHISOMORPHISM _reductionTo;
        private string _complexity = "";

        // --- Properties ---
        public string reductionName => _reductionName;
        public string reductionDefinition => _reductionDefinition;
        public string source => _source;
        public string[] contributors => _contributors;
        public Dictionary<object, object> gadgetMap { get => _gadgetMap; set => _gadgetMap = value; }
        public CLIQUE reductionFrom { get => _reductionFrom; set => _reductionFrom = value; }
        public SUBGRAPHISOMORPHISM reductionTo { get => _reductionTo; set => _reductionTo = value; }

        // --- Constructor ---
        public GreeksForGreeksReduceToSGI(CLIQUE from)
        {
            _reductionFrom = from;
            _reductionTo = reduce();
        }

        /// <summary>
        /// Reduces a CLIQUE instance to a SUBGRAPH ISOMORPHISM instance.
        /// </summary>
        /// <returns> A Subgraph Isomorphism instance </returns>
        public SUBGRAPHISOMORPHISM reduce()
        {
            CLIQUE cliqueInstance = _reductionFrom;
            SUBGRAPHISOMORPHISM reducedSubgraphIso = new SUBGRAPHISOMORPHISM();

            // Step 1: Construct G1 (a complete graph of size k)
            // List<string> cliqueNodes = cliqueInstance.nodes.Take(cliqueInstance.K).ToList();
            // List<KeyValuePair<string, string>> cliqueEdges = new List<KeyValuePair<string, string>>();

            // // Generate all possible edges for a complete graph of size k
            // for (int i = 0; i < cliqueNodes.Count; i++)
            // {
            //     for (int j = i + 1; j < cliqueNodes.Count; j++)
            //     {
            //         cliqueEdges.Add(new KeyValuePair<string, string>(cliqueNodes[i], cliqueNodes[j]));
            //     }
            // }

            int k = cliqueInstance.K;
            List<string> cliqueNodes = new List<string>();

            // Generate node names: A, B, C, ...
            for (int i = 0; i < k; i++)
            {
                char nodeName = (char)('A' + i);
                cliqueNodes.Add(nodeName.ToString());
            }

            Console.WriteLine("Clique Nodes:");
            foreach (var node in cliqueNodes)
            {
                Console.WriteLine(node);
            }

            // Now construct edges for a complete graph (G1)
            List<KeyValuePair<string, string>> cliqueEdges = new List<KeyValuePair<string, string>>();
            for (int i = 0; i < cliqueNodes.Count; i++)
            {
                for (int j = i + 1; j < cliqueNodes.Count; j++)
                {
                    cliqueEdges.Add(new KeyValuePair<string, string>(cliqueNodes[i], cliqueNodes[j]));
                }
            }

            Console.WriteLine("\nClique Edges:");
            foreach (var edge in cliqueEdges)
            {
                Console.WriteLine($"{edge.Key} - {edge.Value}");
            }

            // Step 2: Assign values to the Subgraph Isomorphism problem
            // reducedSubgraphIso.patternNodes = cliqueNodes;
            // reducedSubgraphIso.patternEdges = cliqueEdges;
            // reducedSubgraphIso.targetNodes = cliqueInstance.nodes;  // Full graph nodes
            // reducedSubgraphIso.targetEdges = cliqueInstance.edges;  // Full graph edges

            reducedSubgraphIso.nodesP = cliqueNodes;
            reducedSubgraphIso.edgesP = cliqueEdges;
            reducedSubgraphIso.nodesT = cliqueInstance.nodes;  // Full graph nodes
            reducedSubgraphIso.edgesT = cliqueInstance.edges;

            // Step 3: Generate the instance string
            // string patternNodesString = string.Join(",", reducedSubgraphIso.patternNodes);
            // string patternEdgesString = string.Join(",", reducedSubgraphIso.patternEdges.Select(e => $"{{{e.Key},{e.Value}}}"));

            // string targetNodesString = string.Join(",", reducedSubgraphIso.targetNodes);
            // string targetEdgesString = string.Join(",", reducedSubgraphIso.targetEdges.Select(e => $"{{{e.Key},{e.Value}}}"));

            string patternNodesString = string.Join(",", reducedSubgraphIso.nodesP);
            string patternEdgesString = string.Join(",", reducedSubgraphIso.edgesP.Select(e => $"{{{e.Key},{e.Value}}}"));

            string targetNodesString = string.Join(",", reducedSubgraphIso.nodesT);
            string targetEdgesString = string.Join(",", reducedSubgraphIso.edgesT.Select(e => $"{{{e.Key},{e.Value}}}"));

            string G = "(({" + targetNodesString + "},{" + targetEdgesString + "}),({" + patternNodesString + "},{" + patternEdgesString + "}))";

            // string G = $"(({{targetNodesString}},{{{targetEdgesString}}}),({{patternNodesString}},{{{patternEdgesString}}}))";
            Console.WriteLine("G string " + G);
            reducedSubgraphIso = new SUBGRAPHISOMORPHISM(G);
            _reductionTo = reducedSubgraphIso;
            return reducedSubgraphIso;
        }

        /// <summary>
        /// Maps the solution of the Clique problem to the Subgraph Isomorphism problem.
        /// </summary>
        public string mapSolutions(CLIQUE problemFrom, SUBGRAPHISOMORPHISM problemTo, string problemFromSolution)
        {
            Console.WriteLine("map solutions sgi problemFromSolution", problemFromSolution);
            // Verify the Clique solution first
            if (!problemFrom.defaultVerifier.verify(problemFrom, problemFromSolution))
            {
                return "Clique solution is incorrect: " + problemFromSolution;
            }

            // Parse solution into a list of nodes
            List<string> cliqueSolutionNodes = GraphParser.parseNodeListWithStringFunctions(problemFromSolution);

            // If we found a valid clique, we check if it exists in the isomorphism mapping
            List<string> mappedSolution = new List<string>();
            // foreach (string node in problemTo.patternNodes)
            foreach (string node in problemTo.nodesP)
            {
                if (cliqueSolutionNodes.Contains(node))
                {
                    mappedSolution.Add(node);
                }
            }

            // Convert to expected string format
            string problemToSolution = "{" + string.Join(",", mappedSolution) + "}";
            Console.WriteLine("problem to solution " + problemToSolution);
            return problemToSolution;
        }
    }
}
