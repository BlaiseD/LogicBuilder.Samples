﻿using Contoso.Domain.Entities;
using Contoso.XPlatform.Maui.Tests.Helpers;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.Validatables;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.XPlatform.Maui.Tests
{
    public class PropertiesUpdaterTest
    {
        public PropertiesUpdaterTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapInstructorModelToIValidatableListWithInlineOfficeAssignment()
        {
            //arrange
            InstructorModel instructor = new InstructorModel
            {
                ID = 3,
                FirstName = "John",
                LastName = "Smith",
                HireDate = new DateTime(2021, 5, 20),
                OfficeAssignment = new OfficeAssignmentModel
                {
                    Location = "Location1"
                },
                Courses = new List<CourseAssignmentModel>
                {
                    new CourseAssignmentModel
                    {
                        CourseID = 1,
                        InstructorID = 2,
                        CourseTitle = "Chemistry"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 2,
                        InstructorID = 3,
                        CourseTitle = "Physics"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 3,
                        InstructorID = 4,
                        CourseTitle = "Mathematics"
                    }
                }
            };
            ObservableCollection<IValidatable> properties = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.InstructorFormWithInlineOfficeAssignment,
                typeof(InstructorModel)
            ).Properties;

            //act
            serviceProvider.GetRequiredService<IPropertiesUpdater>().UpdateProperties
            (
                properties,
                instructor,
                Descriptors.InstructorFormWithInlineOfficeAssignment.FieldSettings
            );

            //assert
            IDictionary<string, object?> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(3, propertiesDictionary["ID"]);
            Assert.Equal("John", propertiesDictionary["FirstName"]);
            Assert.Equal("Smith", propertiesDictionary["LastName"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["HireDate"]);
            Assert.Equal("Location1", propertiesDictionary["OfficeAssignment.Location"]);
            Assert.Equal("Chemistry", ((IEnumerable<CourseAssignmentModel>)propertiesDictionary["Courses"]!).First().CourseTitle);
        }

        [Fact]
        public void MapInstructorModelToIValidatableListWithPopupOfficeAssignment()
        {
            //arrange
            InstructorModel instructor = new InstructorModel
            {
                ID = 3,
                FirstName = "John",
                LastName = "Smith",
                HireDate = new DateTime(2021, 5, 20),
                OfficeAssignment = new OfficeAssignmentModel
                {
                    Location = "Location1"
                },
                Courses = new List<CourseAssignmentModel>
                {
                    new CourseAssignmentModel
                    {
                        CourseID = 1,
                        InstructorID = 2,
                        CourseTitle = "Chemistry"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 2,
                        InstructorID = 3,
                        CourseTitle = "Physics"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 3,
                        InstructorID = 4,
                        CourseTitle = "Mathematics"
                    }
                }
            };
            ObservableCollection<IValidatable> properties = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.InstructorFormWithPopupOfficeAssignment,
                typeof(InstructorModel)
            ).Properties;

            //act
            serviceProvider.GetRequiredService<IPropertiesUpdater>().UpdateProperties
            (
                properties,
                instructor,
                Descriptors.InstructorFormWithPopupOfficeAssignment.FieldSettings
            );

            //assert
            IDictionary<string, object?> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(3, propertiesDictionary["ID"]);
            Assert.Equal("John", propertiesDictionary["FirstName"]);
            Assert.Equal("Smith", propertiesDictionary["LastName"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["HireDate"]);
            Assert.Equal("Location1", ((OfficeAssignmentModel)propertiesDictionary["OfficeAssignment"]!).Location);
            Assert.Equal("Chemistry", ((IEnumerable<CourseAssignmentModel>)propertiesDictionary["Courses"]!).First().CourseTitle);
        }

        [Fact]
        public void MapDepartmentModelToIValidatableList()
        {
            //arrange
            DepartmentModel department = new DepartmentModel
            {
                DepartmentID = 1,
                Name = "Mathematics",
                Budget = 100000m,
                StartDate = new DateTime(2021, 5, 20),
                InstructorID = 1,
                Courses = new List<CourseModel>
                {
                    new CourseModel
                    {
                        CourseID = 1,
                        Credits = 3,
                        Title = "Trigonometry"
                    },
                    new CourseModel
                    {
                        CourseID = 2,
                        Credits = 4,
                        Title = "Physics"
                    },
                    new CourseModel
                    {
                        CourseID = 3,
                        Credits = 5,
                        Title = "Calculus"
                    }
                }
            };
            ObservableCollection<IValidatable> properties = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.DepartmentForm,
                typeof(DepartmentModel)
            ).Properties;

            //act
            serviceProvider.GetRequiredService<IPropertiesUpdater>().UpdateProperties
            (
                properties,
                department,
                Descriptors.DepartmentForm.FieldSettings
            );

            //assert
            IDictionary<string, object?> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);

            Assert.Equal(1, propertiesDictionary["DepartmentID"]);
            Assert.Equal("Mathematics", propertiesDictionary["Name"]);
            Assert.Equal(100000m, propertiesDictionary["Budget"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["StartDate"]);
            Assert.Equal(1, propertiesDictionary["InstructorID"]);
            Assert.Equal("Trigonometry", ((IEnumerable<CourseModel>)propertiesDictionary["Courses"]!).First().Title);
        }
    }
}
