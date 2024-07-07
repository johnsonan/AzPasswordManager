namespace AzPasswordManager.Features.Secrets
{

    public class DeletedSecretItem : SecretItem, ISecretItem
    {
        public int DeletedDate { get; set; }
        public string RecoveryId { get; set; }
        public int ScheduledPurgeDate { get; set; }
    }
}
