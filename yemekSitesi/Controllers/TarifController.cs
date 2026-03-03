using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Logging;
using yemekSitesi.Data;
using yemekSitesi.Models;


namespace yemekSitesi.Controllers
{
    public class TarifController : Controller
    {
        public static List<Tarif> tarifler= new();
        private readonly AppDbContext _appDbContext;
        public TarifController(AppDbContext appDbContext)
        {
            _appDbContext=appDbContext;
        }

        public IActionResult Index()
        {
            var tarifler=_appDbContext.Tarifler.ToList();
            return View(tarifler);
        }













        public IActionResult Delete(int id)
        {
            var tarif = tarifler.FirstOrDefault(x=>x.Id==id);
            if (tarif != null)
            {
                tarifler.Remove(tarif);
                TempData["mesaj"]="Yemek Silindi";
            }
            else
            {
                TempData["mesaj"]="Yemek Silinemedi";
            }

            return RedirectToAction("Index","Tarif");
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tarif tarif, IFormFile Foto)
        {
            if (ModelState.IsValid)
            {
                if(Foto != null)
                {
                    //Fotoğrafın uzantısını almak
                    var uzanti=Path.GetExtension(Foto.FileName);
                    var yendiAd=Guid.NewGuid()+"."+ uzanti;
                    var yol=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\img",yendiAd);

                    if(Foto.ContentType=="image/png"|| Foto.ContentType=="image/jpeg" ||Foto.ContentType=="image/jpg")
                    {
                        using(var stream=new FileStream(yol,FileMode.Create))
                        {
                            try
                            {
                                Foto.CopyTo(stream);
                                tarif.Foto=yendiAd;
                                _appDbContext.Tarifler.Add(tarif);// fotoğrafı ekledi
                                _appDbContext.SaveChanges(); //veri tabanına kaydetti
                                return RedirectToAction("Index","Tarif");
                            }
                            catch (Exception ex)
                            {
                                ViewBag.Hata="Dosya Yükleme Hatası :"+ex.Message;
                            }
                        }
                    } else { ViewBag.Hata="jpg ya da png Yükle";}

                }

               
            }else {ViewBag.Hata="Dosya Yükle";}
            return View();
            
            
        }

        public IActionResult Edit(int id)
        {
            if(id != null)
            {
                var tarif=tarifler.FirstOrDefault(x=>x.Id==id);
                if(tarif != null)
                {
                    return View(tarif);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Tarif tarif,IFormFile Fotom)
        {
            if(Fotom == null)
            {
                var tarifim=tarifler.FirstOrDefault(x=>x.Id==tarif.Id);
                tarifler.Remove(tarifim);
                tarifler.Add(tarif);
                return RedirectToAction("Index","Tarif");
            }
            else
            {
                
                //Fotoğrafın uzantısını almak
                var uzanti=Path.GetExtension(Fotom.FileName);
                var yendiAd=Guid.NewGuid()+ uzanti;
                var yol=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\img",yendiAd);

                if(Fotom.ContentType=="image/png"|| Fotom.ContentType=="image/jpeg" ||Fotom.ContentType=="image/jpg")
                {
                    using(var stream=new FileStream(yol,FileMode.Create))
                    {
                        try
                        {
                            Fotom.CopyTo(stream);
                            var tarifim=tarifler.FirstOrDefault(x=>x.Id==tarif.Id);
                            tarifler.Remove(tarifim);
                            tarif.Foto=yendiAd;
                            tarifler.Add(tarif);
                            return RedirectToAction("Index","Tarif");
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Hata="Dosya Yükleme Hatası :"+ex.Message;
                        }
                    }
                } else { ViewBag.Hata="jpg ya da png Yükle";}
            }
            return View(tarif);
        }

    }
}