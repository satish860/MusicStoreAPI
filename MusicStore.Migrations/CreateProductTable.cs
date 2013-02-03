using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace MusicStore.Migrations
{
    [Migration(201302031256)]
    public class CreateProductTable:Migration
    {
        public override void Down()
        {
            Delete.Table("Products");
        }

        public override void Up()
        {
            Create
                .Table("Products")
                .WithColumn("Id").AsInt16().PrimaryKey()
                .WithColumn("Name").AsString()
                .WithColumn("Categories").AsString()
                .WithColumn("Price").AsDouble();
        }
    }
}
