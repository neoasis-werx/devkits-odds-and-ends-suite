// <auto-generated/>
#nullable enable
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace DevKits.Data.Tests.Databases.ChiLaborProductivity;
using DevKits.Data;
using DevKits.Data.Core;


public static partial class LaborProductivity
{
    public static class RawEmrEpicClarity {
        public static readonly string Name = "rawEmrEpicClarity";

        // TABLES
        public static readonly QualifiedTableName EpicArpbTransactions = "rawEmrEpicClarity.Epic_ARPB_TRANSACTIONS";
        public static readonly QualifiedTableName EpicClCostCntr = "rawEmrEpicClarity.Epic_CL_COST_CNTR";
        public static readonly QualifiedTableName EpicClarityAdt = "rawEmrEpicClarity.Epic_CLARITY_ADT";
        public static readonly QualifiedTableName EpicClarityDep = "rawEmrEpicClarity.Epic_CLARITY_DEP";
        public static readonly QualifiedTableName EpicClarityTdlTran = "rawEmrEpicClarity.Epic_CLARITY_TDL_TRAN";
        public static readonly QualifiedTableName EpicClarityUcl = "rawEmrEpicClarity.Epic_CLARITY_UCL";
        public static readonly QualifiedTableName EpicHspAccount = "rawEmrEpicClarity.Epic_HSP_ACCOUNT";
        public static readonly QualifiedTableName EpicHspAcctCptCodes = "rawEmrEpicClarity.Epic_HSP_ACCT_CPT_CODES";
        public static readonly QualifiedTableName EpicHspTransactions = "rawEmrEpicClarity.Epic_HSP_TRANSACTIONS";
        public static readonly QualifiedTableName EpicOrCase = "rawEmrEpicClarity.Epic_OR_CASE";
        public static readonly QualifiedTableName EpicOrCaseSchedHist = "rawEmrEpicClarity.Epic_OR_CASE_SCHED_HIST";
        public static readonly QualifiedTableName EpicOrLnlgSrgstfTms = "rawEmrEpicClarity.Epic_OR_LNLG_SRGSTF_TMS";
        public static readonly QualifiedTableName EpicOrLnlgSurgStaff = "rawEmrEpicClarity.Epic_OR_LNLG_SURG_STAFF";
        public static readonly QualifiedTableName EpicOrLog = "rawEmrEpicClarity.Epic_OR_LOG";
        public static readonly QualifiedTableName EpicOrLogCaseTimes = "rawEmrEpicClarity.Epic_OR_LOG_CASE_TIMES";
        public static readonly QualifiedTableName EpicOrUtilBlock = "rawEmrEpicClarity.Epic_OR_UTIL_BLOCK";
        public static readonly QualifiedTableName EpicPatEnc = "rawEmrEpicClarity.Epic_PAT_ENC";
        public static readonly QualifiedTableName EpicPatEncHsp = "rawEmrEpicClarity.Epic_PAT_ENC_HSP";
        public static readonly QualifiedTableName EpicZcAcctBaseclsHa = "rawEmrEpicClarity.Epic_ZC_ACCT_BASECLS_HA";
        public static readonly QualifiedTableName EpicZcAcctClassHa = "rawEmrEpicClarity.Epic_ZC_ACCT_CLASS_HA";
        public static readonly QualifiedTableName EpicZcDischDisp = "rawEmrEpicClarity.Epic_ZC_DISCH_DISP";
        public static readonly QualifiedTableName EpicZcEventSubtype = "rawEmrEpicClarity.Epic_ZC_EVENT_SUBTYPE";
        public static readonly QualifiedTableName EpicZcEventType = "rawEmrEpicClarity.Epic_ZC_EVENT_TYPE";
        public static readonly QualifiedTableName EpicZcMcAdmType = "rawEmrEpicClarity.Epic_ZC_MC_ADM_TYPE";
        public static readonly QualifiedTableName EpicZcPatClass = "rawEmrEpicClarity.Epic_ZC_PAT_CLASS";
        public static readonly QualifiedTableName ZwipEpicArpbTransactions = "rawEmrEpicClarity.ZWIP_Epic_ARPB_TRANSACTIONS";
        public static readonly QualifiedTableName ZwipEpicClarityAdt = "rawEmrEpicClarity.ZWIP_Epic_CLARITY_ADT";
        public static readonly QualifiedTableName ZwipEpicClarityDep = "rawEmrEpicClarity.ZWIP_Epic_CLARITY_DEP";
        public static readonly QualifiedTableName ZwipEpicClarityUcl = "rawEmrEpicClarity.ZWIP_Epic_CLARITY_UCL";
        public static readonly QualifiedTableName ZwipEpicHspAccount = "rawEmrEpicClarity.ZWIP_Epic_HSP_ACCOUNT";
        public static readonly QualifiedTableName ZwipEpicHspAcctCptCodes = "rawEmrEpicClarity.ZWIP_Epic_HSP_ACCT_CPT_CODES";
        public static readonly QualifiedTableName ZwipEpicHspTransactions = "rawEmrEpicClarity.ZWIP_Epic_HSP_TRANSACTIONS";
        public static readonly QualifiedTableName ZwipEpicOrCase = "rawEmrEpicClarity.ZWIP_Epic_OR_CASE";
        public static readonly QualifiedTableName ZwipEpicOrCaseSchedHist = "rawEmrEpicClarity.ZWIP_Epic_OR_CASE_SCHED_HIST";
        public static readonly QualifiedTableName ZwipEpicOrLnlgSrgstfTms = "rawEmrEpicClarity.ZWIP_Epic_OR_LNLG_SRGSTF_TMS";
        public static readonly QualifiedTableName ZwipEpicOrLnlgSurgStaff = "rawEmrEpicClarity.ZWIP_Epic_OR_LNLG_SURG_STAFF";
        public static readonly QualifiedTableName ZwipEpicOrLog = "rawEmrEpicClarity.ZWIP_Epic_OR_LOG";
        public static readonly QualifiedTableName ZwipEpicOrLogCaseTimes = "rawEmrEpicClarity.ZWIP_Epic_OR_LOG_CASE_TIMES";
        public static readonly QualifiedTableName ZwipEpicOrUtilBlock = "rawEmrEpicClarity.ZWIP_Epic_OR_UTIL_BLOCK";
        public static readonly QualifiedTableName ZwipEpicPatEnc = "rawEmrEpicClarity.ZWIP_Epic_PAT_ENC";
        public static readonly QualifiedTableName ZwipEpicPatEncHsp = "rawEmrEpicClarity.ZWIP_Epic_PAT_ENC_HSP";
        public static readonly QualifiedTableName ZwipEpicZcAcctBaseclsHa = "rawEmrEpicClarity.ZWIP_Epic_ZC_ACCT_BASECLS_HA";
        public static readonly QualifiedTableName ZwipEpicZcAcctClassHa = "rawEmrEpicClarity.ZWIP_Epic_ZC_ACCT_CLASS_HA";
        public static readonly QualifiedTableName ZwipEpicZcDischDisp = "rawEmrEpicClarity.ZWIP_Epic_ZC_DISCH_DISP";
        public static readonly QualifiedTableName ZwipEpicZcEventSubtype = "rawEmrEpicClarity.ZWIP_Epic_ZC_EVENT_SUBTYPE";
        public static readonly QualifiedTableName ZwipEpicZcEventType = "rawEmrEpicClarity.ZWIP_Epic_ZC_EVENT_TYPE";
        public static readonly QualifiedTableName ZwipEpicZcMcAdmType = "rawEmrEpicClarity.ZWIP_Epic_ZC_MC_ADM_TYPE";
    }

}
// Rev: V20240315023146 | Database: DCM_thi_productivity_impl
