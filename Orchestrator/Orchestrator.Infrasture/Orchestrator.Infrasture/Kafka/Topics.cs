namespace Orchestrator.Infrasructure.Kafka;

public class Topics
{
    public string? GetReceipt {get; set;}
    public string? ReceiptResponse { get; set; }
    public string? GetCook { get; set; }
    public string? CookResponse { get; set; }
    public string? HotKitchen { get; set; }
    public string? HotKitchenResponse { get; set; }
    public string? ColdKitchen { get; set; }
    public string? ColdKitchenResponse { get; set; }
    public string? DoughKitchen { get; set; }
    public string? DoughKitchenResponse { get; set; }
    public string? GetListCooks { get; set; }
    public string? GetListCooksResponse { get; set; }
    public string? GetListReceipt { get; set; }
    public string? GetListReceiptResponse { get; set; }
    public string? DeleteReceipt { get; set; }
    public string? DeleteReceiptResponse { get; set; }
    public string? DeleteCook { get; set; }
    public string? DeleteCookResponse { get; set; }
    public string? CreateReceipt { get; set; }
    public string? CreateReceiptResponse { get; set; }
    public string? CreateCook { get; set; }
    public string? CreateCookResponse { get; set; }
    public string? GetCookByName { get; set; }
    public string? GetCookByNameResponse { get; set; }
    public string? GetReceiptById { get; set; }
    public string? GetReceiptByIdResponse { get; set; }
    
}