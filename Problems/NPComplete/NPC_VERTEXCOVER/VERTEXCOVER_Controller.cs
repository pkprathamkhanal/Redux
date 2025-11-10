using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_VERTEXCOVER;
using API.Problems.NPComplete.NPC_ARCSET;
using API.Problems.NPComplete.NPC_VERTEXCOVER.Verifiers;
using API.Problems.NPComplete.NPC_VERTEXCOVER.Solvers;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Problems.NPComplete.NPC_VERTEXCOVER.ReduceTo.NPC_ARCSET;
using API.Problems.NPComplete.NPC_VERTEXCOVER.ReduceTo.NPC_NODESET;
using API.Problems.NPComplete.NPC_VERTEXCOVER.ReduceTo.NPC_SETCOVER;
using API.Interfaces.JSON_Objects.Graphs;
using API.Interfaces.JSON_Objects;
using API.Tools.ApiParameters;

using System.Collections;

[ApiController]
[Route("[controller]")]
[Tags("Vertex Cover")]
#pragma warning disable CS1591
public class KarpVertexCoverToNodeSetController : ControllerBase {
#pragma warning restore CS1591

  
///<summary>Returns a reduction object with info for Graph Coloring to CliqueCover Reduction </summary>
///<response code="200">Returns VertexCoverReduction object</response>

    [ProducesResponseType(typeof(KarpVertexCoverToNodeSet), 200)]
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        VERTEXCOVER defaultVERTEXCOVER = new VERTEXCOVER();
        //SipserReduction reduction = new SipserReduction(defaultSAT3);
        KarpVertexCoverToNodeSet reduction = new KarpVertexCoverToNodeSet(defaultVERTEXCOVER);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

///<summary>Returns a reduction from Graph Coloring to CliqueCover based on the given Graph Coloring instance  </summary>
///<param name="problemInstance" example="{{1,7,12,15} : 28}">Graph Coloring problem instance string.</param>
///<response code="200">Returns Fengs's Graph Coloring to CliqueCover object</response>

    [ProducesResponseType(typeof(KarpVertexCoverToNodeSet), 200)]
    [HttpPost("reduce")]
    public String getReduce([FromBody]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        VERTEXCOVER defaultVERTEXCOVER = new VERTEXCOVER(problemInstance);
        KarpVertexCoverToNodeSet reduction = new KarpVertexCoverToNodeSet(defaultVERTEXCOVER);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

}
[ApiController]
[Route("[controller]")]
[Tags("Vertex Cover")]


#pragma warning disable CS1591

public class KarpVertexCoverToSetCoverController : ControllerBase {
#pragma warning restore CS1591

  
///<summary>Returns a reduction object with info for Graph Coloring to CliqueCover Reduction </summary>
///<response code="200">Returns KarpVertexCoverToSetCover object</response>

    [ProducesResponseType(typeof(KarpVertexCoverToSetCover), 200)]
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        VERTEXCOVER defaultVERTEXCOVER = new VERTEXCOVER();
        //SipserReduction reduction = new SipserReduction(defaultSAT3);
        KarpVertexCoverToSetCover reduction = new KarpVertexCoverToSetCover(defaultVERTEXCOVER);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

///<summary>Returns a reduction from Graph Coloring to CliqueCover based on the given Graph Coloring instance  </summary>
///<param name="problemInstance" example="{{1,7,12,15} : 28}">Graph Coloring problem instance string.</param>
///<response code="200">Returns Fengs's Graph Coloring to CliqueCover object</response>

    [ProducesResponseType(typeof(KarpVertexCoverToSetCover), 200)]
    [HttpPost("reduce")]
    public String getReduce([FromBody]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        VERTEXCOVER defaultVERTEXCOVER = new VERTEXCOVER(problemInstance);
        KarpVertexCoverToSetCover reduction = new KarpVertexCoverToSetCover(defaultVERTEXCOVER);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
[Tags("Vertex Cover")]
#pragma warning disable CS1591
public class LawlerKarpController : ControllerBase {
#pragma warning restore CS1591


///<summary>Returns a reduction object with info for Lawler and Karp's Vertex Cover to Feedback Arc Set reduction </summary>
///<response code="200">Returns LawlerKarp reduction object</response>

    [ProducesResponseType(typeof(LawlerKarp), 200)]
    [HttpGet("info")] // url parameter

      public String getInfo(){
            var options = new JsonSerializerOptions { WriteIndented = true };
            LawlerKarp reduction = new LawlerKarp();
    
            String jsonString = JsonSerializer.Serialize(reduction,options);
            return jsonString;
      }

    
///<summary>Returns a reduction from Vertex Cover to Feedback Arc Set based on the given Vertex Cover instance  </summary>
///<param name="problemInstance" example="({a,b,c,d,e},{{a,b},{a,c},{a,e},{b,e},{c,d}}),3)">Vertex Cover problem instance string.</param>
///<response code="200">Returns Lawler and Karp's Vertex Cover to Feedback Arc Set reduction object</response>

    [ProducesResponseType(typeof(LawlerKarp), 200)]
    [HttpPost("reduce")]
    public String getReduce([FromBody]string problemInstance) {
        //from query is a query parameter

        var options = new JsonSerializerOptions { WriteIndented = true };
        //UndirectedGraph UG = new UndirectedGraph(problemInstance);
        //string reduction = UG.reduction();
        //Boolean response = verifier.verify(ARCSETProblem,certificate);
        // Send back to API user
        VERTEXCOVER vCover = new VERTEXCOVER(problemInstance);
        LawlerKarp reduction = new LawlerKarp(vCover);
       // ARCSET reducedArcset = reduction.reduce();
        //string reducedStr = reducedArcset.instance;

        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;

    }

///<summary>Returns a solution to the a Feedback Arc Set problem, wich has been reduced from Vertex Cover using Lawler and Karp's reduction  </summary>
///<param name="problemFrom" example="({a,b,c,d,e},{{a,b},{a,c},{a,e},{b,e},{c,d}}),3)">3SAT problem instance string.</param>
///<param name="problemTo" example="(({a0,a1,b0,b1,c0,c1,d0,d1,e0,e1},{(a0,a1),(a1,b0),(a1,c0),(a1,e0),(b0,b1),(b1,a0),(b1,e0),(c0,c1),(c1,a0),(c1,d0),(d0,d1),(d1,c0),(e0,e1),(e1,a0),(e1,b0)}),3)">Feedback Arc Set problem instance string reduced from Vertex Cover instance.</param>
///<param name="problemFromSolution" example="{a,b,c}">Solution to Vertex Cover problem.</param>
///<response code="200">Returns solution to the reduced Feedback Arc Set problem instance</response>
    
    [ProducesResponseType(typeof(string), 200)]
    [HttpPost("mapSolution")]
    public String mapSolution([FromBody]MapSolution mapSolution){
        var problemFrom = mapSolution.ProblemFrom;
        var problemTo = mapSolution.ProblemTo;
        var problemFromSolution = mapSolution.ProblemFromSolution;
        var options = new JsonSerializerOptions { WriteIndented = true };
        VERTEXCOVER vertexCover = new VERTEXCOVER(problemFrom);
        ARCSET arcset = new ARCSET(problemTo);
        LawlerKarp reduction = new LawlerKarp();
        string mappedSolution = reduction.mapSolutions(problemFromSolution);
        string jsonString = JsonSerializer.Serialize(mappedSolution, options);
        return jsonString;
    }

}