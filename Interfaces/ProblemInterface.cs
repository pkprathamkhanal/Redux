namespace API.Interfaces;

interface IProblem {
    string problemName{get;}

    string formalDefinition{get;}
    string problemDefinition{get;}
    string source {get;}
    string wikiName {get;}
    string defaultInstance{get;}

    string[] contributors{ get; }
}

interface IProblem<T,U> : IProblem where T : ISolver where U : IVerifier{
    T defaultSolver{get;}
    U defaultVerifier{get;}
}
