namespace SampleDDDMicroserviceApp.TransportPeople.Domain.SharedKernel.Constants
{
    public static class AppConst
    {
        public static readonly string IdentityTokenId = nameof(IdentityTokenId);
        public static readonly string CdnBaseUrl = "https://cdn.com";

        /// <summary>
        /// Default alert modal fade out in Seconds
        /// </summary>
        public const int AlertDefaultTimeout = 7;


        /// <summary>
        /// Authorization Token
        /// </summary>
        public static class AuthToken
        {

        }

        public class UploadLimits
        {
            public static readonly int DocMaxSizeAllowedToUploadInKiloBytes = 10000;
        }

        /// <summary>
        /// Strings that only apply to web app
        /// </summary>
        public static class WebApp
        {
            public static readonly string ClientName = "-";
            public static readonly string ClientId = "-";
            public static readonly string Secret = "-";
        }

        public class Roles
        {
            public const string Administrator = nameof(Administrator);
        }

        public static class Regex
        {
            public const string EmailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

            // See <see href="http://stackoverflow.com/a/7933253/433790" /> to find regex source.
            public const string SubDomainRegex = "[A-Za-z0-9](?:[A-Za-z0-9\\-]{0,61}[A-Za-z0-9])?";

            // See <see href="https://stackoverflow.com/a/17773849/6661314" /> to find regex source.
            public const string UrlRegex = @"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})";

            // See <see href="https://stackoverflow.com/questions/1636350/how-to-identify-a-given-string-is-hex-color-format" /> to find regex source.
            public const string HexColor = "^#(?:[0-9a-fA-F]{3}){1,2}$";

            // See <see href="https://stackoverflow.com/questions/19605150/regex-for-password-must-contain-at-least-eight-characters-at-least-one-number-a"/> to find regex source.
            public static string Password = "^(?=.*[\\w])(?=.*[\\W])[\\w\\W]{8,}$";

            // See <see href="https://www.regextester.com/93648"/> to find regex source.
            public static string Name = "^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$";
        }
    }
}