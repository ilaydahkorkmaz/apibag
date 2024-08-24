using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using metDataEntegre;

namespace metDataEntegre.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ServiceClient _serviceClient;

        public IndexModel()
        {
            // Base64 kodlanmış kullanıcı adı ve şifre ile ServiceClient başlatıyoruz
            string username = "cerkezkoy";
            string password = "cerkezkoy";
            string base64Credentials = Base64Helper.EncodeToBase64(username, password);
            _serviceClient = new ServiceClient(base64Credentials);
        }

        public async Task OnGet()
        {
            try
            {
                // İstek gönder ve teslim numarasını al
                string teslimNo = await _serviceClient.SendRequestAsync("12345678901", "ABC123", Guid.NewGuid().ToString());
                ViewData["TeslimNo"] = teslimNo;

                // Lab sonuçlarını al
                string labResults = await _serviceClient.GetLabResultsAsync("12345678901", Guid.NewGuid().ToString(), "base64Credentials"); // Hangi base64 credentials kullanılırsa onu belirtin
                ViewData["LabResults"] = labResults;
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Hata: {ex.Message}";
            }
        }
    }
}
