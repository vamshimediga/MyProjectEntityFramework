using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddActivitiesAndOpportunitiesAndStoreProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Stored Procedure for Getting All Opportunities
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetAllOpportunities
                AS
                BEGIN
                    SELECT * FROM Opportunities;
                END;
            ");

            // Stored Procedure for Getting an Opportunity by ID
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetOpportunityByID
                    @OpportunityID INT
                AS
                BEGIN
                    SELECT * FROM Opportunities WHERE OpportunityID = @OpportunityID;
                END;
            ");

            // Stored Procedure for Inserting an Opportunity
            migrationBuilder.Sql(@"
                CREATE PROCEDURE InsertOpportunity
                    @OpportunityName NVARCHAR(255),
                    @EstimatedValue DECIMAL(18,2),
                    @CloseDate DATE,
                    @Stage NVARCHAR(50)
                AS
                BEGIN
                    INSERT INTO Opportunities (OpportunityName, EstimatedValue, CloseDate, Stage)
                    VALUES (@OpportunityName, @EstimatedValue, @CloseDate, @Stage);

                    SELECT SCOPE_IDENTITY() AS NewOpportunityID;
                END;
            ");

            // Stored Procedure for Updating an Opportunity
            migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdateOpportunity
                    @OpportunityID INT,
                    @OpportunityName NVARCHAR(255),
                    @EstimatedValue DECIMAL(18,2),
                    @CloseDate DATE,
                    @Stage NVARCHAR(50)
                AS
                BEGIN
                    UPDATE Opportunities
                    SET OpportunityName = @OpportunityName,
                        EstimatedValue = @EstimatedValue,
                        CloseDate = @CloseDate,
                        Stage = @Stage
                    WHERE OpportunityID = @OpportunityID;
                END;
            ");

            // Stored Procedure for Deleting an Opportunity
            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteOpportunity
                    @OpportunityID INT
                AS
                BEGIN
                    DELETE FROM Opportunities WHERE OpportunityID = @OpportunityID;
                END;
            ");

            // Stored Procedure for Getting All Activities
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetAllActivities
                AS
                BEGIN
                    SELECT * FROM Activities;
                END;
            ");

            // Stored Procedure for Getting an Activity by ID
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetActivityByID
                    @ActivityID INT
                AS
                BEGIN
                    SELECT * FROM Activities WHERE ActivityID = @ActivityID;
                END;
            ");

            // Stored Procedure for Inserting an Activity
            migrationBuilder.Sql(@"
                CREATE PROCEDURE InsertActivity
                    @ActivityType NVARCHAR(100),
                    @Description NVARCHAR(255),
                    @ActivityDate DATETIME,
                    @OpportunityID INT
                AS
                BEGIN
                    INSERT INTO Activities (ActivityType, Description, ActivityDate, OpportunityID)
                    VALUES (@ActivityType, @Description, @ActivityDate, @OpportunityID);

                    SELECT SCOPE_IDENTITY() AS NewActivityID;
                END;
            ");

            // Stored Procedure for Updating an Activity
            migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdateActivity
                    @ActivityID INT,
                    @ActivityType NVARCHAR(100),
                    @Description NVARCHAR(255),
                    @ActivityDate DATETIME,
                    @OpportunityID INT
                AS
                BEGIN
                    UPDATE Activities
                    SET ActivityType = @ActivityType,
                        Description = @Description,
                        ActivityDate = @ActivityDate,
                        OpportunityID = @OpportunityID
                    WHERE ActivityID = @ActivityID;
                END;
            ");

            // Stored Procedure for Deleting an Activity
            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteActivity
                    @ActivityID INT
                AS
                BEGIN
                    DELETE FROM Activities WHERE ActivityID = @ActivityID;
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop stored procedures in case of rollback
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllOpportunities;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetOpportunityByID;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS InsertOpportunity;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateOpportunity;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteOpportunity;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllActivities;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetActivityByID;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS InsertActivity;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateActivity;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteActivity;");
        }
    }
}
