using OfficeMart.Business.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Business.Dtos.LibraryDto
{
    public class BaseDto
    {
        public Library Library
        {
            get
            {
                return Library.GetInstance();
            }
        }
    }
}
