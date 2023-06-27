namespace Insightinator.Web.Util
{
    public static class DebugUtil
    {
        public static bool IsDebugMode
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
    }
}
