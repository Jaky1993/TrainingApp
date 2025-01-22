namespace TrainingAppData.MODEL
{
    public partial class User : Entity
    {
        //Use for validation

        public static List<string> UserValidation(User user)
        {
            List<string> errorList = new List<string>();

            if (string.IsNullOrEmpty(user.Name))
            {
                errorList.Add("Name is null or empty");
            }

            if (string.IsNullOrEmpty(user.UserName))
            {
                errorList.Add("UserName is null or empty");
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                errorList.Add("Email is null or empty");
            }

            if (user.Age == 0)
            {
                errorList.Add("Age is equal to 0");
            }

            return errorList;
        }
    }
}
