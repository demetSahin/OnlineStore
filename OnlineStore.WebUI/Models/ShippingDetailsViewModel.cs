using System.ComponentModel.DataAnnotations;

namespace OnlineStore.WebUI.Models
{
    public class ShippingDetailsViewModel
    {
        [Required(ErrorMessage = "Ad Soyad alanı boş bırakılamaz.")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Adres İsmi alanı boş bırakılamaz.")]
        public string AddressName { get; set; }

        [Required(ErrorMessage = "Adres alanı boş bırakılamaz.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Şehir alanı boş bırakılamaz.")]
        public string  City { get; set; }
        [Required(ErrorMessage = "Semt alanı boş bırakılamaz.")]
        public string District { get; set; }
        [Required(ErrorMessage = "Posta Kodu alanı boş bırakılamaz.")]
        public string ZipCode { get; set; }
        public int UserId { get; set; }


    }
}
