using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace ShopStock.Application.Extensions
{
    public static class IdentityExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {

            if (principal != null)
            {

                var data = principal.Claims.SingleOrDefault(i => i.Type == ClaimTypes.NameIdentifier);
                if (data != null)
                {

                    return int.Parse(data.Value);

                }


            }

            return default(int);

        }

        public static int GetUserId(this IPrincipal principal) =>
            (principal is ClaimsPrincipal claim) ? GetUserId(claim) : default;


        // نمایش قیمت به تومان
        public static string ToToman(this decimal price)
        {
            return $"{price:N0} ";
        }

        public static string ToToman(this double price)
        {
            return $"{price:N0} ";
        }

        public static string ToToman(this int price)
        {
            return $"{price:N0} ";
        }

    }
}