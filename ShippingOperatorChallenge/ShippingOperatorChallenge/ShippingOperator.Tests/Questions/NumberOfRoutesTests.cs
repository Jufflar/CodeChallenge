using System.Linq;

namespace ShippingOperator.Tests.Questions
{
    using model;
    using model.Networks;
    using NUnit.Framework;

    [TestFixture]
    class NumberOfRoutesTests
    {
        [Test]
        public void MapJourney_FromLiverpoolToLiverpool_RoutesWithAMaxiumNumberOfThreeStops()
        {
            // arrange
            INetworkConfiguration network = new AtlanticOceanNetwork();

            // act 
            var journey = new Journey(network.Routes, Port.Liverpool);

            journey.CalculateJourney(Port.Liverpool);

            // assert
            Assert.AreEqual(2, journey.JourneyRoutesAvailable);
            Assert.AreEqual(Port.Liverpool, journey.Destination.Last());
        }
    }
}
