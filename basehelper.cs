using System; // Temel C# fonksiyonlarını sağlar.

namespace metDataEntegre // Proje için belirlenen ad alanı.
{
    public static class Base64Helper // Base64 kodlaması yapmak için kullanılan yardımcı sınıf.
    {
        // Bu metod, verilen kullanıcı adı ve şifreyi Base64 formatında kodlar.
        public static string EncodeToBase64(string username, string password)
        {
            // Kullanıcı adı ve şifreyi "username:password" formatında birleştirir.
            var credentials = $"{username}:{password}";

            // Birleştirilen kullanıcı adı ve şifreyi önce UTF-8 baytlara çevirir,
            // ardından bu baytları Base64 formatına dönüştürür ve sonucu döndürür.
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(credentials));
        }
    }
}
