namespace ShippingOperator.Tests.Questions
{
    using model;
    using model.Networks;
    using NUnit.Framework;

    [TestFixture]
    public class TotalDurationTests
    {
        [Test, Description("Buenos Aires > New York > Liverpool")]
        public void AddDestination_ValidRoute1_TotalDurationCalculated()
        {
            // arrange
            INetworkConfiguration network = new AtlanticOceanNetwork();

            // act
            var journey = new Journey(network.Routes, Port.BuenosAires);

            journey.AddDestination(Port.NewYork);
            journey.AddDestination(Port.Liverpool);

            // assert
            Assert.AreEqual(10, journey.TotalDuration);
        }

        [Test, Description("Buenos Aires > Casablanca > Liverpool")]
        public void AddDestination_ValidRoute2_TotalDurationCalculated()
        {
            // arrange
            INetworkConfiguration network = new AtlanticOceanNetwork();

            // act
            var journey = new Journey(network.Routes, Port.BuenosAires);

            journey.AddDestination(Port.CasaBlanca);
            journey.AddDestination(Port.Liverpool);

            // assert
            Assert.AreEqual(8, journey.TotalDuration);
        }

        [Test, Description("Buenos Aires > Capetown > New York > Liverpool > Casablanca")]
        public void AddDestination_ValidRoute3_TotalDurationCalculated()
        {
            // arrange
            INetworkConfiguration network = new AtlanticOceanNetwork();

            // act
            var journey = new Journey(network.Routes, Port.BuenosAires);

            journey.AddDestination(Port.CapeTown);
            journey.AddDestination(Port.NewYork);
            journey.AddDestination(Port.Liverpool);
            journey.AddDestination(Port.CasaBlanca);

            // assert
            Assert.AreEqual(19, journey.TotalDuration);
        }

        [Test, Description("Buenos Aires > Capetown > Casablanca"), ExpectedException("ShippingOperator.model.Exceptions.InvalidJourneyException")]
        public void AddDestination_InValidRoute_TotalDurationCalculated()
        {
            // arrange
            INetworkConfiguration network = new AtlanticOceanNetwork();

            // act
            var journey = new Journey(network.Routes, Port.BuenosAires);

            journey.AddDestination(Port.CapeTown);
            journey.AddDestination(Port.CasaBlanca);

            // assert
            Assert.AreEqual(19, journey.TotalDuration);
        }
    }
}