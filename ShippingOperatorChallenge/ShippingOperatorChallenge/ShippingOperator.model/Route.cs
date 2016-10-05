namespace ShippingOperator.model
{
    public class Route
    {
        public Route(Port from, Port to, decimal duration)
        {
            From = from;
            To = to;
            Duration = duration;
        }

        public Port From { get; set; }
        public Port To { get; set; }
        public decimal Duration { get; set; }
    }
}