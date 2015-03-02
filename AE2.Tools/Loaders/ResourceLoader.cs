using AE2.Tools.Properties;
using java.io;
using java.lang;

namespace AE2.Tools.Loaders
{
    public static class ResourceLoader
    {
        private static String[] resourcesNames;
        private static byte[][] resourcesData;

        public static  void loadResourcesPak(String pakFileName)
        {
            if (resourcesNames == null)
            {
                resourcesNames = null;
                int[] arrayOfInt1 = null;
                int[] arrayOfInt2 = null;
                InputStream stream = ResourceLoader.getResourceAsStream("Resources/1.pak");
                DataInputStream resStream = new DataInputStream(stream);
                int i = resStream.readShort();
                int resLength = resStream.readShort();
                resourcesNames = new String[resLength];
                arrayOfInt1 = new int[resLength];
                arrayOfInt2 = new int[resLength];
                for (int k = 0; k < resLength; k++)
                {
                    resourcesNames[k] = resStream.readUTF();
                    arrayOfInt1[k] = (resStream.readInt() + i);
                    arrayOfInt2[k] = resStream.readShort();
                }
                resourcesData = new byte[resourcesNames.Length][];
                for (int m = 0; m < resourcesNames.Length; m++)
                {
                    resourcesData[m] = new byte[arrayOfInt2[m]];
                    resStream.readFully(resourcesData[m]);
                }
                resStream.close();
            }
        }

        public static void saveUnpackedResources(string dir)
        {
            for (int i = 0; i < resourcesNames.Length;i++ )
            {
                String key = resourcesNames[i];
                string path = System.IO.Path.Combine(dir, key.ToString());
                System.IO.File.WriteAllBytes(path, resourcesData[i]);
            }
        }

        private static InputStream getResourceAsStream(string path)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            using (System.IO.FileStream file = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                byte[] bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
                ms.Write(bytes, 0, (int)file.Length);
            }
            return new DataInputStream(ms);
        }

        public static  InputStream getResourceStream(String resName)
        {
            return new ByteArrayInputStream(getResourceData(resName));
        }

        public static  byte[] getResourceData(String resName)
        {
            for (int i = 0; i < resourcesNames.Length; i++)
            {
                if (resName.Equals(resourcesNames[i]))
                {
                    return resourcesData[i];
                }
            }
            return null;
        }
    }
}
