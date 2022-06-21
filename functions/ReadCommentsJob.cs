

using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using Newtonsoft.Json.Linq;

namespace MyAmbassadorDemo.Function
{
  public class ReadCommentsJob {
    private ApimService _apimService;

    public ReadCommentsJob() {
      _apimService = new ApimService("https://seaki-tokenstore-demo.azure-api.net", "");
    }

    public async Task RunAsync() {
        var comments = await _apimService.ListCommentOfDiscussionAsync();
    }
  }
}