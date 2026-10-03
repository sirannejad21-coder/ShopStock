using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.ViewModels.Common
{
    public class BasePaging<T>
    {


        public BasePaging()
        {
            PageId = 1;
            TakeEntity = 8;
            HowPageAfterAndBefore = 1;
            Entities = new List<T>();
        }
        public int PageId { get; set; }

        public int CountPage { get; set; }

        public int CountAllEntity { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }

        public int TakeEntity { get; set; }
        public int SkipEntity { get; set; }

        public int HowPageAfterAndBefore { get; set; }

        public List<T> Entities { get; set; }


        public async Task<BasePaging<T>> Paging(IQueryable<T> query)
        {
             var AllEntity =  query.Count();

            var countPage = (int)Math.Ceiling(AllEntity /(Double) TakeEntity);

             PageId = CountPage < 1 ? PageId : CountPage < PageId ? CountPage : PageId;
            CountAllEntity = AllEntity;
            CountPage = countPage;

            SkipEntity = (PageId - 1) * TakeEntity;

            StartPage = PageId - HowPageAfterAndBefore <= 1 ? 1 : PageId - HowPageAfterAndBefore;
            EndPage = PageId + HowPageAfterAndBefore >= countPage ? countPage : PageId + HowPageAfterAndBefore;


            Entities=query.Skip(SkipEntity).Take(TakeEntity).ToList();
            return this;
          

        }

        public PageViewModel GetCurrent()
        {


            return new PageViewModel()
            {

                EndPage = this.EndPage,
                StartPage = this.StartPage,
                PageId = this.PageId,
            };


        }
    }


    public class PageViewModel
    {
        public int PageId { get; set; }
        public int StartPage { get; set; }

        public int EndPage { get; set; }

    }
}
