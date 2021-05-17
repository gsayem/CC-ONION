using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CallCentre.Model
{
    public class UserLoginHistory : BaseModel
    {
        public DateTime LoginTime { get; set; }
        public Guid User_Id { set; get; }
        [ForeignKey("User_Id")]
        public virtual ApplicationUser User { set; get; }
    }
}
