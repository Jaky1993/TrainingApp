namespace TrainingAppData.MODEL
{
    public partial class User : Entity<User>
    {
        //Use for validation

        public override List<Tuple<string,string>> DataValidation(User user)
        {
            List<Tuple<string,string>> errorList = new List<Tuple<string,string>>();

            if (string.IsNullOrEmpty(user.Name))
            {
                errorList.Add(new Tuple<string,string>("Name","Name is null or empty"));
            }

            /*
            if (string.IsNullOrEmpty(user.UserName))
            {
                errorList.Add(new Tuple<string, string>("Username", "Username is null or empty"));
            }
            */

            if (string.IsNullOrEmpty(user.Email))
            {
                errorList.Add(new Tuple<string, string>("Email", "Email is null or empty"));
            }

            if (user.Age <= 0)
            {
                errorList.Add(new Tuple<string, string>("Age", "Age is equal to 0"));
            }

            return errorList;
        }
    }
}
