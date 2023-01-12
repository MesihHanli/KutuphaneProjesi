using DataLibrary.DataAccess;
using DataLibrary.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLibrary.BusinessLogic
{
    public static class EmployeeProcessor
    {
        public static List<EmployeeModel> GetEmployee(string tc)
        {
            string sql = $@"select Ad, Soyad, Eposta
                            from dbo.Employee
                            where TcNo = '{tc}'";
            return SqlDataAccess.LoadData<EmployeeModel>(sql);
        }

        public static bool UpdateName(string ad, string soyad, Int64 tcno)
        {
            EmployeeModel data = new EmployeeModel
            {
                Ad = ad,
                Soyad = soyad,
                TcNo = tcno
            };
            string sql = $@"UPDATE dbo.Employee
                         SET Ad = @Ad, Soyad = @Soyad
                         WHERE TcNo = @TcNo;";

            if (SqlDataAccess.SaveData(sql, data) != 0) return true;
            else return false;
        }
        public static bool UpdateMail(string mail, Int64 tcno)
        {
            EmployeeModel data = new EmployeeModel
            {
                Eposta = mail,
                TcNo = tcno
            };
            string sql = $@"UPDATE dbo.Employee
                         SET Eposta = @Eposta
                         WHERE TcNo = @TcNo;";

            if (SqlDataAccess.SaveData(sql, data) != 0) return true;
            else return false;
        }
        public static bool UpdatePassword(string sifre, Int64 tcno)
        {
            EmployeeModel data = new EmployeeModel
            {
                Parola = sifre,
                TcNo = tcno
            };
            string sql = $@"UPDATE dbo.Employee
                         SET Parola = @Parola
                         WHERE TcNo = @TcNo;";

            if (SqlDataAccess.SaveData(sql, data) != 0) return true;
            else return false;
        }

        public static int CreateEmployee(string ad, string soyad, Int64 tcno, string eposta, string parola)
        {
            EmployeeModel data = new EmployeeModel
            {
                Ad = ad,
                Soyad = soyad,
                TcNo = tcno,
                Eposta = eposta,
                Parola = parola
            };

            string sql = @"insert into dbo.Employee(Ad, Soyad, TcNo, Eposta, Parola)
                            values(@Ad, @Soyad, @TcNo, @Eposta, @Parola);";

            return SqlDataAccess.SaveData(sql, data);
        }

        //public static int LoginEmployee(Int64 tcno, string parola)
        //{
        //    EmployeeModel data = new EmployeeModel
        //    {
        //        TcNo = tcno,
        //        Parola = parola
        //    };

        //    string sql = @"select count(Id)
        //                    from dbo.Employee
        //                    where TcNo=@TcNo and Parola=@Parola;";

        //    return SqlDataAccess.CheckData(sql, data);
        //}
        public static bool LoginEmployee(Int64 tcno, string parola)
        {
            EmployeeModel data = new EmployeeModel
            {
                TcNo = tcno,
                Parola = parola
            };

            string sql = @"select Parola
                            from dbo.Employee
                            where TcNo=@TcNo";

            return SqlDataAccess.EncryptedLoginControl(sql, data, parola);
        }

        //Burası kullanmılmayacak
        public static List<EmployeeModel> LoadEmployees()
        {
            string sql = @"select Id, EmployeeId, FirstName, LastName, EmailAddress
                            from dbo.Employee;";
            return SqlDataAccess.LoadData<EmployeeModel>(sql);
        }
    }
}
