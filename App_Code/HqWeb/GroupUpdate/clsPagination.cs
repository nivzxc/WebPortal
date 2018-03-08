using System;
using System.Data;
using System.Data.SqlClient;

namespace HqWeb
{
    namespace GroupUpdate
    {
        public class clsPagination : IDisposable
        {
            public clsPagination()
            {
                
            }

            public void Dispose() { GC.SuppressFinalize(this); }

            private int _intCurrentPage;
            private int _intPagePerForm;
            private int _intTotalCount;

            public int CurrentPage { get { return _intCurrentPage; } set { _intCurrentPage = value; } }
            public int PagePerForm { get { return _intPagePerForm; } set { _intPagePerForm = value; } }
            public int TotalCount { get { return _intTotalCount; } set { _intTotalCount = value; } }

            public int Offset()
            {
                int intReturn = (_intCurrentPage - 1) * _intPagePerForm;
                return intReturn;
            }

            public int TotalPage()
            {
                decimal dcResult = TotalCount.ToString().ToDecimal() / PagePerForm.ToString().ToDecimal();
                int intReturn = Math.Ceiling(dcResult).ToString().ToInt();
                return intReturn;
            }

            public int PreviousPage()
            {
                int intReturn = _intCurrentPage - 1;
                return intReturn;
            }

            public int NextPage()
            {
                int intReturn = _intCurrentPage + 1;
                return intReturn;
            }

            public bool HasPreviousPage()
            {
                bool blnReturn = PreviousPage() >= 1 ? true : false;
                return blnReturn;
            }

            public bool HasNextPage()
            {
                bool blnReturn = NextPage() <= TotalPage() ? true : false;
                return blnReturn;
            }


        }
    }
}