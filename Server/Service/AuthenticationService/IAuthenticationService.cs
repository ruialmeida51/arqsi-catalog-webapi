using Server.Model;

namespace Server.Service
{
    public interface IAuthenticationService
    {
        User Authenticate(User user, string password);
    }
}