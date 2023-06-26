using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class Messages
    {
        public static string DataAlreadyExist = "Böyle bir kayıt zaten mevcuttur !";

        public static string AddMethodSuccessfully = "Ekleme işlemi başarılıdır.";

        public static string UpdateMethodSuccessfully = "Güncelleme işlemi başarılıdır.";

        public static string DeleteMethodSuccessfully = "Silme işlemi başarılıdır.";

        public static string NotFoundId = "Bu veri silinmiş veya değiştirilmiş olabilir ! Kontrol ediniz";

        public static string SuccessGetById = "Veri başarıyla getirildi.";

        public static string SuccessListed= "Veriler başarıyla listelendi..";

        public static string GeneralError= "Bir şeyler ters gitti ! Tekrar deneyiniz";

        public static string NotEnoughStock= "Stok adediniz yoktur!";

        public static string NotSavedProductStock= "Ürününüze ait stok kaydı yoktur !";
    }
}
