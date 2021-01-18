using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XQ.SDK.XQ.Json
{
    public class GroupList
    {
        private List<GroupInfo> _list;

        /// <summary>
        ///     指示是否获取成功
        /// </summary>
        [JsonProperty(PropertyName = "ec")]
        public int Ec { get; set; }

        /// <summary>
        ///     错误码
        /// </summary>
        [JsonProperty(PropertyName = "errcode")]
        public int Errcode { get; set; }

        /// <summary>
        ///     错误Msg
        /// </summary>
        [JsonProperty(PropertyName = "em")]
        public string ErrMessage { get; set; }

        /// <summary>
        ///     群列表
        /// </summary>
        [JsonProperty(PropertyName = "join")]
        public List<GroupInfo> List
        {
            get => Ec == 0 && Errcode == 0
                ? _list
                : throw new ApplicationException(
                    $"XQApi_GroupList 返回值异常\nErrCode : {Errcode}\nErrMessage{ErrMessage}");
            set => _list = value;
        }
    }
}