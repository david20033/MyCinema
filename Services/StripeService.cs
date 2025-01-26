using MyCinema.Services.IServices;
using Stripe;
using Stripe.Checkout;

namespace MyCinema.Services
{
    public class StripeService : IStripeService
    {
        public Session CreateCheckoutSession(string successUrl, string cancelUrl, List<SessionLineItemOptions> lineItems)
        {
            var options = new SessionCreateOptions
            {

                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
                LineItems = lineItems,
                Mode = "payment",
                ExpiresAt = DateTime.UtcNow.AddMinutes(30)
            };

            var service = new SessionService();
            return service.Create(options);
        }
    }
}
