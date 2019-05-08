using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Gridcoin.Rpc
{
    /// <summary>
    /// A basic JSON RPC protocol client.
    /// </summary>
    public class RpcClient
    {
        /// <summary>
        /// Communicates with the JSON RPC server over HTTP.
        /// </summary>
        private HttpClient http;

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcClient" /> class.
        /// </summary>
        public RpcClient(Uri connectionUri, string user, string password)
        {
            this.http = new HttpClient();

            this.http.BaseAddress = connectionUri;

            this.http.DefaultRequestHeaders.Accept.Clear();
            this.http.DefaultRequestHeaders.Add("User-Agent", ".NET Client");

            this.SetCredentials(user, password);
        }

        /// <summary>
        /// Gets the underlying HTTP client that makes JSON RPC requests.
        /// </summary>
        public HttpClient HttpClient
        {
            get
            {
                return this.http;
            }
        }

        /// <summary>
        /// Set the username and password used to authenticate with the JSON
        /// RPC server.
        /// </summary>
        /// <param name="user">RPC username set in the config file.</param>
        /// <param name="pallword">RPC password set in the config file.</param>
        public void SetCredentials(string user, string password)
        {
            var auth = $"{user}:{password}";

            var header = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes(auth)));

            this.http.DefaultRequestHeaders.Authorization = header;
        }

        /// <summary>
        /// Send the supplied request to the JSON RPC server and fetch the
        /// response.
        /// </summary>
        /// <typeparam name="T">Type of the data structure that matches the
        /// data contained in the RPC response result field.</typeparam>
        /// <param name="request">Contains the name of the RPC method and any
        /// parameters.</param>
        /// <returns>A <c>Task</c> object that provides the RPC response upon
        /// completion.</returns>
        /// <exception cref="IOException">If the request terminates because of
        /// a network error.</exception>
        /// <exception cref="HttpRequestException">If the HTTP response returns
        /// with an unsuccessful status code.</exception>
        /// <exception cref="SerializationException">If the JSON response fails
        /// to deserialize into the data structure of type <c>T</c>.</exception>
        public async Task<Response<T>> Execute<T>(Request request)
        {
            var bytes = new ByteArrayContent(request.Serialize());

            // The RPC server expects no path component in the URL, so we set
            // it to an empty string:
            var response = await this.http.PostAsync(string.Empty, bytes);

            // TODO: validate that the response JSON RPC protocol version and
            // request ID match. Not strictly necessary at the moment.

            // The RPC server responds with HTTP code 500 when it handles an
            // invalid request. We store the JSON-encoded error message in case
            // we need to provide the error as feedback:
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return await this.Deserialize<T>(response);
            }
            else
            {
                response.EnsureSuccessStatusCode();
            }

            return await this.Deserialize<T>(response);
        }

        /// <summary>
        /// Call the specified RPC method and fetch the response.
        /// </summary>
        /// <typeparam name="T">Type of the data structure that matches the
        /// data contained in the RPC response result field.</typeparam>
        /// <param name="method">Name of the RPC method to call.</param>
        /// <param name="args">Any input expected by the method.</param>
        /// <returns>A <c>Task</c> object that provides the RPC response upon
        /// completion.</returns>
        /// <exception cref="IOException">If the request terminates because of
        /// a network error.</exception>
        /// <exception cref="HttpRequestException">If the HTTP response returns
        /// with an unsuccessful status code.</exception>
        /// <exception cref="SerializationException">If the JSON response fails
        /// to deserialize into the data structure of type <c>T</c>.</exception>
        public async Task<Response<T>> Execute<T>(string method, params object[] args)
        {
            return await this.Execute<T>(new Request(method, args));
        }

        /// <summary>
        /// Deserialize the provided JSON RPC response into an object.
        /// </summary>
        /// <typeparam name="T">Type of the data structure that matches the
        /// data contained in the RPC response result field.</typeparam>
        /// <param name="response">Contains the result of the request.</param>
        /// <returns>A <c>Task</c> object that provides the deserialized RPC
        /// response object upon completion.</returns>
        /// <exception cref="SerializationException">If the JSON response fails
        /// to deserialize into the data structure of type <c>T</c>.</exception>
        private async Task<Response<T>> Deserialize<T>(HttpResponseMessage response)
        {
            var deserializer = new DataContractJsonSerializer(typeof(Response<T>));
            var stream = await response.Content.ReadAsStreamAsync();

            return deserializer.ReadObject(stream) as Response<T>;
        }
    }
}
