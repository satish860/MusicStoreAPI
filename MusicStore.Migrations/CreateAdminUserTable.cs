using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace MusicStore.Migrations
{
    [Migration(201302031230)]
    public class CreateAdminUserTable:Migration
    {
        public override void Down()
        {
            Delete.Table("AdminUser");
        }

        public override void Up()
        {
            Create.Table("AdminUser")
                .WithColumn("Id")
                .AsInt32()
                .PrimaryKey()
                .Identity()
                .WithColumn("UserName").AsString()
                .WithColumn("Password").AsString();
        }
    }
}
