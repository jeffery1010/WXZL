using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPWeb.Entity.BasicInfo
{
    /// <summary>
    /// Goods
    /// </summary>
    [Table("Goods",Schema ="meta")]
    public class Goods
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public Int32 Id { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public String Code { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Spec
        /// </summary>
        public String Spec { get; set; }

        /// <summary>
        /// Barcode
        /// </summary>
        public String Barcode { get; set; }

        /// <summary>
        /// BatchSN
        /// </summary>
        public Boolean? BatchSN { get; set; }

        /// <summary>
        /// ShortSpell
        /// </summary>
        public String ShortSpell { get; set; }

        /// <summary>
        /// SortId
        /// </summary>
        public Int32? SortId { get; set; }

        /// <summary>
        /// Factstorage
        /// </summary>
        public Double RealQty { get; set; }

        /// <summary>
        /// AccountStoQty
        /// </summary>
        public Double AccountQty { get; set; }

        /// <summary>
        /// PorpertyId
        /// </summary>
        public Int32? PorpertyId { get; set; }

        /// <summary>
        /// SaveId
        /// </summary>
        public Int32? SaveId { get; set; }

        /// <summary>
        /// CostCheckId
        /// </summary>
        public Int32? CostCheckId { get; set; }

        /// <summary>
        /// StockUpper
        /// </summary>
        public Double StockUpper { get; set; }

        /// <summary>
        /// StockLower
        /// </summary>
        public Double StockLower { get; set; }

        /// <summary>
        /// StockQty
        /// </summary>
        public Decimal StockQty { get; set; }

        /// <summary>
        /// ClearPrice
        /// </summary>
        public Double ClearPrice { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        public String Unit { get; set; }

        /// <summary>
        /// UnitRate
        /// </summary>
        public Double UnitRate { get; set; }

        /// <summary>
        /// ShortName
        /// </summary>
        public String ShortName { get; set; }

        /// <summary>
        /// IsInvalid
        /// </summary>
        public Boolean IsInvalid { get; set; }

        /// <summary>
        /// QueryQty
        /// </summary>
        public Int32 QueryQty { get; set; }

        /// <summary>
        /// ProduceDate
        /// </summary>
        public DateTime? ProduceDate { get; set; }

        /// <summary>
        /// EffectDate
        /// </summary>
        public DateTime? EffectDate { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// CreateUserNo
        /// </summary>
        public String CreateUserNo { get; set; }

        /// <summary>
        /// ChUserNo
        /// </summary>
        public String ChUserNo { get; set; }

        /// <summary>
        /// Chtime
        /// </summary>
        public DateTime? Chtime { get; set; }

        /// <summary>
        /// UnitWeight
        /// </summary>
        public Double UnitWeight { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public Decimal? Amount { get; set; }

        /// <summary>
        /// IsBUnit
        /// </summary>
        public Boolean? IsBUnit { get; set; }

        /// <summary>
        /// Image
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// RetailPrice
        /// </summary>
        public Double RetailPrice { get; set; }

        /// <summary>
        /// BatchPrice
        /// </summary>
        public Double BatchPrice { get; set; }

        /// <summary>
        /// MemberPrice
        /// </summary>
        public Double MemberPrice { get; set; }

        /// <summary>
        /// PromotePrice
        /// </summary>
        public Double PromotePrice { get; set; }

        /// <summary>
        /// PurchasePrice
        /// </summary>
        public Double PurchasePrice { get; set; }

        /// <summary>
        /// ReservedPrice
        /// </summary>
        public Double ReservedPrice { get; set; }

        /// <summary>
        /// Degree
        /// </summary>
        public Int32? Degree { get; set; }

        /// <summary>
        /// ExtendF1
        /// </summary>
        public String ExtendF1 { get; set; }

        /// <summary>
        /// ExtendF2
        /// </summary>
        public String ExtendF2 { get; set; }

        /// <summary>
        /// ExtendF3
        /// </summary>
        public String ExtendF3 { get; set; }

        /// <summary>
        /// SortCode
        /// </summary>
        public String SortCode { get; set; }

        /// <summary>
        /// PropertyName
        /// </summary>
        public String PropertyName { get; set; }

        /// <summary>
        /// TradeUnitId
        /// </summary>
        public Int32? TradeUnitId { get; set; }

        /// <summary>
        /// QualityDate
        /// </summary>
        public Int32? QualityDate { get; set; }

        /// <summary>
        /// QualityKind
        /// </summary>
        public String QualityKind { get; set; }

        /// <summary>
        /// TaxRate
        /// </summary>
        public Double TaxRate { get; set; }

        /// <summary>
        /// Flag
        /// </summary>
        public Boolean? Flag { get; set; }

    }
}