using System.ComponentModel;

namespace Futsal.Services
{
 
    /// <summary>
    /// Available roles for the uer, this should match the aspnetroles data we have 
    /// All the roles in the DB. THe description matches the actual role name in the DB and can be used as a constant for identifying roles
    /// </summary>
    public enum UserRoleType
    {
        [Description("UnAssigned")] UnAssigned,  // when user registers the system, its default to unassigned
        [Description("Admin")] Admin,
        [Description("FutsalUser")] FutsalUser,
        [Description("User")] User,
        //[Description("Sos-Member")] SosMember
    }





}
