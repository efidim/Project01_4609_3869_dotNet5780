namespace BE
{
    public static class Enums
    {
        public enum HostingUnitType
        {
            צימר,        
            מלון,
            קמפינג,
        }

        public enum OrderStatus
        {
           טרם_טופך,
           נשלח_מייל,
           נסגרה_מחוסר_הענות_של_הלקוח,
           נסגרה_כי_פג_תוקף,
        }

        public enum RequestStatus
        {
            פתוחה,
            נסגרה_עסקה_דרך_האתר,
            נסגרה_כי_פג_תוקף,
        }

        public enum Area
        {
            צפון,
            דרום,
            מרכז,
            ירושלים,
        }
    }



}