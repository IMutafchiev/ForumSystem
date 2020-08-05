using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumSystem.App.Areas.Admin.Services.Interfaces;
using ForumSystem.App.Areas.Admin.ViewModels.Topics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    [Authorize]
    public class TopicsController : Controller
    {

        private readonly IAdminTopicService _service;

        public TopicsController(IAdminTopicService service)
        {
            _service = service;
        }

        [Route("/Topics/Create")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Route("/Topics/Create")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTopicBindingModel model)
        {
            await _service.CreateTopicAsync(model);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var topic = await _service.GetTopicAsync(id);

            var viewModel = new DetailsTopicViewModel
            {
                Title = topic.Title,
                Content = topic.Content,
                AuthorUserName = topic.Author.UserName,
                CreatedOn = topic.CreatedOn,
                Posts = topic.Posts.ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = new EditTopicBindingModel
            {
                Id = id
            };

            return this.View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTopicBindingModel model)
        {
            var id = int.Parse(HttpContext.Request.Query["id"].ToString().Split().ToArray()[0]);

            await _service.EditTopicAsync(id, model);

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            return this.View();
        }

        public async Task<IActionResult> Delete(int id)
        {


            return RedirectToAction("Index", "Home");
        }
    }
}