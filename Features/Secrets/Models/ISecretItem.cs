namespace AzPasswordManager.Features.Secrets
{
    public interface ISecretItem
    {
        public bool Managed { get; set; }
        public string ContentType { get; set; }
        public string Id { get; set; }
        public SecretAttributes Attributes { get; set; }
        public Dictionary<string, string>? Tags { get; set; }
        public string Name { get; }
        public string? Username { get; }
        public string? VaultLocation { get; }
        public string? PersonalVaultOwner { get; }
        public string? Url { get; }

    }
}
