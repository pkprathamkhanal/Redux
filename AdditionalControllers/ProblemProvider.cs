using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using API.Interfaces;

[ApiController]
[Route("[controller]")]
[Tags("Problem Provider")]
public class ProblemProvider : ControllerBase {
    public static readonly Dictionary<string, Type> Problems = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(IProblem).IsAssignableFrom(p) && p.IsClass).ToDictionary(x => x.Name, x => x);
    public static readonly Dictionary<string, Type> Verifiers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(IVerifier).IsAssignableFrom(p) && p.IsClass).ToDictionary(x => x.Name, x => x);

    static IProblem Problem(string name) {
        #pragma warning disable CS8603 // Possible null reference return.
        return Activator.CreateInstance(Problems[name]) as IProblem; // guaranteed success by `IsAssignableFrom`
        #pragma warning restore CS8603 // Possible null reference return.
    }

    static IVerifier Verifier(string name) {
        #pragma warning disable CS8603 // Possible null reference return.
        return Activator.CreateInstance(Verifiers[name]) as IVerifier; // guaranteed success by `IsAssignableFrom`
        #pragma warning restore CS8603 // Possible null reference return.
    }

    [HttpPost("verify")]
    public string verify([FromBody]Verify verify) {
        // TODO: validate arguments
        return JsonSerializer.Serialize(
            Verifier(verify.Verifier).verify(verify.ProblemInstance, verify.Certificate).ToString(),
            new JsonSerializerOptions { WriteIndented = true }
        );
    }
}

public class Verify {
    public string Verifier { get; set; } = "";
    public string Certificate { get; set; } = "";
    public string ProblemInstance { get; set; } = "";
}
