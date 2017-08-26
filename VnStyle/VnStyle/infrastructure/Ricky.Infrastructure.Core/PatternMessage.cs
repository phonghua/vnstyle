namespace Ricky.Infrastructure.Core
{
    public static class PatternMessage
    {
        public const string EmailFormat = @"Wrong email format, ex: abc@gmail.com";

        //public const string PasswordFormat = @"Wrong password format: at least 8 characters with Upper case and number";
        //public const string PasswordFormat = @"Wrong password format: at least 13 characters and must include: lower-case letters, upper-case letters, special characters, number";

        public const string PasswordMath = @"Your passwords do not match";

        public const string Phone = @"Wrong phone format, ex: (12) 3456 7890";

        public const string Fax = @"Wrong fax format, ex: 12 3456 7890";

        public const string Mobile = @"Wrong mobile format, ex: 1234 567 890";

        //public const string PostCode = @"Wrong PostCode format, the value for this must be contains 3 or 4 digits";
        public const string PostalCode = @"Wrong Postal Code format, the value for this must be starts with 2 characters and ends with 6 digits";

        public const string AlphaNumericSpaces = @"The value for this field only contains alphanumeric or space";

        public const string AlphaNumeric = @"The value for this field only contains alphanumeric";

        public const string AlphabetsOnly = @"The value for this field only contains alphabets only";

        public const string NumericOnly = @"The value for this field only contains number only";
        public const string DigitOnly = @"The value for this field only contains digit only";
        public const string IntegerOnly = @"The value for this field only contains integer number only";

        public const string V0001 = "The field '{0}' is required";
    }

    public static class Pattern
    {
        //public const string PasswordFormat = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%\^&*)(+=._-]).{13,50}$";
        //public const string PasswordFormat = @"(?=^.{13,}$)((?=.*\d)(?=.*[A - Z])(?=.*[a - z])|(?=.*\d)(?=.*[^ A - Za - z0 - 9])(?=.*[a - z])|(?=.*[^ A - Za - z0 - 9])(?=.*[A - Z])(?=.*[a - z])|(?=.*\d)(?=.*[A - Z])(?=.*[^ A - Za - z0 - 9]))^.*$";

        public const string EmailFormat = @"^[0-9a-zA-Z]+([0-9a-zA-Z]*[-._+])*[0-9a-zA-Z]+@[0-9a-zA-Z]+([-.][0-9a-zA-Z]+)*([0-9a-zA-Z]*[.])[a-zA-Z]{2,6}$";
        public const string Date = @"(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)";

        public const string Phone = @"^\(\d{2}\)\s{1}\d{4}\s{1}\d{4}$";

        public const string Fax = @"^\(\d{2}\)\s{1}\d{4}\s{1}\d{4}$";

        public const string Mobile = @"^\d{4}\s{1}\d{3}\s{1}\d{3}$";

        //public const string PostCode = @"\d{3,4}";
        public const string PostCode = @"[A-Z]{2}\d{6,6}";

        public const string AlphaNumericSpaces = @"^[a-zA-Z0-9\s]*$";

        public const string AlphaNumeric = @"^[a-zA-Z0-9]*$";
        public const string AlphabetsOnly = @"^[a-zA-Z]+$";
        public const string NumericOnly = @"^\d+(\.\d{1,4})?$";
        public const string DigitOnly = @"([1-9][0-9]*)";
        public const string IntegerOnly = @"^\d+$";
    }
}
