using java.lang;

using javax.microedition.lcdui;
using java.util;
using java.csharp;
using java.io;
using java.lang;

namespace aeii
{
    public class A_MenuBase
    {

        public static E_MainCanvas mainCanvas;
        public int someCanWidth = mainCanvas.getWidth();
        public int someCanHeight = mainCanvas.getHeight();
        public int someCanWidthDiv2 = mainCanvas.getWidth() >> 1;
        public int someCanHeightDiv2 = mainCanvas.getHeight() >> 1;
        public static String[] langStrings;
        public static short[] sin1024Table = null;
        public static int maxDegrees = 360;
        public static int maxDegreesDiv2 = 0;
        public static int maxDegreesDiv4 = 0;

        public void onLoad()
        {
            //@todo override
        }

        public void sub_865(int inInt1, int inInt2)
        {
            //@todo override or not?
        }

        public virtual void onUpdate()
        {
            //@todo override
        }

        public virtual void onPaint(Graphics graphics)
        {
            //@todo override
        }

        public static  String[] wrapText(String aString, int maxWidth, Font aFont)
        {
            Vector localVector = new Vector();
            int i = 0;
            int k = aString.length();
            Object localObject1 = null;
            int m;
            do
            {
                m = i;
                int n = aString.indexOf('\n', m);
                do
                {
                    int i1 = m;
                    Object localObject2 = localObject1;
                    m = sub_ab5(aString, m);
                    if ((n > -1) && (n < m))
                    {
                        m = n;
                    }
                    localObject1 = aString.subString(i, m).trim();
                    if (aFont.stringWidth((String)localObject1) > maxWidth)
                    {
                        if (i1 == i)
                        {
                            for (int i2 = ((String)localObject1).length() - 1; i2 > 0; i2--)
                            {
                                String str = ((String)localObject1).subString(0,
                                        i2);
                                if (aFont.stringWidth(str) <= maxWidth)
                                {
                                    m = i1 + i2;
                                    localObject1 = str;
                                    break;
                                }
                            }
                        }
                        m = i1;
                        localObject1 = localObject2;
                        break;
                    }
                    if (m == n)
                    {
                        m++;
                        break;
                    }
                } while (m < k);
                localVector.addElement(localObject1);
            } while ((i = m) < k);
            String[] arrayOfString = new String[localVector.size()];
            localVector.copyInto(arrayOfString);
            return arrayOfString;
        }

        private static  int sub_ab5(String aString, int charPos)
        {
            int i = aString.charAt(charPos);
            if (sub_b97(i))
            {
                return charPos + 1;
            }
            int j = 0;
            int k = 0;
            while ((k = aString.indexOf(' ', charPos)) == 0)
            {
                charPos++;
            }
            if ((j = k) == -1)
            {
                j = aString.length();
            }
            else
            {
                j++;
            }
            for (int it = charPos + 1; it < j; it++)
            {
                if (sub_b97(aString.charAt(it)))
                {
                    return it;
                }
            }
            return j;
        }

        private static  bool sub_b97(int paramInt)
        {
            return ((paramInt >= 11904) && (paramInt < 44032))
                    || ((paramInt >= 63744) && (paramInt < 64256))
                    || ((paramInt >= 65280) && (paramInt < 65504));
        }

        public static int loadLangStrings(String langFile, bool unusedBool)
        {
            InputStream stream = B_MainMIDlet.midlet.getClass().getResourceAsStream(langFile);
            DataInputStream dis = new DataInputStream(stream);
            langStrings = new String[dis.readInt()];
            int i = 0;
            int Length = langStrings.Length;
            while (i < Length)
            {
                langStrings[i] = dis.readUTF();
                i++;
            }
            dis.close();
            return langStrings.Length;
        }

        public static  String getLangString(int aStringId)
        {
            return getSomeHelpString(aStringId, false);
        }

        public static  String getSomeHelpString(int strId, bool paramBoolean)
        {
            if (strId < langStrings.Length)
            {
                String str = langStrings[strId];
                if (paramBoolean)
                {
                    String someStr = replaceStringFirst(20, mainCanvas.getKeyName2(16)); //'%U'/select
                    str = replaceString(str, "%K5", someStr, true);
                    str = replaceString(str, "%K0", mainCanvas.getKeyName2(32), true);
                    str = replaceString(str, "%K7", mainCanvas.getKeyName2(256), true);
                    str = replaceString(str, "%K9", mainCanvas.getKeyName2(512), true);
                    if ((str.indexOf("%KM") != -1))
                    {
                        StringBuffer buf = new StringBuffer();
                        String[] keyNames = { mainCanvas.getKeyName2(1),
							mainCanvas.getKeyName2(2), mainCanvas.getKeyName2(4),
							mainCanvas.getKeyName2(8) };
                        buf.append(aStringFormat(17, keyNames)); //'%U', '%U', '%U', '%U'
                        if (buf.length() > 0)
                        {
                            buf.append('/');
                        }
                        buf.append(getLangString(18)); //direction pad
                        str = replaceString(str, "%KM", buf.toString(), true);
                    }
                }
                return str;
            }
            return "?: " + strId;
        }

        public static  String aStringFormat(int paramInt, String[] paramArrayOfString)
        {
            String str = new String(getLangString(paramInt));
            for (int i = 0; i < paramArrayOfString.Length; i++)
            {
                str = replaceString(str, "%U", paramArrayOfString[i], false);
            }
            return str;
        }

        public static  String replaceStringFirst(int strID, String replacement)
        {
            return replaceString(getLangString(strID), "%U", replacement, false);
        }

        public static  String replaceString(String aString,
                String toReplace, String replacement, bool menuTimes)
        {
            String str = aString;
            do
            {
                int index = str.indexOf(toReplace);
                if (index == -1)
                {
                    break;
                }
                str = str.subString(0, index) + replacement
                        + str.subString(index + toReplace.length());
            } while (menuTimes);
            return str;
        }

        public static  void initSin1024()
        {
            //
            maxDegreesDiv2 = maxDegrees >> 1;
            maxDegreesDiv4 = maxDegreesDiv2 >> 1;
            sin1024Table = new short[maxDegrees];
            int j = maxDegrees * 10000 / 2 / 31415;
            int k = 1024 * j;
            int m = 0;
            for (int degree = 0; degree < maxDegrees; degree++)
            {
                int n = m / j;
                sin1024Table[degree] = ((short)n);
                k -= n;
                m += k / j;
            }
            //sin1024Table[(int) 'Â'] = 0; // 'Â´'
            //sin1024Table[270] = -1024;
        }

        public static  short getSin1024(int degree)
        {
            degree %= 360;
            return sin1024Table[degree];
        }

        public static  short getCos2014(int angle)
        {
            angle = (angle + maxDegreesDiv4) % 360;
            return sin1024Table[angle];
        }
    }

}