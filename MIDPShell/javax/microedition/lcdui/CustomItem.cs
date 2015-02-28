namespace javax.microedition.lcdui;

public abstract class CustomItem
  : Item
{
  protected static sealed override int NONE = 0;
  protected static sealed override int TRAVERSE_HORIZONTAL = 1;
  protected static sealed override int POINTER_DRAG = 128;
  protected static sealed override int KEY_REPEAT = 16;
  protected static sealed override int TRAVERSE_VERTICAL = 2;
  protected static sealed override int POINTER_PRESS = 32;
  protected static sealed override int KEY_PRESS = 4;
  protected static sealed override int POINTER_RELEASE = 64;
  protected static sealed override int KEY_RELEASE = 8;
  
  protected CustomItem(String paramString) {}
  
  protected abstract int getMinContentHeight();
  
  protected abstract int getMinContentWidth();
  
  protected abstract int getPrefContentHeight(int paramInt);
  
  protected abstract int getPrefContentWidth(int paramInt);
  
  protected abstract void paint(Graphics paramGraphics, int paramInt1, int paramInt2);
  
  protected bool  traverse(int paramInt1, int paramInt2, int paramInt3, int[] paramArrayOfInt)
  {
    return false;
  }
  
  protected sealed override int getInteractionModes()
  {
    return 0;
  }
  
  protected sealed override void invalidate() {}
  
  protected sealed override void repaint() {}
  
  protected sealed override void repaint(int paramInt1, int paramInt2, int paramInt3, int paramInt4) {}
  
  protected void hideNotify() {}
  
  protected void keyPressed(int paramInt) {}
  
  protected void keyReleased(int paramInt) {}
  
  protected void keyRepeated(int paramInt) {}
  
  protected void pointerDragged(int paramInt1, int paramInt2) {}
  
  protected void pointerPressed(int paramInt1, int paramInt2) {}
  
  protected void pointerReleased(int paramInt1, int paramInt2) {}
  
  protected void showNotify() {}
  
  protected void sizeChanged(int paramInt1, int paramInt2) {}
  
  protected void traverseOut() {}
  
  public int getGameAction(int paramInt)
  {
    return 0;
  }
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.lcdui.CustomItem

 * JD-Core Version:    0.7.0.1

 */