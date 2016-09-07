using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form.HexonetClient
{
    public class Domain
    {
        public Domain()
        {
            this.YearsPeriod = 1;
            this.TransferLock = false;
            this.X_eu_accepttrustee_tac = false;
        }

        public string DomainName { get; set; }

        public int YearsPeriod { get; set; }

        public string OwnerContact { get; set; }

        public string AdminContact { get; set; }

        public string TechContact { get; set; }

        public string BillingContact { get; set; }

        public string Nameserver { get; set; }

        public bool TransferLock { get; set; }

        public string Auth { get; set; }

        public bool X_eu_accepttrustee_tac { get; set; }

        public string X_eu_registrantlang { get; set; }

        public bool X_fr_accepttrustee_tac { get; set; }

        public DateTime X_fr_registrantbirth_date { get; set; }

        public string X_fr_registrantbirth_place { get; set; }

        public string X_fr_registranttrademarknumber { get; set; }

        public string X_fr_registrantlegal_form { get; set; }

        public string X_fr_registrantlegal_form_other { get; set; }

        public string X_fr_registrantlegal_id { get; set; }

        public DateTime X_fr_registrant_jodate_declaration { get; set; }

        public int X_fr_registrant_jodate_number { get; set; }

        public int X_fr_registrant_jodate_page { get; set; }

        public string X_idn_language { get; set; }

        public int X_it_pin { get; set; }

        public bool X_itconsentforpublishing { get; set; }

        public string X_jobscompanyurl { get; set; }

        public string X_nicse_vatid { get; set; }

        public bool X_nl_accepttrustee_tac { get; set; }

        public string X_pl_reason { get; set; }

        public string X_proregistrationtype { get; set; }
    }
}