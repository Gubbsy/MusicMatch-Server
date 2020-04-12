namespace SQLServer.Utils
{
    public static class ValidatorService
    {
        public static bool CheckIsZero(int i)
        {
            return i == 0;
        }

        public static bool CheckIsZero(double i)
        {
            return i == 0;
        }

        public static string CheckIsEmpty(string s)
        {
            return s == "" || s == null ? null : s;
        }

        public static string CheckRoleExists(string role)
        {
            return role != AccountRoles.Artist && role != AccountRoles.EventsManager ? null : role;
        }
    }
}
