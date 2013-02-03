using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace MusicStore.Migrations
{
    [Migration(201302031200)]
    public class CreateCartItemTable:Migration
    {
        public override void Down()
        {
            Delete.Table("CartItem");
        }

        public override void Up()
        {
            Create.Table("CartItem")
                .WithColumn("CartId").AsInt32().ForeignKey("Cart", "Id")
                .WithColumn("ProductName").AsString()
                .WithColumn("ProductId").AsInt32()
                .WithColumn("Quantity").AsInt32()
                .WithColumn("Price").AsDouble();
        }
    }
}
