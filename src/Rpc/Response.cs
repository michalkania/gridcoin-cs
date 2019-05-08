using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Gridcoin
{
    /// <summary>
    /// Represents the body of a JSON RPC response.
    /// </summary>
    /// <typeparam name="T">Type of the data structure that matches the data
    /// contained in the RPC response result field.</typeparam>
    [DataContract]
    public class Response<T>
    {
        /// <summary>
        /// Gets or sets a unique, arbitrary value that identifies a request.
        /// Should match the ID sent in the request.
        /// </summary>
        [DataMember(Name="id")]
        public string RequestId { get; set; }

        /// <summary>
        /// Gets or sets a value that contains the method-specific response
        /// data, if any.
        /// </summary>
        [DataMember(Name="result")]
        public T Result { get; set; }

        /// <summary>
        /// Gets or sets a value that describes an error returned by the RPC
        /// server specific to a particular method call.
        /// </summary>
        [DataMember(Name="error")]
        public Error Error { get; set; }

        /// <summary>
        /// Gets a value indicating whether the request succeeded.
        /// </summary>
        public bool IsSuccessful
        {
            get
            {
                return !this.IsError;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the request failed.
        /// </summary>
        public bool IsError
        {
            get
            {
                return this.Error != null;
            }
        }
    }

    /// <summary>
    /// Describes an error returned by the RPC server specific to a particular
    /// method call.
    /// </summary>
    [DataContract]
    public class Error
    {
        /// <summary>
        /// Gets or sets a value that identifies the type of error.
        /// </summary>
        [DataMember(Name="code")]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets a description of the error.
        /// </summary>
        [DataMember(Name="message")]
        public string Message { get; set; }
    }
}
