using TrainingAppData.DB.DBCONTROLLER.USER.Sql;
using TrainingAppData.MODEL;

namespace TrainingApp.Test;

[TestClass]
public class UserCRUDTest
{
    [TestMethod]
    public void TestMethod1()
    {
    }

    [TestMethod]
    public void TestCreateUser()
    {
        CreateSqlUser createUser = new CreateSqlUser();

        User user = new User()
        {
            Name = "Jacopo",
            Description = "Descrizione",
            Email = "jacopo@gmail.com",
            UserName = "Jacoby1993",
            Age = 31,
            VersionId = 1
            //TODO versionID
        };

        createUser.Create(user);
    }

    [TestMethod]
    public void TestSelectUserByGuid()
    {
        SelectSqlUser selectUser = new SelectSqlUser();

        User user = new User();

        Guid guid = Guid.Parse("A5E33AFD-9AA0-4C98-A836-550CF0106665");

        user = selectUser.Select(guid);
    }
}
