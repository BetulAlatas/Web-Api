using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using Data.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Data.Models.M_Models;


namespace Data.Methods
{
    /* Urunlerle ilgili metodların bulundugu class */

    public class ProductMethods
    {
        public List<M_Products> GetProducts(int count) // Gelen istek sayısına göre ürünleri geri donduren metod
        {

            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {
                context.Database.Connection.Open();

                List<Products> productList = context.Products.OrderBy(x => x.ProductID)
                    .Skip(count * 20).Take(20).ToList(); // Gelen istek sayısına göre 20 ürünü getiren linq sorgusu

                List<M_Products> product1List = new List<M_Products>();
                // Veritabanındaki product tablosunda gerekli olmayan alanları kırparak olusturulan M_Product

                foreach (var item in productList)
                {
                    M_Products products1 = new M_Products
                    // Veritabanıdan gelen sorguların M_Product tipine donusturulme  ve listesine ekleme işlemi
                    {
                        ProductID = item.ProductID,
                        Name = item.Name,
                        City = item.City,
                        DateAdded = item.DateAdded,
                        MemberID = item.MemberID,
                        CategoryID = item.CategoryID,
                        Photo = item.Photo,
                        Price = item.Price,
                        Status = item.Status,
                        ProductNote = item.ProductNote,
                        TotalLike = item.TotalLike


                    };

                    product1List.Add(products1);

                }
                context.Database.Connection.Close();
                return product1List;
            }

        }

        public M_Products GetProductById(int id) // Ürun detayı için metod  
        {
            M_Products products1 = new M_Products();
            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {
                context.Database.Connection.Open();
                Products product = context.Products. // Gelen id'ye göre veritabanından ürünü getiren linq sorgusu
                    FirstOrDefault(x => x.ProductID == id);

                if (product != null) //Gelen sonucun M_Product nesnesine donusturulmesi
                {
                    products1.ProductID = product.ProductID;
                    products1.Name = product.Name;
                    products1.City = product.City;
                    products1.DateAdded = product.DateAdded;
                    products1.ProductNote = product.ProductNote;
                    products1.MemberID = product.MemberID;
                    products1.CategoryID = product.CategoryID;
                    products1.Photo = product.Photo;
                    products1.Price = product.Price;
                    products1.TotalLike = product.TotalLike;
                    products1.Status = product.Status;
                }
            }
            return products1;
        }

        public HttpResponseMessage GetProductImage(int id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            string filePath = HostingEnvironment.MapPath("~/Images/Products/Product" + id + ".jpg");
            char[] path = filePath.ToCharArray();
            char[] newPath = new char[path.Length - 3];
            /*
                        for (int i = 0; i < path.Length; i++)
                        {
                            newPath[i] = path[i];
                            if (i != 0 && i + 1 != path.Length)
                            {
                                if (path[i - 1].Equals('\\') && path[i].Equals('U') && path[i+1].Equals('L') && path[i + 2].Equals('\\'))
                                {

                                    for(int j=i+3;j<path.Length;j++)
                                    {

                                        newPath[i] = path[j];
                                        i += 1;
                                    }
                                    i = path.Length;
                                    filePath=new string(newPath);
                                }
                            }

                        }
                        */
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            Image image = Image.FromStream(fileStream);
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            result.Content = new ByteArrayContent(memoryStream.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return result;
        } // Ürünlerin resimleri için metod

        public bool PostProduct([FromBody] string imageBase64, string productName, string productDescription,
            string productPrice, int memberID, int categoryID, string productCity)
        {
            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {

                try
                {
                    context.Database.Connection.Open();

                    Products newproduct = new Products();

                    newproduct.Name = productName;
                    newproduct.Price = productPrice;
                    newproduct.DateAdded = DateTime.Now;
                    newproduct.Status = true;
                    newproduct.ProductNote = productDescription;
                    newproduct.CategoryID = categoryID;
                    newproduct.MemberID = memberID;
                    newproduct.City = productCity;
                    newproduct.TotalLike = 0;

                    var number = context.Products.Max(x => x.Photo);
                    if (number == null)
                    {
                        number = 1;
                    }
                    else number += 1;
                    string imageName = "Product" + number + ".jpg";
                    newproduct.Photo = number;
                    String path = HttpContext.Current.Server.MapPath("~/Images/Products"); //Path
                    string imgPath = Path.Combine(path, imageName);
                    imageBase64 = imageBase64.Replace(' ', '+');
                    byte[] imageBytes = Convert.FromBase64String(imageBase64);
                    File.WriteAllBytes(imgPath, imageBytes);


                    context.Products.Add(newproduct);

                    context.SaveChanges();
                    context.Database.Connection.Close();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }


            }
        }    // Ürün ekleme metodu

        public bool SoldProduct(int productId)
        {
            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {
                try
                {
                    context.Database.Connection.Open();

                    var soldProduct = context.Products.FirstOrDefault(x => x.ProductID == productId);
                    if (soldProduct != null) soldProduct.Status = false;


                    context.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }   //Ürün satış işlemini kaydeden metod

        public List<M_Products> FilterProducts(string productName, int count)
        {
            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {
                context.Database.Connection.Open();
                List<Products> products = context.Products.Where(x => x.Name.Contains(productName)).OrderBy(x => x.ProductID).Skip(count * 20).Take(20).ToList();

                List<M_Products> productList = new List<M_Products>();
                foreach (var item in products)
                {                
                   M_Products product1 = new M_Products()
                    {
                        ProductID = item.ProductID,
                        Name = item.Name,
                        City = item.City,
                        DateAdded = item.DateAdded,
                        ProductNote = item.ProductNote,
                        Photo = item.Photo,
                        Price = item.Price,
                        TotalLike = item.TotalLike,
                        Status = item.Status,
                        MemberID = item.MemberID,
                        CategoryID = item.CategoryID
                    };
                    productList.Add(product1);
                }

                

                context.Database.Connection.Close();

                return productList;
            }
        }   //Ürün Filtreleme
    }
}
