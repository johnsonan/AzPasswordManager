using System.Collections.ObjectModel;

namespace AzPasswordManager.Features.Secrets.Models
{
    namespace AzPasswordManager.Features.Secrets
    {
        public class DeletedSecretListResult
        {
            public string NextLink { get; set; }

            // Covariance works for interfaces, but not for concrete types
            // e.g. List<ISecretValue> Value = new List<DeletedSecretItem>(); is not valid
            public List<DeletedSecretItem> Value { get; set; }

        }
    }

}
