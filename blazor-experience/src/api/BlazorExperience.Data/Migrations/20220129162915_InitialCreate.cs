using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorExperience.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "Title" },
                values: new object[,]
                {
                    { 1L, "Peter Ducker", "We live in an age of unprecedented opportunity: with ambition, drive, and talent, you can rise to the top of your chosen profession, regardless of where you started out...", "Managing Oneself" },
                    { 2L, "David Buss", "Evolutionary Psychology: The New Science of the Mind, 5th edition provides students with the conceptual tools of evolutionary psychology, and applies them to empirical research on the human mind...", "Evolutionary Psychology" },
                    { 3L, "Dale Carnegie", "Millions of people around the world have improved their lives based on the teachings of Dale Carnegie. In How to Win Friends and Influence People, he offers practical advice and techniques for how to get out of a mental rut and make life more rewarding...", "How to Win Friends & Influence People" },
                    { 4L, "Richard Dawkins", "Professor Dawkins articulates a gene’s eye view of evolution. A view giving center stage to these persistent units of information, and in which organisms can be seen as vehicles for their replication. This imaginative, powerful, and stylistically brilliant work not only brought the insights of Neo-Darwinism to a wide audience. But galvanized the biology community, generating much debate and stimulating whole new areas of research...", "The Selfish Gene" },
                    { 5L, "Will & Ariel Durant", "Will and Ariel Durant have succeeded in distilling for the reader the accumulated store of knowledge and experience from their five decades of work on the eleven monumental volumes of The Story of Civilization...", "The Lessons of History" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
