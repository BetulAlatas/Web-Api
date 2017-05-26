using Data.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using Data.Models.M_Models;

namespace Data.Methods
{
    public class MembersMethods
    {
        private static int FACEBOOKLOGIN = 1;
        private static int APPLOGIN = 2;

        public int AddMember([FromBody] string imageBase64, string memberName, string memberSurname,
            string memberUserName, string memberPassword, string memberCity, int memberType, string facebookPhotoURL, string fireBaseToken)
        {
            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {
                try
                {
                    context.Database.Connection.Open();

                    Members newmember = new Members();
                    newmember.Name = memberName;
                    newmember.Surname = memberSurname;
                    newmember.UserName = memberUserName;
                    newmember.Password = memberPassword;
                    newmember.City = memberCity;
                    newmember.MemberStatus = true;
                    newmember.RecordDate = DateTime.Now;
                    newmember.PointScore = 0;
                    newmember.FireBaseToken = fireBaseToken;
                    if (memberType == FACEBOOKLOGIN)
                    {
                        newmember.MemberType = FACEBOOKLOGIN;
                        newmember.FacebookPhotoURL = facebookPhotoURL;
                    }
                    else if (memberType == APPLOGIN && imageBase64 != null && imageBase64 != "")
                    {
                        newmember.MemberType = APPLOGIN;
                        var number = context.Members.Max(x => x.Photo);
                        if (number == null)
                        {
                            number = 1;
                        }
                        else number += 1;
                        string imageName = "Member" + number + ".jpg";
                        newmember.Photo = number;
                        String path = HttpContext.Current.Server.MapPath("~/Images/Members"); //Path
                        string imgPath = Path.Combine(path, imageName);
                        imageBase64 = imageBase64.Replace(' ', '+');
                        byte[] imageBytes = Convert.FromBase64String(imageBase64);
                        File.WriteAllBytes(imgPath, imageBytes);
                    }

                    context.Members.Add(newmember);
                    context.SaveChanges();
                    return context.Members.Max(x => x.MemberID);
                }
                catch (Exception)
                {

                    return 0;
                }
            }


        } //Üye ekleme metodu

        public M_Members MemberLogin(string memberUserName, string memberPassword)
        {

            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {

                try
                {
                    context.Database.Connection.Open();

                    var result =
                        context.Members.FirstOrDefault(x => x.UserName == memberUserName && x.Password == memberPassword);
                    if (result != null)

                    {
                        M_Members member = new M_Members
                        {
                            MemberID = result.MemberID,
                            MemberType = result.MemberType,
                            FacebookPhotoURL = result.FacebookPhotoURL

                        };

                        return member;
                    }
                    return null;


                }
                catch (Exception)
                {

                    return null;
                }
            }
        } //Login işlemi

        public HttpResponseMessage GetMemberImage(int id)
        {

            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                string filePath = HostingEnvironment.MapPath("~/Images/Members/Member" + id + ".jpg");
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
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public M_Members GetMemberById(int id)
        {
            M_Members member = new M_Members();

            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {
                context.Database.Connection.Open();

                Members memberid = context.Members.FirstOrDefault(x => x.MemberID == id);

                if (memberid != null)
                {
                    member.MemberID = memberid.MemberID;
                    member.Name = memberid.Name;
                    member.Surname = memberid.Surname;
                    member.UserName = memberid.UserName;
                    // member.Password = memberid.Password;
                    member.RecordDate = memberid.RecordDate;
                    member.City = memberid.City;
                    member.PointScore = memberid.PointScore;
                    member.Photo = memberid.Photo;
                    member.MemberType = memberid.MemberType;
                    member.FacebookPhotoURL = memberid.FacebookPhotoURL;
                    member.MemberStatus = memberid.MemberStatus;
                    member.FireBaseToken = memberid.FireBaseToken;


                }
            }
            return member;



        } //Üye Bilgilerini veritanabından çeken metod

        public bool EditMember(int memberId, [FromBody] string imageBase64, string memberName, string memberSurname,
            string memberUserName, string memberPassword, string memberCity)
        {

            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {
                context.Database.Connection.Open();

                try
                {
                    Members editmember = context.Members.FirstOrDefault(x => x.MemberID == memberId);

                    editmember.Name = memberName;
                    editmember.Surname = memberSurname;
                    editmember.UserName = memberUserName;
                    editmember.Password = memberPassword;
                    editmember.City = memberCity;

                    if (imageBase64 != " " && imageBase64 != null)
                    {
                        var number = editmember.Photo;
                        string imageName = "Member" + number + ".jpg";
                        String path = HttpContext.Current.Server.MapPath("~/Images/Members");
                        string imgPath = Path.Combine(path, imageName);
                        imageBase64 = imageBase64.Replace(' ', '+');
                        byte[] imageBytes = Convert.FromBase64String(imageBase64);
                        File.WriteAllBytes(imgPath, imageBytes);
                    }
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
        } //Üye bilgilerini güncelleme metodu

        public bool DeleteMember(int memberId)
        {
            using (ShoppingProjectEntities1 context = new ShoppingProjectEntities1())
            {

                try
                {
                    context.Database.Connection.Open();


                    Members deletemember = context.Members.FirstOrDefault(x => x.MemberID == memberId);

                    deletemember.MemberStatus = false;

                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }   //Üyelik silme işlemini gerçekleştiren metod
    }
}
