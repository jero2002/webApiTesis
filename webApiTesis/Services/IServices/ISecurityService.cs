namespace webApiTesis.Services.IServices
{
    public interface ISecurityService
    {
        string? GetUserName();
        bool CheckUserHasroles(string[] roles);
        int GetUserId();
    }
}
