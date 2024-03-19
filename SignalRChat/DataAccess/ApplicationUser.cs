namespace SignalRChat.DataAccess;

public class ApplicationUser
{
    public int Id{ get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
}