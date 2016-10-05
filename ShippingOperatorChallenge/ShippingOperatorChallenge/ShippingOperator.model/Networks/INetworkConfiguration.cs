namespace ShippingOperator.model.Networks
{
    using System.Collections.Generic;

    public interface INetworkConfiguration
    {
        IEnumerable<Route> Routes { get; set; } 
    }
}