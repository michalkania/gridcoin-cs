using System;
using System.Runtime.Serialization;
    
namespace Gridcoin
{
    /// <summary>
    /// Represents an error specific to a particular RPC method call.
    /// </summary>
    [Serializable]
    public class RpcException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RpcException" /> class
        /// with a default message.
        /// </summary>
        public RpcException() : base("The RPC server responded with an error.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcException"/> class
        /// with the provided error message.
        /// </summary>
        /// <param name="message">The error description.</param>
        public RpcException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcException"/> class
        /// from the provided RPC error object.
        /// </summary>
        /// <param name="error">Describes the RPC error that occurred.</param>
        public RpcException(Error error) : base(error.Message)
        {
            this.Code = error.Code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcException"/> class
        /// with the provided message and the exception that caused this
        /// exception.
        /// </summary>
        /// <param name="message">The error description.</param>
        /// <param name="innerException">The exception instance that caused
        /// this exception.</param>
        public RpcException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcException"/> class
        /// when de-serializing the exception object after transmission.
        /// </summary>
        /// <param name="serializationInfo">Contains serialized data.</param>
        /// <param name="streamingContext">Describes the source or destination
        /// of the serialized data.</param>
        protected RpcException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        /// <summary>
        /// Gets or sets the code that describes the type of RPC error.
        /// </summary>
        public int Code { get; protected set; }
    }
}
