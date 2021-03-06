﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;

namespace Enrollment.Domain.Entities
{
    public class ContactInfoModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_UserId")]
		public int UserId { get; set; }

		[Display(Name = "Could your records be under another name?")]
		[Required]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_HasFormerName")]
		public bool HasFormerName { get; set; }

		[Display(Name = "Former First Name:")]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_FormerFirstName")]
		public string FormerFirstName { get; set; }

		[Display(Name = "Former Middle Name:")]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_FormerMiddleName")]
		public string FormerMiddleName { get; set; }

		[Display(Name = "Former Last Name:")]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_FormerLastName")]
		public string FormerLastName { get; set; }

		[Display(Name = "Date Of Birth:")]
		[Required]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_DateOfBirth")]
		public System.DateTime DateOfBirth { get; set; }

		[Display(Name = "Social Security #:")]
		[Required]
		[RegularExpression(@"^\d{3}-\d{2}-\d{4}$", MatchTimeoutInMilliseconds = 2000)]
		[StringLength(11)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_SocialSecurityNumber")]
		public string SocialSecurityNumber { get; set; }

        public string ConfirmSocialSecurityNumber { get => SocialSecurityNumber; set { } }

        [Display(Name = "Gender:")]
		[Required]
		[StringLength(1)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_Gender")]
		public string Gender { get; set; }

		[Display(Name = "Race:")]
		[Required]
		[StringLength(3)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_Race")]
		public string Race { get; set; }

		[Display(Name = "Ethnicity:")]
		[Required]
		[StringLength(3)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_Ethnicity")]
		public string Ethnicity { get; set; }

		[Display(Name = "Energency Contact First Name:")]
		[Required]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_EnergencyContactFirstName")]
		public string EnergencyContactFirstName { get; set; }

		[Display(Name = "Energency Contact Last Name:")]
		[Required]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_EnergencyContactLastName")]
		public string EnergencyContactLastName { get; set; }

		[Display(Name = "Energency Contact Relationship:")]
		[Required]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_EnergencyContactRelationship")]
		public string EnergencyContactRelationship { get; set; }

		[Display(Name = "Energency Contact Phone #:")]
		[RegularExpression(@"^\d{3}-\d{3}-\d{4}$", MatchTimeoutInMilliseconds = 2000)]
		[Required]
		[StringLength(12)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("ContactInfo_EnergencyContactPhoneNumber")]
		public string EnergencyContactPhoneNumber { get; set; }
    }
}