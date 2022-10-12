using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductNameInvalid = "Ürün adı geçersiz.";
        public static string MaintanceTime = "Sistem bakımda.";
        public static string ProductDeleted = "Ürün silindi";
        public static string ProductsList = "Ürünler Listelendi";
        public static string ProductsDetailList = "Ürünler Listelendi";

        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir.";

        public static string UnableToAddProductWithSameName = "Aynı ada sahip ürün eklenemez!";

        public static string CategoryLimitExceeded = "Kategori sayısı mevcut limitin üstünde.";
    }
}
