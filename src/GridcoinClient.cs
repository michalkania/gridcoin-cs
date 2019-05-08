using System;
using System.Threading.Tasks;
using Gridcoin.Rpc;

namespace Gridcoin
{
    /// <summary>
    /// A client that interfaces with the Gridcoin core agent through the JSON
    /// RPC protocol.
    /// </summary>
    public class GridcoinClient
    {
        /// <summary>
        /// Sends JSON RPC requests to the Gridcoin core agent and parses the
        /// JSON responses.
        /// </summary>
        private RpcClient rpc;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridcoinClient"/> class.
        /// </summary>
        /// <param name="host">The Gridcoin core agent hostname or IP address
        /// to connect to.</param>
        /// <param name="user">Username used to authenticate.</param>
        /// <param name="password">Password used to authenticate.</param>
        /// <param name="port">RPC listening port to connect to.</param>
        /// <param name="secure">If <c>true</c>, connect via SSL/TLS. Requires
        /// a Gridcoin agent configured for SSL (<c>-rpcssl</c>).</param>
        public GridcoinClient(
            string host,
            string user,
            string password,
            ushort port = 15715, // testnet: 25715
            bool secure = false)
        {
            var scheme = secure ? "https" : "http";
            var connectionUri = new Uri($"{scheme}://{host}:{port}/");

            this.rpc = new RpcClient(connectionUri, user, password);
        }

        /// <summary>
        /// Gets the underlying client that makes JSON RPC requests.
        /// </summary>
        public RpcClient RpcClient
        {
            get
            {
                return this.rpc;
            }
        }

        /// <summary>
        /// Get basic overview information from the current Gridcoin core agent.
        /// </summary>
        /// <exception cref="IOException">If the request terminates because of
        /// a network error.</exception>
        /// <exception cref="HttpRequestException">If the HTTP response returns
        /// with an unsuccessful status code.</exception>
        /// <exception cref="SerializationException">If the JSON response fails
        /// to deserialize into the data structure of type <c>T</c>.</exception>
        /// <exception cref="RpcException">If the RPC server responds with an
        /// error specific to the RPC method call.</exception>
        public async Task<ServerInfo> GetInfo()
        {
            return await this.Execute<ServerInfo>("getinfo");
        }

        /// <summary>
        /// Get the current blockchain height.
        /// </summary>
        /// <exception cref="IOException">If the request terminates because of
        /// a network error.</exception>
        /// <exception cref="HttpRequestException">If the HTTP response returns
        /// with an unsuccessful status code.</exception>
        /// <exception cref="SerializationException">If the JSON response fails
        /// to deserialize into the data structure of type <c>T</c>.</exception>
        /// <exception cref="RpcException">If the RPC server responds with an
        /// error specific to the RPC method call.</exception>
        public async Task<long> GetBlockCount()
        {
            return await this.Execute<long>("getblockcount");
        }


        // TODO: fill out the remaining RPC methods.


        /// <summary>
        /// Execute the specified RPC method, converting any RPC errors into
        /// exceptions.
        /// </summary>
        /// <typeparam name="T">Type of the data structure that matches the
        /// data contained in the RPC response result field.</typeparam>
        /// <param name="method">Name of the RPC method to call.</param>
        /// <param name="args">Any input expected by the method.</param>
        /// <returns>A <c>Task</c> object that provides the RPC response result
        /// data upon completion.</returns>
        /// <exception cref="IOException">If the request terminates because of
        /// a network error.</exception>
        /// <exception cref="HttpRequestException">If the HTTP response returns
        /// with an unsuccessful status code.</exception>
        /// <exception cref="SerializationException">If the JSON response fails
        /// to deserialize into the data structure of type <c>T</c>.</exception>
        /// <exception cref="RpcException">If the RPC server responds with an
        /// error specific to the RPC method call.</exception>
        private async Task<T> Execute<T>(string method, params object[] args)
        {
            var response = await this.rpc.Execute<T>(method, args);

            if (response.IsError)
            {
                throw new RpcException(response.Error);
            }

            return response.Result;
        }
    }
}
