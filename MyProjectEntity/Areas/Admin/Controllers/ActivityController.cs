using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ActivityController : Controller
    {
        public readonly IActivity _activity;
        public readonly IMapper _mapper;
        public ActivityController(IActivity activity,IMapper mapper) {
           _activity = activity;
           _mapper = mapper;

        }
        // GET: ActivityController
        public async Task<ActionResult> Index()
        {
            List<Activity> activities = (List<Activity>)await _activity.GetAllActivitiesAsync();
            List<ActivityViewModel> viewModel = _mapper.Map<List<ActivityViewModel>>(activities); 
            return View(viewModel);
        }

        // GET: ActivityController/Details/5
        public async Task<ActionResult> Details(int id)
        {

            Activity activity = await _activity.GetActivityByIdAsync(id);
            ActivityViewModel activityViewModel = _mapper.Map<ActivityViewModel>(activity);

            return View(activityViewModel);
        }

        // GET: ActivityController/Create
        public async Task<ActionResult> Create()
        {
            ActivityViewModel activityViewModel = new ActivityViewModel();
            return View(activityViewModel);
        }

        // POST: ActivityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ActivityViewModel activityViewModel)
        {
            try
            {
                Activity activity =  _mapper.Map<Activity>(activityViewModel);
                int id = await _activity.InsertActivityAsync(activity);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ActivityController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Activity activity = await _activity.GetActivityByIdAsync(id);
            ActivityViewModel activityViewModel = _mapper.Map<ActivityViewModel>(activity);
           
            return View(activityViewModel);
        }

        // POST: ActivityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ActivityViewModel activityViewModel)
        {
            try
            {
                Activity activity = _mapper.Map<Activity>(activityViewModel);
                bool b= await _activity.UpdateActivityAsync(activity);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ActivityController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Activity activity = await _activity.GetActivityByIdAsync(id);
            ActivityViewModel activityViewModel = _mapper.Map<ActivityViewModel>(activity);
            return View(activityViewModel);
        }

        // POST: ActivityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            { 
                bool b =  await _activity.DeleteActivityAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
