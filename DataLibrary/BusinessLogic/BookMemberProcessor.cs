using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using DataLibrary.DataAccess;

namespace DataLibrary.BusinessLogic
{
    public static class BookMemberProcessor
    {
        public static List<MemberModel> GetMembersOnBook(int bookId)
        {
            string sql = $@"select BookMember.MemberId as Id , Member.AdSoyad
                            from dbo.BookMember
                            inner join dbo.Member on BookMember.memberId = Member.Id
                            where BookMember.bookId = {bookId}
                            order by BookMember.TeslimTarihi desc;";

            return SqlDataAccess.LoadData<MemberModel>(sql);
        }
        public static bool ExtendDeadline(int bookid)
        {
            BookMemberModel data = new BookMemberModel
            {
                BookId = bookid
            };

            string sql = $@"UPDATE dbo.BookMember
                         SET teslimtarihi = CONVERT(DATE, DATEADD(day, 7 ,teslimtarihi))
                         WHERE TeslimDurumu = 0 and BookId = @BookId;";

            if (SqlDataAccess.SaveData(sql, data) != 0) return true;
            else return false;

        }
        public static List<BookMemberModel> GetCloseDeadline()
        {
            string sql = @"select BookMember.MemberId, BookMember.BookId, Book.isim as BookName, Convert(VARCHAR, BookMember.TeslimTarihi, 103) as TeslimTarihi
                            from dbo.BookMember
                            inner join Book on BookMember.bookId = Book.Id
                            where BookMember.TeslimDurumu = 0 and BookMember.TeslimTarihi > CONVERT(DATE, GETDATE()) and BookMember.TeslimTarihi < CONVERT(DATE, DATEADD(day, 4 ,GETDATE()));";

            return SqlDataAccess.LoadData<BookMemberModel>(sql);
        }
        public static List<BookMemberModel> GetLateBooks()
        {
            string sql = @"select BookMember.MemberId, BookMember.BookId, Book.isim as BookName, Convert(VARCHAR, BookMember.TeslimTarihi, 103) as TeslimTarihi
                            from dbo.BookMember
                            inner join Book on BookMember.bookId = Book.Id
                            where BookMember.TeslimDurumu = 0 and BookMember.TeslimTarihi < CONVERT(DATE, GETDATE());";

            return SqlDataAccess.LoadData<BookMemberModel>(sql);
        }

        public static List<int> GetMembersPuan(int id)
        {
            string sql = $@"select puan
                            from dbo.BookMember
                            where memberId = {id}
                            order by Id desc;";

            return SqlDataAccess.LoadData<int>(sql);
        }

        public static int SetMembersPuan(int memberid, int bookid, int puan)
        {
            BookMemberModel data = new BookMemberModel
            {
                MemberId = memberid,
                BookId = bookid,
                Puan = puan
            };

            string sql = $@"UPDATE dbo.BookMember
                            SET puan = @puan
                            WHERE MemberId=@MemberId and BookId=@BookId;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<PopularBookModel> GetPopularBooks()
        {
            string sql = @"select Book.isim as BookName, Book.Yazar , cast(AVG(cast(puan as decimal(2,1)))as decimal(2,1) ) as OrtalamaPuan, Book.isim as TeslimTarihi
                            from KutuphaneDB.dbo.Book
                            inner join KutuphaneDB.dbo.BookMember on bookID = Book.Id
                            where puan > 0
                            group by book.isim, Book.yazar
                            order by OrtalamaPuan desc;";

            return SqlDataAccess.LoadData<PopularBookModel>(sql);
        }

        public static List<BookMemberModel> GetMembersBook(int memberid)
        {
            string sql = $@"select Book.isim as BookName, BookMember.bookID, Convert(VARCHAR, BookMember.TeslimTarihi, 103) as TeslimTarihi, Book.yazar as Yazar, BookMember.puan as OrtalamaPuan
                            from KutuphaneDB.dbo.BookMember
                            inner join KutuphaneDB.dbo.Book on bookID = Book.Id
                            where memberID = {memberid} and teslimdurumu = 0;";
            return SqlDataAccess.LoadData<BookMemberModel>(sql);
        }

    }
}
