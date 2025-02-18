using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_INTPROGRAMMING01;
using API.Problems.NPComplete.NPC_INTPROGRAMMING01.Verifiers;
using API.Problems.NPComplete.NPC_INTPROGRAMMING01.Solvers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_INTPROGRAMMING01;


[ApiController]
[Route("[controller]")]
[Tags("0-1 Integer Programming")]
#pragma warning disable CS1591
public class INTPROGRAMMING01GenericController : ControllerBase {
#pragma warning restore CS1591

///<summary>Returns a default 0-1 Integer Programming object</summary>

    [ProducesResponseType(typeof(INTPROGRAMMING01), 200)]
    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new INTPROGRAMMING01(), options);
        return jsonString;
    }
///<summary>Returns a 0-1 Integer Programming object created from a given instance </summary>
///<param name="problemInstance" example="(-1 1 -1),(0 0 -1),(-1 -1 1)&lt;=(0 0 0)">0-1 Integer Programming problem instance string.</param>
///<response code="200">Returns INTPROGRAMMING01 problem object</response>

    [ProducesResponseType(typeof(INTPROGRAMMING01), 200)]
    [HttpPost("instance")]
    public String getInstance([FromBody]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new INTPROGRAMMING01(problemInstance), options);
        return jsonString;
    }
}

[ApiController]
[Route("[controller]")]
[Tags("0-1 Integer Programming")]
#pragma warning disable CS1591
public class GenericVerifier01INTPController : ControllerBase {
#pragma warning restore CS1591

///<summary>Returns information about the 0-1 Integer Programming generic verifier </summary>
///<response code="200">Returns GenericVerifier01INTP object</response>

    [ProducesResponseType(typeof(GenericVerifier01INTP), 200)]
    [HttpGet("info")]
    public String getGeneric() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        GenericVerifier01INTP verifier = new GenericVerifier01INTP();
        string jsonString = JsonSerializer.Serialize(verifier, options);
        return jsonString;
    }

///<summary>Verifies if a given certificate is a solution to a given Graph Coloring problem</summary>
///<param name="certificate" example="(1 0 0)">certificate solution to 0-1 Integer Programming problem.</param>
///<param name="problemInstance" example="(-1 1 -1),(0 0 -1),(-1 -1 1)&lt;=(0 0 0)">0-1 Integer Programming problem instance string.</param>
///<response code="200">Returns a boolean</response>
    
    [ProducesResponseType(typeof(Boolean), 200)]
    [HttpPost("verify")]
    public String verifyInstance([FromBody]Tools.ApiParameters.Verify verify) {
        var certificate = verify.Certificate;
        var problemInstance = verify.ProblemInstance;
        var options = new JsonSerializerOptions { WriteIndented = true };
        INTPROGRAMMING01 INTPROGRAMMING01_Problem = new INTPROGRAMMING01(problemInstance);
        GenericVerifier01INTP verifier = new GenericVerifier01INTP();
        bool response = verifier.verify(INTPROGRAMMING01_Problem,certificate);
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
[Tags("0-1 Integer Programming")]
#pragma warning disable CS1591
public class IntegerProgrammingBruteForceController : ControllerBase {
#pragma warning restore CS1591

///<summary>Returns a info about the 0-1 Integer Programming Brute Force solver </summary>
///<response code="200">Returns IntegerProgrammingBruteForce solver Object</response>

    [ProducesResponseType(typeof(IntegerProgrammingBruteForce), 200)]
    [HttpGet("info")]
    public String getGeneric() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        IntegerProgrammingBruteForce solver = new IntegerProgrammingBruteForce();
        string jsonString = JsonSerializer.Serialize(solver, options);
        return jsonString;
    }

///<summary>Returns a solution to a given  0-1 Integer Programming problem instance </summary>
///<param name="problemInstance" example="(-1 1 -1),(0 0 -1),(-1 -1 1)&lt;=(0 0 0)">0-1 Integer Programming problem instance string.</param>
///<response code="200">Returns solution string </response>
    
    [ProducesResponseType(typeof(string), 200)]
    [HttpPost("solve")]
    public String solveInstance([FromBody]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        INTPROGRAMMING01 problem = new INTPROGRAMMING01(problemInstance);
        string solution = problem.defaultSolver.solve(problem);
        string jsonString = JsonSerializer.Serialize(solution, options);
        return jsonString;
    }

}
    





