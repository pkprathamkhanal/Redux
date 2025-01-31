
namespace API.Tools.ApiParameters;

/// <summary>API parameters for the verify routes.</summary>
public class Verify {
    /// <summary>The certificate solution to the problem.</summary>
    public string Certificate { get; set; } = "";
    /// <summary>The problem instance.</summary>
    public string ProblemInstance { get; set; } = "";
}
