using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Extentions
{
    public static class FixText
    {

        public static string FixEmail(this string email)
        {

          return  email.Trim().ToLower();
        }

        public static string FixUsername(this string username)
        {

            return username.Trim().ToLower();
        }


    }
}
