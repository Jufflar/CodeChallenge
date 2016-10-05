namespace ShippingOperator.model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Networks;

    public class Journey : INetworkConfiguration
    {
        public Journey(IEnumerable<Route> routes, Port startingPort)
        {
            Routes = routes;
            StartingPort = startingPort;
            Destination = new List<Port>();
        }

        public IEnumerable<Route> Routes { get; set; }
        public Port StartingPort { get; private set; }
        public List<Port> Destination { get; private set; }
        public decimal TotalDuration { get; private set; }

        public void AddDestination(Port newDestination)
        {
            TotalDuration += CalculateTotalDuration(newDestination);

            Destination.Add(newDestination);
        }

        private decimal CalculateTotalDuration(Port newDestination)
        {
            Port previousPort = GetPreviousPort();

            try
            {
                return Routes.Where(f => f.From == previousPort && f.To == newDestination).Select(s => s.Duration).First();
            }
            catch (Exception)
            {
                throw new InvalidJourneyException();
            }
        }

        public void CalculateJourney(Port destination)
        {
            var journeys = new List<Journey>();

            SetInitialRoutes(journeys);

            foreach (Journey journey in journeys)
            {
                if (!journey.Destination.Any(a => a.Equals(destination)))
                {
                    List<Route> routes = GetValidRoutesFromPort(journey.Destination.Last());

                    foreach (Route route in routes)
                    {
                        if (route == routes.Last())
                        {
                            journey.AddDestination(route.To);
                        }
                        else
                        {
                            var jou = new Journey(journey.Routes, journey.StartingPort)
                            {
                                Destination = journey.Destination
                            };
                            jou.AddDestination(route.To);
                            journeys.Add(jou);
                        }
                    }
                }
            }

            Destination = journeys.OrderBy(t => t.TotalDuration).Select(s => s.Destination).First();
            TotalDuration = journeys.OrderBy(t => t.TotalDuration).Select(s => s.TotalDuration).First();
        }

        private void SetInitialRoutes(List<Journey> validJourneys)
        {
            List<Route> validRoutes = GetValidRoutesFromPort(StartingPort);

            foreach (Route r in validRoutes)
            {
                var j = new Journey(Routes, StartingPort);

                j.AddDestination(r.To);

                validJourneys.Add(j);
            }
        }

        private List<Route> GetValidRoutesFromPort(Port fromPort)
        {
            List<Route> validRoutes = Routes.Where(w => w.From == fromPort).ToList();
            return validRoutes;
        }

        private Port GetPreviousPort()
        {
            return Destination.Count == 0 ? StartingPort : Destination.Last();
        }
    }
}