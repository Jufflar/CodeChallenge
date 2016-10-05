namespace ShippingOperator.Tests
{
    using System.Collections.Generic;
    using model;
    using model.Networks;

    public class TestNetwork : INetworkConfiguration
    {
        public TestNetwork()
        {
            Routes = new List<Route>
            {
                new Route(Port.NewYork, Port.Liverpool, 4),
                new Route(Port.Liverpool, Port.CasaBlanca, 3),
                new Route(Port.Liverpool, Port.CapeTown, 6),
                new Route(Port.CasaBlanca, Port.Liverpool, 3),
                new Route(Port.CasaBlanca, Port.CapeTown, 6),
                new Route(Port.CapeTown, Port.NewYork, 8),
                new Route(Port.BuenosAires, Port.CasaBlanca, 5),
                new Route(Port.BuenosAires, Port.CapeTown, 4),
                new Route(Port.BuenosAires, Port.NewYork, 6)
            };
        }

        public IEnumerable<Route> Routes { get; set; }
    }
}