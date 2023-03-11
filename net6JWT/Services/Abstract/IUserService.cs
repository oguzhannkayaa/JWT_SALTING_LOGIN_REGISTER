using net6JWT.Dtos;

namespace net6JWT.Services.Abstract
{
    public interface IUserService
    {

        public string Login(UserDto user);
        public bool Register(UserDto user);


    }
}
