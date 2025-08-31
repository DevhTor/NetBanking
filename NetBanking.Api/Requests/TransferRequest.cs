namespace NetBanking.Api.Requests
{
    public class TransferRequest
    {
        public int SourceAccountId { get; set; }
        public string DestinationAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}