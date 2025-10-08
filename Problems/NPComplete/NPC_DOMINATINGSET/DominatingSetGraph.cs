// TODO: implement graph class if Dominating Set is a graphing problem
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using API.Interfaces.Graphs;
using System.Linq;

namespace API.Problems.NPComplete.NPC_DOMINATINGSET;

class DominatingSetGraph : UnweightedUndirectedGraph
  {
      public DominatingSetGraph(List<string> nodeNames, List<KeyValuePair<string, string>> edgePairs, int kValue): base(nodeNames, edgePairs, kValue)
      {
          _nodeStringList = new List<string>(nodeNames);
          _edgesKVP = new List<KeyValuePair<string, string>>(edgePairs);
          _K = kValue;
      }

  }

           