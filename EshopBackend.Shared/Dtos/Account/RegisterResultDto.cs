namespace EshopBackend.Shared.Dtos.Account
{
    public class RegisterResultDto
    {
        public bool Success { get; set; }
        public bool DuplicateEmail { get; set; }
    }
}
