using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrapQL.Migrations
{
    public partial class spGetAllOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[spGetAllOwner]
                    @PageIndex int,
                    @PageSize int
                AS
                BEGIN
                    select *,COUNT(*) OVER () as TotalRecords from Owners
                    order by Id
                    OFFSET (@PageIndex - 1)*@PageSize ROWS
                    FETCH NEXT @PageSize ROWS ONLY
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
        }
    }
}
