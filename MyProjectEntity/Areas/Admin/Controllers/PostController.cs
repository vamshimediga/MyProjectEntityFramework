using AutoMapper;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly ServiceLayer<PostDomainModel> _service;
        private readonly IMapper _mapper;
        private readonly ApiService _apiService;

        public PostController(ServiceLayer<PostDomainModel> service, IMapper mapper, ApiService apiService)
        {
            _service = service;
            _mapper = mapper;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            List<PostDomainModel> postsDto = await _service.GetAllAsync(_apiService.GetApiUrl(ApiEndpoint.Post));
            List<PostViewModel> viewModel = _mapper.Map<List<PostViewModel>>(postsDto);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            PostDomainModel postDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Post), id);
            PostViewModel viewModel = _mapper.Map<PostViewModel>(postDto);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View(new PostViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            try
            {
                PostDomainModel post = _mapper.Map<PostDomainModel>(postViewModel);
                int id = await _service.AddAsync(_apiService.GetApiUrl(ApiEndpoint.Post), post);

                if (id > 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating post: " + ex.Message);
            }
            return View(postViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            PostDomainModel postDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Post), id);
            PostViewModel viewModel = _mapper.Map<PostViewModel>(postDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostViewModel postViewModel)
        {
            try
            {
                PostDomainModel post = _mapper.Map<PostDomainModel>(postViewModel);
                bool isSuccess = await _service.UpdateAsync($"{_apiService.GetApiUrl(ApiEndpoint.Post)}/{id}", post);

                if (isSuccess)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating post: " + ex.Message);
            }
            return View(postViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            PostDomainModel postDto = await _service.GetByIdAsync(_apiService.GetApiUrl(ApiEndpoint.Post), id);
            PostViewModel viewModel = _mapper.Map<PostViewModel>(postDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool isSuccess = await _service.DeleteAsync(_apiService.GetApiUrl(ApiEndpoint.Post), id);
            if (isSuccess)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error deleting post.");
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
