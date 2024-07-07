namespace AzPasswordManager.Features.Secrets
{
    public interface ISecretListResult
    {
        public string NextLink { get; set; }
        public IEnumerable<ISecretItem> Value { get; set; }
    }
}
