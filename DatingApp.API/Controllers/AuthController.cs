using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;

namespace DatingApp.API.Controllers {
    [Route ("api/controller")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthRepository _authRepository;
        public AuthController (IAuthRepository _authRepository) {
            
            this._authRepository = _authRepository;

        }
    }
}