using CBS.APICaller.Helper.LoginModel.Authenthication;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using CBS.APICaller.Helper;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CBS.PortfolioManagement.Helper
{
    public abstract class APICallHelper
    {

        public static async Task<LoginDto> AuthenthicationFromIdentityServer(PathHelper _pathHelper)
        {
            AuthRequest auth = new AuthRequest(_pathHelper.UserName, _pathHelper.Password);
            var ApiCallerHelper = new ApiCallerHelper(_pathHelper.IdentityServerBaseUrl);
            var responseobj = await ApiCallerHelper.PostAsync<LoginDto>(_pathHelper.AuthenthicationUrl, auth);
            return responseobj;
        }

        public static async Task<T> PostData<T>(string baseURL,string url, string stringifyJsonObject, string token)
        {
            var ApiCallerHelper = new ApiCallerHelper(baseURL, token);
            var responseobj = await ApiCallerHelper.PostAsync<T>(url, stringifyJsonObject);
            return responseobj;
        }

        public static async Task<T> GetData<T>(string baseURL,string url, string token)
        {
            var ApiCallerHelper = new ApiCallerHelper(baseURL, token);
            var responseobj = await ApiCallerHelper.GetAsync<T>(url);
            return responseobj;
        }

        public static async Task<T> GetObjectById<T>(string baseURL, string endUrl, string token,string id)
        {
            var ApiCallerHelper = new ApiCallerHelper(baseURL, token);
            var responseObj = await ApiCallerHelper.GetAsync<T>(string.Format(endUrl, id));
            return responseObj;
        }

        public static async Task AuditLogger<T>(string userName, string action, T objectToSerialize, string detailMessage, string level, int statuscode, string token,string corollationid=null)
        where T : class
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string baseUrl = configuration.GetSection("AuditTrails:BaseUrl").Value;
            string auditTrailEndpoint = configuration.GetSection("AuditTrails:AuditTrailEndpoint").Value;
            string microserviceName = configuration.GetSection("AuditTrails:MicroserviceName").Value;
            string stringifyObject = objectToSerialize != null ? JsonConvert.SerializeObject(objectToSerialize) : null;
            AuditTrailLogger logger = new AuditTrailLogger(action, userName, microserviceName, stringifyObject, detailMessage, level, statuscode, corollationid);

            var ApiCallerHelper = new ApiCallerHelper(baseUrl, token);
            await ApiCallerHelper.PostAsync(auditTrailEndpoint, logger);
        }

        public static async Task AuditLogger<T>(
                string action,
                T objectToSerialize,
                string detailMessage,
                string level,
                int statuscode,
                string correlationId,
                string userName,
                string token)
                where T : class
        {
            try
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string baseUrl = configuration.GetSection("AuditTrails:BaseUrl").Value;
                string auditTrailEndpoint = configuration.GetSection("AuditTrails:AuditTrailEndpoint").Value;
                string microserviceName = configuration.GetSection("AuditTrails:MicroserviceName").Value;

                string stringifyObject = objectToSerialize != null ? JsonConvert.SerializeObject(objectToSerialize) : null;

                AuditTrailLogger logger = new AuditTrailLogger(action, userName, microserviceName, stringifyObject, detailMessage, level, statuscode, correlationId);

                var apiCallerHelper = new ApiCallerHelper(baseUrl, token);
                await apiCallerHelper.PostAsync(auditTrailEndpoint, logger);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to log audit trail: {ex.Message}");
            }
        }
    }
}
