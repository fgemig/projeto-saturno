using System.ComponentModel.DataAnnotations;

namespace Saturno.Domain.Enums
{
    public enum UserRole
    {
        [Display(Name = "User")]
        User = 1,
        [Display(Name = "Administrador")]
        Admin
    }
}
