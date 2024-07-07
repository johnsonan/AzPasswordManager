using System.Text.Json.Serialization;

namespace AzPasswordManager.Features.Secrets
{
    public class SecretBundle : ISecretBundle
    {
        public SecretAttributes? Attributes { get; set; }
        public string ContentType { get; set; }
        public string Id { get; set; }
        public string KId { get; set; }
        public bool Managed { get; set; }
        public Dictionary<string, string> Tags { get; set; }
        public string Value { get; set; }

        [JsonIgnore]
        public string Name
        {
            get
            {
                return Id.Split("/").Last();
            }

            set { }
        }
    }
}
