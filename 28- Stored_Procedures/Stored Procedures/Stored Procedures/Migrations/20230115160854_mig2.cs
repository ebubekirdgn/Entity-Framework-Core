using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProcedures.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"CREATE PROCEDURE sp_PersonOrders1
                                    AS
	                                    SELECT p.Name,COUNT(*) FROM Persons P
	                                    JOIN Orders o
		                                    ON P.PersonId = o.PersonId
	                                    GROUP BY p.Name
	                                    ORDER BY COUNT(*) DESC");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP PROC sp_PersonOrders1");
        }
    }
}
