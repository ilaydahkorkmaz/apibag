using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using metDataEntegre.Models;


namespace metDataEntegre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabResultsController : ControllerBase
    {
        private readonly ServiceClient _serviceClient;

        public LabResultsController()
        {
            _serviceClient = new ServiceClient(); // ServiceClient sınıfını kullanıyoruz.
        }

        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddPatient([FromBody] PatientRequestModel patientRequest)
        {
            try
            {
                // Kullanıcı adı ve şifreyi Base64 formatına çeviriyoruz
                var base64Credentials = Base64Helper.EncodeToBase64(patientRequest.Username, patientRequest.Password);

                // Servise hasta kaydı eklemek için istekte bulunuyoruz
                var response = await _serviceClient.IstekYapAsync(patientRequest.TcKimlikNo, patientRequest.IslemKodu, patientRequest.UniqueId, base64Credentials);

                if (response == "0") // Başarıyla kayıt yapıldı
                {
                    return Ok("Hasta kaydı başarılı.");
                }
                else
                {
                    return BadRequest($"Kayıt Hatası: {response}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Hata: {ex.Message}");
            }
        }
    }
}
