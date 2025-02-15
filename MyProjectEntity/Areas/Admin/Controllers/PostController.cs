using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {

        private readonly IPosts _posts;
        private readonly IMapper _mapper;
        public PostController(IPosts posts, IMapper mapper)
        {

            _posts = posts;
            _mapper = mapper;
        }
        // GET: PostController
        public async Task<ActionResult> Index()
        {
            List<PostDomainModel> posts = await _posts.Getpostlist();
            List<PostViewModel> postsViewModel = _mapper.Map<List<PostViewModel>>(posts);
            return View(postsViewModel);
        }

        // GET: PostController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            PostDomainModel postDomainModel = await _posts.GetPostByIdAsync(id);
            PostViewModel postViewModel = _mapper.Map<PostViewModel>(postDomainModel);
            return View(postViewModel);
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            PostViewModel postViewModel = new PostViewModel();
            return View(postViewModel);
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostViewModel postViewModel)
        {
            try
            {
                PostDomainModel postDomainModel =  _mapper.Map<PostDomainModel>(postViewModel);
                int b = await _posts.InsertPostAsync(postDomainModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            PostDomainModel postDomainModel = await _posts.GetPostByIdAsync(id);
            PostViewModel postViewModel = _mapper.Map<PostViewModel>(postDomainModel);
            return View(postViewModel);
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PostViewModel postViewModel)
        {
            try
            {
                PostDomainModel postDomainModel = _mapper.Map<PostDomainModel>(postViewModel);
                int b = await _posts.UpdatePostAsync(postDomainModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            PostDomainModel postDomainModel = await _posts.GetPostByIdAsync(id);
            PostViewModel postViewModel = _mapper.Map<PostViewModel>(postDomainModel);
            return View(postViewModel);
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                int b = await _posts.DeletePostAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
