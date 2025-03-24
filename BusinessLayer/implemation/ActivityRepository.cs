using Data;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Entities;
namespace BusinessLayer.implemation
{
    public class ActivityRepository:IActivity
    {
        private readonly ApplicationDbContext _context;

        public ActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.Activity>> GetAllActivitiesAsync()
        {
            List<Entities.Activity> activities = await _context.Activities.FromSqlRaw("EXEC GetAllActivities").ToListAsync();
            // Explicitly load Opportunity reference for each Activity
            foreach (Entities.Activity activity in activities)
            {
                await _context.Entry(activity)
                    .Reference(a => a.Opportunity)
                    .LoadAsync();
            }
            return activities;
        }

        // Get Activity By ID
        public async Task<Entities.Activity> GetActivityByIdAsync(int activityId)
        {
            var param = new SqlParameter("@ActivityID", activityId);
            var result = await _context.Activities.FromSqlRaw("EXEC GetActivityByID @ActivityID", param).ToListAsync();
            return result.FirstOrDefault();
        }

        // Insert Activity
        public async Task<int> InsertActivityAsync(Entities.Activity activity)
        {
            var parameters = new[]
            {
                new SqlParameter("@ActivityType", activity.ActivityType),
                new SqlParameter("@Description", activity.Description),
                new SqlParameter("@ActivityDate", activity.ActivityDate),
                new SqlParameter("@OpportunityID", activity.OpportunityID)
            };

            return await _context.Database.ExecuteSqlRawAsync("EXEC InsertActivity @ActivityType, @Description, @ActivityDate, @OpportunityID", parameters);
        }

        // Update Activity
        public async Task<bool> UpdateActivityAsync(Entities.Activity activity)
        {
            var parameters = new[]
            {
                new SqlParameter("@ActivityID", activity.ActivityID),
                new SqlParameter("@ActivityType", activity.ActivityType),
                new SqlParameter("@Description", activity.Description),
                new SqlParameter("@ActivityDate", activity.ActivityDate),
                new SqlParameter("@OpportunityID", activity.OpportunityID)
            };

            var result = await _context.Database.ExecuteSqlRawAsync("EXEC UpdateActivity @ActivityID, @ActivityType, @Description, @ActivityDate, @OpportunityID", parameters);
            return result > 0;
        }

        // Delete Activity
        public async Task<bool> DeleteActivityAsync(int activityId)
        {
            var param = new SqlParameter("@ActivityID", activityId);
            var result = await _context.Database.ExecuteSqlRawAsync("EXEC DeleteActivity @ActivityID", param);
            return result > 0;
        }

        
    }
}

