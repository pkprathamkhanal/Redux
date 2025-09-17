using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using API.Interfaces.Graphs;
namespace API.Problems.NPComplete.NPC_STEINERTREE;

class SteinerGraph : UnweightedUndirectedGraph
{
    public SteinerGraph(List<string> nl, List<KeyValuePair<string, string>> el, int kVal) : base(nl, el, kVal)
    {

    }
    
}