using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Data.Methods;
using Data.Models;
using System.Threading.Tasks;
using System.Net;
using System;
using System.IO;

namespace UL.Controllers
{

    /* Urunler ilgili işlemlerin yapıldığı controller */

    public class ProductController : ApiController
    {
        [HttpGet] // Metodun tipi
        [ActionName("GetProducts")] // Url'de metodun hangi isimle çağırılacağının belirlendiği attribute
        public List<M_Products> GetProducts(int count)
        // ilgili aralıktaki ürünlerin döndürümesi için int tipinden parametre
        {
            return new ProductMethods().GetProducts(count);
            // Data katmanında bulunan urunleri donduren metodun çağırılması
        }

        [HttpGet]
        [ActionName("GetProductById")]
        public M_Products GetProductById(int id)
        {
            return new ProductMethods().GetProductById(id);
            // Data katmanında bulunan ürün detayı için yazılmıs metodun çağırılması
        }

        [HttpGet]
        [ActionName("GetProductImage")]
        public HttpResponseMessage GetProductImage(int id)
        {
            return new ProductMethods().GetProductImage(id);
        }

        [HttpPost]
        [ActionName("UserImage")]
        [AllowAnonymous]
        public HttpResponseMessage UserImage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {



                            var filePath =
                                HttpContext.Current.Server.MapPath("~/Images/" + postedFile.FileName + extension);

                            postedFile.SaveAs(filePath);

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1);
                    ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        } //İmage kaydeden metod(sunucuda 502 hatası)


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [ActionName("AddProduct")]
        public bool AddProduct([FromBody] string imageBase64, string productName, string productDescription, string productPrice, int memberID, int categoryID, string productCity)
        {
            return new ProductMethods().PostProduct(imageBase64, productName, productDescription, productPrice, memberID, categoryID, productCity);
        }        //Ürün ekleme metodu

        #region
        [HttpGet]
        [ActionName("SoldProduct")]
        public bool SoldProduct(int productId)
        {
            return new ProductMethods().SoldProduct(productId);
        }   //Ürün satışı
        #endregion
        [HttpGet]
        [ActionName("FilterProducts")]
        public List<M_Products> FilterProducts(string productName, int count)
        {
            return new ProductMethods().FilterProducts(productName, count);
        }    //Ürün Fİltreleme

    }


}

