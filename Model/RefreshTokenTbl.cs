using System.ComponentModel.DataAnnotations;

namespace ApiProject.Model
{
    public class RefreshTokenTbl
    {
        [Key]
        public int UserId { get; set; }

        public string TokenId { get; set; }

        public string RefreshTokens { get; set; }
    }
}
