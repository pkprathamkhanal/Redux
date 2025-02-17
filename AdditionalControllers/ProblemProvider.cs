using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using API.Interfaces;

[ApiController]
[Route("[controller]")]
[Tags("Problem Provider")]
public class ProblemProvider : ControllerBase {
    public static readonly Dictionary<string, Type> Problems = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(IProblem).IsAssignableFrom(p) && p.IsClass).ToDictionary(x => x.Name, x => x);
    public static readonly Dictionary<string, Type> Verifiers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(IVerifier).IsAssignableFrom(p) && p.IsClass).ToDictionary(x => x.Name, x => x);
    public static readonly Dictionary<string, Type> Solvers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ISolver).IsAssignableFrom(p) && p.IsClass).ToDictionary(x => x.Name, x => x);

    #pragma warning disable CS8603 // Possible null reference return.
    static IProblem Problem(string name) {
        return Activator.CreateInstance(Problems[name]) as IProblem; // guaranteed success by `IsAssignableFrom`
    }

    static IProblem ProblemInstance(string name, string instance) {
        return Activator.CreateInstance(Problems[name], instance) as IProblem; // guaranteed success by `IsAssignableFrom`
    }

    static IVerifier Verifier(string name) {
        return Activator.CreateInstance(Verifiers[name]) as IVerifier; // guaranteed success by `IsAssignableFrom`
    }

    static ISolver Solver(string name) {
        return Activator.CreateInstance(Solvers[name]) as ISolver; // guaranteed success by `IsAssignableFrom`
    }
    #pragma warning restore CS8603 // Possible null reference return.

    [ProducesResponseType(typeof(bool), 200)]
    [HttpPost("verify")]
    public string verify(string verifier, [FromBody]Verify verify) {
        // TODO: validate arguments
        return JsonSerializer.Serialize(
            Verifier(verifier).verify(verify.ProblemInstance, verify.Certificate).ToString(),
            new JsonSerializerOptions { WriteIndented = true }
        );
    }

    [HttpPost("solve")]
    public string solve(string solver, [FromBody]string problemInstance) {
        // TODO: validate arguments
        return JsonSerializer.Serialize(
            Solver(solver).solve(problemInstance),
            new JsonSerializerOptions { WriteIndented = true }
        );
    }

    [ProducesResponseType(typeof(IProblem), 200)]
    [HttpGet("problemInfo")]
    public string problemInfo(string problem) {
        // TODO: validate arguments
        return Newtonsoft.Json.JsonConvert.SerializeObject(Problem(problem));
    }

    [ProducesResponseType(typeof(IProblem), 200)]
    [HttpPost("problemInstance")]
    public string problemInstance(string problem, [FromBody]string problemInstance) {
        // TODO: validate arguments
        return Newtonsoft.Json.JsonConvert.SerializeObject(ProblemInstance(problem, problemInstance));
    }
}

public class Verify {
    public string Certificate { get; set; } = "";
    public string ProblemInstance { get; set; } = "";
}
