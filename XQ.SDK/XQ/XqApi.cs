namespace XQ.SDK.XQ
{
    public class XqApi
    {
        public FrameApi FrameApi;
        public PsKeyApi PsKeyApi;
        public TencentApi TencentApi;

        public XqApi(byte[] authId)
        {
            TencentApi = new TencentApi(authId);
            FrameApi = new FrameApi(authId);
            PsKeyApi = new PsKeyApi(authId);
        }
    }
}