using System;
namespace Clouder.Server.Admin
{
	public class AppInfo
    {
        public static AppInfo Instance => new AppInfo();

        public static string SellerSupportEmail => "sellersupport@kardasti.com";
        public string ApiBaseAddress => "http://localhost:7071/api"; //Local
    }
}
