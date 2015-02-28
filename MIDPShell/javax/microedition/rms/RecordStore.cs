using java.lang;
namespace javax.microedition.rms
{

    public class RecordStore
    {
        public static sealed override int AUTHMODE_PRIVATE = 0;
        public static sealed override int AUTHMODE_ANY = 1;

        public byte[] getRecord(int paramInt)
        {
            return null;
        }

        public int addRecord(byte[] paramArrayOfByte, int paramInt1, int paramInt2)
        {
            return 0;
        }

        public int getNextRecordID()
        {
            return 0;
        }

        public int getNumRecords()
        {
            return 0;
        }

        public int getRecord(int paramInt1, byte[] paramArrayOfByte, int paramInt2)
        {
            return 0;
        }

        public int getRecordSize(int paramInt)
        {
            return 0;
        }

        public int getSize()
        {
            return 0;
        }

        public int getSizeAvailable()
        {
            return 0;
        }

        public int getVersion()
        {
            return 0;
        }

        public String getName()
        {
            return null;
        }

        public RecordEnumeration enumerateRecords(RecordFilter paramRecordFilter, RecordComparator paramRecordComparator, bool paramBoolean)
        {
            return null;
        }

        public long getLastModified()
        {
            return 0L;
        }

        public static String[] listRecordStores()
        {
            return null;
        }

        public static RecordStore openRecordStore(String paramString, bool paramBoolean)
        {
            return null;
        }

        public static RecordStore openRecordStore(String paramString, bool paramBoolean1, int paramInt, bool paramBoolean2)
        {
            return null;
        }

        public static RecordStore openRecordStore(String paramString1, String paramString2, String paramString3)
        {
            return null;
        }

        public static void deleteRecordStore(String paramString)

        { }

        public void addRecordListener(RecordListener paramRecordListener) { }

        public void closeRecordStore()

        { }

        public void deleteRecord(int paramInt)

        { }

        public void removeRecordListener(RecordListener paramRecordListener) { }

        public void setMode(int paramInt, bool paramBoolean)

        { }

        public void setRecord(int paramInt1, byte[] paramArrayOfByte, int paramInt2, int paramInt3)

        { }
    }

}