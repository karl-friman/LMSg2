﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Util
{
    public class MapperUtil
    {
        public static bool IdExistInList(int checkFor, ICollection<string> listCollection)
        {
            foreach (var listItem in listCollection)
            {
                var split = listItem.Split(", ");
                if (Int32.TryParse(split[0], out var id))
                {
                    return checkFor == id;
                }

                return false;
            }

            return false;
        }

    }
}
