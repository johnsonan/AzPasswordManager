using System.ComponentModel.DataAnnotations;

namespace AzPasswordManager.Features.Secrets
{
    public class SecretEditModel
    {

        [Required]
        [RegularExpression("^[0-9a-zA-Z-]+$", ErrorMessage = "Secret name must contain only numbers and letters.")]
        public string SecretName { get; set; } = string.Empty;

        [Required]
        public string SecretUsername { get; set; } = string.Empty;

        [Required]
        public string SecretValue { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(SecretValue))]
        public string SecretValue2 { get; set; } = string.Empty;

        [Required]
        [RegularExpression("[Global|Personal]")]
        public string VaultLocation { get; set; } = string.Empty;

        [EmailAddress]
        public string PersonalVaultOwner { get; set; } = string.Empty;

        [Url]
        public string SecretUrl { get; set; } = string.Empty;

    }
}
