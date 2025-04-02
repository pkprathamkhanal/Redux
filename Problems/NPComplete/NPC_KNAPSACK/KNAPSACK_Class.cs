using System.Text.Json.Serialization;
using API.Interfaces;
using API.Problems.NPComplete.NPC_KNAPSACK.Solvers;
using API.Problems.NPComplete.NPC_KNAPSACK.Verifiers;
using DiscreteParser;


namespace API.Problems.NPComplete.NPC_KNAPSACK;

class KNAPSACK : IProblem<KnapsackBruteForce, KnapsackVerifier> {

    // --- Fields ---
    public string problemName {get;} = "Knapsack (Binary)";

    public string formalDefinition {get;} = "KNAPSACK = {<H, W, V> | H is a set of items (w,v) and there is a subset of items in H whose collective weight is less than or equal to W and whose collective value is equal or greater than V.}";

    public string problemDefinition {get;} = "The 0-1 KNAPSACK decision problem is given a knapsack with a maximum capacity W and target value V and a set of n items x_1, x_2,... x_n with weights w_1,w_2,... w_n and values v_1,v_2,... v_n find the combination of singular items that provide greater than V value while staying under W. ";

    // How we want format
    public string source {get;} = "";

    public string[] contributors {get;} = { "Garret Stouffer", "Daniel Igbokwe"};
    
    public string instance {get;set;} = string.Empty;


    private static readonly string _defaultInstance = "({(10,60),(20,100),(30,120)},50,220)";
    public string defaultInstance {get;} = _defaultInstance;
 

    public string wikiName {get;} = "Knapsack";

    public UtilCollection items { get; set; }

    private int _W = 0;

    public int V { get; set; }


    public KnapsackBruteForce defaultSolver {get;} = new KnapsackBruteForce();
    public KnapsackVerifier defaultVerifier {get;} = new KnapsackVerifier();

    // --- Properties ---
    public int W {
        get {
            return _W;
        }
        set {
            _W = value;
        }
    }

    // --- Methods Including Constructors ---
    public KNAPSACK() : this(_defaultInstance) {

    }

    public KNAPSACK(string HWVInput) {
        StringParser parser = new("{(i, w, v) | i subset int cross int, w is int, v is int}");
        parser.parse(HWVInput);
        items = parser["i"];
        W = int.Parse(parser["w"].ToString());
        V = int.Parse(parser["v"].ToString());
/*
        UtilCollection collection = new UtilCollection(HWVInput);
        instance = collection.ToString();
        collection.assertPair(3);
        items = collection[0];
        items.assertUnordered();
        W = int.Parse(collection[1].ToString());
        V = int.Parse(collection[2].ToString());
        foreach (UtilCollection item in items) item.assertPair();
        */
    }
}