using System;
using Braintree;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class BrainTreeController : Controller
{
    public IBrainTreeGate _brain { get; set; }
    public BrainTreeController(IBrainTreeGate brain)
    {
        _brain = brain;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var gateWay = _brain.CreateGateway();
        var clientToken = gateWay.ClientToken.Generate();
        ViewBag.ClientToken = clientToken;
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(IFormCollection collection)
    {
        Random rnd = new Random();
        string paymentMethodNonce = collection["payment_method_nonce"];
        var request = new TransactionRequest
        {
            Amount = rnd.Next(1000,9999) , 
            PaymentMethodNonce = paymentMethodNonce , 
            OrderId = "55501" , 
            Options = new TransactionOptionsRequest
            {
                SubmitForSettlement = true
            }
        };
        var gateway = _brain.GetGateway();
        Result<Transaction> result = gateway.Transaction.Sale(request);
        if(result.Target.ProcessorResponseText == "Approved")
        {
            TempData["Success"] = "Transaction was successful Transaction ID " + result.Target.Id + ", Amount Charged : $" + result.Target.Amount;
        }

        return View();
    }
    
}