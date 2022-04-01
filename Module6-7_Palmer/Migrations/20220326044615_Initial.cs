using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Module6_7_Palmer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    City = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    State = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Zipcode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderPrice = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    // added this foreign key because this is how it should have been set up especially since
                    // Entity set up the customerID as a FK so productID should be a FK and Product should not have a Order FK
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    OrderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    // Took this FK out because the FK should have been in the orders table
                    //table.ForeignKey(
                    //    name: "FK_Products_Orders_OrderID",
                    //    column: x => x.OrderID,
                    //    principalTable: "Orders",
                    //    principalColumn: "OrderID",
                    //    onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "City", "EmailAddress", "FirstName", "LastName", "PhoneNumber", "State", "Street", "Zipcode" },
                values: new object[,]
                {
                    { 1, "Arlington", "MoJoJOJO1@example.com", "Mojo", "Jojo", "712-909-1207", "Texas", "143 Brentwood", "90938" },
                    { 2, "Festus", "KHalls@example.com", "Kevin", "Hall", "314-884-1035", "Missouri", "909 Acton", "73644" },
                    { 3, "Wood River", "Tman29@example.com", "Tenton", "Argusta", "618-435-8765", "Illinois", "214 Edwardston", "62002" },
                    { 4, "Da Hood", "SteveO@example.com", "Steve", "O", "948-492-0394", "Missouri", "84 Da street", "74893" },
                    { 5, "Warrenton", "AHolmes@example.com", "Anthoney", "Holmes", "314-988-5575", "Missouri", "123 Something", "28273" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Description", "Discriminator", "Image", "Name", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "A traditional japaneese Bonsi Tree", "Product", "bonsiTree.jpg", "Bonsi Tree", 19.989999999999998 },
                    { 2, "A banana that is a dog", "Product", "bananaDog.jpg", "Banana Dog", 100.98999999999999 },
                    { 3, "A friendly chicken", "Product", "chicken.jpg", "Chicken", 2.75 },
                    { 4, "A see through digital clock", "Product", "digitalClock.jpg", "Digitl Clock", 35.990000000000002 },
                    { 5, "A fancy chair from the future", "Product", "fancyChair.jpg", "Fancy Future Chair", 49.990000000000002 },
                    { 6, "Wireless headphones", "Product", "headPhones.jpg", "Bluetooth headphones", 19.989999999999998 },
                    { 7, "A mini camera for mini picutres", "Product", "miniCamera.jpg", "Mini Camera", 69.989999999999995 },
                    { 8, "A smart coffee maker, make coffee from your phone", "Product", "smartCofee.jpg", "Smart Coffee Maker", 74.989999999999995 },
                    { 9, "A smart toaster maker, make toast from your phone", "Product", "smartToaster.jpg", "Smart Toaster", 25.989999999999998 },
                    { 10, "Cut watermealons with ease", "Product", "waterMelonCutter.jpg", "Watermelon Cutter", 34.990000000000002 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderID",
                table: "Products",
                column: "OrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
