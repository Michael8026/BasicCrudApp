namespace BasicCrudApp.Domain
{
    public class CreateUserDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
    }
}
