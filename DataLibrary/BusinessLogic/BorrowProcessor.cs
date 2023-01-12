using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class BorrowProcessor
    {
        public static bool BorrowBook(int bookid, int memberid)
        {
            BookMemberModel data = new BookMemberModel
            {
                BookId = bookid,
                MemberId = memberid
            };

            BookModel bookdata = new BookModel
            {
                Id = bookid
            };

            string sql = @"insert into dbo.BookMember(BookId, MemberID)
                            values(@BookId, @MemberId);";

            string booksql = @"UPDATE dbo.Book
                                SET durum = 'uye'
                                WHERE Id=@Id;";

            if (SqlDataAccess.SaveData(sql, data) != 0 && SqlDataAccess.SaveData(booksql, bookdata) != 0)
            {
                return true;
            }
            else return false;

        }

        public static bool ReturnBook(int bookid, int teslimDurumu)
        {
            BookMemberModel bmdata = new BookMemberModel
            {
                BookId = bookid,
                TeslimDurumu = teslimDurumu
            };

            BookModel bdata = new BookModel
            {
                Id = bookid
            };

            string bmsql = @"UPDATE dbo.BookMember
                                SET teslimdurumu = @TeslimDurumu
                                WHERE bookID=@BookId;";

            string bsql = @"UPDATE dbo.Book
                                SET durum = 'kutuphane'
                                WHERE Id=@Id;";

            if (SqlDataAccess.SaveData(bmsql, bmdata) != 0 && SqlDataAccess.SaveData(bsql, bdata) != 0)
            {
                return true;
            }
            else return false;
        }
    }
}
