namespace TicketReSail.Core.Infrastructure
{
    public sealed class OperationDetails
    {
        public OperationDetails(bool succeeded, string message, string prop)
        {
            Succeeded = succeeded;
            Message = message;
            Property = prop;
        }
        public bool Succeeded { get; }
        public string Message { get; }
        public string Property { get; }
    }
}
