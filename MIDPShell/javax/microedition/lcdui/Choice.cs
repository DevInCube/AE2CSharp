namespace javax.microedition.lcdui;

public abstract interface Choice
{
  public static sealed override int TEXT_WRAP_DEFAULT = 0;
  public static sealed override int EXCLUSIVE = 1;
  public static sealed override int TEXT_WRAP_ON = 1;
  public static sealed override int MULTIPLE = 2;
  public static sealed override int TEXT_WRAP_OFF = 2;
  public static sealed override int IMPLICIT = 3;
  public static sealed override int POPUP = 4;
  
  public abstract bool  isSelected(int paramInt);
  
  public abstract int append(String paramString, Image paramImage);
  
  public abstract int getFitPolicy();
  
  public abstract int getSelectedFlags(bool [] paramArrayOfBoolean);
  
  public abstract int getSelectedIndex();
  
  public abstract int size();
  
  public abstract String getString(int paramInt);
  
  public abstract Font getFont(int paramInt);
  
  public abstract Image getImage(int paramInt);
  
  public abstract void delete(int paramInt);
  
  public abstract void deleteAll();
  
  public abstract void insert(int paramInt, String paramString, Image paramImage);
  
  public abstract void set(int paramInt, String paramString, Image paramImage);
  
  public abstract void setFitPolicy(int paramInt);
  
  public abstract void setFont(int paramInt, Font paramFont);
  
  public abstract void setSelectedFlags(bool [] paramArrayOfBoolean);
  
  public abstract void setSelectedIndex(int paramInt, bool  paramBoolean);
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.lcdui.Choice

 * JD-Core Version:    0.7.0.1

 */