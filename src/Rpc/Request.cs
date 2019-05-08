using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Gridcoin.Rpc
{
    /// <summary>
    /// Represents the body of a JSON RPC request.
    /// </summary>
    [DataContract]
    public class Request
    {
        /// <summary>
        /// Serializes a <see cref="Request" /> object into JSON.
        /// </summary>
        private static DataContractJsonSerializer Serializer;

        /// <summary>
        /// Initializes the static state of the <see cref="Request" /> class.
        /// </summary>
        static Request()
        {
            Serializer = new DataContractJsonSerializer(typeof(Request));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Request" /> class.
        /// </summary>
        public Request()
        {
            this.Parameters = new List<object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Request" /> class.
        /// </summary>
        /// <param name="method">Name of the RPC method to call.</param>
        public Request(string method) : base()
        {
            this.Method = method;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Request" /> class.
        /// </summary>
        /// <param name="method">Name of the RPC method to call.</param>
        /// <param name="parameters">Any input expected by the method.</param>
        public Request(string method, params object[] parameters)
        {
            this.Method = method;
            this.Parameters = new List<object>(parameters);
        }

        /// <summary>
        /// Gets or sets a value that describes the JSON RPC protocol version.
        /// </summary>
        [DataMember(Name="jsonrpc")]
        public string ProtocolVersion { get; set; } = "1.0";

        /// <summary>
        /// Gets or sets a unique, arbitrary value that identifies a request.
        /// Should match the ID returned in the response.
        /// </summary>
        [DataMember(Name="id")]
        public string RequestId { get; set; }

        /// <summary>
        /// Gets or sets the name of the RPC method to call.
        /// </summary>
        [DataMember(Name="method")]
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the sequence of any parameters expected by the method.
        /// </summary>
        [DataMember(Name="params")]
        public List<object> Parameters { get; set; }

        /// <summary>
        /// Serialize the request object into a JSON string.
        /// </summary>
        /// <returns>The JSON representation of the request as bytes.</returns>
        public byte[] Serialize()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.WriteObject(stream, this);

                return stream.ToArray();
            }
        }
    }
}
