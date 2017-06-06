namespace ShippingOperator.Tests
{
    using model;
    using model.Networks;
    using NUnit.Framework;

    [TestFixture]
    public class AtlanticOceanJourneyTests
    {
        [Test]
        public void ConstructJourney_ValidNetworkAndStartingPort_NewJourneyConstructed()
        {
            INetworkConfiguration networkConfiguration = new AtlanticOceanNetwork();

            var journey = new Journey(networkConfiguration.Routes, Port.BuenosAires);

            Assert.IsInstanceOf(typeof(Journey), journey);
        }

        [Test]
        public void CalculateTotalDuration_StartingPortOnly_NewDestinationAdded()
        {
            INetworkConfiguration networkConfiguration = new AtlanticOceanNetwork();

            var journey = new Journey(networkConfiguration.Routes, Port.NewYork);

            journey.AddDestination(Port.Liverpool);

            Assert.AreEqual(4, journey.TotalDuration);
        }

        [Test]
        public void CalculateTotalDuration_StartingPortAndExistingDestination_NewDestinationAdded()
        {
            INetworkConfiguration networkConfiguration = new AtlanticOceanNetwork();

            var journey = new Journey(networkConfiguration.Routes, Port.NewYork);

            journey.AddDestination(Port.Liverpool);
            journey.AddDestination(Port.CasaBlanca);

            Assert.AreEqual(7, journey.TotalDuration);
        }
    }
}