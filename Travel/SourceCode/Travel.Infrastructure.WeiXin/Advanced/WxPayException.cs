namespace Travel.Infrastructure.WeiXin.Advanced
{
    public class WxPayException : System.Exception
    {
        public WxPayException(string msg)
            : base(msg)
        {
        }
    }
}
