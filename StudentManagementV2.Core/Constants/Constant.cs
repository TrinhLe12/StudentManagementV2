using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Core.Constants
{
    public class Constant
    {
        public enum RoleId
        {
            R01, // Student
            R02, // Instructor   
            R03  // Admin
        }

        public static readonly int PAGE_SIZE = 5;
    }
}
