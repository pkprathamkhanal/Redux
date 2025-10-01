using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using API.Interfaces.Graphs;
namespace API.Problems.NPComplete.NPC_HAMILTONIAN;

class HamiltonianGraph : UnweightedUndirectedGraph
{
    public HamiltonianGraph(List<string> nodes, List<KeyValuePair<string, string>> edges) : base(nodes, edges, 0)
    {

    }

}