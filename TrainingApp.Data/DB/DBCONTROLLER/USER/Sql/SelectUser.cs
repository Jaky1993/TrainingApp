using TrainingAppData.DB.INTERFACE;
using TrainingAppData.MODEL;

namespace TrainingAppData.DB.DBCONTROLLER.USER.Sql
{
    public class SelectUser : ISelect<User>
    {
        public User Select(Guid guid)
        {
            User user = new User();

            return user;
        }

        public User Select(int id)
        {
            User user = new User();

            return user;
        }
    }
}
