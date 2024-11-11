using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eProjectP3.Data;
using eProjectP3.Models;

namespace eProjectP3.Controllers
{
    public class CartController : Controller
    {
        private eProjectP3Context db = new eProjectP3Context();

        public ActionResult Index()
        {
            var cart = Session["cart"] as List<CartItem>;
            ViewBag.CartData = cart;
            return View();
        }
        public ActionResult CheckOutPage()
        {
            return View();
        }
        public class CartItem
        {
            public int productId { get; set; }
            public int quantity { get; set; }
            public string productName { get; set; }
            public decimal productPrice { get; set; }
            public string productType { get; set; }
        }

        [HttpPost]
        public JsonResult AddToCart(CartItem item)
        {

            var cart = Session["cart"] as List<CartItem> ?? new List<CartItem>();
            cart.Add(item);
            Session["cart"] = cart;
            return Json(new { message = "Product added to cart successfully!" });

        }

        [HttpPost]
        public JsonResult RemoveItem(int productId)
        {
            var cart = Session["cart"] as List<CartItem> ?? new List<CartItem>();

            var itemToRemove = cart.FirstOrDefault(x => x.productId == productId);

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                Session["cart"] = cart;

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public JsonResult EditQuantity(int productId, int newQuantity)
        {
            var cart = Session["cart"] as List<CartItem> ?? new List<CartItem>();

            var itemToEdit = cart.FirstOrDefault(x => x.productId == productId);

            if (itemToEdit != null)
            {
                itemToEdit.quantity = newQuantity;
                Session["cart"] = cart;

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public ActionResult Checkout()
        {
            List<CartItem> cart = Session["cart"] as List<CartItem> ?? new List<CartItem>();
            if (cart.Any())
            {
                var clientId = (int)Session["Client_ID"];

                var purchase = new online_purchase
                {
                    Client_ID = clientId,
                    Total_Amount = cart.Sum(item => item.productPrice * item.quantity),
                    Purchase_Date = DateTime.Now,
                    PurchaseDetails = new List<online_purchase_detail>()
                };

                foreach (var item in cart)
                {
                    var purchaseDetail = new online_purchase_detail
                    {
                        Item_Type = item.productType,
                        Item_ID = item.productId,
                        Quantity = item.quantity
                    };

                    purchase.PurchaseDetails.Add(purchaseDetail);
                }

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.online_purchases.Add(purchase);
                        db.SaveChanges();

                        transaction.Commit();

                        Session.Remove("cart");
                        ViewBag.AlertMessage = "Purchase completed successfully! Thank you for shopping with us.";
                        return RedirectToAction("Index", "Home");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw; // Rethrow the exception to handle it at a higher level or log it
                    }
                }
            }
            else
            {
                TempData["Message"] = "Your cart is empty. Add items to your cart before checking out.";
                return RedirectToAction("Index", "medical_application");
            }
        }
    }
}
