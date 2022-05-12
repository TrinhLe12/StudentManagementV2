using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Business.Validators
{
    public interface ICommonValidator
    {
        Boolean PastDateValidator (DateTime datetime); 
    }
}
