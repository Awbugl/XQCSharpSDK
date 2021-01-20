using System;
using System.Runtime.InteropServices;

namespace XQ.SDK.XQ
{
    public static class XqDll
    {
        private const string DllName = "xqapi.dll";
        
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetFriendList")]
        public static extern IntPtr GetFriendList(byte[] autoid, string qq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupList")]
        public static extern IntPtr GetGroupList(byte[] autoid, string qq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetOnLineList")]
        public static extern IntPtr GetOnLineList(byte[] autoid);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_Getbotisonline")]
        public static extern bool GetBotIsOnline(byte[] autoid, string qq);
        
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupCard")]
        public static extern IntPtr GetGroupCard(byte[] autoid, string robotQq, string group, string qq);
        
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupAdmin")]
        public static extern IntPtr GetGroupAdmin(byte[] autoid, string robotQq, string group);
        
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_IsShutUp")]
        public static extern bool IsShutUp(byte[] autoid, string robotQq, string group, string qq);
       
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_IfFriend")]
        public static extern bool IfFriend(byte[] autoid, string robotQq, string qq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupPsKey")]
        public static extern IntPtr GetGroupPsKey(byte[] autoid, string robotQq);
        
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetZonePsKey")]
        public static extern IntPtr GetZonePsKey(byte[] autoid, string robotQq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetCookies")]
        public static extern IntPtr GetCookies(byte[] autoid, string robotQq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetObjVote")]
        public static extern int GetObjVote(byte[] autoid, string robotQq, string qq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_IsEnable")]
        public static extern bool IsEnable(byte[] autoid);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetQQList")]
        public static extern IntPtr GetQQList(byte[] autoid);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetNick")]
        public static extern IntPtr GetNick(byte[] autoid, string robotQq, string qq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetFriendsRemark")]
        public static extern IntPtr GetFriendsRemark(byte[] autoid, string robotQq, string qq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetClientkey")]
        public static extern IntPtr GetClientkey(byte[] autoid, string robotQq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetLongClientkey")]
        public static extern IntPtr GetLongClientkey(byte[] autoid, string robotQq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetClassRoomPsKey")]
        public static extern IntPtr GetClassRoomPsKey(byte[] autoid, string robotQq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetRepPsKey")]
        public static extern IntPtr GetRepPsKey(byte[] autoid, string robotQq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetTenPayPsKey")]
        public static extern IntPtr GetTenPayPsKey(byte[] autoid, string robotQq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetBkn")]
        public static extern IntPtr GetBkn(byte[] autoid, string robotQq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupList_B")]
        public static extern IntPtr GetGroupList_B(byte[] autoid, string robotQq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetFriendList_B")]
        public static extern IntPtr GetFriendList_B(byte[] autoid, string robotQq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupName")]
        public static extern IntPtr GetGroupName(byte[] autoid, string robotQq, string group);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupMemberNum")]
        public static extern IntPtr GetGroupMemberNum(byte[] autoid, string robotQq, string group);
        
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupMemberList_B")]
        public static extern IntPtr GetGroupMemberList_B(byte[] autoid, string robotQq, string group);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupMemberList_C")]
        public static extern IntPtr GetGroupMemberList_C(byte[] autoid, string robotQq, string group);
        
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetRInf")]
        public static extern IntPtr GetRInf(byte[] autoid, string robotQq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetAnon")]
        public static extern bool GetAnon(byte[] autoid, string robotQq, string group);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetPicLink")]
        public static extern IntPtr GetPicLink(byte[] autoid, string robotQq, int imageType, string group, string imageGuid);
        
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetAge")]
        public static extern int GetAge(byte[] autoid, string robotQq, string qq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGender")]
        public static extern int GetGender(byte[] autoid, string robotQq, string qq);
        
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_UpLoadPic")]
        public static extern IntPtr UpLoadPic(byte[] autoid, string robotQq, int messageType, string groupOrQq, byte[] message);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_ShutUP")]
        public static extern void ShutUP(byte[] autoid, string robotQq, string group, string qq, int time);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SetGroupCard")]
        public static extern bool SetGroupCard(byte[] autoid, string robotQq, string group, string qq, string card);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_KickGroupMBR")]
        public static extern void KickGroupMBR(byte[] autoid, string robotQq, string group, string qq, bool allow);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SetRInf")]
        public static extern void SetRInf(byte[] autoid, string robotQq, string onLineType, string message);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_Uninstall")]
        public static extern bool Uninstall(byte[] autoid);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_Reload")]
        public static extern bool Reload(byte[] autoid);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_OutPutLog")]
        public static extern void OutPutLog(byte[] autoid, string message);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_OcrPic")]
        public static extern IntPtr OcrPic(byte[] autoid, string robotQq, byte[] imageMessage);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_JoinGroup")]
        public static extern void JoinGroup(byte[] autoid, string robotQq, string group, string message);
        
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_HandleFriendEvent")]
        public static extern void HandleFriendEvent(byte[] autoid, string robotQq, string qq, int messageType, string message);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_HandleGroupEvent")]
        public static extern void HandleGroupEvent(byte[] autoid, string robotQq, int requestType, string qq, string group, string seq, int messageType, string message);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_LoginQQ")]
        public static extern void LoginQQ(byte[] autoid, string robotQq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_OffLineQQ")]
        public static extern void OffLineQQ(byte[] autoid, string robotQq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_DelFriend")]
        public static extern bool DelFriend(byte[] autoid, string robotQq, string qq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SetFriendsRemark")]
        public static extern void SetFriendsRemark(byte[] autoid, string robotQq, string qq, string message);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_InviteGroup")]
        public static extern void InviteGroup(byte[] autoid, string robotQq, string group, string qq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_InviteGroupMember")]
        public static extern bool InviteGroupMember(byte[] autoid, string robotQq, string group, string groupY, string qq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_CreateDisGroup")]
        public static extern IntPtr CreateDisGroup(byte[] autoid, string robotQq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_QuitGroup")]
        public static extern void QuitGroup(byte[] autoid, string robotQq, string group);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SetShieldedGroup")]
        public static extern void SetShieldedGroup(byte[] autoid, string robotQq, string group, bool messageType);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_Setcation")]
        public static extern void Setcation(byte[] autoid, string robotQq, int messageType);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_Setcation_problem_A")]
        public static extern void Setcation_problem_A(byte[] autoid, string robotQq, string problem, string answer);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_Setcation_problem_B")]
        public static extern void Setcation_problem_B(byte[] autoid, string robotQq, string problem1, string problem2, string problem3);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_AddFriend")]
        public static extern bool AddFriend(byte[] autoid, string robotQq, string qq, string message, int xxlay);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SendJSON")]
        public static extern void SendJSON(byte[] autoid, string robotQq, int sendType, int messageType, string group, string qq, string jsonMessage);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SendXML")]
        public static extern void SendXML(byte[] autoid, string robotQq, int sendType, int messageType, string group, string qq, string xmlMessage);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_UpLoadVoice")]
        public static extern IntPtr UpLoadVoice(byte[] autoid, string robotQq, int sendType, string group, byte[] message);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetVoiLink")]
        public static extern IntPtr GetVoiLink(byte[] autoid, string robotQq, string message);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SetAnon")]
        public static extern bool SetAnon(byte[] autoid, string robotQq, string group, bool kg);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SetHeadPic")]
        public static extern bool SetHeadPic(byte[] autoid, string robotQq, byte[] message);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_VoiToText")]
        public static extern IntPtr VoiToText(byte[] autoid, string robotQq, string ckdx, int cklx, string yyGuid);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_ShakeWindow")]
        public static extern bool ShakeWindow(byte[] autoid, string robotQq, string qq);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SendMsgEX_V2")]
        public static extern IntPtr SendMsgEX_V2(byte[] autoid, string robotQq, int messageType, string group,
            string qq, string message, int bubbleId, bool anonymous, string jsonMessage);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_WithdrawMsgEX")]
        public static extern IntPtr WithdrawMsgEX(byte[] autoid, string robotQq, int withdrawType, string group,
            string qq, string messageNumber, string messageId, string messageTime);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_DbgName")]
        public static extern void DbgName(byte[] autoid, string message);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_Mark")]
        public static extern void Mark(byte[] autoid, string message);
    }
}