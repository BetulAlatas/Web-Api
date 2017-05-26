using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data.Methods;
using Data.Models;
using Data.Models.M_Models;

namespace UL.Controllers
{
    public class FavoritesController : ApiController
    {

        [HttpGet]
        [ActionName("AddFavorite")]
        public bool AddFavorite(int productId, int memberId)
        {

            return new FavoritesMethods().AddFavorite(productId, memberId);
        }   //Ürünü favoriye ekleme

        [HttpGet]
        [ActionName("DeleteFavorite")]
        public bool DeleteFavorite(int productId, int memberId)
        {
            return new FavoritesMethods().DeleteFavorite(productId, memberId);
        }   //Ürünü favorilerimden silme

        [HttpGet]
        [ActionName("GetFavorites")]
        public List<M_Products> GetFavorites(int memberId)
        {
            return new FavoritesMethods().GetFavorites(memberId);
        }    //Favori ürünlerimi listeleyen metodun çağrılması

        [HttpGet]
        [ActionName("GetFavoriteId")]
        public List<M_Favorites> GetFavoriteId(int memberId)
        {
            return new FavoritesMethods().GetFavoriteId(memberId);
        }
    }
}
