﻿namespace TrainingAppData.MODEL
{
    public partial class User : Entity
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
    }
}
