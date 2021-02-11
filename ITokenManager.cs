using System;

namespace Blog_JwtTokenSample
{
    public interface ITokenManager
    {
        String GenerateJwtToken();
    }
}
