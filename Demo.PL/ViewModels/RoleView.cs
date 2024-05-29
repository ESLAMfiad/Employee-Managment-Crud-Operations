using System;

namespace Demo.PL.ViewModels
{
    public class RoleView
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public RoleView()
        {
            Id=Guid.NewGuid().ToString();
        }
    }
}
