namespace TrainingAppData.MODEL
{
    public partial class User : Entity<User>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public short Age { get; set; }
        public string Password { get; set; }
    }
}
