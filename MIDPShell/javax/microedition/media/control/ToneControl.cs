namespace javax.microedition.media.control
{

    using javax.microedition.media.Control;

    public interface ToneControl
      : Control
    {
        public static sealed override sbyte SILENCE = -1;
        public static sealed override sbyte VERSION = -2;
        public static sealed override sbyte TEMPO = -3;
        public static sealed override sbyte RESOLUTION = -4;
        public static sealed override sbyte BLOCK_START = -5;
        public static sealed override sbyte BLOCK_END = -6;
        public static sealed override sbyte PLAY_BLOCK = -7;
        public static sealed override sbyte SET_VOLUME = -8;
        public static sealed override sbyte REPEAT = -9;
        public static sealed override sbyte C4 = 60;

        void setSequence(sbyte[] paramArrayOfByte);
    }

}