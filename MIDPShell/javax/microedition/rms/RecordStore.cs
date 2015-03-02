using java.lang;
using MIDP.WPF.Data;
using System.Collections.Generic;
namespace javax.microedition.rms
{

    public class RecordStore
    {
        public static readonly int AUTHMODE_PRIVATE = 0;
        public static readonly int AUTHMODE_ANY = 1;

        private List<byte[]> records = new List<byte[]>();

        public byte[] getRecord(int paramInt)
        {
            return records[paramInt];
        }

        public int addRecord(byte[] data, int offset, int numBytes)
        {
            records.Add(data);//@todo offset numBytes
            return records.Count - 1;
        }

        public int getNextRecordID()
        {
            return 0;
        }

        public int getNumRecords()
        {
            return records.Count;
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
            return int.MaxValue; //@todo
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

        public static RecordStore openRecordStore(String recordStoreName, bool createIfNecessary)
        {
            if (RecordStoreManager.Contains(recordStoreName))
            {
                return RecordStoreManager.Get(recordStoreName);
            }
            else
            {
                if (createIfNecessary)
                {
                    RecordStoreManager.Create(recordStoreName);
                    return RecordStoreManager.Get(recordStoreName);
                }
                throw new RecordStoreNotFoundException(recordStoreName);
            }
        }

        public static RecordStore openRecordStore(String recordStoreName, bool createIfNecessary, int authmode, bool writable)
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

        public void setRecord(int recordId, byte[] newData, int offset, int numBytes)
        {
            if (recordId < 0 || recordId >= records.Count)
                throw new InvalidRecordIDException(recordId.ToString());
            records[recordId] = newData; //@todo offset numBytes
        }
    }

}