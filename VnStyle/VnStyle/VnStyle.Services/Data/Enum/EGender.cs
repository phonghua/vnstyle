using System.ComponentModel;

namespace VnStyle.Services.Data.Enum
{
    public enum EGender
    {
        [Description("Male")]
        Male = 1,

        [Description("Female")]
        Female = 2,

        [Description("Other")]
        Other = 100
    }
}