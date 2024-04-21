using System.Collections.Generic;

namespace ValantDemoApi.Models.GetMazes;
 
public sealed record GetMazesResponse(int total, IEnumerable<string> items);
