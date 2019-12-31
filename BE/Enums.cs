namespace BE
{
    public static class Enums
    {
        public enum HostingUnitType
        {
            Zimmer,
            Hotel,
            Camping,
        }

        public enum OrderStatus
        {
            Not_yet_addressed,
            Mail_sent,
            Closed_due_to_customer_unresponsiveness,
            Closed_with_customer_responsiveness,
        }

        public enum RequestStatus
        {
            Active,
            Inactive,
        }

        public enum Area
        {
            North,
            South,
            Center,
            Jerusalem,
        }
    }



}