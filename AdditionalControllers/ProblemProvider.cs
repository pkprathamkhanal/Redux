using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using API.Interfaces;
using API.Interfaces.JSON_Objects.Graphs;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using API.Interfaces.Tools;
using API.Interfaces.JSON_Objects;

[ApiController]
[Route("[controller]")]
[Tags("Problem Provider")]
public class ProblemProvider : ControllerBase {
    public static readonly Dictionary<string, Type> Problems = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(IProblem).IsAssignableFrom(p) && p.IsClass).ToDictionary(x => x.Name.ToLower(), x => x);
    public static readonly Dictionary<string, Type> GraphProblems = Problems.Where(p => typeof(IGraphProblem).IsAssignableFrom(p.Value)).ToDictionary(x => x.Key, x => x.Value);
    public static readonly Dictionary<string, Type> Verifiers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(IVerifier).IsAssignableFrom(p) && p.IsClass).ToDictionary(x => x.Name.ToLower(), x => x);
    public static readonly Dictionary<string, Type> Solvers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ISolver).IsAssignableFrom(p) && p.IsClass).ToDictionary(x => x.Name.ToLower(), x => x);
    public static readonly Dictionary<string, Type> Visualizers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(IVisualization).IsAssignableFrom(p) && p.IsClass).ToDictionary(x => x.Name.ToLower(), x => x);
    public static readonly Dictionary<string, Type> Interfaces = (new[] {Problems, Verifiers, Solvers, Visualizers}).SelectMany(d => d).ToDictionary(x => x.Key, x => x.Value);

    #pragma warning disable CS8603 // Possible null reference return.
    static IProblem Problem(string name) {
        return Activator.CreateInstance(Problems[name.ToLower()]) as IProblem; // guaranteed success by `IsAssignableFrom`
    }

    static IProblem ProblemInstance(string name, string instance) {
        return Activator.CreateInstance(Problems[name.ToLower()], instance) as IProblem; // guaranteed success by `IsAssignableFrom`
    }

    static IGraphProblem GraphProblem(string name, string instance) {
        return Activator.CreateInstance(GraphProblems[name.ToLower()], instance) as IGraphProblem; // guaranteed success by `IsAssignableFrom`
    }

    static IVerifier Verifier(string name) {
        return Activator.CreateInstance(Verifiers[name.ToLower()]) as IVerifier; // guaranteed success by `IsAssignableFrom`
    }

    static ISolver Solver(string name) {
        return Activator.CreateInstance(Solvers[name.ToLower()]) as ISolver; // guaranteed success by `IsAssignableFrom`
    }

    static IVisualization Visualization(string name)
    {
        return Activator.CreateInstance(Visualizers[name.ToLower()]) as IVisualization;
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

    [ProducesResponseType(typeof(object), 200)]
    [HttpGet("info")]
    public string info(string @interface) {
        // TODO: validate arguments
        return Newtonsoft.Json.JsonConvert.SerializeObject(Activator.CreateInstance(Interfaces[@interface.ToLower()]));
    }

    [ProducesResponseType(typeof(IProblem), 200)]
    [HttpPost("problemInstance")]
    public string problemInstance(string problem, [FromBody]string problemInstance) {
        // TODO: validate arguments
        return Newtonsoft.Json.JsonConvert.SerializeObject(ProblemInstance(problem, problemInstance));
    }

    [HttpPost("visualize")]
    public string visualize(string visualizationName, [FromBody] string instance)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        options.Converters.Add(new API_JSON_Converter<API_JSON>());
        API_JSON visual = Visualization(visualizationName).visualize(instance);
        return JsonSerializer.Serialize(
            visual,
            options
        );
    }

    [HttpPost("solvedVisualize")]
    public string solvedVisualize(string visualizationName, [FromBody] string instance)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        options.Converters.Add(new API_JSON_Converter<API_JSON>());
        IVisualization visual = Visualization(visualizationName);
        return JsonSerializer.Serialize(
            visual.getSolvedVisualization(instance),
            options
        );
    }

    [HttpPost("steps")]
    public string steps(string solver, [FromBody] string instance)
    {
        return JsonSerializer.Serialize(
            Solver(solver).getSteps(instance),
            new JsonSerializerOptions { WriteIndented = true }
        );
    }
}

public class Verify {
    public string Certificate { get; set; } = "";
    public string ProblemInstance { get; set; } = "";
}
