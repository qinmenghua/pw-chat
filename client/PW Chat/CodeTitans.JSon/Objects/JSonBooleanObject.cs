using System;

namespace CodeTitans.JSon.Objects
{
    /// <summary>
    /// Internal wrapper class that describes JSON boolean value.
    /// </summary>
    internal sealed class JSonBooleanObject : JSonDecimalObject<Boolean>
    {
        /// <summary>
        /// Init constructor.
        /// </summary>
        public JSonBooleanObject(bool value)
            : base(value, value ? JSonReader.TrueString : JSonReader.FalseString)
        {
        }

        #region IJSonWritable Members

        public override void Write(IJSonWriter output)
        {
            // writes current decimal value as boolean:
            output.WriteValue(BooleanValue);
        }

        #endregion

        public override string ToString()
        {
            return BooleanValue ? JSonReader.TrueString : JSonReader.FalseString;
        }
    }
}
