using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XQ.SDK.Model.Json
{
    public class GroupList
    {
        private List<GroupInfoJson> _joinList;
        private List<GroupInfoJson> _manageList;
        private List<GroupInfoJson> _createList;

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
        ///     加入的群列表
        /// </summary>
        [JsonProperty(PropertyName = "join")]
        public List<GroupInfoJson> JoinList
        {
            get => Ec == 0 && Errcode == 0
                ? _joinList
                : throw new ApplicationException(
                    $"XQApi_GroupList 返回值异常\nErrCode : {Errcode}\nErrMessage{ErrMessage}");
            set => _joinList = value;
        }

        /// <summary>
        ///     管理的群列表
        /// </summary>
        [JsonProperty(PropertyName = "manage")]
        public List<GroupInfoJson> ManageList
        {
            get => Ec == 0 && Errcode == 0
                ? _manageList
                : throw new ApplicationException(
                    $"XQApi_GroupList 返回值异常\nErrCode : {Errcode}\nErrMessage{ErrMessage}");
            set => _manageList = value;
        }

        /// <summary>
        ///     创建的群列表
        /// </summary>
        [JsonProperty(PropertyName = "create")]
        public List<GroupInfoJson> CreateList
        {
            get => Ec == 0 && Errcode == 0
                ? _createList
                : throw new ApplicationException(
                    $"XQApi_GroupList 返回值异常\nErrCode : {Errcode}\nErrMessage{ErrMessage}");
            set => _createList = value;
        }
    }
}