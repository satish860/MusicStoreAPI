using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace MusicStore.Migrations
{
    [Migration(201302031251)]
    public class CreateCartTable:Migration
    {
        public override void Down()
        {
            Delete.Table("Cart");
        }

        public override void Up()
        {
            Create.Table("Cart")
                .WithColumn("Id").AsInt32().PrimaryKey()
                .WithColumn("Price").AsDouble();
        }
    }
}
