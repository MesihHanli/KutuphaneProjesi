using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class MemberProcessor
    {
        public static bool UpdateMemberPassword(string eposta, string oldpassword, string newpassword)
        {
            if(LoginMember(eposta,oldpassword) != 0)
            {
                MemberModel data = new MemberModel
                {
                    Parola = newpassword,
                    Eposta = eposta
                };
                string sql = @"update dbo.Member
                                set Parola = @Parola
                                where Eposta = @Eposta;";

                if (SqlDataAccess.SaveData(sql, data) != 0) return true;
                else return false;
            }
            else
            {
                return false;
            }
        }
        public static List<MemberModel> getMembers()
        {
            string sql = "select Id, AdSoyad from dbo.Member;";
            return SqlDataAccess.LoadData<MemberModel>(sql);
        }

        public static List<MemberModel> GetMember(int id)
        {
            string sql = $@"select Id, AdSoyad, Eposta
                            from dbo.Member
                            where Id = {id};";
            return SqlDataAccess.LoadData<MemberModel>(sql);
        }

        public static List<MemberModel> searchMembers(string searchPhrase)
        {
            string sql = $@"select Id, AdSoyad, Telefon, Eposta
                            from dbo.Member
                            where AdSoyad like '%{searchPhrase}%' or Telefon like '%{searchPhrase}%' or Id like '%{searchPhrase}%' or Eposta like '%{searchPhrase}%';";

            return SqlDataAccess.LoadData<MemberModel>(sql);
        }

        public static int CreateMember(string adsoyad, Int64 telefon, string eposta, string parola)
        {
            MemberModel data = new MemberModel
            {
                AdSoyad = adsoyad,
                Telefon = telefon,
                Eposta = eposta,
                Parola = parola
            };

            string sql = @"insert into dbo.Member(AdSoyad, Telefon, Eposta, Parola)
                            values(@AdSoyad, @Telefon, @Eposta, @Parola);";

            string sqlId = @"select Id from dbo.Member where Eposta=@Eposta";

            return SqlDataAccess.SaveDataId(sql, sqlId,data);
        }

        public static int LoginMember(string eposta, string parola)
        {
            MemberModel data = new MemberModel
            {
                Eposta = eposta,
                Parola = parola
            };

            string sql = @"select Parola
                            from dbo.Member
                            where Eposta=@Eposta;";

            string sqlId = @"select Id
                            from dbo.Member
                            where Eposta=@Eposta";

            return SqlDataAccess.EncryptedLoginControlId(sql, sqlId, data, parola);
        }
    }
}
