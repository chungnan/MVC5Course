namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MVC5Course.Models.InputValidations;

    [MetadataType(typeof(ClientMetaData))]
    public partial class Client : IValidatableObject
    {
        // 實作模型驗證
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 通常此判斷為當資料為新增時需要判斷的模型驗證
            if (this.ClientId == 0)
            {
                // 實作模型驗證
            }

            if (this.Longitude.HasValue != this.Latitude.HasValue)
            {
                yield return new ValidationResult("經緯度欄位必須一起設定", new string[] { "Longitude", "Latitude" });
            }

            // 模型驗證可以放入多組 下方為示範
            // 模型驗證為後端驗證，因此送出後才會顯示錯誤訊息內容
            //if (this.DateOfBirth.Value.Year > 1980 && this.City == "Taipei")
            //{
            //    yield return new ValidationResult("條件錯誤", new string[] { "DateOfBirth", "City" });
            //}
        }

        partial void Init()
        {
        }
    }

    public partial class ClientMetaData
    {
        [Required]
        public int ClientId { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "欄位長度不得大於 40 個字元")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "欄位長度不得大於 40 個字元")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "欄位長度不得大於 40 個字元")]
        public string LastName { get; set; }

        [Required]
        [StringLength(1, ErrorMessage = "欄位長度不得大於 1 個字元")]
        public string Gender { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [Required]
        public Nullable<double> CreditRating { get; set; }

        [StringLength(7, ErrorMessage = "欄位長度不得大於 7 個字元")]
        public string XCode { get; set; }

        public Nullable<int> OccupationId { get; set; }

        [StringLength(20, ErrorMessage = "欄位長度不得大於 20 個字元")]
        public string TelephoneNumber { get; set; }

        [StringLength(100, ErrorMessage = "欄位長度不得大於 100 個字元")]
        public string Street1 { get; set; }

        [StringLength(100, ErrorMessage = "欄位長度不得大於 100 個字元")]
        public string Street2 { get; set; }

        [StringLength(100, ErrorMessage = "欄位長度不得大於 100 個字元")]
        public string City { get; set; }

        [StringLength(15, ErrorMessage = "欄位長度不得大於 15 個字元")]
        public string ZipCode { get; set; }

        public Nullable<double> Longitude { get; set; }

        public Nullable<double> Latitude { get; set; }

        public string Notes { get; set; }

        [IdentificationId]
        public string IdNumber { get; set; }

        public bool Active { get; set; }

        public virtual Occupation Occupation { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
