// TODO: implement graph class if Dominating Set is a graphing problem
 using System;
 using System.Collections;
 using System.Collections.Generic;
 using System.Text.Json;
 using System.Text.Json.Serialization;
 using System.Text.RegularExpressions;
 using API.Interfaces.Graphs;
 using System.Linq;




namespace API.Problems.NPComplete.NPC_DOMINATINGSET
{
    /// <summary>
    /// Parses/represents a Dominating-Set graph instance.
    /// Canonical string form expected by this parser:
    ///   (({v1,v2,...},{ {u1,v1},{u2,v2},... }),K)
    /// Example:
    ///   (({0,1,2,3,4},{{1,0},{0,3},{1,2},{2,4},{1,3},{3,4},{4,1}}),5)
    /// </summary>
    class DominatingSetGraph : UnweightedUndirectedGraph
    {


        /// <summary>
        /// Preferred constructor that validates & parses the Dominating Set instance string.
        /// Expected format: (({v1,v2,...},{{a,b},{c,d},...}),K)
        /// </summary>
        public DominatingSetGraph(string input, bool usingCliqueNodes)
        {
            // Regex validating the DS format (nodes, edges, K). Terminals are not part of this problem.
            // (({NODES},{EDGES}),K)
            string pattern = @"\(\(\{([\w!]+)(,([\w!]+))*\},\{\{([\w!]+),([\w!]+)\}(,\{([\w!]+),([\w!]+)\})*\}\),\d+\)";
            var reg = new Regex(pattern);
            bool inputIsValid = reg.IsMatch(input);


            //Invalid input
            if (!inputIsValid)
            {
                Console.WriteLine("NOT VALID INPUT for Regex evaluation! INITIALIZATION FAILED");
                return;
            }

            // ---- NODES ----
            // First {...} in the string is the node set
            string nodePattern = @"{((([\w!]+))*(([\w!]+),)*)+}";
            MatchCollection nMatches = Regex.Matches(input, nodePattern);
            string nodeStr = nMatches[0].ToString();
            nodeStr = nodeStr.TrimStart('{');
            nodeStr = nodeStr.TrimEnd('}');
            string[] nodeStringList = nodeStr.Split(',');

            foreach (string nodeName in nodeStringList)
            {
                _nodeList.Add(new DominatingSetNode(nodeName, string.Empty));
            }

            // ---- EDGES ----
            // Find the set that contains "{u,v}" pairs
            string edgeSetPattern = @"{(\{([\w!]+),([\w!]+)\}(,\{([\w!]+),([\w!]+)\})*)*}";
            MatchCollection eMatches = Regex.Matches(input, edgeSetPattern);
            if (eMatches.Count == 0) { Console.WriteLine("No edge set found"); return; }

            string edgeStr = eMatches[eMatches.Count - 1].Value;

            string edgePairPattern = @"([\w!]+),([\w!]+)";
            MatchCollection ePairs = Regex.Matches(edgeStr, edgePairPattern);
            foreach (Match m in ePairs)
            {
                string[] ends = m.Value.Split(',');
                Node n1 = new DominatingSetNode(ends[0], string.Empty);
                Node n2 = new DominatingSetNode(ends[1], string.Empty);
                _edgeList.Add(new Edge(n1, n2));
            }

            //K

            string endNumPatternOuter =  @"\),\d+\)$";
            Match numTail = Regex.Match(input, endNumPatternOuter);
            if (!numTail.Success) { Console.WriteLine("No K found"); return; }

            Match kMatch = Regex.Match(numTail.Value, @"\d+");
            if (!kMatch.Success) { Console.WriteLine("No K found"); return; }
            _K = int.Parse(kMatch.Value);

            // Populate string lists for nodes and edges
            foreach (Node n in _nodeList) _nodeStringList.Add(n.name);
            foreach (Edge e in _edgeList)
                _edgesKVP.Add(new KeyValuePair<string, string>(e.source.name, e.target.name));
        }


        public List<string> nodesStringList => _nodeStringList;
        public List<KeyValuePair<string, string>> edgesTuple => _edgesKVP;
        public int K => _K;

        public override string ToString()
        {
            string nodesJoined = string.Join(",", _nodeStringList);
            string edgesJoined = string.Join(",", _edgesKVP.Select(kvp => $"{{{kvp.Key},{kvp.Value}}}"));
            return $"(({{{nodesJoined}}},{{{edgesJoined}}}),{_K})";


        }


    }
}

// {
//   public DominatingSetGraph(string dominatingSetInput) : base(dominatingSetInput) {
//
//   }
//
//   public DominatingSetGraph(string dominatingSetInput, bool decoy) : base(dominatingSetInput, decoy) {
//
//   }
// }
