using System;
using System.Threading.Tasks;

namespace PlacesAPI.CustomServices
{
    public interface IUniversalAuthenticationService
    {
        public Task<String> GetAuthenticationToken();
    }
}