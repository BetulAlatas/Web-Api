using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.Models.M_Models;

namespace Data.Methods
{
    public class SalesMethods
    {
        public List<M_Products> SoldProductList(int memberId)
        {
            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {

                context.Database.Connection.Open();

                List<Products> product = context.Products.Where(x => x.MemberID == memberId && x.Status == false).ToList();

                List<M_Products> soldProductList = new List<M_Products>();

                foreach (var item in product)
                {
                    M_Products soldProduct = new M_Products()
                    {
                        ProductID = item.ProductID,
                        Name = item.Name,
                        Price = item.Price,
                        DateAdded = item.DateAdded,
                        Status = item.Status,
                        Photo = item.Photo,
                        City = item.City,
                        ProductNote = item.ProductNote,
                        MemberID = item.MemberID,
                        CategoryID = item.CategoryID,
                        TotalLike = item.TotalLike
                    };
                    soldProductList.Add(soldProduct);
                }
                context.Database.Connection.Close();
                return soldProductList;
            }
        }    //Satılan ürünlerimin listelendiği metod

        public List<M_Products> SellProductList(int memberId)
        {
            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {

                context.Database.Connection.Open();

                List<Products> product = context.Products.Where(x => x.MemberID == memberId && x.Status== true).ToList();

                List<M_Products> sellProductList = new List<M_Products>();

                foreach (var item in product)
                {
                    M_Products sellProduct = new M_Products()
                    {
                        ProductID = item.ProductID,
                        Name = item.Name,
                        Price = item.Price,
                        DateAdded = item.DateAdded,
                        Status = item.Status,
                        Photo = item.Photo,
                        City = item.City,
                        ProductNote = item.ProductNote,
                        MemberID = item.MemberID,
                        CategoryID = item.CategoryID,
                        TotalLike = item.TotalLike
                    };
                    sellProductList.Add(sellProduct);
                }
                context.Database.Connection.Close();
                return sellProductList;
            }

        }    //Satışta olan ürünlerimin listelendiği meotd
    }
}
