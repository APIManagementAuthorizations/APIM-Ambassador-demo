

using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyAmbassadorDemo.Function
{
  public class ReadCommentsJob {
    private ApimService _apimService;

    public ReadCommentsJob() {
      _apimService = new ApimService("https://seaki-tokenstore-demo.azure-api.net", "");
    }

    public async Task RunAsync() {
        var comments = await _apimService.ListDiscussionCommentsAsync();
        var newComments = comments.Where(c => c.CreatedAt > DateTimeOffset.UtcNow.AddSeconds(-12)).ToArray();

        if (newComments.Any()) {
          var aliases = newComments.Where(c => c.Body.Contains(" - ")).Select(c => c.Body.Split(" - ")[0]).ToArray();
          var result = await _apimService.PostDiscussionCommentAsync(aliases);
        }
    }
  }
}