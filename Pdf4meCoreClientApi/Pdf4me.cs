using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pdf4me.Client
{
    public class Pdf4meClient
    {

        //public static readonly Pdf4meClient Instance = new Pdf4meClient();

        //string _api = "https://api-dev.pdf4me.com";
        string _authString = "https://login.microsoftonline.com/devynooxlive.onmicrosoft.com";
        // string _clientId = "";
        // string _key = "";
        private AuthConfig _authConfig;
        private HttpClient _httpClient;

        // Instance constructor is private to enforce singleton semantics
        public Pdf4meClient(AuthConfig authConfig) 
        {
            _authConfig = authConfig;
            //_clientId = ConfigurationManager.AppSettings["Pdf4meClientId"];
            //_key = ConfigurationManager.AppSettings["Pdf4meSecret"];
        }



        public DocumentClient DocumentClient
        {
            get
            {
                
                return new DocumentClient(getApi());
            }
        }

        public ManagementClient ManagementClient
        {
            get
            {
                return new ManagementClient( getApi());
            }
        }

        public PdfClient PdfClient
        {
            get
            {
                return new PdfClient(getApi());
            }
        }

        public LightClient LightClient
        {
            get
            {
                return new LightClient(getApi());
            }
        }

        public HttpClient getApi()
        {
            if (_httpClient != null) return _httpClient;

            //string tenantName = "devynooxlive.onmicrosoft.com";
            //string authString = "https://login.microsoftonline.com/" + tenantName;

            // SLApp
            //string clientId = "98a707a7-1860-4bbb-b956-51d95f1f338c";
            //string key = "o6YE76EHPPdnia7h/juHKIdDf7bWYgcu3PbzHuK6qJk=";
                       

            //string resource = clientId;


            ClientCredential clientCred = new ClientCredential(_authConfig.ClientId, _authConfig.Secret);

            string token;

            AuthenticationContext authenticationContext = new AuthenticationContext(_authString, false);
            AuthenticationResult authenticationResult = authenticationContext.AcquireTokenAsync(_authConfig.ClientId, clientCred).ConfigureAwait(false).GetAwaiter().GetResult();
            token = authenticationResult.AccessToken;
            //txtToken.Text = token;


            // Do Stamp

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization","Bearer " + token);
            //client.SetBearerToken(token);

            Uri apiUri = new Uri(_authConfig.Uri);
            
            client.BaseAddress = apiUri;

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            return _httpClient = client;
        }


    }
}
