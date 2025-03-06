using System;
using System.Collections.Generic;

namespace BT01_03
{
    public class QLSV
    {
        public List<string> GetAllLSH()
        {
            List<string> li = new List<string>();
            foreach (SV i in CSDL.Instance.li)
            {
                if (!li.Contains(i.LSH))
                {
                    li.Add(i.LSH);
                }
            }
            return li;
        }

        public List<SV> ListSV()
        {
            return ListSV("All", "");
        }

        public List<SV> ListSV(string LSH, string txt)
        {
            List<SV> result = new List<SV>();
            foreach (SV sv in CSDL.Instance.li)
            {
                if ((LSH == "All" || sv.LSH == LSH) && sv.NameSV.Contains(txt))
                {
                    result.Add(sv);
                }
            }
            return result;
        }

        public void DelSV(List<string> liDel)
        {
            for (int i = CSDL.Instance.li.Count - 1; i >= 0; i--)
            {
                if (liDel.Contains(CSDL.Instance.li[i].MSSV))
                {
                    CSDL.Instance.li.RemoveAt(i);
                }
            }
        }

        public List<SV> ListSort(List<string> liview, string s)
        {
            List<SV> sortedList = new List<SV>();

            foreach (SV sv in CSDL.Instance.li)
            {
                foreach (string id in liview)
                {
                    if (sv.MSSV == id)
                    {
                        sortedList.Add(sv);
                        break;
                    }
                }
            }

            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                for (int j = i + 1; j < sortedList.Count; j++)
                {
                    if (string.Compare(sortedList[i].NameSV, sortedList[j].NameSV) > 0)
                    {
                        SV temp = sortedList[i];
                        sortedList[i] = sortedList[j];
                        sortedList[j] = temp;
                    }
                }
            }

            return sortedList;
        }

        public List<SV> ListSVNow(List<string> liview)
        {
            List<SV> li = new List<SV>();
            foreach (string i in liview)
            {
                foreach (SV j in CSDL.Instance.li)
                {
                    if (i == j.MSSV)
                    {
                        li.Add(j);
                        break;
                    }
                }
            }
            return li;
        }

        public SV GetSVByMSSV(string m)
        {
            foreach (SV i in CSDL.Instance.li)
            {
                if (i.MSSV == m)
                {
                    return i;
                }
            }
            return null;
        }
        public void AddUpdate(SV s)
        {
            bool found = false;
            foreach (SV sv in CSDL.Instance.li)
            {
                if (sv.MSSV == s.MSSV)
                {
                    sv.NameSV = s.NameSV;
                    sv.LSH = s.LSH;
                    sv.DTB = s.DTB;
                    sv.Gender = s.Gender;
                    sv.NS = s.NS;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                CSDL.Instance.li.Add(s);
            }
        }

        public bool Check(string m)
        {
            foreach (SV i in CSDL.Instance.li)
            {
                if (i.MSSV == m)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
