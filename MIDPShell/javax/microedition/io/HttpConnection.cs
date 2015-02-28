namespace javax.microedition.io;

using java.io.IOException;

public abstract interface HttpConnection
  : ContentConnection
{
  public static sealed override int HTTP_OK = 200;
  public static sealed override int HTTP_CREATED = 201;
  public static sealed override int HTTP_ACCEPTED = 202;
  public static sealed override int HTTP_NOT_AUTHORITATIVE = 203;
  public static sealed override int HTTP_NO_CONTENT = 204;
  public static sealed override int HTTP_RESET = 205;
  public static sealed override int HTTP_PARTIAL = 206;
  public static sealed override int HTTP_MULT_CHOICE = 300;
  public static sealed override int HTTP_MOVED_PERM = 301;
  public static sealed override int HTTP_MOVED_TEMP = 302;
  public static sealed override int HTTP_SEE_OTHER = 303;
  public static sealed override int HTTP_NOT_MODIFIED = 304;
  public static sealed override int HTTP_USE_PROXY = 305;
  public static sealed override int HTTP_TEMP_REDIRECT = 307;
  public static sealed override int HTTP_BAD_REQUEST = 400;
  public static sealed override int HTTP_UNAUTHORIZED = 401;
  public static sealed override int HTTP_PAYMENT_REQUIRED = 402;
  public static sealed override int HTTP_FORBIDDEN = 403;
  public static sealed override int HTTP_NOT_FOUND = 404;
  public static sealed override int HTTP_BAD_METHOD = 405;
  public static sealed override int HTTP_NOT_ACCEPTABLE = 406;
  public static sealed override int HTTP_PROXY_AUTH = 407;
  public static sealed override int HTTP_CLIENT_TIMEOUT = 408;
  public static sealed override int HTTP_CONFLICT = 409;
  public static sealed override int HTTP_GONE = 410;
  public static sealed override int HTTP_LENGTH_REQUIRED = 411;
  public static sealed override int HTTP_PRECON_FAILED = 412;
  public static sealed override int HTTP_ENTITY_TOO_LARGE = 413;
  public static sealed override int HTTP_REQ_TOO_LONG = 414;
  public static sealed override int HTTP_UNSUPPORTED_TYPE = 415;
  public static sealed override int HTTP_UNSUPPORTED_RANGE = 416;
  public static sealed override int HTTP_EXPECT_FAILED = 417;
  public static sealed override int HTTP_INTERNAL_ERROR = 500;
  public static sealed override int HTTP_NOT_IMPLEMENTED = 501;
  public static sealed override int HTTP_BAD_GATEWAY = 502;
  public static sealed override int HTTP_UNAVAILABLE = 503;
  public static sealed override int HTTP_GATEWAY_TIMEOUT = 504;
  public static sealed override int HTTP_VERSION = 505;
  public static sealed override String GET = "GET";
  public static sealed override String HEAD = "HEAD";
  public static sealed override String POST = "POST";
  
  public abstract int getHeaderFieldInt(String paramString, int paramInt)
    ;
  
  public abstract int getPort();
  
  public abstract int getResponseCode()
    ;
  
  public abstract String getFile();
  
  public abstract String getHeaderField(int paramInt)
    ;
  
  public abstract String getHeaderField(String paramString)
    ;
  
  public abstract String getHeaderFieldKey(int paramInt)
    ;
  
  public abstract String getHost();
  
  public abstract String getProtocol();
  
  public abstract String getQuery();
  
  public abstract String getRef();
  
  public abstract String getRequestMethod();
  
  public abstract String getRequestProperty(String paramString);
  
  public abstract String getResponseMessage()
    ;
  
  public abstract String getURL();
  
  public abstract long getDate()
    ;
  
  public abstract long getExpiration()
    ;
  
  public abstract long getHeaderFieldDate(String paramString, long paramLong)
    ;
  
  public abstract long getLastModified()
    ;
  
  public abstract void setRequestMethod(String paramString)
    ;
  
  public abstract void setRequestProperty(String paramString1, String paramString2)
    ;
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.io.HttpConnection

 * JD-Core Version:    0.7.0.1

 */