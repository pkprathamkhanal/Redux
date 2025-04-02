using API.Interfaces;
using API.Problems.NPComplete.NPC_SAT.Solvers;
using API.Problems.NPComplete.NPC_SAT.Verifiers;



namespace API.Problems.NPComplete.NPC_SAT;

 class SAT : IProblem<SATBruteForceSolver, SATVerifier> {


    #region Fields

    // --- Fields ---
    public string problemName {get;} = "SAT";
    public string formalDefinition {get;} = "SAT = {Φ | Φ is a satisfiable Boolean formula}";
    public string problemDefinition {get;} = "SAT, or the Boolean satisfiability problem, is a problem that asks for a list of assignments to the literals of phi to result in 'True'";
    public string source {get;} = ".";
    public string[] contributors {get;} = { "Daniel Igbokwe" };

    public string defaultInstance {get;} = "(x1 | !x2 | x3) & (!x1 | x3 | x1) & (x2 | !x3 | x1) & (!x3 | x4 | !x2 | x1) & (!x4 | !x1) & (x4 | x3 | !x1)";
    public string instance {get;set;} = string.Empty;

    public string wikiName {get;} = "";
    private List<List<string>> _clauses = new List<List<string>>();
    private List<string> _literals = new List<string>();
   
    public SATBruteForceSolver defaultSolver {get;} = new SATBruteForceSolver();
    public SATVerifier defaultVerifier {get;} = new SATVerifier();

    #endregion


    #region Properties
    // --- Properties ---
     public List<List<string>> clauses {
        get {
            return _clauses;
        }
        set {
            _clauses = value;
        }
    }
    public List<string> literals {
        get {
            return _literals;
        }
        set {
            _literals = value;
        }
    }

    #endregion

    #region Constructors
    // --- Methods Including Constructors ---
    public SAT() {
        instance = defaultInstance;
         clauses = getClauses(instance);
        literals = getLiterals(instance);
      
    }
    public SAT(string phiInput) {
        instance = phiInput;
         clauses = getClauses(phiInput);
        literals = getLiterals(phiInput);
     
    }

    #endregion


    #region Methods

    public void ParseProblem(string phiInput) {
    }

     public List<List<string>> getClauses(string phiInput) {
        
        List<List<string>> clauses = new List<List<string>>();

        // Strip extra characters
        string strippedInput = phiInput.Replace(" ", "").Replace("(", "").Replace(")","");

        // Parse on | to collect each clause
        string[] rawClauses = strippedInput.Split('&');

        foreach(string clause in rawClauses) {
            List<string> clauseToAdd = new List<string>();
            string[] literals = clause.Split('|');

            foreach(string literal in literals) {
                clauseToAdd.Add(literal);
            }
            clauses.Add(clauseToAdd);
        }

        return clauses;

    }

    public List<string> getLiterals(string phiInput) {
        
        List<string> literals = new List<string>();
        string strippedInput = phiInput.Replace(" ", "").Replace("(", "").Replace(")","");

        // Parse on | to collect each clause
        string[] rawClauses = strippedInput.Split('|');

        foreach(string clause in rawClauses) {
            string[] rawLiterals = clause.Split('&');

            foreach(string literal in rawLiterals) {
                literals.Add(literal);
            }
        }
        return literals;
    }

    #endregion
        
}

