namespace MoneyPlus.Services.Models
{
    public class Transfer : Wallet
    {

        public Wallet OriginWallet { get; set; }
        public int OriginWalletID { get; set; }
        public Wallet DestinationWallet { get; set; }
        public int DestinationWalletID { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

    }
}
