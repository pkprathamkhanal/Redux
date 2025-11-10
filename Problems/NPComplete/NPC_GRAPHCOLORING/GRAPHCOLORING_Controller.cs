
using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_GRAPHCOLORING;
using System.Text.Json;
using API.Interfaces.JSON_Objects.Graphs;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.Verifiers;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.Solvers;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.ReduceTo.NPC_SAT;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.ReduceTo.NPC_CLIQUECOVER;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.ReduceTo.NPC_EXACTCOVER;

namespace API.Problems.NPComplete.NPC_GRAPHCOLORING;

[ApiController]
[Route("[controller]")]
[Tags("Graph Coloring")]


#pragma warning disable CS1591

public class GraphColoringToCliqueCoverController : ControllerBase {
#pragma warning restore CS1591

  
///<summary>Returns a reduction object with info for Graph Coloring to CliqueCover Reduction </summary>
///<response code="200">Returns CliqueCoverReduction object</response>

    [ProducesResponseType(typeof(GraphColoringToCliqueCover), 200)]
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        GRAPHCOLORING defaultGRAPHCOLORING = new GRAPHCOLORING();
        //SipserReduction reduction = new SipserReduction(defaultSAT3);
        GraphColoringToCliqueCover reduction = new GraphColoringToCliqueCover(defaultGRAPHCOLORING);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

///<summary>Returns a reduction from Graph Coloring to CliqueCover based on the given Graph Coloring instance  </summary>
///<param name="problemInstance" example="{{1,7,12,15} : 28}">Graph Coloring problem instance string.</param>
///<response code="200">Returns Fengs's Graph Coloring to CliqueCover object</response>

    [ProducesResponseType(typeof(GraphColoringToCliqueCover), 200)]
    [HttpPost("reduce")]
    public String getReduce([FromBody]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        GRAPHCOLORING defaultGRAPHCOLORING = new GRAPHCOLORING(problemInstance);
        GraphColoringToCliqueCover reduction = new GraphColoringToCliqueCover(defaultGRAPHCOLORING);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
[Tags("Graph Coloring")]
#pragma warning disable CS1591
public class KarpReduceSATController : ControllerBase
{
#pragma warning restore CS1591

    ///<summary>Returns a reduction object with info for Karps's Graph Coloring to SAT reduction </summary>
    ///<response code="200">Returns KarpReduceSAT reduction object</response>

    [ProducesResponseType(typeof(KarpReduceSAT), 200)]
    [HttpGet("info")]
    public String getInfo(){
        var options = new JsonSerializerOptions { WriteIndented = true };
        GRAPHCOLORING defaultGRAPHCOLORING = new GRAPHCOLORING();
        KarpReduceSAT reduction = new KarpReduceSAT(defaultGRAPHCOLORING);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

    ///<summary>Returns a reduction from Graph Coloring to AT based on the given Graph Coloring instance  </summary>
    ///<param name="problemInstance" example="{{a,b,c,d,e,f,g,h,i},{{a,b},{b,a},{b,c},{c,a},{a,c},{c,b},{a,d},{d,a},{d,e},{e,a},{a,e},{e,d},{a,f},{f,a},{f,g},{g,a},{a,g},{g,f},{a,h},{h,a},{h,i},{i,a},{a,i},{i,h}},3}">Graph Coloring problem instance string.</param>
    ///<response code="200">Returns Karps's Graph Coloring to SAT KarpReduceSAT reduction object</response>

    [ProducesResponseType(typeof(KarpReduceSAT), 200)]
    [HttpPost("reduce")]
    public String getReduce([FromBody]string problemInstance){
        KarpReduceSAT reduction = new KarpReduceSAT(new GRAPHCOLORING(problemInstance));
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

}


[ApiController]
[Route("[controller]")]
[Tags("Exact Cover")]


#pragma warning disable CS1591

public class KarpGraphColorToExactCoverController : ControllerBase {
#pragma warning restore CS1591

  
///<summary>Returns a reduction object with info for Graph Coloring to CliqueCover Reduction </summary>
///<response code="200">Returns CliqueCoverReduction object</response>

    [ProducesResponseType(typeof(KarpGraphColorToExactCover), 200)]
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        GRAPHCOLORING defaultGC = new GRAPHCOLORING();
        KarpGraphColorToExactCover reduction = new KarpGraphColorToExactCover(defaultGC);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

///<summary>Returns a reduction from Graph Coloring to CliqueCover based on the given Graph Coloring instance  </summary>
///<param name="problemInstance" example="{{1,7,12,15} : 28}">Graph Coloring problem instance string.</param>
///<response code="200">Returns Fengs's Graph Coloring to CliqueCover object</response>

    [ProducesResponseType(typeof(KarpGraphColorToExactCover), 200)]
    [HttpPost("reduce")]
    public String getReduce([FromBody]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        GRAPHCOLORING defaultGC = new GRAPHCOLORING(problemInstance);
        KarpGraphColorToExactCover reduction = new KarpGraphColorToExactCover(defaultGC);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

}

