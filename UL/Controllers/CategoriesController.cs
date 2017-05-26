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
    public class CategoriesController : ApiController
    {
        [HttpGet]
        [ActionName("GetListOfCategories")]
        public List<M_Categories> GetListOfCategories()
        {
            return new CategoriesMethods().GetListOfCategories();
        }   //Kategorilerin Listelenmesi


        [HttpGet]
        [ActionName("SelectedCategory")]
        public List<M_Products> SelectedCategory(int categoryId, int count)
        {
            return new CategoriesMethods().SelectedCategory(categoryId,count);
        }    //Kategoriye göre ürün listeleme

    }
}
