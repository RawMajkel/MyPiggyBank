﻿using MyPiggyBank.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// based on https://code-maze.com/paging-aspnet-core-webapi/
namespace MyPiggyBank.Data.Repository
{
	public class PagedList<T> : List<T> {
		public int CurrentPage { get; private set; }
		public int TotalPages { get; private set; }
		public int PageSize { get; private set; }
		public int TotalCount { get; private set; }

		public bool HasPrevious => CurrentPage > 1;
		public bool HasNext => CurrentPage < TotalPages;
		public object PagingData()
			=> new {
				TotalCount,
				PageSize,
				CurrentPage,
				TotalPages,
				HasNext,
				HasPrevious
			};

		public PagedList(List<T> items, int count, int pageNumber, int pageSize) {
			TotalCount = count;
			PageSize = pageSize;
			CurrentPage = pageNumber;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);

			AddRange(items);
		}

		public static object ToPagedList(IQueryable<Resource> queryable) {
			throw new NotImplementedException();
		}

		public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize) {
			var count = source.Count();
			var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

			return new PagedList<T>(items, count, pageNumber, pageSize);
		}
	}
}
