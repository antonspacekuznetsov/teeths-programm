//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Teeths
{
    using System;
    using System.Collections.Generic;
    
    public partial class GeneralPart
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string viewPlan { get; set; }
        public string curePlan { get; set; }
        public string cureFeatures { get; set; }
        public string signConsulation { get; set; }
        public string terms { get; set; }
    
        public virtual Client Client { get; set; }
    }
}
