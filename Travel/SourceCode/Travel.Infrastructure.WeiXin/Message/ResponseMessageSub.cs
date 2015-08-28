namespace Travel.Infrastructure.WeiXin.Message
{
    /// <summary>
    /// （响应）文件消息
    /// </summary>
    public class ResponseMessageSub : ResponseMessage
    {
        public ResponseMessageSub()
        {
            MsgType = MessageType.Text;
        }
    }

    /// <summary>
    /// （响应）音乐消息
    /// </summary>
    public class RepMusicMessage : ResponseMessage
    {
        public RepMusicMessage()
        {
            MsgType = MessageType.Music;
        }
    }

    /// <summary>
    /// （响应）图文消息
    /// </summary>
    public class RepNewsMessage : ResponseMessage
    {
        public RepNewsMessage()
        {
            MsgType = MessageType.News;
        }
    }
}
