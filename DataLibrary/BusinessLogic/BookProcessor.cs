using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class BookProcessor
    {
        public static bool DeleteBook(int id)
        {
            BookModel data = new BookModel
            {
                Id = id,
                Durum = "silindi"
            };
            string sql = $@"UPDATE dbo.Book
                                SET Durum = @Durum
                                WHERE id=@Id;";

            if (SqlDataAccess.SaveData(sql, data) != 0) return true;
            else return false;
        }

        public static int CreateBook(string isim, string yazar, string tur, int sayfa)
        {
            BookModel data = new BookModel
            {
                Isim = isim,
                Yazar = yazar,
                Tur = tur,
                Sayfa = sayfa,
            };

            string sql = @"insert into dbo.Book(isim, Yazar, Tur, Sayfa)
                            values(@Isim, @Yazar, @Tur, @Sayfa);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<BookModel> SearchBooks(string searchPhrase)
        {
            //string sql = string.Format("select * from dbo.Book where isim like '%{1}%';",searchPhrase, searchPhrase);
            string sql = $@"select *
                            from dbo.Book
                            where isim like '%{searchPhrase}%' or id like '%{searchPhrase}%' or yazar like '%{searchPhrase}%';";

            return SqlDataAccess.LoadData<BookModel>(sql);
        }

        public static List<BookModel> GetMembersBooks(int id, string searchPhrase)
        {
            string sql = $@"select Book.Id, Book.Isim, Book.Yazar, Book.Tur, Book.Sayfa, Book.Durum
                            from dbo.Book
                            inner join dbo.BookMember on Book.Id = BookMember.BookId
                            where memberId = {id} and book.isim like '%{searchPhrase}%'
                            order by BookMember.Id desc;";

            return SqlDataAccess.LoadData<BookModel>(sql);
        }
    }
}
