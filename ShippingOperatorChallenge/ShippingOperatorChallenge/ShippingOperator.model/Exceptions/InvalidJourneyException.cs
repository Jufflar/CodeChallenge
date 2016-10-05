namespace ShippingOperator.model.Exceptions
{
    using System;

    public class InvalidJourneyException : Exception
    {
        public override string Message
        {
            get { return "Invalid Journey"; }
        }
    }
}