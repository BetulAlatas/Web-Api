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
    public class SalesController : ApiController
    {
        [HttpGet]
        [ActionName("SoldProductList")]
        public List<M_Products> SoldProductList(int memberId)
        {
            return new SalesMethods().SoldProductList(memberId);
        }   //Satılan ürünlerimin listelenmesi

        [HttpGet]
        [ActionName("SellProductList")]
        public List<M_Products> SellProductList(int memberId)
        {
            return new SalesMethods().SellProductList(memberId);
        }   //SAtıştaki ürünlerimin listelenmesi
    }
}
