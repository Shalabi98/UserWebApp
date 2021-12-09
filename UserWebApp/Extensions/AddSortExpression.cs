using Kendo.Mvc;
using Kendo.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UserWebApp.ViewModels;

namespace UserWebApp.Extensions
{
    public static class AddSortExpression
    {
        public static IQueryable<StudentViewModel> ApplyOrdersSorting(this IQueryable<StudentViewModel> data,
                  IList<GroupDescriptor> groupDescriptors, IList<SortDescriptor> sortDescriptors)
        {
            if (groupDescriptors != null && groupDescriptors.Any())
            {
                foreach (var groupDescriptor in groupDescriptors.Reverse())
                {
                    data = AddSort(data, groupDescriptor.SortDirection, groupDescriptor.Member);
                }
            }

            if (sortDescriptors != null && sortDescriptors.Any())
            {
                foreach (SortDescriptor sortDescriptor in sortDescriptors)
                {
                    data = AddSort(data, sortDescriptor.SortDirection, sortDescriptor.Member);
                }
            }

            return data;
        }

        public static IQueryable<StudentViewModel> AddSort(IQueryable<StudentViewModel> data, ListSortDirection
                    sortDirection, string memberName)
        {
            if (sortDirection == ListSortDirection.Ascending)
            {
                switch (memberName)
                {
                    case "StudentID":
                        data = data.OrderBy(order => order.StudentId);
                        break;
                    case "Username":
                        data = data.OrderBy(order => order.Username);
                        break;
                    case "FirstName":
                        data = data.OrderBy(order => order.FirstName);
                        break;
                    case "LastName":
                        data = data.OrderBy(order => order.LastName);
                        break;
                    case "EnrollmentDate":
                        data = data.OrderBy(order => order.EnrollmentDate);
                        break;
                    case "Gender":
                        data = data.OrderBy(order => order.Gender);
                        break;
                    case "DateOfBirth":
                        data = data.OrderBy(order => order.DateOfBirth);
                        break;
                    case "Email":
                        data = data.OrderBy(order => order.Email);
                        break;
                    case "MobilePhoneNo":
                        data = data.OrderBy(order => order.MobilePhoneNo);
                        break;
                    case "City":
                        data = data.OrderBy(order => order.City);
                        break;
                    case "Address":
                        data = data.OrderBy(order => order.Address);
                        break;
                    case "IsGraduating":
                        data = data.OrderBy(order => order.IsGraduating);
                        break;
                }
            }
            else
            {
                switch (memberName)
                {
                    case "StudentID":
                        data = data.OrderByDescending(order => order.StudentId);
                        break;
                    case "Username":
                        data = data.OrderByDescending(order => order.Username);
                        break;
                    case "FirstName":
                        data = data.OrderByDescending(order => order.FirstName);
                        break;
                    case "LastName":
                        data = data.OrderByDescending(order => order.LastName);
                        break;
                    case "EnrollmentDate":
                        data = data.OrderByDescending(order => order.EnrollmentDate);
                        break;
                    case "Gender":
                        data = data.OrderByDescending(order => order.Gender);
                        break;
                    case "DateOfBirth":
                        data = data.OrderByDescending(order => order.DateOfBirth);
                        break;
                    case "Email":
                        data = data.OrderByDescending(order => order.Email);
                        break;
                    case "MobilePhoneNo":
                        data = data.OrderByDescending(order => order.MobilePhoneNo);
                        break;
                    case "City":
                        data = data.OrderByDescending(order => order.City);
                        break;
                    case "Address":
                        data = data.OrderByDescending(order => order.Address);
                        break;
                    case "IsGraduating":
                        data = data.OrderByDescending(order => order.IsGraduating);
                        break;
                }
            }
            return data;
        }

        //public static IQueryable<StudentViewModel> ApplyOrdersFiltering(this IQueryable<StudentViewModel> data,
        //   IList<IFilterDescriptor> filterDescriptors)
        //{
        //    if (filterDescriptors.Any())
        //    {
        //        data = data.Where(ExpressionBuilder.Expression<StudentViewModel>(filterDescriptors, false));
        //    }
        //    return data;
        //}
    }
}
