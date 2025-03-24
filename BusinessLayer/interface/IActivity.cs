using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IActivity
    {
        Task<List<Activity>> GetAllActivitiesAsync();
        Task<Activity> GetActivityByIdAsync(int activityId);
        Task<int> InsertActivityAsync(Activity activity);
        Task<bool> UpdateActivityAsync(Activity activity);
        Task<bool> DeleteActivityAsync(int activityId);
    }
}
