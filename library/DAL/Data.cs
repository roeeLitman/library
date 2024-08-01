namespace library.DAL
{
    

    public class Data
    {
        // יצירת משתנה סטטי שיכיל בצורה מסויימת את החיבור
        static Data GetData;

        // 
        string ConnctionsString = "server=ROEE\\SQLEXPRESS; initial catalog=library; user id=sa; password=1234; TrustServerCertificate=Yes";

        private Data() 
        {
            //
            Layer = new DataLayer(ConnctionsString);
        }

        // המשתנה שיחזיק בפועל את החיבור
        public DataLayer Layer { get; set; }

        //משתנה שדרכו נלקח החיבור, בודק האם יש חיבור אם אין יוצר אותו במשתנה גאט-דאטה מסוג דאטה, ולאחר מכן מחזיר מתוך המשתנה החדש את  הלייהוט שלו?

        public static DataLayer Get
        {
            get
            {
                if (GetData == null)
                {
                    GetData = new Data();

                }
                return GetData.Layer;
            }
        }
    }
}
