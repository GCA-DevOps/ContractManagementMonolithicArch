using System.ComponentModel.DataAnnotations;

public class TokenRequestVM
{

    [Required]
    public string Token { get; set; }

    [Required]
    public string RefreshToken { get; set; }


    }
}
