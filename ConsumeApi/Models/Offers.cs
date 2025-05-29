namespace ConsumeApi.Models
{
    public class Offers
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CouponCode { get; set; }
        public Color BgColor { get; set; }


        public Offers(string title, string description, string couponCode, Color bgColor)
        {
            Title = title;
            Description = description;
            CouponCode = couponCode;
            BgColor = bgColor;
        }

        public static readonly String[] _lightColors = new string[]
        {
            "#e1f1e7", "dad1f9", "#d0f200", "#e28083", "#7fbd7", "#ea978d"
        };

        public static Color RandomColor => Color.FromHex(_lightColors.OrderBy(c => Guid.NewGuid()).First());

        public static IEnumerable<Offers> GetOffers()
        {
            yield return new Offers("Up to 30% off", "Enjoy upto 30% off on all electronic Gadgets form Grandma Store", "FRT30", RandomColor);
            yield return new Offers("Big May Sales", "Buy 1 get 50% on 4 more", "YyuO90", RandomColor);
            yield return new Offers("Print Bulk at low Price", "Print 40 pages for only 20 cedis this Month", "gryyahh09", RandomColor);
        }
    }
}
