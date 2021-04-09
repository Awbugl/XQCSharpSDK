using XQ.SDK.Model.MessageObject;
using XQ.SDK.XQ;

namespace XQ.Enhance
{
    public static class QqHeadPic
    {
        public static ImageMessage Get(XqApi xqApi, string robotqq, string qqid) => ImageMessage.FromUrl(xqApi, robotqq, $"https://q1.qlogo.cn/g?b=qq&nk={qqid}&s=640");
    }
}
