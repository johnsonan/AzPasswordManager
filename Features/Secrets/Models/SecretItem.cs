using System.Text.Json.Serialization;

namespace AzPasswordManager.Features.Secrets
{
    public class SecretItem : ISecretItem
    {
        public bool Managed { get; set; }
        public string ContentType { get; set; }
        public string Id { get; set; }
        public SecretAttributes Attributes { get; set; }
        public Dictionary<string, string>? Tags { get; set; }

        [JsonIgnore]
        public string Name
        {
            get => Id.Split("/").Last();

            set { }
        }

        [JsonIgnore]
        public string? Username
        {
            get => Tags?.Where(Tag => Tag.Key == "Username")
                    .Select(Tag => Tag.Value)
                    .FirstOrDefault() ?? string.Empty;
        }

        [JsonIgnore]
        public string? VaultLocation
        {
            get => Tags?.Where(Tag => Tag.Key == "VaultLocation")
                    .Select(Tag => Tag.Value)
                    .FirstOrDefault() ?? string.Empty;
        }

        [JsonIgnore]
        public string? PersonalVaultOwner
        {
            get => Tags?.Where(Tag => Tag.Key == "PersonalVaultOwner")
                    .Select(Tag => Tag.Value)
                    .FirstOrDefault() ?? string.Empty;
        }

        [JsonIgnore]
        public string? Url
        {
            get => Tags?.Where(Tag => Tag.Key == "Url")
                    .Select(Tag => Tag.Value)
                    .FirstOrDefault() ?? string.Empty;
        }

        [JsonIgnore]
        public List<string>? UserTags
        {
            get => Tags?.Where(Tag => Tag.Key == "UserTags")
                    .Select(Tag => Tag.Value)
                    .FirstOrDefault()?.Split(";").ToList();
        }
    }
}
