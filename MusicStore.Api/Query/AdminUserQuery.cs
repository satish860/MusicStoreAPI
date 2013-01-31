using MusicStore.Api.Models;
using Simple.Data;

namespace MusicStore.Api.Query
{
    public class AdminUserQuery : IQueryFor<AdminModel, AdminModel>
    {
        public AdminUserQuery()
        {
        }

        public AdminModel Execute(AdminModel input)
        {
            AdminModel adminUser;
            var databaseAdminUser = Database.Open().AdminUser;
            adminUser = databaseAdminUser.Get(input.UserName);
            return adminUser;
        }
    }
}