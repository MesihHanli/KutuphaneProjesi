using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public static class NotificationProcessor
    {
        public static bool DeleteNotification(int notificationid)
        {
            NotificationModel data = new NotificationModel
            {
                Id = notificationid,
                isActive = 0
            };
            string sql = @"update dbo.Notification
                            set isActive = @isActive
                            where Id = @Id;";
            if (SqlDataAccess.SaveData(sql, data) != 0) return true;
            else return false;
        }

        public static List<NotificationModel> GetNotifications(int memberid)
        {
            string sql = $@"select Id, Bildirim
                            from dbo.Notification
                            where MemberId = {memberid} and isActive = 1;";
            return SqlDataAccess.LoadData<NotificationModel>(sql);
        }

        public static bool InsertNotification(int memberid, string notification)
        {
            NotificationModel data = new NotificationModel
            {
                MemberId = memberid,
                Bildirim = notification
            };

            string sql = @"insert into dbo.Notification(MemberId, Bildirim)
                            values(@MemberId, @Bildirim);";

            if (SqlDataAccess.SaveData(sql, data) != 0)
            {
                return true;
            }
            else return false;
        }
    }
}
