// // ---------------------------------------------------------
// // <copyright file="IPagedList.cs" >
// // Co., Ltd
// // Author:  Hua Dai Phong
// // Created date: 22/07/2014
// // </copyright>
// // ---------------------------------------------------------

using System.Collections.Generic;

namespace Ricky.Infrastructure.Core
{
    public interface IPagedList<T> : IList<T>
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }

        Paging Paging { get; }
    }
}