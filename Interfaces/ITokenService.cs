using ToDo.Models.Entities;

namespace ToDo.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}