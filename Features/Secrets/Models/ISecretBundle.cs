namespace AzPasswordManager.Features.Secrets
{
    public interface ISecretBundle
    {
        public SecretAttributes? Attributes { get; set; }
        public string ContentType { get; set; }
        public string Id { get; set; }
        public string KId { get; set; }
        public bool Managed { get; set; }
        public Dictionary<string, string> Tags { get; set; }
        public string Value { get; set; }
        public string Name { get; }

    }
}
