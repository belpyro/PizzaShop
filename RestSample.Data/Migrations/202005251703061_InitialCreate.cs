namespace RestSample.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IngredientDbs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PizzaDb_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShopPizzas", t => t.PizzaDb_Id)
                .Index(t => t.PizzaDb_Id);

            CreateTable(
                "dbo.ShopPizzas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Price = c.Int(nullable: false),
                        CreatorId = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.IngredientDbs", "PizzaDb_Id", "dbo.ShopPizzas");
            DropIndex("dbo.IngredientDbs", new[] { "PizzaDb_Id" });
            DropTable("dbo.ShopPizzas");
            DropTable("dbo.IngredientDbs");
        }
    }
}
