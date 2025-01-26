using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.VIEWMODEL
{
    public class UserViewModel : EntityViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public short Age { get; set; }
        public string Password { get; set; }
    }
}
