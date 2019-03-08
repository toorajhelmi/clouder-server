using System;
namespace Clouder.Server.Prop
{
    public class AccountActivation
    {
        public bool Activated { get; set; }
        public bool Suspended { get; set; }
        public string ActivationCode { get; set; }
    }
}
