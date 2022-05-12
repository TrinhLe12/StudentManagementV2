using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Business.Validators
{
    public class CommonValidator : ICommonValidator
    {
        public bool PastDateValidator(DateTime datetime)
        {
            if (datetime != null)
            {
                DateTime date = datetime.Date;

                DateTime currentDatetime = DateTime.Now;
                DateTime currentDate = currentDatetime.Date;

                if (date.CompareTo(currentDate) >= 0)
                {

                    return false;

                }
                else
                {
                    return true;
                }

            }
            else
            {
                return true;
            }
        }
    }
}
