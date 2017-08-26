using System.ComponentModel;

namespace VnStyle.Services.Data.Enum
{
    public enum EMaritalStatus
    {
        [Description("Single")]
        Single = 1,

        [Description("Engaged")]
        Engaged = 2,

        [Description("Married")]
        Married = 3,

        [Description("Divorced")]
        Divorced = 4,

        [Description("Separated")]
        Separated = 5
    }
}