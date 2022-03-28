using System;

namespace Abstraction.Models
{
    public class Player : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Score { get; set; }
    }
    
}
