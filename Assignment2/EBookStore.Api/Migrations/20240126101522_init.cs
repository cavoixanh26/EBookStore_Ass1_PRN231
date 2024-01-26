using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBookStore.Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    last_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    first_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    city = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    state = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    zip = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    email_address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    pub_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    publisher_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    city = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    state = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    country = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Publishe__2515F222BF42AD2A", x => x.pub_id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_desc = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    pub_id = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    advance = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    royalty = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    ytd_sales = table.Column<int>(type: "int", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    published_date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.book_id);
                    table.ForeignKey(
                        name: "FK__Book__pub_id__45F365D3",
                        column: x => x.pub_id,
                        principalTable: "Publisher",
                        principalColumn: "pub_id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email_address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    source = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    first_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    middle_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    last_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: true),
                    pub_id = table.Column<int>(type: "int", nullable: true),
                    hire_date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id);
                    table.ForeignKey(
                        name: "FK__User__pub_id__412EB0B6",
                        column: x => x.pub_id,
                        principalTable: "Publisher",
                        principalColumn: "pub_id");
                    table.ForeignKey(
                        name: "FK__User__role_id__403A8C7D",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "role_id");
                });

            migrationBuilder.CreateTable(
                name: "BookAuthor",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    author_order = table.Column<int>(type: "int", nullable: true),
                    royalty_percentage = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookAuth__82C1BA61FD0706B3", x => new { x.author_id, x.book_id });
                    table.ForeignKey(
                        name: "FK__BookAutho__autho__48CFD27E",
                        column: x => x.author_id,
                        principalTable: "Author",
                        principalColumn: "author_id");
                    table.ForeignKey(
                        name: "FK__BookAutho__book___49C3F6B7",
                        column: x => x.book_id,
                        principalTable: "Book",
                        principalColumn: "book_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_pub_id",
                table: "Book",
                column: "pub_id");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_book_id",
                table: "BookAuthor",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_pub_id",
                table: "User",
                column: "pub_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_role_id",
                table: "User",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthor");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
