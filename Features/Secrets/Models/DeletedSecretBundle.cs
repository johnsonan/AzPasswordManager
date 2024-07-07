namespace AzPasswordManager.Features.Secrets
{
    public class DeletedSecretBundle : SecretBundle, ISecretBundle
    {
        public int DeletedDate { get; set; }
        public string RecoveryId { get; set; }
        public int ScheduledPurgeDate { get; set; }
    }
}
