namespace ShippingOperator.Tests.Questions
{
    using System.Linq;
    using model;
    using model.Networks;
    using NUnit.Framework;

    [TestFixture]
    public class ShortestJourneyTests
    {
        [Test, Description("Buenos Aires > Liverpool")]
        public void MapJourney_FromBuenosAiresToLiverpool_ShortestJourneyCalculated()
        {
            // arrange
            INetworkConfiguration network = new AtlanticOceanNetwork();

            // act 
            var journey = new Journey(network.Routes, Port.BuenosAires);

            journey.CalculateJourney(Port.Liverpool);

            // assert
            Assert.AreEqual(8, journey.TotalDuration);
            Assert.AreEqual(Port.Liverpool, journey.Destination.Last());
        }

        [Test, Description("New York > New York")]
        public void MapJourney_FromNewYorkToNewYork_ShortestJourneyCalculated()
        {
            // arrange
            INetworkConfiguration network = new AtlanticOceanNetwork();

            // act 
            var journey = new Journey(network.Routes, Port.NewYork);

            journey.CalculateJourney(Port.NewYork);

            // assert
            Assert.AreEqual(18, journey.TotalDuration);
            Assert.AreEqual(Port.NewYork, journey.Destination.Last());
        }
    }
}