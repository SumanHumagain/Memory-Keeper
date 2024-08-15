using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_App.Services
{
    public interface IUserSessionService
    {
        string? Email { get; set; }
    }
    public class SessionService : IUserSessionService
    {
        public string? Email { get; set; }
    }
}
