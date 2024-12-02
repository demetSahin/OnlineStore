using System.Security.Claims;
using System.Xml.Linq;

namespace OnlineStore.WebUI.Extensions
{
    //Cookie de tutulan verilerin kontrollerini yapmak içim-n kullanılacak class.
    //nesneye bağımlı olmayacak bu yüzden static yaptım
    public static class ClaimsPrincipalExtensions
    {
        // this -> bu sayede metot sondan çağırılır.
        // User.IsLoggged() tarzında.

        public static bool IsLogged(this ClaimsPrincipal user)
        {
            if (user.Claims.FirstOrDefault(x => x.Type == "id") != null)
                return true;
            else
                return false;
        }

        public static string GetUserFirstName(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == "firstName")?.Value;
        }

        public static string GetUserLastName(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == "lastName")?.Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            if (user.Claims.FirstOrDefault(x => x.Type == "userType")?.Value == "Admin")
                return true;
            else
                return false;
        }
        public static int GetId(this ClaimsPrincipal user)
        {
            return Convert.ToInt32(user.Claims.FirstOrDefault(x => x.Type == "id")?.Value);
        }

    }
}
