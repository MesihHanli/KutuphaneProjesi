using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public static class BookTypeProcessor
    {
        public static List<BookTypeModel> getBookTypes()
        {
            string sql = "select * from dbo.BookType;";

            return SqlDataAccess.LoadData<BookTypeModel>(sql);
        }

        public static bool addBookType(string tur)
        {
            BookTypeModel data = new BookTypeModel
            {
                Tur = tur
            };
            string sql = @"insert into dbo.BookType(tur)
                            values(@Tur)";

            int affectedRows = SqlDataAccess.SaveData(sql, data);

            if (affectedRows > 0) return true;
            else return false;
        }

        public static bool deleteBookType(int id)
        {
            BookTypeModel data = new BookTypeModel
            {
                Id = id
            };

            string sql = @"Delete from dbo.BookType
                            Where Id = @Id";
            int affectedRows = SqlDataAccess.SaveData(sql, data);

            if (affectedRows > 0) return true;
            else return false;
        }

    }
}
