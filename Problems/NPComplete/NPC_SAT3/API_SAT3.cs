

using System.Security.Cryptography.Xml;
using API.Interfaces.JSON_Objects;
using API.Problems.NPComplete.NPC_CLIQUE.Inherited;
using API.Problems.NPComplete.NPC_SAT3;
using Microsoft.VisualBasic;

class API_SAT3 : API_JSON
{
    private List<List<string>> _clauses;
    public API_SAT3(SAT3 instance)
    {
        _clauses = instance.clauses;
    }

    public List<List<string>> clauses
    {
        get
        {
            return _clauses;
        }
    }


}