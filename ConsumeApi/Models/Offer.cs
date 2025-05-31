namespace ConsumeApi.Models
{
    public class Offer
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CouponCode { get; set; }
        public Color BgColor { get; set; }


        public Offer()
        {
        }

        public Offer(string title, string description, string couponCode, Color bgColor)
        {
            Title = title;
            Description = description;
            CouponCode = couponCode;
            BgColor = bgColor;
        }

        public static readonly String[] _lightColors = new string[]
        {
            "#e1f1e7", "#dad1f9", "#d0f200", "#e28083", "#7fbd7", "#ea978d"
        };

        public static readonly Random _random = new();
        public static Color RandomColor => Color.FromHex(_lightColors[_random.Next(_lightColors.Length)]);

        public static List<Offer> GetOffers()
        {
            return new List<Offer>
            {
                new Offer("Free Shipping", "Get free shipping on orders over 100 cedis", "FREESHIP", RandomColor),
                new Offer("Buy 1 Get 1 Free", "Buy one product and get another one free", "BOGO", RandomColor),
                new Offer("20% Off on First Order", "Get 20% off on your first order with us", "FIRST20", RandomColor),
                new Offer("Flash Sale", "Limited time offer: 50% off on selected items", "FLASH50", RandomColor)
            };
        }
    }
}
