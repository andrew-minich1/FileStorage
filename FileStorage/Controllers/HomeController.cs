using BLL_Interface.Entities;
using BLL_Interface.Services;
using FileStorage.Models.AccountViewModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using FileStorage.Infrastructura;
using FileStorage.Infrastructura.Attribute;
using FileStorage.Models;
using System;
using FileStorage.Models.Mapper;
using System.IO;
using System.Collections.Generic;




namespace FileStorage.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileService fileService;
        private readonly IUserService userService;

        public HomeController(IFileService fileService, IUserService userService)
        {
            this.fileService = fileService;
            this.userService = userService;
        }

        public ActionResult Index(string sortOrder)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = userService.GetOneByPredicate(u => u.Login == User.Identity.Name);
                var model = fileService.GetAllByPredicate(f=>f.UserId==user.Id).ToList();
                var viewModel = model.GetFilesViewModel();
                Session["files"] = model.Where(f => f.IsDelete != true);
                ViewBag.FileAction = true;
                return View(viewModel);
            }
            return View("StartView");
        }

     
        public ActionResult ShowUserFiles(int id)
        {
            var files =  fileService.GetAllByPredicate(f => f.UserId == id).ToList().GetFilesViewModel();
            Session["files"] = fileService.GetAllByPredicate(f => f.UserId == id);
            ViewBag.FileAction = true;
            return PartialView("_FilesView", files);
        }



        [HttpPost]
        public ActionResult Search(string search)
       {
            if (User.Identity.IsAuthenticated)
            {
                var user = userService.GetAllByPredicate(u => u.Login == User.Identity.Name).FirstOrDefault();
                var files = fileService.GetAllByPredicate(file => file.UserId == user.Id && file.IsDelete != true && file.Name.Contains(search)).ToList().GetFilesViewModel();
                ViewBag.FileAction = false;
                return PartialView("_FilesView", files);
            }
            return View("StartView",true);
        }


        [HttpPost]
        public ActionResult Upload(UploadFileViewModel upload)
        {
            string currentUser = User.Identity.Name;
            var user = userService.GetOneByPredicate(u => u.Login == currentUser);
           
            if (ModelState.IsValid)
            {
                if (upload != null && upload.File.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(upload.File.FileName);
                    int size = upload.File.ContentLength;
                    var bllfile = new FileEntity()
                    {
                        UserId = user.Id,
                        Name = Path.GetFileNameWithoutExtension(upload.File.FileName),
                        Path = Server.MapPath("~/Storage/" + user.Login + "/" + fileName),
                        Type = Path.GetExtension(upload.File.FileName),
                        Size = upload.File.ContentLength
                    };
                    fileService.Create(bllfile);
                    upload.File.SaveAs(Server.MapPath("~/Storage/" + user.Login + "/" + fileName));
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ButtonNameAction]
        public ActionResult Load(int Id)
        { 
            var file = fileService.GetById(Id);
            return File(file.Path, System.Net.Mime.MediaTypeNames.Application.Octet, file.Name);
        }


        [HttpPost]
        public void DeleteFile(int id)
        {
            var file = fileService.GetOneByPredicate(f => f.Id == id);
            fileService.Delete(file);
        }


        [HttpPost]
        public ActionResult InBox(int id)
        {
            var file = fileService.GetAllByPredicate(f => f.Id == id).FirstOrDefault();
            fileService.AddToCart(file);
            int userId = file.UserId;
            var files = fileService.GetAllByPredicate(f => f.UserId == userId && f.IsDelete == true).ToList().GetFilesViewModel();
            return PartialView("_Box", files);
        }


        [HttpPost]
        public ActionResult Restore(int id)
        {
            var file = fileService.GetOneByPredicate(f => f.Id == id);
            fileService.Restore(file);
            int userId = file.UserId;
            var files = fileService.GetAllByPredicate(f => f.UserId == userId && f.IsDelete == false).ToList().GetFilesViewModel();
            ViewBag.FileAction = true;
            return PartialView("_FilesView", files);
        }


        [HttpPost]
        public ActionResult FileSearch(string name)
        {
            var user = userService.GetOneByPredicate(u => u.Login == name);
            if (user != null)
            {
                var files = user.Files.Where(f => f.IsOpen == true);
                Session["files"] = files;
                var openFiles = files.ToList().GetFilesViewModel();
                ViewBag.FileAction = false;
                return PartialView("_FilesView", openFiles);
            }
            return null;
        }

        [HttpPost]
        public ActionResult MakeOpen(int id)
        {
            fileService.MakeOpen(id);
            return RedirectToAction("Index");
        }

        public ActionResult Sort(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date"; 

            var files = Session["files"];
            var model = fileService.SortFiles((files as IEnumerable<FileEntity>), sortOrder).ToList().GetFilesViewModel();
            ViewBag.FileAction = false;
            return PartialView("_FilesView", model);
        }

        public ActionResult RenameFile(int id, string newName)
        {
            return Content(fileService.RenameFile(id, newName));
        }

    }
}
