using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
// using Google.Apis.Gmail.v1.Data;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyAmbassadorDemo.Function
{
    internal class ApimService
    {
        private readonly string _baseUrl;
        private readonly string _subscriptionKey;
        private readonly HttpClient _httpClient;

        public ApimService(string baseUrl, string subscriptionKey, string identityToken = null)
        {
            _baseUrl = baseUrl;
            _subscriptionKey = subscriptionKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            // _httpClient.DefaultRequestHeaders.Add("Authorization", identityToken);
        }

        public async Task<string> GetTokenBackAsync(string providerId, string authorizationId)
        {
            var endpoint = $"{_baseUrl}/token-store/fetch?provider-id={providerId}&authorization-id={authorizationId}";
            var response = await _httpClient.GetAsync(endpoint);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Fetch token call unsuccessful: {content}");
            }
            return content;
        }

        public async Task<GithubDiscussionComment[]> ListCommentOfDiscussionAsync() 
        {
            var githubGraphQLClient = new GraphQLHttpClient(_baseUrl + "/api/github-gql", new NewtonsoftJsonSerializer());
            githubGraphQLClient.HttpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);

            var request = new GraphQLRequest() {
                Query = @"
query {
	repository(name: ""APIM-Ambassador-demo"", owner: ""APIManagementAuthorizations"") {
		discussion(number: 1) {
			comments(last: 10) {
				nodes {
					author {
						login
					}
					body
					createdAt
					id
					url
				}
			}
		}
	}
}
"
            };
            var response = await githubGraphQLClient.SendQueryAsync<JObject>(request);
            var comments = response.Data["repository"]["discussion"]["comments"]["nodes"].ToObject<GithubDiscussionComment[]>();
            return comments;
        }
    }
}