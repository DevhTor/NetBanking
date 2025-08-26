namespace NetBanking.Api.Requests
{
    public class TransferRequest
    {
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}