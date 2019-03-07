using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Pdf4meClient
{
    public class Pdf4me
    {

        public static readonly Pdf4me Instance = new Pdf4me();

        string _api = "https://api.pdf4me.com";
        string _basicToken = "";

        static Pdf4me()
        {


        }

        // Instance constructor is private to enforce singleton semantics
        private Pdf4me() : base()
        {
        }

        /// <summary>
        /// Initialise with basic token
        /// </summary>
        /// <param name="basicToken">Token</param>
        /// <param name="api">Api url to be set. If passed null, url will https://api-dev.pdf4me.com</param>
        public void Init(string basicToken, string api = "")
        {
            _basicToken = basicToken;

            if (!string.IsNullOrEmpty(api))
            {
                _api = api;
            }
        }

        public ConvertClient ConvertClient
        {
            get
            {
                return new ConvertClient(getApi());
            }
        }

        public DocumentClient DocumentClient
        {
            get
            {
                return new DocumentClient(getApi());
            }
        }

        public ExtractClient ExtractClient
        {
            get
            {
                return new ExtractClient(getApi());
            }
        }

        public ImageClient ImageClient
        {
            get
            {
                return new ImageClient(getApi());
            }
        }

        /*public JobClient JobClient
        {
            get
            {
                return new JobClient(getApi());
            }
        }*/

        /*public ManagementClient ManagementClient
        {
            get
            {
                return new ManagementClient(getApi());
            }
        }*/

        public MergeClient MergeClient
        {
            get
            {
                return new MergeClient(getApi());
            }
        }

        public OcrClient OcrClient
        {
            get
            {
                return new OcrClient(getApi());
            }
        }

        public OptimizeClient OptimizeClient
        {
            get
            {
                return new OptimizeClient(getApi());
            }
        }

        public PdfAClient PdfAClient
        {
            get
            {
                return new PdfAClient(getApi());
            }
        }

        public SplitClient SplitClient
        {
            get
            {
                return new SplitClient(getApi());
            }
        }

        public StampClient StampClient
        {
            get
            {
                return new StampClient(getApi());
            }
        }

        public BarcodeClient BarcodeClient
        {
            get
            {
                return new BarcodeClient(getApi());
            }
        }

        public HttpClient getApi()
        {
            HttpClient client;

            if (!string.IsNullOrEmpty(_basicToken))
            {

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                client = new HttpClient();
                client.Timeout = new TimeSpan(0, 5, 0);


                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _basicToken);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.UserAgent.Clear();
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("pdf4me-core", "0.5.5"));

                Uri apiUri = new Uri(_api);
                client.BaseAddress = apiUri;
            }
            else
                throw new ApplicationException("Missing token for authentication, please give BasicToken");



            return client;
        }
    }
}
