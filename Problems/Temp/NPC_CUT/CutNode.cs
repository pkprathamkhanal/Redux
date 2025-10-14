using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using API.Interfaces.Graphs;
namespace API.Problems.NPComplete.NPC_CUT;

class CutNode : Node
{
 protected string _cluster;

    public CutNode():base(){
        _cluster = "0";
    }
    public CutNode(string name, string cluster){
        this._name = name;
        this._cluster = cluster;

    }

public string cluster{
    get{
            return _cluster;
        }
}
}