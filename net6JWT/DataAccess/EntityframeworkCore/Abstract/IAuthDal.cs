using net6JWT.Core.DataAccess.EntityframeworkCore;
using net6JWT.Model;

namespace net6JWT.DataAccess.EntityframeworkCore.Abstract
{
    public interface IAuthDal : IEntityRepository<User>
    {
    }
}
