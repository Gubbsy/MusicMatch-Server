using Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class IntroductionsDbo : Introductions
    {
        public new int Id { get => base.Id; set => base.Id = value; }
        public new string UId1 { get => base.UId1; set => base.UId1 = value; }
        public new string UId2 { get => base.UId2; set => base.UId2 = value; }
        public new bool Requested { get => base.Requested; set => base.Requested = value; }
    }
}
