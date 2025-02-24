using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class spforcoalminecoalproduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert CoalMine
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_InsertCoalMine
                    @MineName VARCHAR(100),
                    @Location VARCHAR(255),
                    @CapacityInTons INT,
                    @EstablishedDate DATE
                AS
                BEGIN
                    INSERT INTO CoalMines (MineName, Location, CapacityInTons, EstablishedDate)
                    VALUES (@MineName, @Location, @CapacityInTons, @EstablishedDate);
                END;
            ");

            // Get All Coal Mines
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_GetAllCoalMines
                AS
                BEGIN
                    SELECT * FROM CoalMines;
                END;
            ");

            // Update CoalMine
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_UpdateCoalMine
                    @MineID INT,
                    @MineName VARCHAR(100),
                    @Location VARCHAR(255),
                    @CapacityInTons INT,
                    @EstablishedDate DATE
                AS
                BEGIN
                    UPDATE CoalMines
                    SET MineName = @MineName, Location = @Location, CapacityInTons = @CapacityInTons, EstablishedDate = @EstablishedDate
                    WHERE MineID = @MineID;
                END;
            ");

            // Delete CoalMine
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_DeleteCoalMine
                    @MineID INT
                AS
                BEGIN
                    DELETE FROM CoalMines WHERE MineID = @MineID;
                END;
            ");

            // Insert Coal Production
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_InsertCoalProduction
                    @MineID INT,
                    @ProductionYear INT,
                    @TotalProductionInTons INT
                AS
                BEGIN
                    INSERT INTO CoalProduction (MineID, ProductionYear, TotalProductionInTons)
                    VALUES (@MineID, @ProductionYear, @TotalProductionInTons);
                END;
            ");

            // Get Coal Production by Mine
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_GetCoalProductionByMine
                    @MineID INT
                AS
                BEGIN
                    SELECT * FROM CoalProduction WHERE MineID = @MineID;
                END;
            ");

            // Delete Coal Production
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_DeleteCoalProduction
                    @ProductionID INT
                AS
                BEGIN
                    DELETE FROM CoalProduction WHERE ProductionID = @ProductionID;
                END;
            ");

            // Get All Coal Productions
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_GetAllCoalProductions
                AS
                BEGIN
                    SELECT * FROM CoalProductions;
                END;
            ");

            // Get Coal Production By ID
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_GetCoalProductionByID
                    @ProductionID INT
                AS
                BEGIN
                    SELECT * FROM CoalProductions WHERE ProductionID = @ProductionID;
                END;
            ");

            // Insert Coal Production
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_InsertCoalProduction
                    @MineID INT,
                    @ProductionYear INT,
                    @TotalProductionInTons INT
                AS
                BEGIN
                    INSERT INTO CoalProductions (MineID, ProductionYear, TotalProductionInTons)
                    VALUES (@MineID, @ProductionYear, @TotalProductionInTons);
                END;
            ");

            // Update Coal Production
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_UpdateCoalProduction
                    @ProductionID INT,
                    @MineID INT,
                    @ProductionYear INT,
                    @TotalProductionInTons INT
                AS
                BEGIN
                    UPDATE CoalProductions
                    SET MineID = @MineID,
                        ProductionYear = @ProductionYear,
                        TotalProductionInTons = @TotalProductionInTons
                    WHERE ProductionID = @ProductionID;
                END;
            ");

            // Delete Coal Production
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_DeleteCoalProduction
                    @ProductionID INT
                AS
                BEGIN
                    DELETE FROM CoalProductions WHERE ProductionID = @ProductionID;
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop Procedures on Rollback
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_InsertCoalMine;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetAllCoalMines;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_UpdateCoalMine;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_DeleteCoalMine;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_InsertCoalProduction;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetCoalProductionByMine;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_DeleteCoalProduction;");

            // Drop Stored Procedures
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetAllCoalProductions;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetCoalProductionByID;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_InsertCoalProduction;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_UpdateCoalProduction;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_DeleteCoalProduction;");
        }
    }
}
