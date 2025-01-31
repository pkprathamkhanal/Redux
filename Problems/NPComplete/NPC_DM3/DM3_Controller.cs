using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_DM3;
using API.Problems.NPComplete.NPC_DM3.Verifiers;
using API.Problems.NPComplete.NPC_DM3.Solvers;


using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_3DM;

[ApiController]
[Route("[controller]")]
[Tags("3 Dimensional Matching")]
#pragma warning disable CS1591
public class DM3GenericController : ControllerBase {
#pragma warning restore CS1591

///<summary>Returns a default 3 Dimensional Matching object</summary>

    [ProducesResponseType(typeof(DM3), 200)]
    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new DM3(), options);
        return jsonString;
    }

///<summary>Returns a 3 Dimensional Matching object created from a given instance </summary>
///<param name="problemInstance" example="{Paul,Sally,Dave}{Madison,Austin,Bob}{Chloe,Frank,Jake}{Paul,Madison,Chloe}{Paul,Austin,Jake}{Sally,Bob,Chloe}{Sally,Madison,Frank}{Dave,Austin,Chloe}{Dave,Bob,Chloe}">3 Dimensional Matching problem instance string.</param>
///<response code="200">Returns DM3 problem object</response>

    [ProducesResponseType(typeof(DM3), 200)]
    [HttpGet("{instance}")]
    public String getInstance([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new DM3(problemInstance), options);
        return jsonString;
    }
}

[ApiController]
[Route("[controller]")]
[Tags("3 Dimensional Matching")]
#pragma warning disable CS1591
public class GenericVerifierDM3Controller : ControllerBase {
#pragma warning restore CS1591

///<summary>Returns information about the 3 Dimensional Matching generic Verifier </summary>
///<response code="200">Returns GenericVerifierDM3 Object</response>

    [ProducesResponseType(typeof(GenericVerifierDM3), 200)]
    [HttpGet("info")]
    public String getGeneric() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        GenericVerifierDM3 verifier = new GenericVerifierDM3();
        string jsonString = JsonSerializer.Serialize(verifier, options);
        return jsonString;
    }

///<summary>Verifies if a given certificate is a solution to a given 3 Dimensional Matching problem</summary>
///<param name="certificate" example="{{Paul,Austin,Jake},{Sally,Madison,Frank},{Dave,Bob,Chloe}}">certificate solution to 3 Dimensional Matching problem.</param>
///<param name="problemInstance" example="{Paul,Sally,Dave}{Madison,Austin,Bob}{Chloe,Frank,Jake}{Paul,Madison,Chloe}{Paul,Austin,Jake}{Sally,Bob,Chloe}{Sally,Madison,Frank}{Dave,Austin,Chloe}{Dave,Bob,Chloe}">3 Dimentional Matching problem instance string.</param>
///<response code="200">Returns a boolean</response>
    
    [ProducesResponseType(typeof(Boolean), 200)]
    [HttpPost("verify")]
    public String verifyInstance([FromBody]Tools.ApiParameters.Verify verify) {
        var certificate = verify.Certificate;
        var problemInstance = verify.ProblemInstance;
        var options = new JsonSerializerOptions { WriteIndented = true };
        DM3 DM3_PROBLEM = new DM3(problemInstance);
        GenericVerifierDM3 verifier = new GenericVerifierDM3();
        Boolean response = verifier.verify(DM3_PROBLEM,certificate);
        string responseString;
        if(response){
            responseString = "True";
        }
        else{responseString = "False";}
        string jsonString = JsonSerializer.Serialize(responseString, options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
[Tags("3 Dimensional Matching")]
#pragma warning disable CS1591
public class ThreeDimensionalMatchingBruteForceController : ControllerBase {
#pragma warning restore CS1591


    // Return Generic Solver Class
///<summary>Returns information about the 3 Dimensional Matching brute force solver </summary>
///<response code="200">Returns ThreeDimensionalMatchingBruteForce solver Object</response>

    [ProducesResponseType(typeof(ThreeDimensionalMatchingBruteForce), 200)]
    [HttpGet("info")]
    public String getGeneric() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        ThreeDimensionalMatchingBruteForce solver = new ThreeDimensionalMatchingBruteForce();
        string jsonString = JsonSerializer.Serialize(solver, options);
        return jsonString;
    }

///<summary>Returns a solution to a given  3 Dimensional Matching problem instance </summary>
///<param name="problemInstance" example="{Paul,Sally,Dave}{Madison,Austin,Bob}{Chloe,Frank,Jake}{Paul,Madison,Chloe}{Paul,Austin,Jake}{Sally,Bob,Chloe}{Sally,Madison,Frank}{Dave,Austin,Chloe}{Dave,Bob,Chloe}"> 3 Dimensional Matching problem instance string.</param>
///<response code="200">Returns solution string </response>
    
    [ProducesResponseType(typeof(string), 200)]
    [HttpGet("solve")]
    public String solveInstance([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        DM3 problem = new DM3(problemInstance);
        string solution = problem.defaultSolver.solve(problem);
        string jsonString = JsonSerializer.Serialize(solution, options);
        return jsonString;
    }

}

