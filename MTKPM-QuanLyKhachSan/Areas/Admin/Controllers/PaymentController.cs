using Microsoft.AspNetCore.Mvc;
using PayPal.Api;

namespace MTKPM_QuanLyKhachSan.Areas.Admin.Controllers
{
    public class PaymentController : Controller
    {
        private readonly APIContext apiContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentController(APIContext apiContext, IHttpContextAccessor httpContextAccessor)
        {
            this.apiContext = apiContext;
            this._httpContextAccessor = httpContextAccessor;
        }

        public ActionResult PaymentWithPaypal(string cancel = null)
        {
            try
            {
                var payerId = _httpContextAccessor.HttpContext.Request.Query["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    var baseURI = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Payment/PaymentWithPaypal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);

                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    _httpContextAccessor.HttpContext.Session.SetString(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = _httpContextAccessor.HttpContext.Request.Query["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, _httpContextAccessor.HttpContext.Session.GetString(guid));

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception)
            {
                return View("FailureView");
            }

            return View("SuccessView");
        }

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var itemList = new ItemList() { items = new List<Item>() };
            itemList.items.Add(new Item()
            {
                name = "Item Name comes here",
                currency = "USD",
                price = "1",
                quantity = "1",
                sku = "sku"
            });

            var payer = new Payer() { payment_method = "paypal" };
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "1"
            };

            var amount = new Amount()
            {
                currency = "USD",
                total = "3", // Total must be equal to sum of tax, shipping and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();
            var paypalOrderId = DateTime.Now.Ticks;

            transactionList.Add(new Transaction()
            {
                description = $"Invoice #{paypalOrderId}",
                invoice_number = paypalOrderId.ToString(),
                amount = amount,
                item_list = itemList
            });

            var payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return payment.Create(apiContext);
        }
    }
}
