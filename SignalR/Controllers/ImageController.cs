using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using muagicungban.Models;
using muagicungban.Repositories;

namespace muagicungban.Controllers
{
    public class ImageController : Controller
    {
        private ImagesRepository imagesRepository;
        public ImageController()
        {
            imagesRepository = new ImagesRepository(Connection.connectionString);
        }

        //
        // GET: /Image/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Image/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Image/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Image/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection, HttpPostedFileBase image)
        {
            ItemImage _image = new ItemImage();
            _image.ImageName = collection["ImageName"];
            _image.ItemID = 2;
            if (image != null)
            {
                _image.MimeType = image.ContentType;
                _image.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(_image.ImageData, 0, image.ContentLength);
            }
            imagesRepository.Add(_image);
            return View();
        }
        
        //
        // GET: /Image/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Image/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Image/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Image/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public FileContentResult GetImage(long ImageID)
        {
            var image = imagesRepository.itemImages.First(i => i.ImageID == ImageID);
            return File(image.ImageData, image.MimeType);
        }

        public List<FileContentResult> GetImages(long ItemID)
        {
            List<ItemImage> _images = imagesRepository.itemImages.Where(i => i.ItemID == ItemID).ToList();
            List<FileContentResult> images = new List<FileContentResult>();
            foreach (var item in _images)
            {
                images.Add(File(item.ImageData, item.MimeType));
            }
            return images;
        }
    }
}
