using System.ComponentModel.DataAnnotations;


namespace HouseBroker.Domain.Validation
{
    public class MustContainCommaIfMultipleWordsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string str && !string.IsNullOrWhiteSpace(str))
            {
                var wordCount = str.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).Length;

                // If there's more than one word and no comma, it's invalid
                return wordCount <= 1 || str.Contains(',');
            }

            return false; // null or empty is invalid
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must contain a comma if multiple words are provided.";
        }
    }

}
