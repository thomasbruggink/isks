using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.Users
{
    public interface IUserRepository
    {
        void CreateUser(string username);
    }
}
