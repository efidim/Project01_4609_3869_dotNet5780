namespace BE
{
    public static class Enums
    {
        public enum hostingUnitType 
        {
            צימר,        
            מלון,
            קמפינג,
        }

        public enum orderStatus
        {
           טרם_טופל,
           נשלח_מייל,
           נסגרה_מחוסר_הענות_של_הלקוח,
           נסגרה_בהצלחה
        }

        public enum requestStatus
        {
            פתוחה,
            נסגרה_עסקה_דרך_האתר,
            נסגרה_כי_פג_תוקף,
        }

        public enum area
        {
            צפון,
            דרום,
            מרכז,
            ירושלים,
        }
        public enum response
        {
            אפשרי,    
           לא_מעוניין,
            הכרחי
        }
   
    }



}
