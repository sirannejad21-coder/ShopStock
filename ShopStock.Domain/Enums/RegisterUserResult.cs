using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Enums
{
    public enum RegisterUserResult
    {
        Succes,
        InvalidInout,
        EmailDuplecate,
        UserDuplecate,
        SendActiveEmail,
        Faild

    }
}
