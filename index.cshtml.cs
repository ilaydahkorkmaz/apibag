using Microsoft.AspNetCore.Mvc; // ASP.NET Core MVC özellikleri sağlar.
using Microsoft.AspNetCore.Mvc.RazorPages; // Razor Pages için gerekli olan temel sınıflar.
using System.Threading.Tasks; // Asenkron programlama için gerekli.

namespace metDataEntegre.Pages // Proje için belirlenen ad alanı.
{
    public class IndexModel : PageModel // Razor Page modelini temsil eder.
    {
        private readonly ILogger<IndexModel> _logger; // Hata ve bilgi kaydı için bir logger.
        private readonly ServiceClient _serviceClient; // WCF servisiyle etkileşim kuran sınıfın örneği.

        [BindProperty] // Sayfa ile form verileri arasında veri bağlama sağlar.
        public string TcKimlikNo { get; set; } // Kullanıcıdan alınacak TC Kimlik No
        [BindProperty]
        public string IslemKodu { get; set; } // Kullanıcıdan alınacak işlem kodu
        [BindProperty]
        public string UniqueId { get; set; } // Kullanıcıdan alınacak Unique ID
        [BindProperty]
        public string Username { get; set; } // Kullanıcı adı
        [BindProperty]
        public string Password { get; set; } // Şifre
        public string LabResults { get; set; } // Lab sonuçları - bu sonuçlar sayfada görüntülenecek.

        // Constructor: Logger ve ServiceClient bağımlılıklarını enjekte eder.
        public IndexModel(ILogger<IndexModel> logger, ServiceClient serviceClient)
        {
            _logger = logger; // Logger'ı initialize eder.
            _serviceClient = serviceClient; // ServiceClient'ı initialize eder.
        }

        public void OnGet()
        {
            // Sayfa ilk yüklendiğinde çalışacak kodlar, şu anda boş.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Form doğrulama başarısız olursa, aynı sayfaya geri dön.
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Base64 şifreleme işlemi (kullanıcı adı ve şifreyi şifreler).
                var base64Credentials = Base64Helper.EncodeToBase64(Username, Password);

                // Servis üzerinden lab sonuçlarını al (servis çağrısı asenkron olarak yapılır).
                LabResults = await _serviceClient.GetLabResultsAsync(TcKimlikNo, UniqueId, base64Credentials);

                // Başarılı işlem sonrası sayfayı tekrar yükler.
                return Page();
            }
            catch (Exception ex)
            {
                // Hata olursa, log'a yaz ve kullanıcıya hata mesajı göster.
                _logger.LogError($"Hata: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Bir hata oluştu, lütfen tekrar deneyiniz.");
                return Page();
            }
        }
    }
}
