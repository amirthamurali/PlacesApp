using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using PlacesAPI.CustomServices;

namespace PlacesAPI.CustomHandlers
{
    public class BearerTokenHandler : DelegatingHandler
    {
        private readonly IUniversalAuthenticationService _universalAuthenticationService;

        public BearerTokenHandler(IUniversalAuthenticationService universalAuthenticationService)
        {
            _universalAuthenticationService = universalAuthenticationService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authToken = await _universalAuthenticationService.GetAuthenticationToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}