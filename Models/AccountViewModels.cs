using System;
using System.ComponentModel.DataAnnotations;

namespace StockApp.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Nom utilisateur")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [Display(Name = "Nom utilisateur")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe courant")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de passe")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le nouveau mot de passe")]
        [Compare("NewPassword", ErrorMessage = "la confirmation de votre mot de passe est incorect.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Identifiant")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Souviens de moi?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Role de l'utilisateur")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nom Utilisateur")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Le nom Complet")]
        public string NomComplet { get; set; }
        public string CreerPar { get; set; }
        public Nullable<System.DateTime> DateCreer { get; set; }
       
        public Nullable<int> Id_direction { get; set; }
       

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer mot de passe")]
        [Compare("Password", ErrorMessage = "la confirmation de votre mot de passe est incorect.")]
        public string ConfirmPassword { get; set; }
    }
}
