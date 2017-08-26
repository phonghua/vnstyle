using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ricky.Infrastructure.Core.DataTables
{
    public class DataTableJsResult<T>
    {
        /// <summary>
        /// Gets the Draw counter for DataTables.
        /// </summary>
        //[DataMember(Name = "draw")]
        public int draw { get; private set; }

        /// <summary>
        /// Gets the Data collection.
        /// </summary>
        public IEnumerable<T> data { get;  set; }

        /// <summary>
        /// Gets the total number of records (without filtering - total dataset).
        /// </summary>
        public int recordsTotal { get; private set; }

        /// <summary>
        /// Gets the resulting number of records after filtering.
        /// </summary>
        public int recordsFiltered { get; private set; }

        //public Paging Paging
        //{
        //    get
        //    {
        //        var pagedList = new PagedList<T>(Data,);
        //        return new Paging
        //        {
                   
        //        };
        //    }
        //}

        /// <summary>
        /// Creates a new DataTables response object with it's elements.
        /// </summary>
        /// <param name="draw">The Draw counter as received from the DataTablesRequest.</param>
        /// <param name="data">The Data collection (data page).</param>
        /// <param name="recordsFiltered">The resulting number of records after filtering.</param>
        /// <param name="recordsTotal">The total number of records (total dataset).</param>
        public DataTableJsResult(int draw, IEnumerable<T> data, int recordsFiltered, int recordsTotal)
        {
            this.draw = draw;
            this.data = data;
            this.recordsFiltered = recordsFiltered;
            this.recordsTotal = recordsTotal;
        }
    }
}