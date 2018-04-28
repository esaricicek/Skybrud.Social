using Skybrud.Social.Http;
using Skybrud.Social.Interfaces.Http;

namespace Skybrud.Social.OAuth.Models {

    /// <summary>
    /// Class representing the response body of a call to get an OAuth 1.0a refresh token.
    /// </summary>
    public class SocialOAuthRequestToken {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent OAuth client.
        /// </summary>
        public SocialOAuthClient Client { get; }

        /// <summary>
        /// Gets the request token.
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Gets the request token secret.
        /// </summary>
        public string TokenSecret { get; }

        /// <summary>
        /// Is the callback confirmed?
        /// </summary>
        public bool IsCallbackConfirmed { get; }

        /// <summary>
        /// Gets the authentication URL for this request token.
        /// </summary>
        public string AuthorizeUrl { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="client"/> and <paramref name="query"/>.
        /// </summary>
        /// <param name="client">The parent OAuth client.</param>
        /// <param name="query">The query string as specified by the response body.</param>
        protected SocialOAuthRequestToken(SocialOAuthClient client, IHttpQueryString query) {
            Client = client;
            Token = query["oauth_token"];
            TokenSecret = query["oauth_token_secret"];
            IsCallbackConfirmed = query["oauth_callback_confirmed"] == "true";
            AuthorizeUrl = client.AuthorizeUrl + "?oauth_token=" + query["oauth_token"];
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses a query string received from the API.
        /// </summary>
        /// <param name="client">The parent OAuth client.</param>
        /// <param name="str">The query string.</param>
        public static SocialOAuthRequestToken Parse(SocialOAuthClient client, string str) {

            // Convert the query string to an IHttpQueryString
            IHttpQueryString query = SocialHttpQueryString.ParseQueryString(str);

            // Initialize a new instance
            return new SocialOAuthRequestToken(client, query);

        }

        #endregion

    }

}