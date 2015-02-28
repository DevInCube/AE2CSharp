
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

        public static String operator +(String s1, String s2)
        {
            return s1.ToString() + s2.ToString();
        }
        public static String operator +(String s1, object s2)
        {
            return s1.ToString() + s2.ToString();
        }

        public override string ToString()
        {
            return value;
        }

        public int length
        {
            get
            {
                return value.Length;
            }
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

        public String subString(int p, int i2)
        {
            return value.Substring(p, i2);
        }

        public String subString(int p)
        {
            return value.Substring(p);
        }
    }
}
