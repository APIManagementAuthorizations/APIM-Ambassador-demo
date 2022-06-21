

using System;
using System.Net.Http;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ReadCommentsJob {
  private readonly GraphQLHttpClient _graphQLClient;

  public ReadCommentsJob() {
    _graphQLClient = new GraphQLHttpClient("https://seaki-tokenstore-demo.azure-api.net/github-gql", new NewtonsoftJsonSerializer());
    _graphQLClient.HttpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "");
  }

  public async Task RunAsync() {

    // var requestBody = new 
    var request = new GraphQLRequest() {
      Query = @"
query {
	repository(name: ""APIM-Ambassador-demo"", owner: ""APIManagementAuthorizations"") {
		discussion(number: 1) {
			comments(first: 10) {
				nodes {
					body
					id
				}
			}
		}
	}
}
      "
    };
    var response = await _graphQLClient.SendQueryAsync<JObject>(request);
    Console.WriteLine(response.Data);
    return;    
  }
}