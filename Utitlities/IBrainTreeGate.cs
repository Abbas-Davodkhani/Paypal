using Braintree;

public interface IBrainTreeGate
{
    IBraintreeGateway CreateGateway();
    IBraintreeGateway GetGateway();
}