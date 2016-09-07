using Contact_Form.EconomicRestClient;
using Contact_Form.EuropeBank;
using Contact_Form.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace Contact_Form.Controllers
{
    public class WebshopSaleController : SurfaceController
    {
        public static string ORDER_SESSION = "orderSession";
        public int umbracoFailRedictPage;
        private string SuccessAddProduct = "SuccessAddProduct";
        
        public void StartSale(int id, int umbracoFailRedictPage)
        {
            this.umbracoFailRedictPage = umbracoFailRedictPage;
            Session[ORDER_SESSION] = new OrderViewModel();
        }

        public ActionResult AddDomainProduct(DomainViewModel model)
        {
            TempData[SuccessAddProduct] = false;

            if (Session[ORDER_SESSION] != null)
            {
                var product = CustomerClient.GetProduct(model.ID);

                var amountProduct = new AmountProduct();
                amountProduct.Amount = 1;

                if (product.ProductType == EconomicRestClient.Models.ProductType.Domain)
                {
                    amountProduct.Product = model;
                }

                ((OrderViewModel)Session[ORDER_SESSION]).AmountProduct = amountProduct;
                TempData[SuccessAddProduct] = true;
            }
            else
            {
                return RedirectToUmbracoPage(umbracoFailRedictPage);
            }

            return CurrentUmbracoPage();
        }

        public ActionResult AddWebhotelProduct(WebhotelViewModel model)
        {
            TempData[SuccessAddProduct] = false;

            if (Session[ORDER_SESSION] != null)
            {
                var product = CustomerClient.GetProduct(model.ID);

                var amountProduct = new AmountProduct();
                amountProduct.Amount = 1;

                if (product.ProductType == EconomicRestClient.Models.ProductType.Webhotel)
                {
                    amountProduct.Product = model;
                }

                ((OrderViewModel)Session[ORDER_SESSION]).AmountProduct = amountProduct;
                TempData[SuccessAddProduct] = true;
            }
            else
            {
                return RedirectToUmbracoPage(umbracoFailRedictPage);
            }

            return CurrentUmbracoPage();
        }

        public ActionResult AddGameServerProduct(GameServerViewModel model)
        {
            TempData[SuccessAddProduct] = false;

            if (Session[ORDER_SESSION] != null)
            {
                var product = CustomerClient.GetProduct(model.ID);

                var amountProduct = new AmountProduct();
                amountProduct.Amount = 1;

                if (product.ProductType == EconomicRestClient.Models.ProductType.GameServer)
                {
                    amountProduct.Product = model;
                }

                ((OrderViewModel)Session[ORDER_SESSION]).AmountProduct = amountProduct;
                TempData[SuccessAddProduct] = true;
            }
            else
            {
                return RedirectToUmbracoPage(umbracoFailRedictPage);
            }

            return CurrentUmbracoPage();
        }
    }
}