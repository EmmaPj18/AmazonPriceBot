using System.ComponentModel;

namespace AmazonPriceBot.Models
{
    public enum Actions
    {
        [Description("Get information and command list of the bot")]
        help = 0,
        [Description("Search a product by amazon's url or by Name (Lowest price)")]
        search = 1      
    }
}