namespace javax.microedition.rms
{

    public interface RecordListener
    {
        void recordAdded(RecordStore paramRecordStore, int paramInt);

        void recordChanged(RecordStore paramRecordStore, int paramInt);

        void recordDeleted(RecordStore paramRecordStore, int paramInt);
    }

}