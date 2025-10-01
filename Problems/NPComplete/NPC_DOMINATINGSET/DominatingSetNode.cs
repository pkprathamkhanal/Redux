using System;
using API.Interfaces.Graphs;

namespace API.Problems.NPComplete.NPC_DOMINATINGSET;

class DominatingSetNode : Node
{
 protected string _cluster;

    public DominatingSetNode() : base()
    {
        _cluster = "0";
    }
    public DominatingSetNode(string name, string cluster) : base(name)
    {
        this._cluster = cluster;

    }

public string cluster{
    get{
            return _cluster;
        }
}
}