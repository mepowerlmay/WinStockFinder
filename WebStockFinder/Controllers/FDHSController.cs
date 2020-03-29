using Alonso.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinStockFinder.DAL;

namespace WebStockFinder.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    public class FDHSController : Controller
    {
       

        TwstockDAL twstockDAL { get; set; }

        public FDHSController()
        {
            twstockDAL = new TwstockDAL();
        }

        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public JsonResult Index(string chDate, string chDays, string chStock)
        {

//            策略11.最低價格的日期往後抓 價格

//假設1 / 2價格為10 那麼1/ 2 之後的max價格一定要 > 10 ，相除最好有100 % 以上


//1 / 2之後的價格 = 20(除法)  1 / 2價格 = 10    =>>> 100 %


//      股票的最低價 = 6 ~29  的股票要有50 % 漲幅

//股票的最低價 = 30  ~60 的股票要有  50 % 漲幅

//股票的最低價 = 60~100 的股票 要有30% 的漲幅



            chDate = (DateTime.Now.Year - 1911).ToString() + DateTime.Now.ToString("MMdd");
            string bf360Days = (DateTime.Now.AddDays(-360).Year - 1911).ToString() + DateTime.Now.ToString("MMdd");

            IEnumerable<string>  stocks = twstockDAL. GetOneFileds("  distinct [StockCode]  ", " ");
            var result = new List<WinStockFinder.Model.Twstock>();
            foreach (var stock in stocks)
            {
                string cond = string.Format(" chDate >='{0}' and chDate <='{1}' and  StockCode='{2}' ", bf360Days, chDate, stock);
                var tempData = twstockDAL.GetListArray(cond);

                double? minZ = tempData.Where( i => i.ClosePrice !=0).Min(obj => obj.ClosePrice );
                if (minZ == null) continue;
                

                var minObj = tempData.Where(obj => obj.ClosePrice == minZ).FirstOrDefault();

//                var maxZ = tempData.Max(obj => obj.ClosePrice);
                double? maxZ = tempData.Where(obj =>   Convert.ToInt32( obj.chDate ) >= Convert.ToInt32( minObj.chDate)   ).Max( i => i.ClosePrice);

                double? priceresult = maxZ / minZ;

                priceresult -= 1;

                if (minZ <30 && priceresult > 1.00 )
                {
                    result.Add( new WinStockFinder.Model.Twstock {  StockCode= stock } );
                }

                if (minZ >29 && minZ <=60 && priceresult > 0.45)
                {
                    result.Add(new WinStockFinder.Model.Twstock { StockCode = stock });
                }

                if (minZ >60 && minZ <101 && priceresult > 0.3)
                {
                    result.Add(new WinStockFinder.Model.Twstock { StockCode = stock });
                }



            }


     
        


         

            return Json(result);
        }
    }
}