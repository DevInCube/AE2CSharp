
namespace java.lang
{
    public class String : Object
    {
        private string value;

        public String(string d) {
            this.value = d;
        }

        public String(String d)
        {
            this.value = d.ToString();
        }

        public String(byte[] charBytes)
        {
            char[] chars = new char[charBytes.Length];
            for (int i = 0; i < charBytes.Length; i++)
                chars[i] = (char)charBytes[i];

            this.value = new System.String(chars);
        }

        public String trim()
        {
            return value.Trim();
        }

        public bool startsWith(String str)
        {
            return value.StartsWith(str.ToString());
        }

        public static implicit operator String(string d)
        {
            return new String(d);
        }

        public static implicit operator string(String d)
        {
            return d.value;
        }

        public static String operator +(String s1, String s2)
        {
            string ss1 = (s1 == null) ? "" : s1.ToString();
            string ss2 = (s2 == null) ? "" : s2.ToString();
            return ss1 + ss2;
        }
        public static String operator +(String s1, object s2)
        {
            string ss1 = (s1 == null) ? "" : s1.ToString();
            string ss2 = (s2 == null) ? "" : s2.ToString();
            return ss1 + ss2;
        }

        public override string ToString()
        {
            return value;
        }

        public int indexOf(String toReplace)
        {
            return value.IndexOf(toReplace.ToString());
        }

        public int indexOf(char p, int charPos)
        {
            return value.IndexOf(p, charPos);
        }

        public int charAt(int it)
        {
            return value[it];
        }

        public String subString(int startIndex, int endIndex)
        {
            int length = endIndex - startIndex;
            return value.Substring(startIndex, length);
        }

        public String subString(int p)
        {
            return value.Substring(p);
        }

        public override bool Equals(object obj)
        {
            String str = obj as String;
            return this.value.Equals(str.value);
        }

        private String[] toArr(string[] strs)
        {
            String[] res = new String[strs.Length];
            for (int i = 0; i < strs.Length; i++)
                res[i] = strs[i];
            return res;
        }

        public String[] split(string p)
        {
            string[] strs = this.value.Split(new string[] { p }, System.StringSplitOptions.None);
            return toArr(strs);
        }

        public int length()
        {
            return value.Length;
        }

        public bool equalsIgnoreCase(string p)
        {
            return this.value.Equals(p, System.StringComparison.OrdinalIgnoreCase);
        }

        public String[] split(char p)
        {
            string[] strs = this.value.Split(p);
            return toArr(strs);
        }
    }
}
