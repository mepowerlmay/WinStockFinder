using Alonso.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinStockFinder.DAL;
namespace WebStockFinder.Controllers
{
    public class TwstockController : Controller
    {
        TwstockDAL TwstockDAL { get; set; }
        FutureStockDAL futureStockDAL { get; set; }

        public TwstockController()
        {
            TwstockDAL = new TwstockDAL();
            futureStockDAL = new FutureStockDAL();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Index(string chDate, string chDays, string chStock)
        {
            if (string.IsNullOrEmpty(chDate))
            {
                chDate = (DateTime.Now.Year - 1911).ToString() + DateTime.Now.ToString("MMdd");
            }
            if (string.IsNullOrEmpty(chDays))
            {
                chDays = "60";
            }
            var result = new List<WinStockFinder.Model.Twstock>();
            string where = " 1=1 ";
            WhereClausesBuilder whereClausesBuilder = new WhereClausesBuilder();
            whereClausesBuilder.appendCriteriaText(chDate, " chDate = '{0}'", ref where);
            whereClausesBuilder.appendCriteriaText(chStock, " chStock = '{0}'", ref where);
            whereClausesBuilder.appendCriteriaText(chStock, " chStock = '{0}'", ref where);
            string cond1 = where + "  and  ClosePrice > 50 and  ClosePrice < 101 ";
            string cond2 = where + "  and  ClosePrice > 150 and  ClosePrice < 201 ";
            string cond3 = where + "  and  ClosePrice > 250 and  ClosePrice < 301 ";
            var result1 = TwstockDAL.GetListArray(cond1);
            var result2 = TwstockDAL.GetListArray(cond2);
            var result3 = TwstockDAL.GetListArray(cond3);

            int iDays = Convert.ToInt32(chDays);
            DateTime tempRange = DateTime.Parse(TypeChange.ChineseToWestSDate(chDate)).AddDays(iDays * -1);
            string stempRange = (tempRange.Year - 1911).ToString() + tempRange.ToString("MMdd");
            foreach (var item in result1)
            {
                double? dcloseprice = item.ClosePrice;
                bool IsAdd = false;
                where = "";
                where = $" 1=1 and chDate >'{stempRange}' and  chDate <='{chDate}' and StockCode='{ item.StockCode}'";
                var tempResult = TwstockDAL.GetListArray(where);
                foreach (var item2 in tempResult)
                {
                    IsAdd = true;
                    double? dcloseprice2 = item2.ClosePrice;
                    if (dcloseprice2 > 100)
                    {
                        IsAdd = false;
                        break;
                    }
                    decimal dhPrice = decimal.Parse(item2.HightPrice);
                    if (dhPrice > 100)
                    {
                        IsAdd = false;
                        break;
                    }
                }
                if (IsAdd)
                {
                    //確認是否為期貨指數

                    int tempcount = futureStockDAL.CalcCount(string.Format(" Code ='{0}'", item.StockCode));
                    if (tempcount > 0)
                    {
                        result.Add(item);
                    }
                 
                }
            }
            foreach (var item in result2)
            {
                double? dcloseprice = item.ClosePrice;

                bool IsAdd = false;
                where = "";
                where = $" 1=1 and chDate >'{stempRange}' and  chDate <='{chDate}'  and StockCode='{ item.StockCode}'";
                var tempResult = TwstockDAL.GetListArray(where);
                foreach (var item2 in tempResult)
                {
                    IsAdd = true;
                    double? dcloseprice2 = item2.ClosePrice;
                    if (dcloseprice2 > 200)
                    {
                        IsAdd = false;
                        break;
                    }
                    decimal dhPrice = decimal.Parse(item2.HightPrice);
                    if (dhPrice > 200)
                    {
                        IsAdd = false;
                        break;
                    }
                }
                if (IsAdd)
                {
                    //確認是否為期貨指數
                    int tempcount = futureStockDAL.CalcCount(string.Format(" Code ='{0}'", item.StockCode));
                    if (tempcount > 0)
                    {
                        result.Add(item);
                    }
                }
            }
            foreach (var item in result3)
            {
                double? dcloseprice = item.ClosePrice;
                bool IsAdd = false;
                where = "";
                where = $" 1=1 and chDate >'{stempRange}' and  chDate <='{chDate}' and  StockCode='{ item.StockCode}'";
                var tempResult = TwstockDAL.GetListArray(where);
                foreach (var item2 in tempResult)
                {
                    IsAdd = true;
                    double? dcloseprice2 = item2.ClosePrice;
                    if (dcloseprice2 > 300)
                    {
                        IsAdd = false;
                        break;
                    }
                    decimal dhPrice = decimal.Parse(item2.HightPrice);
                    if (dhPrice > 300)
                    {
                        IsAdd = false;
                        break;
                    }
                }
                if (IsAdd)
                {
                    //確認是否為期貨指數
                    int tempcount = futureStockDAL.CalcCount(string.Format(" Code ='{0}'", item.StockCode));
                    if (tempcount > 0)
                    {
                        result.Add(item);
                    }
                }
            }
            return Json(result);
        }
    }
}
