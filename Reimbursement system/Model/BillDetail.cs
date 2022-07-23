using System.ComponentModel.DataAnnotations;

namespace Reimbursement_system.Model
{
    public class BillDetail
    {
        [Key]
        public int BillId { get; set; } 
        public string BillName { get; set; }= null!;

        public string Email{ get; set; } = null!;
        public int? ContactNumber { get; set; } 

        public DateTime? BillDate { get; set; }

        public string Reason { get; set; } = null!;

    }
}
