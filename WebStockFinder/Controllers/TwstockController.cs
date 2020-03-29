using AlonsoAdoNET;
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

        TwstockDAL dal { get; set; }

        public TwstockController() {
            dal = new TwstockDAL();
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


            string cond1 = where + "  and  ClosePrice > 90 and  ClosePrice < 101 ";
            string cond2 = where + "  and  ClosePrice > 170 and  ClosePrice < 201 ";
            string cond3 = where + "  and  ClosePrice > 250 and  ClosePrice < 301 ";


            var result1 = dal.GetListArray(cond1);
            var result2 = dal.GetListArray(cond2);
            var result3 = dal.GetListArray(cond3);

            int iDays = Convert.ToInt32(chDays);
            DateTime tempRange = DateTime.Now.AddDays(iDays * -1);
            string stempRange = (tempRange.Year - 1911).ToString() + tempRange.ToString("MMdd");

            foreach (var item in result1)
            {
                if (item.StockCode == "3504")
                {
                    string test = "";
                }


                double? dcloseprice = item.ClosePrice;
                bool IsAdd = false;
                where = "";
                where = $" 1=1 and chDate >'{stempRange}' and  chDate <='{chDate}' and StockCode='{ item.StockCode}'";
                var tempResult = dal.GetListArray(where);
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
                    result.Add(item);
                }
            }

            foreach (var item in result2)
            {
                double? dcloseprice = item.ClosePrice;

           
                bool IsAdd = false;
                where = "";
                where = $" 1=1 and chDate >'{stempRange}' and  chDate <='{chDate}'  and StockCode='{ item.StockCode}'";
                var tempResult = dal.GetListArray(where);
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
                    result.Add(item);
                }
            }


            foreach (var item in result3)
            {
                double? dcloseprice = item.ClosePrice;
                bool IsAdd = false;
                where = "";
                where = $" 1=1 and chDate >'{stempRange}' and  chDate <='{chDate}' and  StockCode='{ item.StockCode}'";
                var tempResult = dal.GetListArray(where);
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
                    result.Add(item);
                }
            }





            return Json(result);
        }
    }
}
