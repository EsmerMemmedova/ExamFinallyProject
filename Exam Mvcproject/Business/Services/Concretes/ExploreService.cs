using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepostoryAbstract;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class ExploreService : IExploreService
    { 
        private readonly IExploreRepostory _exploreRepostory;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExploreService(IExploreRepostory exploreRepostory, IWebHostEnvironment webHostEnvironment)
        {
            _exploreRepostory = exploreRepostory;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddExplore(Explore explore)
        {
            if (explore == null) throw new ArgumentNullException("Explore TAPLIMADI!");
            if (explore.ImgFile == null) throw new ImageFileException("Sekil File tapilmadi");
            if (!explore.ImgFile.ContentType.Contains("image/")) throw new ContentTypeException("Filein tipi duzgun daxil edilmeyib");
            string path = _webHostEnvironment.WebRootPath + @"\Uploads\Explores\" + explore.ImgFile.FileName;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                explore.ImgFile.CopyTo(stream);
            }
            explore.ImgUrl = explore.ImgFile.FileName;
            _exploreRepostory.Add(explore);
            _exploreRepostory.Commit();
          
        }

        public Explore GetExplore(Func<Explore, bool>? func = null)
        {
          return _exploreRepostory.Get(func);
        }

        public List<Explore> GetAllExplore(Func<Explore, bool>? func = null)
        {
            return _exploreRepostory.GetAll(func);
        }

        public void RemoveExplore(int id)
        {
            var oldexplore=_exploreRepostory.Get(x=> x .Id == id);
            if (oldexplore == null) throw new ArgumentNullException("Explore tapilmadi");
            string oldpath = _webHostEnvironment.WebRootPath + @"\Uploads\Explores\" + oldexplore.ImgUrl;
            FileInfo fileInfo= new FileInfo( oldpath );
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
           _exploreRepostory.Remove(oldexplore); 
            _exploreRepostory.Commit();
        }

        public void UpdateExplore(int id, Explore newexplore)
        {
            var oldexplore= _exploreRepostory.Get(x=> x .Id == id);
            if (oldexplore == null) throw new ArgumentNullException("Explore tapilmadi");
            if (newexplore.ImgFile != null)
            {
                if (newexplore.ImgFile.Length > 2097152) throw new FileSizeException("Filenin olchusu duzgun deyil 2mb artiq olmamalidir!");
                if (!newexplore.ImgFile.ContentType.Contains("image/")) throw new ContentTypeException("Filenin tipi duzgun deyi!");
                string oldpath = _webHostEnvironment.WebRootPath + @"\Uploads\Explores\" + oldexplore.ImgUrl;
                FileInfo fileInfo = new FileInfo(oldpath);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
               
                string path = _webHostEnvironment.WebRootPath + @"\Uploads\Explores\" + newexplore.ImgFile.FileName;
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    newexplore.ImgFile.CopyTo(stream);
                }
                oldexplore.ImgUrl = newexplore.ImgFile.FileName;
            }
            oldexplore.FullName = newexplore.FullName;
            oldexplore.Description = newexplore.Description;
            _exploreRepostory.Commit();
        }
    }
}
