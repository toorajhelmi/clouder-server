using System;
namespace Clouder.Server.Api.Constant
{
    public class Localization
    {
        public static Localization Instance => new Localization();

        //Categories
        public string AllCategories => "همه گروهها";

        //Attributes
        public string PrimaryColor => "رنگ اصلی";
        public string SecondaryColor => "رنگ فرعی";
        public string Diameter => "قطر";
        public string Fabric => "نوع پارچه";
        public string Height => "ارتفاع";
        public string Length => "طول";
        public string Width => "عرض";
        public string Material => "نوع ماده";
        public string Pattern => "طرح";
        public string Style => "استایل";
        public string Weight => "وزن";
        public string Beige => "بژ";
        public string Black => "سیاه";
        public string Blue => "آبی";
        public string Bronze => "برنزی";
        public string Brown => "قهوه ای";
        public string Clear => "شفاف";
        public string Copper => "مسی";
        public string Gold => "طلایی";
        public string Gray => "خاکستری";
        public string Green => "سبز";
        public string Orange => "نارنجی";
        public string Pink => "صورتی";
        public string Purple => "سرخابی";
        public string Rainbow => "رنگین کمونی";
        public string Red => "قرمز";
        public string Silver => "نقره ای";
        public string White => "سفید";
        public string Yellow => "زرد";

        //Email
        public string Clouder => "کاردستی";
        public string OrderPlaced => "سفارش دریافت شد";
        public string OrderShipped => "سفارش پست شد";
        public string OrderCancelled => "سفارش لغو شد";
        public string OrderConfirmed => "سفارش دریافت شد";
        public string OrderRejected => "دریافت سفارش رد شد";
        public string OrderRejectionRejected = "با رد سفارش موافقت شد";
        public string OrderRejectionAccepted = "با رد سفارش مخالفت شد";
        public string None => "وجود ندارد";
        public string NotReceived => "به دستم نرسید";
        public string Damaged => "آسیب دیده به دستم رسید";
        public string ActivateAcount => "کاردستی، فعال کردن حساب";
        public string ShippingReminder => "یادآوری پست";
        public string ResolutionReminder => "یادآوری رفع اشکال";
    }
}
