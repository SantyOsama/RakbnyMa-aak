using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RakbnyMa_aak.Utilities
{
    public class Enums
    {
        public enum UserType
        {
            Driver,
            User,
            Admin
        }

        public enum Gender
        {
            [Display(Name = "ذكر")]
            Male,

            [Display(Name = "أنثى")]
            Female
        }

        public enum CarType
        {
            [Display(Name = "سيدان")]
            سيدان,

            [Display(Name = "دفع رباعي")]
            دفع_رباعي,

            [Display(Name = "فان")]
            فان,

            [Display(Name = "حافلة")]
            حافلة,

            [Display(Name = "تاكسي")]
            تاكسي,

            [Display(Name = "دراجة نارية")]
            دراجة_نارية,

            [Display(Name = "شاحنة بيك أب")]
            شاحنة_بيك_أب,

            [Display(Name = "حافلة صغيرة")]
            حافلة_صغيرة
        }


        public enum CarColor
        {
            [Display(Name = "أسود")]
            أسود,

            [Display(Name = "أبيض")]
            أبيض,

            [Display(Name = "أحمر")]
            أحمر,

            [Display(Name = "أزرق")]
            أزرق,

            [Display(Name = "أخضر")]
            أخضر,

            [Display(Name = "أصفر")]
            أصفر,

            [Display(Name = "فضي")]
            فضي,

            [Display(Name = "رمادي")]
            رمادي,

            [Display(Name = "بني")]
            بني,

            [Display(Name = "برتقالي")]
            برتقالي,

            [Display(Name = "ذهبي")]
            ذهبي,

            [Display(Name = "بنفسجي")]
            بنفسجي,

            [Display(Name = "وردي")]
            وردي,

            [Display(Name = "بيج")]
            بيج,

            [Display(Name = "نبيتي")]
            نبيتي,

            [Display(Name = "كحلي")]
            كحلي
        }



        public enum TripStatus
        {
            [Display(Name = "مجدولة")]
            مجدولة,

            [Display(Name = "قيد التنفيذ")]
            قيد_التنفيذ,

            [Display(Name = "مكتملة")]
            مكتملة,

            [Display(Name = "ملغاة")]
            ملغاة
        }


        public enum RequestStatus
        {
            [Display(Name = "قيد الانتظار")]
            قيد_الانتظار,

            [Display(Name = "مؤكدة")]
            مؤكدة,

            [Display(Name = "ملغاة")]
            ملغاة,

            [Display(Name = "مرفوضة")]
            مرفوضة
        }


        public enum PaymentMethod
        {
            [Display(Name = "نقداً")]
            نقداً,

            [Display(Name = "فودافون كاش")]
            فودافون_كاش,

            [Display(Name = "بطاقة ائتمان")]
            بطاقة_ائتمان,

            [Display(Name = "محفظة إلكترونية")]
            محفظة_إلكترونية
        }


        public enum PaymentStatus
        {
            [Display(Name = "قيد المعالجة")]
            قيد_المعالجة,

            [Display(Name = "مكتمل")]
            مكتمل,

            [Display(Name = "فشل")]
            فشل,

            [Display(Name = "تم الاسترداد")]
            تم_الاسترداد
        }


        public enum PaymentType
        {
            BookingPayment,
            WalletTopUp,
            DriverPayout
        }
        public enum TransactionType
        {
            ائتمان,
            خصم
        }


        public enum TransactionStatus
        {
            قيد_الانتظار,
            مكتمل,
            فشل
        }


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum NotificationType
        {
            تسجيل_السائق,
            تم_حجز_الرحلة,
            تمت_الموافقة_على_الحجز,
            تم_إلغاء_الرحلة,
            الدفع,
            رسالة_محادثة,
            إشعار_مخصص
        }

    }
}
