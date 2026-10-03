using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Enums
{
    public enum AdminUserResult
    {
        Succes,
        Error,
        UsernameDuplicate,
       EmailDuplicate,
        MobileDuplicate,
        InvalidImage

    }
}
