using Stripe.Checkout;

namespace MyCinema.Services.IServices
{
    public interface IStripeService
    {
        Session CreateCheckoutSession(string successUrl, string cancelUrl, List<SessionLineItemOptions> lineItems);
    }
}
