using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Services
{
    public interface ISessionService
    {
        public string GetCurrentUserId();
    }
}
