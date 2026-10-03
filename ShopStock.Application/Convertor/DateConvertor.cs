using ShopStock.Domain.Models.Catgories;
using ShopStock.Domain.ViewModels.Catgory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ShopStock.Application.Convertor
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime date)
        {

            var Pc = new PersianCalendar();

            return Pc.GetYear(date) + "/" + 
                Pc.GetMonth(date).ToString("00") + "/" +
                Pc.GetDayOfMonth(date).ToString("00");

        }

        public static string ToShamsiWithTime(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date) + "/" +
                pc.GetMonth(date).ToString("00") + "/" +
                pc.GetDayOfMonth(date).ToString("00") + " - " +
                pc.GetHour(date).ToString("00") + ":" +
                pc.GetMinute(date).ToString("00");
        }


     
        
        
        }


    }

