using DataAccess.Models;

namespace Client.Models
{
    public class Cart
    {
        public short Quantity { get; set; }
        public Product Product { get; set; }
    }
}
