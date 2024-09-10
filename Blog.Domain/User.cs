using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public class User : Entity
    {
        [Required]
        [EmailAddress(ErrorMessage = "Neispravna email adresa")]
        public string Email { get; set; }
        public string Username { get; set; } 
        public string Photo { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\$\.!_\-?])[A-Za-z\d\$\.!_\-?]{8,}$",
        ErrorMessage = "Šifra mora početi velikim slovom, imati najmanje 8 karaktera i barem jedan specijalan karakter ($.!_-?)")]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z]+(-[A-Za-z]+)?$", ErrorMessage = "Ime može sadržati samo slova i znak '-'")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z]+( [A-Za-z]+)?$", ErrorMessage = "Prezime može sadržati samo slova i razmak")]
        public string LastName { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
        public virtual ICollection<PostLike> PostLikes { get; set; } = new HashSet<PostLike>();

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<CommentLike> CommentLikes { get; set; } = new HashSet<CommentLike>();
        public virtual ICollection<UserUseCase> UseCases { get; set; } = new HashSet<UserUseCase>();
    }

    public class UserUseCase
    {
        public int UserId { get; set; }
        public int UseCaseId { get; set; }
        public virtual User User { get; set; }
    }
}
