using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.Models.M_Models;

namespace Data.Methods
{
    public class FavoritesMethods
    {


        public bool AddFavorite(int productId, int memberId)
        {
            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {

                try
                {
                    context.Database.Connection.Open();

                    Products product = context.Products.FirstOrDefault(x => x.ProductID == productId);
                    product.TotalLike += 1;

                    Favorites newfavorite = new Favorites();

                    newfavorite.MemberID = memberId;
                    newfavorite.ProductID = productId;

                    context.Favorites.Add(newfavorite);
                    context.SaveChanges();
                    return true;

                }
                catch (Exception)
                {

                    return false;
                }
            }
        } //Ürünü Favoriye ekleme metodu

        public bool DeleteFavorite(int productId, int memberId)
        {
            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {

                try
                {
                    context.Database.Connection.Open();

                    Products product = context.Products.FirstOrDefault(x => x.ProductID == productId);
                    product.TotalLike -= 1;

                    Favorites deletefavorite = new Favorites();

                    deletefavorite =
                        context.Favorites.FirstOrDefault(x => x.ProductID == productId && x.MemberID == memberId);

                    context.Favorites.Remove(deletefavorite);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
        } //ürünü favorilerimden silen metod

        public List<M_Products> GetFavorites(int memberId)
        {
            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {

                context.Database.Connection.Open();

                List<Favorites> favorites = context.Favorites.Where(x => x.MemberID == memberId).ToList();

                List<M_Products> productList = new List<M_Products>();

                foreach (var item in favorites)
                {
                    Products product = context.Products.FirstOrDefault(x => x.ProductID == item.ProductID);

                    M_Products product1 = new M_Products()
                    {
                        ProductID = product.ProductID,
                        Name = product.Name,
                        City = product.City,
                        DateAdded = product.DateAdded,
                        ProductNote = product.ProductNote,
                        Photo = product.Photo,
                        Price = product.Price,
                        TotalLike = product.TotalLike,
                        Status = product.Status,
                        MemberID = product.MemberID,
                        CategoryID = product.CategoryID
                    };

                    productList.Add(product1);
                }

                context.Database.Connection.Close();
                return productList;
            }
        } //Favori ürünlerimi listeleyen metod

        public List<M_Favorites> GetFavoriteId(int memberId)
        {
            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {

                context.Database.Connection.Open();

                List<Favorites> favorites = context.Favorites.Where(x => x.MemberID == memberId).ToList();

                List<M_Favorites> favorites1 = new List<M_Favorites>();

                foreach (var item in favorites)
                {
                    M_Favorites list = new M_Favorites()
                    {
                        FavoriteID = item.FavoriteID,
                        MemberID = item.MemberID,
                        ProductID = item.ProductID
                    };

                    favorites1.Add(list);
                }
                context.Database.Connection.Close();
                return favorites1;
            }


        }

    }
}
