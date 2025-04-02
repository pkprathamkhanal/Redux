using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_SUBSETSUM.Verifiers;
using API.Problems.NPComplete.NPC_SUBSETSUM.Solvers;

using System.Text.Json;
using System.Text.Json.Serialization;
using API.Problems.NPComplete.NPC_SUBSETSUM.ReduceTo.NPC_KNAPSACK;
using API.Problems.NPComplete.NPC_SUBSETSUM.ReduceTo.NPC_PARTITION;
using API.Problems.NPComplete.NPC_PARTITION;

namespace API.Problems.NPComplete.NPC_SUBSETSUM;

[ApiController]
[Route("[controller]")]
[Tags("Subset Sum")]


#pragma warning disable CS1591

public class FengController : ControllerBase {
#pragma warning restore CS1591

  
///<summary>Returns a reduction object with info for Feng's Subset Sum to Knapsack reduction </summary>
///<response code="200">Returns FengReduction bbject</response>

    [ProducesResponseType(typeof(FengReduction), 200)]
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SUBSETSUM defaultSUBSETSUM = new SUBSETSUM();
        //SipserReduction reduction = new SipserReduction(defaultSAT3);
        FengReduction reduction = new FengReduction(defaultSUBSETSUM);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

///<summary>Returns a reduction from Subset Sum to Knapsack based on the given Subset Sum instance  </summary>
///<param name="problemInstance" example="{{1,7,12,15} : 28}">Subset Sum problem instance string.</param>
///<response code="200">Returns Fengs's Subset Sum to Knapsack FengReduction object</response>

    [ProducesResponseType(typeof(FengReduction), 200)]
    [HttpPost("reduce")]
    public String getReduce([FromBody]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SUBSETSUM defaultSUBSETSUM = new SUBSETSUM(problemInstance);
        FengReduction reduction = new FengReduction(defaultSUBSETSUM);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

    [ProducesResponseType(typeof(string), 200)]
    [HttpPost("mapSolution")]
    public String mapSolution([FromBody]Tools.ApiParameters.MapSolution mapSolution){
        var problemFrom = mapSolution.ProblemFrom;
        var problemTo = mapSolution.ProblemTo;
        var problemFromSolution = mapSolution.ProblemFromSolution;
        Console.WriteLine(problemTo);
        var options = new JsonSerializerOptions { WriteIndented = true };
        SUBSETSUM sSum = new SUBSETSUM(problemFrom);
        PARTITION part = new PARTITION(problemTo);
        PartitionReduction reduction = new PartitionReduction(sSum);
        string mappedSolution = reduction.mapSolutions(sSum,part,problemFromSolution);
        string jsonString = JsonSerializer.Serialize(mappedSolution, options);
        return jsonString;
    }
}


[ApiController]
[Route("[controller]")]
[Tags("Subset Sum")]


#pragma warning disable CS1591

public class SubsetSumToPartitionReductionController : ControllerBase {
#pragma warning restore CS1591

  
///<summary>Returns a reduction object with info for Subset Sum to Partition Reduction </summary>
///<response code="200">Returns PartitionReduction object</response>

    [ProducesResponseType(typeof(PartitionReduction), 200)]
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SUBSETSUM defaultSUBSETSUM = new SUBSETSUM();
        //SipserReduction reduction = new SipserReduction(defaultSAT3);
        PartitionReduction reduction = new PartitionReduction(defaultSUBSETSUM);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

///<summary>Returns a reduction from Subset Sum to Partition based on the given Subset Sum instance  </summary>
///<param name="problemInstance" example="{{1,7,12,15} : 28}">Subset Sum problem instance string.</param>
///<response code="200">Returns Fengs's Subset Sum to Partition object</response>

    [ProducesResponseType(typeof(PartitionReduction), 200)]
    [HttpPost("reduce")]
    public String getReduce([FromBody]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SUBSETSUM defaultSUBSETSUM = new SUBSETSUM(problemInstance);
        PartitionReduction reduction = new PartitionReduction(defaultSUBSETSUM);
        string jsonString = JsonSerializer.Serialize(reduction, options);
       // Console.WriteLine("reduced form is: "+ jsonString);
        return jsonString;
    }

    ///<summary>Returns a solution set to the 3 Dimensional Matching problem, wich has been reduced from 3SAT using Garey and Johnsons's reduction  </summary>
///<param name="problemFrom" example="(x1 | !x2 | x3) &amp; (!x1 | x3 | x1) &amp; (x2 | !x3 | x1)">3SAT problem instance string.</param>
///<param name="problemTo" example="{a[x1][1],a[x1][2],a[x1][3],a[x2][1],a[x2][2],a[x2][3],a[x3][1],a[x3][2],a[x3][3],g1[1],g1[2],g1[3],g1[4],g1[5],g1[6],s1[1],s1[2],s1[3]}{b[x1][1],b[x1][2],b[x1][3],b[x2][1],b[x2][2],b[x2][3],b[x3][1],b[x3][2],b[x3][3],g2[1],g2[2],g2[3],g2[4],g2[5],g2[6],s2[1],s2[2],s2[3]}{[!x1][1],[x1][1],[!x1][2],[x1][2],[!x1][3],[x1][3],[!x2][1],[x2][1],[!x2][2],[x2][2],[!x2][3],[x2][3],[!x3][1],[x3][1],[!x3][2],[x3][2],[!x3][3],[x3][3]}{a[x1][1],b[x1][1],[!x1][1]}{a[x1][2],b[x1][1],[x1][1]}{g1[1],g2[1],[x1][1]}{g1[1],g2[1],[!x1][1]}{g1[2],g2[2],[x1][1]}{g1[2],g2[2],[!x1][1]}{g1[3],g2[3],[x1][1]}{g1[3],g2[3],[!x1][1]}{g1[4],g2[4],[x1][1]}{g1[4],g2[4],[!x1][1]}{g1[5],g2[5],[x1][1]}{g1[5],g2[5],[!x1][1]}{g1[6],g2[6],[x1][1]}{g1[6],g2[6],[!x1][1]}{a[x1][2],b[x1][2],[!x1][2]}{a[x1][3],b[x1][2],[x1][2]}{g1[1],g2[1],[x1][2]}{g1[1],g2[1],[!x1][2]}{g1[2],g2[2],[x1][2]}{g1[2],g2[2],[!x1][2]}{g1[3],g2[3],[x1][2]}{g1[3],g2[3],[!x1][2]}{g1[4],g2[4],[x1][2]}{g1[4],g2[4],[!x1][2]}{g1[5],g2[5],[x1][2]}{g1[5],g2[5],[!x1][2]}{g1[6],g2[6],[x1][2]}{g1[6],g2[6],[!x1][2]}{a[x1][3],b[x1][3],[!x1][3]}{a[x1][1],b[x1][3],[x1][3]}{g1[1],g2[1],[x1][3]}{g1[1],g2[1],[!x1][3]}{g1[2],g2[2],[x1][3]}{g1[2],g2[2],[!x1][3]}{g1[3],g2[3],[x1][3]}{g1[3],g2[3],[!x1][3]}{g1[4],g2[4],[x1][3]}{g1[4],g2[4],[!x1][3]}{g1[5],g2[5],[x1][3]}{g1[5],g2[5],[!x1][3]}{g1[6],g2[6],[x1][3]}{g1[6],g2[6],[!x1][3]}{a[x2][1],b[x2][1],[!x2][1]}{a[x2][2],b[x2][1],[x2][1]}{g1[1],g2[1],[x2][1]}{g1[1],g2[1],[!x2][1]}{g1[2],g2[2],[x2][1]}{g1[2],g2[2],[!x2][1]}{g1[3],g2[3],[x2][1]}{g1[3],g2[3],[!x2][1]}{g1[4],g2[4],[x2][1]}{g1[4],g2[4],[!x2][1]}{g1[5],g2[5],[x2][1]}{g1[5],g2[5],[!x2][1]}{g1[6],g2[6],[x2][1]}{g1[6],g2[6],[!x2][1]}{a[x2][2],b[x2][2],[!x2][2]}{a[x2][3],b[x2][2],[x2][2]}{g1[1],g2[1],[x2][2]}{g1[1],g2[1],[!x2][2]}{g1[2],g2[2],[x2][2]}{g1[2],g2[2],[!x2][2]}{g1[3],g2[3],[x2][2]}{g1[3],g2[3],[!x2][2]}{g1[4],g2[4],[x2][2]}{g1[4],g2[4],[!x2][2]}{g1[5],g2[5],[x2][2]}{g1[5],g2[5],[!x2][2]}{g1[6],g2[6],[x2][2]}{g1[6],g2[6],[!x2][2]}{a[x2][3],b[x2][3],[!x2][3]}{a[x2][1],b[x2][3],[x2][3]}{g1[1],g2[1],[x2][3]}{g1[1],g2[1],[!x2][3]}{g1[2],g2[2],[x2][3]}{g1[2],g2[2],[!x2][3]}{g1[3],g2[3],[x2][3]}{g1[3],g2[3],[!x2][3]}{g1[4],g2[4],[x2][3]}{g1[4],g2[4],[!x2][3]}{g1[5],g2[5],[x2][3]}{g1[5],g2[5],[!x2][3]}{g1[6],g2[6],[x2][3]}{g1[6],g2[6],[!x2][3]}{a[x3][1],b[x3][1],[!x3][1]}{a[x3][2],b[x3][1],[x3][1]}{g1[1],g2[1],[x3][1]}{g1[1],g2[1],[!x3][1]}{g1[2],g2[2],[x3][1]}{g1[2],g2[2],[!x3][1]}{g1[3],g2[3],[x3][1]}{g1[3],g2[3],[!x3][1]}{g1[4],g2[4],[x3][1]}{g1[4],g2[4],[!x3][1]}{g1[5],g2[5],[x3][1]}{g1[5],g2[5],[!x3][1]}{g1[6],g2[6],[x3][1]}{g1[6],g2[6],[!x3][1]}{a[x3][2],b[x3][2],[!x3][2]}{a[x3][3],b[x3][2],[x3][2]}{g1[1],g2[1],[x3][2]}{g1[1],g2[1],[!x3][2]}{g1[2],g2[2],[x3][2]}{g1[2],g2[2],[!x3][2]}{g1[3],g2[3],[x3][2]}{g1[3],g2[3],[!x3][2]}{g1[4],g2[4],[x3][2]}{g1[4],g2[4],[!x3][2]}{g1[5],g2[5],[x3][2]}{g1[5],g2[5],[!x3][2]}{g1[6],g2[6],[x3][2]}{g1[6],g2[6],[!x3][2]}{a[x3][3],b[x3][3],[!x3][3]}{a[x3][1],b[x3][3],[x3][3]}{g1[1],g2[1],[x3][3]}{g1[1],g2[1],[!x3][3]}{g1[2],g2[2],[x3][3]}{g1[2],g2[2],[!x3][3]}{g1[3],g2[3],[x3][3]}{g1[3],g2[3],[!x3][3]}{g1[4],g2[4],[x3][3]}{g1[4],g2[4],[!x3][3]}{g1[5],g2[5],[x3][3]}{g1[5],g2[5],[!x3][3]}{g1[6],g2[6],[x3][3]}{g1[6],g2[6],[!x3][3]}{s1[1],s2[1],[x1][1]}{s1[1],s2[1],[!x2][1]}{s1[1],s2[1],[x3][1]}{s1[2],s2[2],[!x1][2]}{s1[2],s2[2],[x3][2]}{s1[2],s2[2],[x1][2]}{s1[3],s2[3],[x2][3]}{s1[3],s2[3],[!x3][3]}{s1[3],s2[3],[x1][3]}">3 Dimensional Matching problem instance string reduced from 3SAT instance.</param>
///<param name="problemFromSolution" example="(x1:True)">Solution to 3SAT problem.</param>
///<response code="200">Returns solution to the reduced 0-1 Integer Programming instance</response>
    
    [ProducesResponseType(typeof(string), 200)]
    [HttpPost("mapSolution")]
    public String mapSolution([FromBody]Tools.ApiParameters.MapSolution mapSolution){
        var problemFrom = mapSolution.ProblemFrom;
        var problemTo = mapSolution.ProblemTo;
        var problemFromSolution = mapSolution.ProblemFromSolution;
        var options = new JsonSerializerOptions { WriteIndented = true };
        SUBSETSUM sSum = new SUBSETSUM(problemFrom);
        PARTITION partition = new PARTITION(problemTo);
        PartitionReduction reduction = new PartitionReduction(sSum);
        string mappedSolution = reduction.mapSolutions(sSum,partition,problemFromSolution);
        string jsonString = JsonSerializer.Serialize(mappedSolution, options);
        return jsonString;
    }
}
