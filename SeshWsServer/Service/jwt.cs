using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using JWT.Serializers;
using SeshWsServer.Model;

namespace SeshWsServer.Service
{
    public enum JwtDecodeResult
    {
        ok,
        expError,
        signError
    }
    class jwt
    {
        private readonly string secret;
        private IJwtDecoder decoder;

        public jwt()
        {
            secret = Environment.GetEnvironmentVariable("secret");
            IJsonSerializer serializer = new JsonNetSerializer();
            UtcDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
            decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
        }

        public jwt(string secret)
        {
            this.secret = secret;
            IJsonSerializer serializer = new JsonNetSerializer();
            UtcDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
            decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
        }

        public JwtDecodeResult validateToke(string token, out string payload)
        {
            payload = string.Empty;

            try
            {
                payload = decoder.Decode(token, secret, true);

            }
            catch (TokenExpiredException)
            {

                return JwtDecodeResult.expError;
            }
            catch (SignatureVerificationException)
            {
                return JwtDecodeResult.signError;
            }

            return JwtDecodeResult.ok;
        }

        public JwtDecodeResult validateToke(string token)
        {
            try
            {
                decoder.Decode(token, secret, true);
            }
            catch (TokenExpiredException)
            {

                return JwtDecodeResult.expError;
            }
            catch (SignatureVerificationException)
            {
                return JwtDecodeResult.signError;
            }

            return JwtDecodeResult.ok;
        }

        public JwtDecodeResult validateTokenToObj(string token, out TokenUserPayload payload)
        {
            payload = null;

            try
            {
                payload = decoder.DecodeToObject<TokenUserPayload> (token, secret, true);
            }
            catch (TokenExpiredException)
            {

                return JwtDecodeResult.expError;
            }
            catch (SignatureVerificationException)
            {
                return JwtDecodeResult.signError;
            }
            catch (Exception ex)
            {
                consoleInfo.onError(ex.Message);
                payload = null;
            }

            return JwtDecodeResult.ok;
        }

        public bool isValidateToke(string token)
        {
            try
            {
                decoder.Decode(token, secret, true);
            }
            catch (TokenExpiredException)
            {

                return false;
            }
            catch (SignatureVerificationException)
            {
                return false;
            }

            return true;
        }

        public T getClaimByName<T>(string token, string claimName)
        {
            return (T)decoder.DecodeToObject(token)[claimName];
        }
    }
}
