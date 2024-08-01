using Microsoft.EntityFrameworkCore;
using library.Models;

namespace library.DAL
{
    //
    public class DataLayer : DbContext
    {
        //קונסרקטור שמקבל מחרוזת חיבור ומעביר אותה לקונסטרקטור האב
        public DataLayer(string connectionString) : base(GetOptions(connectionString))
        {
            // בודק האם המסד נתונים קיים, במקרה שלא צור אותו
            Database.EnsureCreated();

            // להכניס נתונים בפעם הראשונה
            Seed();

        }

        //פונקציה להכנסת ערך למסד הנתונים במקרה והוא רק
        private void Seed()
        {
           Ark ark;
           Shelf shelf;
           Book book;
            if (Arks.Any())
            {
                return;
            }
            ark = new Ark();
            ark.genre = "בדיקה ספריה";
            Arks.Add(ark);
            SaveChanges();

        }


        // יצירת שני ליסטים שגם יוצרים את עצמם במסד הנתונים
        public DbSet<Ark> Arks { get; set; }

        public DbSet<Shelf> Shelves { get; set; }

        public DbSet<Book> Books { get; set; }

        // מחזירה את אופציות החיבור למסד הנתונים
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), connectionString)
                .Options;
        }
    }
}
