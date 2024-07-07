namespace AzPasswordManager.Features.Secrets
{
    public class SecretAttributes
    {
        public bool Enabled { get; set; }
        public int Created { get; set; }
        public int Updated { get; set; }
        public string? RecoveryLabel { get; set; }

    }
}
