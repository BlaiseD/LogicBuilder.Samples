﻿using Contoso.Domain.Entities;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Tests.Helpers;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.Validatables;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Contoso.XPlatform.Tests
{
    public class EntityUpdaterTest
    {
        public EntityUpdaterTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapIValidatableListModelWithInlineOfficeAssignmentToInstructor()
        {
            //arrange
            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.InstructorFormWithInlineOfficeAssignment,
                typeof(InstructorModel)
            )
            .CreateFields()
            .Properties;

            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["ID"].Value = 3;
            propertiesDictionary["FirstName"].Value = "John";
            propertiesDictionary["LastName"].Value = "Smith";
            propertiesDictionary["HireDate"].Value = new DateTime(2021, 5, 20);
            propertiesDictionary["OfficeAssignment.Location"].Value = "Location1";
            propertiesDictionary["Courses"].Value = new ObservableCollection<CourseAssignmentModel>
            (
                new List<CourseAssignmentModel>
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
                        InstructorID = 2,
                        CourseTitle = "Physics"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 2,
                        InstructorID = 2,
                        CourseTitle = "Mathematics"
                    }
                }
            );

            //act
            InstructorModel instructorModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (InstructorModel?)null,
                new Dictionary<string, object?>(),
                properties,
                Descriptors.InstructorFormWithInlineOfficeAssignment.FieldSettings
            );

            //assert
            Assert.Equal(3, instructorModel.ID);
            Assert.Equal("John", instructorModel.FirstName);
            Assert.Equal("Smith", instructorModel.LastName);
            Assert.Equal(new DateTime(2021, 5, 20), instructorModel.HireDate);
            Assert.Equal("Location1", instructorModel.OfficeAssignment.Location);
            Assert.Equal("Chemistry", instructorModel.Courses.First().CourseTitle);
        }

        [Fact]
        public void MapIValidatableListModelWithPopupOfficeAssignmentToInstructor()
        {
            //arrange
            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.InstructorFormWithPopupOfficeAssignment,
                typeof(InstructorModel)
            )
            .CreateFields()
            .Properties;

            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["ID"].Value = 3;
            propertiesDictionary["FirstName"].Value = "John";
            propertiesDictionary["LastName"].Value = "Smith";
            propertiesDictionary["HireDate"].Value = new DateTime(2021, 5, 20);
            propertiesDictionary["OfficeAssignment"].Value = new OfficeAssignmentModel { Location = "Location1" };
            propertiesDictionary["Courses"].Value = new ObservableCollection<CourseAssignmentModel>
            (
                new List<CourseAssignmentModel>
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
                        InstructorID = 2,
                        CourseTitle = "Physics"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 2,
                        InstructorID = 2,
                        CourseTitle = "Mathematics"
                    }
                }
            );

            //act
            InstructorModel instructorModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (InstructorModel?)null,
                new Dictionary<string, object?>(),
                properties,
                Descriptors.InstructorFormWithPopupOfficeAssignment.FieldSettings
            );/*does not return null*/

            //assert
            Assert.Equal(3, instructorModel.ID);
            Assert.Equal("John", instructorModel.FirstName);
            Assert.Equal("Smith", instructorModel.LastName);
            Assert.Equal(new DateTime(2021, 5, 20), instructorModel.HireDate);
            Assert.Equal("Location1", instructorModel.OfficeAssignment.Location);
            Assert.Equal("Chemistry", instructorModel.Courses.First().CourseTitle);
        }

        [Fact]
        public void MapIValidatableListModelToDepartment()
        {
            //arrange
            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.DepartmentForm,
                typeof(DepartmentModel)
            )
            .CreateFields()
            .Properties;

            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["DepartmentID"].Value = 1;
            propertiesDictionary["Name"].Value = "Mathematics";
            propertiesDictionary["Budget"].Value = 100000m;
            propertiesDictionary["StartDate"].Value = new DateTime(2021, 5, 20);
            propertiesDictionary["InstructorID"].Value = 1;
            propertiesDictionary["Courses"].Value = new ObservableCollection<CourseModel>
            (
                new List<CourseModel>
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
            );

            //act
            DepartmentModel departmentModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (DepartmentModel?)null,
                new Dictionary<string, object?>(),
                properties,
                Descriptors.DepartmentForm.FieldSettings
            );/*does not return null*/

            //assert
            Assert.Equal(1, departmentModel.DepartmentID);
            Assert.Equal("Mathematics", departmentModel.Name);
            Assert.Equal(100000m, departmentModel.Budget);
            Assert.Equal(new DateTime(2021, 5, 20), departmentModel.StartDate);
            Assert.Equal(1, departmentModel.InstructorID);
            Assert.Equal("Trigonometry", departmentModel.Courses.First().Title);
        }

        [Fact]
        public void MapIValidatableListModelToDepartment_WhenSingleValueFieldIsNotRequired()
        {
            //arrange
            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.DepartmentForm,
                typeof(DepartmentModel)
            )
            .CreateFields()
            .Properties;

            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["DepartmentID"].Value = 1;
            //propertiesDictionary["Name"].Value = "Mathematics";
            propertiesDictionary["Budget"].Value = 100000m;
            propertiesDictionary["StartDate"].Value = new DateTime(2021, 5, 20);
            propertiesDictionary["InstructorID"].Value = 1;
            propertiesDictionary["Courses"].Value = new ObservableCollection<CourseModel>
            (
                new List<CourseModel>
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
            );

            //act
            DepartmentModel departmentModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (DepartmentModel?)null,
                new Dictionary<string, object?>(),
                properties,
                Descriptors.DepartmentForm.FieldSettings
            );

            //assert
            Assert.Equal(1, departmentModel.DepartmentID);
            Assert.Equal("", departmentModel.Name);
            Assert.Equal(100000m, departmentModel.Budget);
            Assert.Equal(new DateTime(2021, 5, 20), departmentModel.StartDate);
            Assert.Equal(1, departmentModel.InstructorID);
            Assert.Equal("Trigonometry", departmentModel.Courses.First().Title);
        }

        [Fact]
        public void MapIValidatableListModelToDepartment_WhenSingleValueFieldIsDefaults()
        {
            //arrange
            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.DepartmentForm,
                typeof(DepartmentModel)
            )
            .CreateFields()
            .Properties;

            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);

            propertiesDictionary["Name"].Value = null;

            //act
            DepartmentModel departmentModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (DepartmentModel?)null,
                new Dictionary<string, object?>(),
                properties,
                Descriptors.DepartmentForm.FieldSettings
            );

            //assert
            Assert.Null(departmentModel.Name);
        }

        [Fact]
        public void MapIValidatableListModelWithInlineOfficeAssignmentToInstructor_WnenChildObjectInlineIsNotRequired()
        {
            //arrange
            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.InstructorFormWithInlineOfficeAssignment,
                typeof(InstructorModel)
            )
            .CreateFields()
            .Properties;

            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["ID"].Value = 3;
            propertiesDictionary["FirstName"].Value = "John";
            propertiesDictionary["LastName"].Value = "Smith";
            propertiesDictionary["HireDate"].Value = new DateTime(2021, 5, 20);
            //propertiesDictionary["OfficeAssignment.Location"].Value = "Location1";
            propertiesDictionary["Courses"].Value = new ObservableCollection<CourseAssignmentModel>
            (
                new List<CourseAssignmentModel>
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
                        InstructorID = 2,
                        CourseTitle = "Physics"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 2,
                        InstructorID = 2,
                        CourseTitle = "Mathematics"
                    }
                }
            );

            //act
            InstructorModel instructorModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (InstructorModel?)null,
                new Dictionary<string, object?>(),
                properties,
                Descriptors.InstructorFormWithInlineOfficeAssignment.FieldSettings
            );

            //assert
            Assert.Equal(3, instructorModel.ID);
            Assert.Equal("John", instructorModel.FirstName);
            Assert.Equal("Smith", instructorModel.LastName);
            Assert.Equal(new DateTime(2021, 5, 20), instructorModel.HireDate);
            Assert.Null(instructorModel.OfficeAssignment.Location);
            Assert.Equal("Chemistry", instructorModel.Courses.First().CourseTitle);
        }

        [Fact]
        public void MapIValidatableListModelWithInlineOfficeAssignmentToInstructor_WnenChildObjectInlineIsNull()
        {
            //arrange
            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.InstructorFormWithInlineOfficeAssignment,
                typeof(InstructorModel)
            )
            .CreateFields()
            .Properties;

            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["OfficeAssignment.Location"].Value = null;

            //act
            InstructorModel instructorModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (InstructorModel?)null,
                new Dictionary<string, object?>(),
                properties,
                Descriptors.InstructorFormWithInlineOfficeAssignment.FieldSettings
            );

            //assert
            Assert.Null(instructorModel.OfficeAssignment.Location);
        }

        [Fact]
        public void MapIValidatableListModelWithPopupOfficeAssignmentToInstructor_WnenChildObjectOpupIsNotRequired()
        {
            //arrange
            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.InstructorFormWithPopupOfficeAssignment,
                typeof(InstructorModel)
            )
            .CreateFields()
            .Properties;

            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["ID"].Value = 3;
            propertiesDictionary["FirstName"].Value = "John";
            propertiesDictionary["LastName"].Value = "Smith";
            propertiesDictionary["HireDate"].Value = new DateTime(2021, 5, 20);
            //propertiesDictionary["OfficeAssignment"].Value = new OfficeAssignmentModel { Location = "Location1" };
            propertiesDictionary["Courses"].Value = new ObservableCollection<CourseAssignmentModel>
            (
                new List<CourseAssignmentModel>
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
                        InstructorID = 2,
                        CourseTitle = "Physics"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 2,
                        InstructorID = 2,
                        CourseTitle = "Mathematics"
                    }
                }
            );

            //act
            InstructorModel instructorModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (InstructorModel?)null,
                new Dictionary<string, object?>(),
                properties,
                Descriptors.InstructorFormWithPopupOfficeAssignment.FieldSettings
            );

            //assert
            Assert.Equal(3, instructorModel.ID);
            Assert.Equal("John", instructorModel.FirstName);
            Assert.Equal("Smith", instructorModel.LastName);
            Assert.Equal(new DateTime(2021, 5, 20), instructorModel.HireDate);
            Assert.Null(instructorModel.OfficeAssignment);
            Assert.Equal("Chemistry", instructorModel.Courses.First().CourseTitle);
        }

        [Fact]
        public void MapIValidatableListModelWithPopupOfficeAssignmentToInstructor_WnenChildObjectOpupIsNull()
        {
            //arrange
            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.InstructorFormWithPopupOfficeAssignment,
                typeof(InstructorModel)
            )
            .CreateFields()
            .Properties;

            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["OfficeAssignment"].Value = null;

            //act
            InstructorModel instructorModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (InstructorModel?)null,
                new Dictionary<string, object?>(),
                properties,
                Descriptors.InstructorFormWithPopupOfficeAssignment.FieldSettings
            );

            //assert
            Assert.Null(instructorModel.OfficeAssignment);
        }

        [Fact]
        public void MapIValidatableListModelToInstructor_WnenMultiSelectFieldNotRequired()
        {
            //arrange
            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.InstructorFormWithPopupOfficeAssignment,
                typeof(InstructorModel)
            )
            .CreateFields()
            .Properties;

            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["ID"].Value = 3;
            propertiesDictionary["FirstName"].Value = "John";
            propertiesDictionary["LastName"].Value = "Smith";
            propertiesDictionary["HireDate"].Value = new DateTime(2021, 5, 20);

            //act
            InstructorModel instructorModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (InstructorModel?)null,
                new Dictionary<string, object?>(),
                properties,
                Descriptors.InstructorFormWithPopupOfficeAssignment.FieldSettings
            );

            //assert
            Assert.Equal(3, instructorModel.ID);
            Assert.Equal("John", instructorModel.FirstName);
            Assert.Equal("Smith", instructorModel.LastName);
            Assert.Equal(new DateTime(2021, 5, 20), instructorModel.HireDate);
            //Assert.Equal("Location1", instructorModel.OfficeAssignment.Location);
            Assert.Null(instructorModel.Courses);
        }

        [Fact]
        public void MapIValidatableListModelToInstructor_WnenMultiSelectFieldIsNull()
        {
            //arrange
            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.InstructorFormWithPopupOfficeAssignment,
                typeof(InstructorModel)
            )
            .CreateFields()
            .Properties;

            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["Courses"].Value = (ObservableCollection<CourseAssignmentModel>?)propertiesDictionary["Courses"].Value;

            //act
            InstructorModel instructorModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (InstructorModel?)null,
                new Dictionary<string, object?>(),
                properties,
                Descriptors.InstructorFormWithPopupOfficeAssignment.FieldSettings
            );

            //assert
            Assert.Null(instructorModel.Courses);
        }

        [Fact]
        public void MapIValidatableListModelToDepartment_WhenFormArrayIsNotRequired()
        {
            //arrange
            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.DepartmentForm,
                typeof(DepartmentModel)
            )
            .CreateFields()
            .Properties;

            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["DepartmentID"].Value = 1;
            propertiesDictionary["Name"].Value = "Mathematics";
            propertiesDictionary["Budget"].Value = 100000m;
            propertiesDictionary["StartDate"].Value = new DateTime(2021, 5, 20);
            propertiesDictionary["InstructorID"].Value = 1;

            //act
            DepartmentModel departmentModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (DepartmentModel?)null,
                new Dictionary<string, object?>(),
                properties,
                Descriptors.DepartmentForm.FieldSettings
            );

            //assert
            Assert.Equal(1, departmentModel.DepartmentID);
            Assert.Equal("Mathematics", departmentModel.Name);
            Assert.Equal(100000m, departmentModel.Budget);
            Assert.Equal(new DateTime(2021, 5, 20), departmentModel.StartDate);
            Assert.Equal(1, departmentModel.InstructorID);
            Assert.Empty(departmentModel.Courses);
        }

        private IFieldsCollectionBuilder GetFieldsCollectionBuilder(DataFormSettingsDescriptor dataFormSettingsDescriptor, Type modelType)
        {
            return serviceProvider.GetRequiredService<ICollectionBuilderFactory>().GetFieldsCollectionBuilder
            (
                modelType,
                dataFormSettingsDescriptor.FieldSettings,
                dataFormSettingsDescriptor,
                dataFormSettingsDescriptor.ValidationMessages,
                null,
                null
            );
        }
    }
}
