using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using API.Interfaces.Graphs;
namespace API.Problems.NPComplete.NPC_VERTEXCOVER;

class VertexCoverNode : Node
{
 protected string _solutionState;

    public VertexCoverNode():base(){
        _solutionState = "";
    }
    public VertexCoverNode(string name){
        this._name = name;
        _solutionState = "";

    }

public string solutionState{
    get{
            return _solutionState;
        }
    set{
            _solutionState = value;
        }
    
}
}