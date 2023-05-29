namespace Chat.Infra.Security.Security
{
    public static class PrivilegeConstants
    {
        public const string AdminRolePrivilage = "1";
        public const string DriverPrivilage = "1,2";

        #region Service
        public const string Makatrip = "2";
        public const string Uaa = "3";
        public const string MakaCommunicate = "4";
        public const string MakaFinance = "5";
        public const string MakaRoute = "6";
        public const string RemoteService = "7";

        #endregion

        #region Controller

        public const string MakaCommunicate_Notify = MakaCommunicate + "01";
        public const string MakaCommunicate_PushNotification = MakaCommunicate + "02";
        public const string MakaCommunicate_User = MakaCommunicate + "03";
        //public const string MakaCommunicate_ = MakaCommunicate + "04";
        //public const string MakaCommunicate_ = MakaCommunicate + "05";
        //public const string MakaCommunicate_ = MakaCommunicate + "06";
        //public const string MakaCommunicate_ = MakaCommunicate + "07";
        //public const string MakaCommunicate_ = MakaCommunicate + "08";
        //public const string MakaCommunicate_ = MakaCommunicate + "09";
        //public const string MakaCommunicate_ = MakaCommunicate + "10";
        //public const string MakaCommunicate_ = MakaCommunicate + "11";
        //public const string MakaCommunicate_ = MakaCommunicate + "12";
        //public const string MakaCommunicate_ = MakaCommunicate + "13";
        //public const string MakaCommunicate_ = MakaCommunicate + "14";
        //public const string MakaCommunicate_ = MakaCommunicate + "15";
        //public const string MakaCommunicate_ = MakaCommunicate + "16";
        //public const string MakaCommunicate_ = MakaCommunicate + "17";
        //public const string MakaCommunicate_ = MakaCommunicate + "18";
        //public const string MakaCommunicate_ = MakaCommunicate + "19";
        //public const string MakaCommunicate_ = MakaCommunicate + "20";


        #endregion

    }
}
