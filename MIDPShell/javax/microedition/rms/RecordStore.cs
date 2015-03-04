using java.lang;
using MIDP.WPF.Data;
using System.Collections.Generic;
namespace javax.microedition.rms
{

    public class RecordStore
    {

        public const int AUTHMODE_PRIVATE = 0;
        public const int AUTHMODE_ANY = 1;

        private String name;

        private RecordStore(String name)
        {
            this.name = name;
        }

        private List<byte[]> records = new List<byte[]>();

        public byte[] getRecord(int index)
        {
            return records[index];
        }

        public int addRecord(byte[] data, int offset, int numBytes)
        {
            records.Add(data);//@todo offset numBytes
            return records.Count - 1;
        }

        public int getNextRecordID()
        {
            return getNumRecords() + 1;
        }

        public int getNumRecords()
        {
            return records.Count;
        }

        public int getRecord(int recordId, byte[] buffer, int offset)
        {
            return 0;
        }

        public int getRecordSize(int recordId)
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
            return name;
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
            if (string.IsNullOrEmpty(recordStoreName)) throw new IllegalArgumentException("recordStoreName");

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
            RecordStore store = openRecordStore(recordStoreName, createIfNecessary);
            //@todo
            return store;
        }

        /// <summary>
        ///  Open a record store associated with the named MIDlet suite.
        /// </summary>
        /// <param name="recordStoreName"></param>
        /// <param name="vendorName"></param>
        /// <param name="suiteName"></param>
        /// <returns></returns>
        public static RecordStore openRecordStore(String recordStoreName, String vendorName, String suiteName)
        {
            return null;
        }

        public static void deleteRecordStore(String name)
        {
            RecordStoreManager.Delete(name);
        }

        public void addRecordListener(RecordListener listenter) 
        { 
            //
        }

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