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
    public class MemberController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [ActionName("AddMember")]
        public int AddMember([FromBody] string imageBase64, string memberName, string memberSurname,
            string memberUserName, string memberPassword, string memberCity, int memberType, string facebookPhotoURL, string fireBaseToken)
        {
            return  new MembersMethods().AddMember( imageBase64,  memberName,  memberSurname,
             memberUserName, memberPassword,  memberCity , memberType, facebookPhotoURL ,fireBaseToken);
        }  //Üye kaydetme metodunu çağıran metod

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("MemberLogin")]
        public M_Members MemberLogin(string memberUserName, string memberPassword)
        {
            return new MembersMethods().MemberLogin(memberUserName, memberPassword);
        }      // Login kontrolünü yapan metodun çağırılması

        [HttpGet]
        [ActionName("GetMemberImage")]
        public HttpResponseMessage GetMemberImage(int id)
        {
            return new MembersMethods().GetMemberImage(id);           
        }

        [HttpGet]
        [ActionName("GetMemberById")]
        public M_Members GetMemberById(int id)
        {
            return new MembersMethods().GetMemberById(id);
        }  //Üye Profil Bilgileri

        [HttpGet]
        [ActionName("EditMember")]
        public bool EditMember(int memberId,[FromBody] string imageBase64, string memberName, string memberSurname,
            string memberUserName, string memberPassword, string memberCity)
        {
            return new MembersMethods().EditMember(memberId,imageBase64, memberName, memberSurname,
                memberUserName, memberPassword, memberCity);
        }  //Üye Bİlgileri Düzenleme

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [ActionName("DeleteMember")]
        public bool DeleteMember(int memberId)
        {
            return new MembersMethods().DeleteMember(memberId);
        }         //Üyelik silme işlemi

    }
}
