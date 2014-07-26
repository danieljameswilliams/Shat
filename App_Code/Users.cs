using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.ComponentModel;

namespace Shat
{
 
    public class Users
    {
        //constructor
        //methods
        //static methods
        //properties
        //enum

        #region methods
        //public string GetEnumRegion(int enumInt)
        //{
        //    minRegion f = (minRegion)enumInt;
        //    string returnStr = GetDescription<minRegion>(f);
        //    return returnStr;
        //}
        #endregion

        #region Static methods
        public static string GetEnumStatus(int enumInt)
        {
            Status f = (Status)enumInt;
            string returnStr = GetDescription<Status>(f);
            return returnStr;
        }

        private static string GetDescription<T>(T value)
        {
            DescriptionAttribute[] d = (DescriptionAttribute[])(typeof
            (T).GetField
            (value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute),
            false));
            return d[0].Description;
        }

        #endregion

        #region enums
        public enum Status : byte
        {
            [Description("Single")]
            Single = 1,
            [Description("I et forhold")]
            iEtForhold = 2,
            [Description("Forlovet")]
            Forlovet = 3,
            [Description("Gift")]
            Gift = 4,
            [Description("Det er kompliceret")]
            Kompliceret = 5,
            [Description("I et åbent forhold")]
            AabentForhold = 6,
            [Description("Enke / Enkemand")]
            Enke = 7
        }

        //public enum minRegion : byte
        //{
        //    [Description("Single")]
        //    Single = 1,
        //    [Description("I et forhold")]
        //    iEtForhold = 2,
        //    [Description("Forlovet")]
        //    Forlovet = 3,
        //    [Description("Gift")]
        //    Gift = 4,
        //    [Description("Det er kompliceret")]
        //    Kompliceret = 5,
        //    [Description("I et åbent forhold")]
        //    AabentForhold = 6,
        //    [Description("Enke / Enkemand")]
        //    Enke = 7
        //}
        #endregion
    }
}