using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BT01_03
{
    public class CSDL
    {
        public BindingList<SV> li { get; private set; }
        private static CSDL _Instance;

        public static CSDL Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new CSDL();
                return _Instance;
            }
            private set { }
        }

        private CSDL()
        {
            li = new BindingList<SV>()
            {
                new SV {MSSV = "101", NameSV = "NVA", LSH = "20T1", DTB = 1.1, Gender = true, NS = new DateTime(2003, 5, 10) },
                new SV {MSSV = "102", NameSV = "NVB", LSH = "20T1", DTB = 1.5, Gender = false, NS = new DateTime(2004, 1, 10) },
                new SV {MSSV = "103", NameSV = "NVC", LSH = "20T2", DTB = 2.9, Gender = true, NS = new DateTime(2003, 5, 12) },
                new SV {MSSV = "104", NameSV = "NVD", LSH = "20T3", DTB = 3.5, Gender = true, NS = new DateTime(2003, 8, 24) },
            };
        }

        public string GetNewMSSV()
        {
            if (li.Count == 0) return "101"; 

            int maxMSSV = 0;
            foreach (SV sv in li)
            {
                int currentMSSV;
                if (int.TryParse(sv.MSSV, out currentMSSV) && currentMSSV > maxMSSV)
                {
                    maxMSSV = currentMSSV;
                }
            }
            return (maxMSSV + 1).ToString(); 
        }
    }
}
