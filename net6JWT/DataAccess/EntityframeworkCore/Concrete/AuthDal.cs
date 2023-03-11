using net6JWT.Core.DataAccess.EntityframeworkCore;
using net6JWT.DataAccess.EntityframeworkCore.Abstract;
using net6JWT.Db;
using net6JWT.Model;

namespace net6JWT.DataAccess.EntityframeworkCore.Concrete
{
    public class AuthDal : EfEntitiyRepositoryBase<User, DatabaseContext>, IAuthDal
    {
         
    }
}
