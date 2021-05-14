using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperSocket.messageTool
{
    public class DiirectoryMessage
    {
        /// <summary>
        /// 消息
        /// </summary>
        public static Dictionary<string, List<MessageModel>> keyValuePairs = new Dictionary<string, List<MessageModel>>();

        public static MessageModel GetMessage(string key, string value) 
        {
            if (key == null)
                return null;
            //var data = keyValuePairs.FirstOrDefault(x => x.Key == key);
            var data = keyValuePairs[key];
            if (data != null)
            {
                return data.FirstOrDefault(x => x.FID == value);
            }
            return null;
        }
        
        /// <summary>
        /// 确认消息
        /// </summary>
        /// <returns></returns>
        public static bool affirmMessage(string key,string value) 
        {
            if (key == null)
                return false;
            var data = keyValuePairs[key];
            if (data != null)
            {
                var model = data.FirstOrDefault(x => x.FID == value);
                if (model != null) 
                {
                    model.state = 1;
                    data.Remove(model);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 离线消息
        /// </summary>
        /// <returns></returns>
        public static bool LxMessage(string key , MessageModel messageModel) 
        {
            if (key == "")
                return false;
            var data = keyValuePairs[key];
            if (data == null)
            {
                keyValuePairs.Add(key, new List<MessageModel>() { messageModel });
            }
            else {
                data.Add(messageModel);
            }
            return true;
        }
    }
}