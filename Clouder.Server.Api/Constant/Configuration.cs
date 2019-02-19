using System;
namespace Clouder.Server.Api.Constant
{
    public static class Configuration
    {
        public static string SendGridApiKey = "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";
        public static string BuyerSupportEmail = "buyersupport@kardasti.me";
        public static string SellerSupportEmail = "sellersupport@kardasti.me";
        public static string GeneralSupportEmail = "sellersupport@kardasti.me";
        public static string ActivationLink = "http://localhost:7071/api/User_Activate?uid={0}&c={1}";
        public static string BuyerLogoUrl = "https://kardastiitemphotosdev.blob.core.windows.net/ref-images/buyer-logo.png";
        public static string SellerLogoUrl = "https://kardastiitemphotosdev.blob.core.windows.net/ref-images/seller-logo.png";
    }
}
