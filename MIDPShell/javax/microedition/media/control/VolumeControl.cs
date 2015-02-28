namespace javax.microedition.media.control
{

    using javax.microedition.media.Control;

    public interface VolumeControl
      : Control
    {
        bool isMuted();

        int getLevel();

        int setLevel(int paramInt);

        void setMute(bool paramBoolean);
    }


}