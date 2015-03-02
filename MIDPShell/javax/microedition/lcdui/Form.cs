using java.lang;
namespace javax.microedition.lcdui
{

    public class Form : Screen
    {

        private System.Windows.Controls.GroupBox control = new System.Windows.Controls.GroupBox();

        public override System.Windows.FrameworkElement WPFControl
        {
            get { return control; }
        }

        public Form(String paramString) { }

        public Form(String paramString, Item[] paramArrayOfItem) { }

        public int append(String paramString)
        {
            return 0;
        }

        public int append(Image paramImage)
        {
            return 0;
        }

        public int append(Item paramItem)
        {
            return 0;
        }

        public int getHeight()
        {
            return 0;
        }

        public int getWidth()
        {
            return 0;
        }

        public int size()
        {
            return 0;
        }

        public Item get(int paramInt)
        {
            return null;
        }

        public void delete(int paramInt) { }

        public void deleteAll() { }

        public void insert(int paramInt, Item paramItem) { }

        public void set(int paramInt, Item paramItem) { }

        public void setItemStateListener(ItemStateListener paramItemStateListener) { }
    }

}