using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using yemekSitesi.Models;


namespace yemekSitesi.Controllers
{
    public class TarifController : Controller
    {
        public static List<Tarif> tarifler= new();
        public TarifController()
        {
            if(!tarifler.Any())
            {
                tarifler.Add(new Tarif {Id=1, Ad="Domates Çorbası", Malzemeler="Domates", Yonerge="Domatesleri doğra..", Foto="yemek1.jpg"});
                tarifler.Add(new Tarif {Id=2, Ad="Tarhana Çorbası", Malzemeler="Tarhana", Yonerge="Tarhanayı suyla beraber pişir", Foto="yemek2.jpg"});
                tarifler.Add(new Tarif {Id=3, Ad="Yayla Çorbası", Malzemeler="Pirinçle, Su", Yonerge="Pirinci suyla beraber pişir", Foto="yemek3.jpg"});
                tarifler.Add(new Tarif {Id=4, Ad="Şehriye Çorbası", Malzemeler="Şehriye, Su", Yonerge="Şehriyeyi suyla beraber pişir", Foto="yemek4.jpg"});
                tarifler.Add(new Tarif {Id=5, Ad="Kelle Paça Çorbası", Malzemeler="kelle eti, paça eti, Su", Yonerge="kelle ve paça etleri kaynatılır", Foto="yemek5.jpg"});
            }
        }

        public IActionResult Index()
        {
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
                            var id=tarifler.Count()+1;
                            tarif.Id=id;
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