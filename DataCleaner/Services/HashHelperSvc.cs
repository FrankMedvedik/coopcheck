namespace DataClean.Services
{
    public static class HashHelperSvc
    {
        public static int GetNullSafeHashCode<T>(this T value) where T : class
        {
            if (value is string)
            {
                var v = ((string) ((object) value)).Trim().ToUpper();
                return v == "" ? 1 : v.GetHashCode();
            }
            return value == null ? 1 :  value.GetHashCode();
        }

        public static int GetHashCode(params object[] values)
        {
            int hash = 17;
            foreach (object value in values)
            {
                hash = hash * 23 + value.GetNullSafeHashCode();
            }
            return hash;
        }
    }

}
