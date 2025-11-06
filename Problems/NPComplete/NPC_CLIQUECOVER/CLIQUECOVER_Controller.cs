using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Interfaces.JSON_Objects.Graphs;
using API.Interfaces.Graphs.GraphParser;
using API.Problems.NPComplete.NPC_CLIQUECOVER;
using API.Problems.NPComplete.NPC_CLIQUECOVER.Solvers;
using API.Problems.NPComplete.NPC_CLIQUECOVER.Verifiers;


namespace API.Problems.NPComplete.NPC_CLIQUECOVER;
