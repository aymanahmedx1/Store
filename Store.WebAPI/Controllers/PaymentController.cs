using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Store.Service.BasketService.Dtos;
using Store.Service.PaymentServices;

using Stripe;

namespace Store.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController(IPaymentService _paymentService, ILogger<PaymentController> _logger) : ControllerBase
    {
        const string endPointSecret = "whsec_953b3a01ce164c4bd88b783e3be18c9737a489c9db8f3f3dd3a907c993e38125";
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(CustomerBasketDto basket)
        {

            return Ok(await _paymentService.CreateOrUpdatePaymentIntent(basket));
        }
        [HttpPost]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], endPointSecret);
                PaymentIntent paymentIntent;
                if (stripEvent.Type == "payment_intent.payment_failed")
                {
                    paymentIntent = stripEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Faild ", paymentIntent);
                    await _paymentService.UpdateOrderPaymentFailed(paymentIntent.Id);
                }
                else if (stripEvent.Type == "payment_intent.succeeded")
                {
                    paymentIntent = stripEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Succeeded ", paymentIntent);
                    await _paymentService.UpdateOrderPaymentSucceded(paymentIntent.Id);
                }
                else if (stripEvent.Type == "payment_intent.created")
                {
                    _logger.LogInformation("Payment Created ");
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }
    }
}
