using System.Linq;
using java.io;
using java.lang;

namespace AE2.Tools
{
    public class E_MainCanvas
    {
        private static String[] _resourcesNames;
        private static byte[][] _resourcesData;

        public static void loadResourcesPak(String pakFileName)
        {
            if (_resourcesNames == null)
            {
                _resourcesNames = null;
                int[] arrayOfInt1 = null;
                int[] arrayOfInt2 = null;
                var stream = E_MainCanvas.getResourceAsStream("Resources/1.pak");
                var resStream = new DataInputStream(stream);
                int i = resStream.readShort();
                int resLength = resStream.readShort();
                _resourcesNames = new String[resLength];
                arrayOfInt1 = new int[resLength];
                arrayOfInt2 = new int[resLength];
                for (var k = 0; k < resLength; k++)
                {
                    _resourcesNames[k] = resStream.readUTF();
                    arrayOfInt1[k] = (resStream.readInt() + i);
                    arrayOfInt2[k] = resStream.readShort();
                }
                _resourcesData = new byte[_resourcesNames.Length][];
                for (var m = 0; m < _resourcesNames.Length; m++)
                {
                    _resourcesData[m] = new byte[arrayOfInt2[m]];
                    resStream.readFully(_resourcesData[m]);
                }
                resStream.close();
            }
        }

        public static void saveUnpackedResources(string dir)
        {
            for (var i = 0; i < _resourcesNames.Length; i++)
            {
                var key = _resourcesNames[i];
                var path = System.IO.Path.Combine(dir, key.ToString());
                System.IO.File.WriteAllBytes(path, _resourcesData[i]);
            }
        }

        private static InputStream getResourceAsStream(string path)
        {
            var ms = new System.IO.MemoryStream();
            //using (ms)
            {
                using (var file = new System.IO.FileStream(
                    path: path,
                    mode: System.IO.FileMode.Open,
                    access: System.IO.FileAccess.Read))
                {
                    var bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int) file.Length);
                    ms.Write(bytes, 0, (int) file.Length);
                }
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                return new DataInputStream(ms);
            }
        }

        public static InputStream getResourceStream(String resName)
        {
            return new ByteArrayInputStream(getResourceData(resName));
        }

        public static byte[] getResourceData(String resName)
        {
            for (var i = 0; i < _resourcesNames.Length; i++)
            {
                if (resName.Equals(_resourcesNames[i]))
                {
                    return _resourcesData[i];
                }
            }
            return null;
        }
    }
}
