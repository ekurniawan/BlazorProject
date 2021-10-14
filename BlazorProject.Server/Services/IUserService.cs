using BlazorProject.Server.Models;
using BlazorProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server.Services
{
    public interface IUserService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
