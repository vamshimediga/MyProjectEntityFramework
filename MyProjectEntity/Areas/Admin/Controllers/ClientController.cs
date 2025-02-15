using AutoMapper;
using BusinessLayer;
using Entities;
using EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MyProjectEntity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClientController : Controller
    {

        private readonly IClient _client;
        private readonly IMapper _mapper;
        private readonly ILawyer _lwayer;
        public ClientController(IClient client, IMapper mapper, ILawyer lwayer)
        {
            _client = client;
            _mapper = mapper;
            _lwayer = lwayer;   
        }
        // GET: ClientController
        public async Task<ActionResult> Index()
        {
            List<Client> clients =  await _client.GetClients();
            List<ClientViewModel> clientsViewModel = _mapper.Map<List<ClientViewModel>>(clients);
            return View(clientsViewModel);
        }

     

        // GET: ClientController/Create
        public async Task<ActionResult> Create()
        {
            ClientViewModel clientViews = new ClientViewModel();
            List<Lawyer> lawyers = (List<Lawyer>)await _lwayer.GetAllLawyers();
            List<LawyerViewModel> lawyerViewModels = _mapper.Map<List<LawyerViewModel>>(lawyers);
            clientViews.Lawyers = lawyerViewModels;
            return View(clientViews);
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClientViewModel clientViews)
        {
            try
            {
                Client client = _mapper.Map<Client>(clientViews);
                await _client.insert(client);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Client client =  await _client.GetClient(id);   
            ClientViewModel clientViews = _mapper.Map<ClientViewModel>(client);
            List<Lawyer> lawyers = (List<Lawyer>)await _lwayer.GetAllLawyers();
            List<LawyerViewModel> lawyerViewModels = _mapper.Map<List<LawyerViewModel>>(lawyers);
            clientViews.Lawyers = lawyerViewModels;
            return View(clientViews);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ClientViewModel clientViews)
        {
            try
            {
                Client client = _mapper.Map<Client>(clientViews);
                await _client.update(client);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Client client = await _client.GetClient(id);
            ClientViewModel clientViews = _mapper.Map<ClientViewModel>(client);
            return View(clientViews);
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _client.delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
