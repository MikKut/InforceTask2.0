namespace PhotoGallery.Api.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}
