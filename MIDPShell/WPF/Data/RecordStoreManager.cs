using javax.microedition.rms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDP.WPF.Data
{
    public static class RecordStoreManager
    {

        static Dictionary<string, RecordStore> stores = new Dictionary<string, RecordStore>();

        internal static void Create(java.lang.String recordStoreName)
        {
            stores.Add(recordStoreName.ToString(), new RecordStore());
        }

        internal static javax.microedition.rms.RecordStore Get(java.lang.String recordStoreName)
        {
            return stores[recordStoreName.ToString()];
        }

        internal static bool Contains(java.lang.String recordStoreName)
        {
            return stores.ContainsKey(recordStoreName.ToString());
        }
    }
}
