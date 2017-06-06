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

        public List<Port> Destination { get; private set; }
        public decimal JourneyRoutesAvailable { get; set; }

        public IEnumerable<Route> Routes { get; set; }
        public Port StartingPort { get; private set; }
        public decimal TotalDuration { get; private set; }

        public void AddDestination(Port newDestination)
        {
            TotalDuration += CalculateTotalDuration(newDestination);

            Destination.Add(newDestination);
        }

        public void CalculateJourney(Port destination)
        {
            var journeys = new List<Journey>();

            SetInitialRoutes(journeys);

            var available = CalculateSubJourneys(destination, journeys.ToArray(), journeys);

            JourneyRoutesAvailable = available.Count(w => w.Destination.Any(a => a == destination));
            Destination = available.Where(w => w.Destination.Any(a => a == destination)).OrderBy(t => t.TotalDuration).Select(s => s.Destination).First();
            TotalDuration = available.Where(w => w.Destination.Any(a => a == destination)).OrderBy(t => t.TotalDuration).Select(s => s.TotalDuration).First();
        }

        private static Journey[] RouteMultipleJourneys(Port destination, Journey[] available, int i, Route route, List<Journey> journeys)
        {
            if (available[i].Destination.All(a => a != route.To))
            {
                var jou = new Journey(available[i].Routes, available[i].StartingPort);

                foreach (var port in available[i].Destination)
                    if (port != destination)
                        jou.AddDestination(port);

                jou.AddDestination(route.To);

                journeys.Add(jou);
                available = journeys.ToArray();
            }
            return available;
        }

        private Journey[] CalculateSubJourneys(Port destination, Journey[] available, List<Journey> journeys)
        {
            for (var i = 0; i < available.Length; i++)
                if (!available[i].Destination.Any(a => a.Equals(destination)))
                {
                    var routes = GetValidRoutesFromPort(available[i].Destination.Last());

                    if (routes.Count == 1)
                        available[i].AddDestination(routes[0].To);
                    else
                        foreach (var route in routes)
                            if (route.To == destination)
                                available[i].AddDestination(route.To);
                            else
                                available = RouteMultipleJourneys(destination, available, i, route, journeys);
                }
            return available;
        }

        private decimal CalculateTotalDuration(Port newDestination)
        {
            var previousPort = GetPreviousPort();

            try
            {
                return Routes.Where(f => f.From == previousPort && f.To == newDestination).Select(s => s.Duration).First();
            }
            catch (Exception)
            {
                throw new InvalidJourneyException();
            }
        }

        private Port GetPreviousPort()
        {
            return Destination.Count == 0 ? StartingPort : Destination.Last();
        }

        private List<Route> GetValidRoutesFromPort(Port fromPort)
        {
            var validRoutes = Routes.Where(w => w.From == fromPort).ToList();
            return validRoutes;
        }

        private void SetInitialRoutes(List<Journey> validJourneys)
        {
            var validRoutes = GetValidRoutesFromPort(StartingPort);

            foreach (var r in validRoutes)
            {
                var j = new Journey(Routes, StartingPort);

                j.AddDestination(r.To);

                validJourneys.Add(j);
            }
        }
    }
}