using Braintree;
using Microsoft.Extensions.Options;

public class BrainTreeGate : IBrainTreeGate
{
    public BrainTreeSettings Options { get; set; }
    private IBraintreeGateway BraintreeGateway { get; set; }
    public BrainTreeGate(IOptions<BrainTreeSettings> options)
    {
        Options = options.Value;
    }
    public IBraintreeGateway CreateGateway()
    {
        return new BraintreeGateway(Options.Environment, Options.MerchantID, Options.Publickey, Options.Privatekey);
    }

    public IBraintreeGateway GetGateway()
    {
        if(BraintreeGateway == null)
        {
            BraintreeGateway = CreateGateway();
        }
        return BraintreeGateway;
    }
}