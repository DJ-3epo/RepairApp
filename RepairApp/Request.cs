//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RepairApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Request
    {
        public int RequestID { get; set; }
        public System.DateTime RequestDate { get; set; }
        public int EquipmentID { get; set; }
        public int ClientID { get; set; }
        public int StatusID { get; set; }
        public string FaultType { get; set; }
        public string ProblemDescription { get; set; }
        public Nullable<int> WorkerID { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual RequestStatus RequestStatus { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
