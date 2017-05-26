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
    public class CategoriesMethods
    {

        public List<M_Categories> GetListOfCategories()
        {
            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {

                context.Database.Connection.Open();

                List<Categories> categories = context.Categories.ToList();

                List<M_Categories> categories1 = new List<M_Categories>();

                foreach (var item in categories)
                {
                    M_Categories addcategories = new M_Categories()
                    {
                        CategoryID = item.CategoryID,
                        CategoryName = item.CategoryName
                    };

                    categories1.Add(addcategories);
                }
                context.Database.Connection.Close();
                return categories1;
            }

        }   //Kategorilerin Listelenmesi


        public List<M_Products> SelectedCategory(int categoryId, int count)
        {
            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {
                context.Database.Connection.Open();
                List<Products> product = context.Products.Where(x => x.CategoryID == categoryId).OrderBy(x => x.ProductID).Skip(count * 20).Take(20).ToList();

                List<M_Products> productList = new List<M_Products>();

                foreach (var item in product)
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
        }    //KAtegoriye göre ürün filtreleme metodu
    }
}
