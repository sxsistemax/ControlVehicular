using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

//
// ASP.NET code-behind class (Page)
//

partial class Personasadd: AspNetMaker9_ControlVehicular {

	// Page object
	public cPersonas_add Personas_add;	

	//
	// Page Class
	//
	public class cPersonas_add: AspNetMakerPage, IDisposable {

		// Used by system generated functions
		private ArrayList RsWrk; // ArrayList of OrderedDictionary

		private DbDataReader drWrk; // DataReader

		private string sSqlWrk;

		private string sWhereWrk;

		private string sFilterWrk; 

		private string[] arwrk;

		private ArrayList alwrk;

		private OrderedDictionary odwrk;

		private string[] armultiwrk;

//		protected string m_DebugMsg = "";
//		public string DebugMsg {
//			get { return (ew_NotEmpty(m_DebugMsg)) ? "<p>" + m_DebugMsg + "</p>" : m_DebugMsg; }
//			set {
//				if (ew_NotEmpty(m_DebugMsg))	{	// Append
//					m_DebugMsg += "<br />" + value;
//				} else {
//					m_DebugMsg = value;
//				}
//			}
//		}
		// Show Message
		public void ShowMessage() {
			string sMessage = Message;
			Message_Showing(ref sMessage, "");
			if (ew_NotEmpty(sMessage)) {
				ew_Write("<p class=\"ewMessage\">" + sMessage + "</p>");
				ew_Session[EW_SESSION_MESSAGE] = ""; // Clear message in Session
			}

			// Success message
			string sSuccessMessage = SuccessMessage;
			Message_Showing(ref sSuccessMessage, "success");
			if (ew_NotEmpty(sSuccessMessage)) { // Message in Session, display
				ew_Write("<p class=\"ewSuccessMessage\">" + sSuccessMessage + "</p>");
				ew_Session[EW_SESSION_SUCCESS_MESSAGE] = ""; // Clear message in Session
			}

			// Failure message
			string sErrorMessage = FailureMessage;
			Message_Showing(ref sErrorMessage, "failure");
			if (ew_NotEmpty(sErrorMessage)) { // Message in Session, display
				ew_Write("<p class=\"ewErrorMessage\">" + sErrorMessage + "</p>");
				ew_Session[EW_SESSION_FAILURE_MESSAGE] = ""; // Clear message in Session
			}
		}

		// Page URL
		public string PageUrl {
			get {
				string Url = ew_CurrentPage() + "?";
				if (Personas.UseTokenInUrl)
					Url += "t=" + Personas.TableVar + "&"; // Add page token
				return Url;
			}
		}

		public string PageHeader = "";

		public string PageFooter = "";

		// Show Page Header
		public void ShowPageHeader() {
			string sHeader = PageHeader;
			Page_DataRendering(ref sHeader);
			if (ew_NotEmpty(sHeader)) // Header exists, display
				ew_Write("<p class=\"aspnetmaker\">" + sHeader + "</p>");
		}

		// Show Page Footer
		public void ShowPageFooter() {
			string sFooter = PageFooter;
			Page_DataRendered(ref sFooter);
			if (ew_NotEmpty(sFooter)) // Fotoer exists, display
				ew_Write("<p class=\"aspnetmaker\">" + sFooter + "</p>");
		}

		// Validate page request
		public bool IsPageRequest() {
			if (Personas.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (Personas.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (Personas.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public Personasadd AspNetPage { 
			get { return (Personasadd)m_ParentPage; }
		}

		// Personas	
		public cPersonas Personas { 
			get {				
				return ParentPage.Personas;
			}
			set {
				ParentPage.Personas = value;	
			}	
		}		

		// Areas	
		public cAreas Areas { 
			get {				
				return ParentPage.Areas;
			}
			set {
				ParentPage.Areas = value;	
			}	
		}		

		// Usuarios	
		public cUsuarios Usuarios { 
			get {				
				return ParentPage.Usuarios;
			}
			set {
				ParentPage.Usuarios = value;	
			}	
		}		

		// VehiculosAutorizados_grid	
		public cVehiculosAutorizados_grid VehiculosAutorizados_grid { 
			get {				
				return ParentPage.VehiculosAutorizados_grid;
			}
			set {
				ParentPage.VehiculosAutorizados_grid = value;	
			}	
		}		

		// VehiculosAutorizados	
		public cVehiculosAutorizados VehiculosAutorizados { 
			get {				
				return ParentPage.VehiculosAutorizados;
			}
			set {
				ParentPage.VehiculosAutorizados = value;	
			}	
		}		

		//
		//  Page class constructor
		//
		public cPersonas_add(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "add";
			m_PageObjName = "Personas_add";
			m_PageObjTypeName = "cPersonas_add";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (Personas == null)
				Personas = new cPersonas(this);
			if (Areas == null)
				Areas = new cAreas(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);
			if (VehiculosAutorizados_grid == null)
				VehiculosAutorizados_grid = new cVehiculosAutorizados_grid(ParentPage);
			if (VehiculosAutorizados == null)
				VehiculosAutorizados = new cVehiculosAutorizados(this);

			// Table
			m_TableName = "Personas";
			m_Table = Personas;
			CurrentTable = Personas;

			//CurrentTableType = Personas.GetType();
			// Initialize URLs
			// Connect to database

			if (Conn == null)
				Conn = new cConnection();
		}

		//
		//  Page_Init
		//
		public void Page_Init() {
			Security = new cAdvancedSecurity(this);
			if (!Security.IsLoggedIn()) Security.AutoLogin();
			if (!Security.IsLoggedIn()) {
				Security.SaveLastUrl();
				Page_Terminate("login.aspx");
			}

			// Table Permission loading event
			Security.TablePermission_Loading();
			Security.LoadCurrentUserLevel(TableName);

			// Table Permission loaded event
			Security.TablePermission_Loaded();
			if (!Security.IsLoggedIn()) {
				Security.SaveLastUrl();
				Page_Terminate("login.aspx");
			}
			if (!Security.CanAdd) {
				Security.SaveLastUrl();
				Page_Terminate("Personaslist.aspx");
			}

			// UserID_Loading event
			Security.UserID_Loading();
			if (Security.IsLoggedIn()) Security.LoadUserID();

			// UserID_Loaded event
			Security.UserID_Loaded();

			// Create form object
			ObjForm = new cFormObj();

			// Global page loading event (in ewglobal*.cs)
			ParentPage.Page_Loading();

			// Page load event, used in current page
			Page_Load();
		}

		//
		//  Class terminate
		//  - clean up page object
		//
		public void Dispose()	{
			Page_Terminate("");
		}

		//
		//  Sub Page_Terminate
		//  - called when exit page
		//  - clean up connection and objects
		//  - if URL specified, redirect to URL
		//
		public void Page_Terminate(string url) {

			// Page unload event, used in current page
			Page_Unload();

			// Global page unloaded event (in ewglobal*.cs)
			ParentPage.Page_Unloaded();

			// Go to URL if specified
			string sRedirectUrl = url;
			Page_Redirecting(ref sRedirectUrl);

			// Close connection
			Conn.Dispose();
			Personas.Dispose();
			Areas.Dispose();
			Usuarios.Dispose();
			VehiculosAutorizados_grid.Dispose();
			VehiculosAutorizados.Dispose();
			if (ew_NotEmpty(sRedirectUrl)) {
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.Redirect(sRedirectUrl); 
			}
		}

	public string DbMasterFilter;

	public string DbDetailFilter;

	public int Priv;

	public SqlDataReader OldRecordset = null;

	public bool CopyRecord;

	//
	// Page main processing
	//
	public void Page_Main()
	{

		// Set up master detail parameters
		SetUpMasterParms();

		// Process form if post back
		if (ew_NotEmpty(ObjForm.GetValue("a_add"))) {
			Personas.CurrentAction = ObjForm.GetValue("a_add"); // Get form action
			CopyRecord = LoadOldRecord(); // Load old recordset
			LoadFormValues(); // Load form values

			// Set up detail parameters
			SetUpDetailParms();

			// Validate Form
			if (!ValidateForm()) {
				Personas.CurrentAction = "I"; // Form error, reset action
				Personas.EventCancelled = true; // Event cancelled
				RestoreFormValues(); // Restore form values
				FailureMessage = gsFormError;
			}

		// Not post back
		} else {

			// Load key values from QueryString
			CopyRecord = true;
			if (ew_NotEmpty(ew_Get("IdPersona"))) {
				Personas.IdPersona.QueryStringValue = ew_Get("IdPersona");
				Personas.SetKey("IdPersona", Personas.IdPersona.CurrentValue); // Set up key
			} else {
				Personas.SetKey("IdPersona", ""); // Clear key
				CopyRecord = false;
			}
			if (CopyRecord) {
				Personas.CurrentAction = "C"; // Copy Record
			} else {
				Personas.CurrentAction = "I"; // Display Blank Record
				LoadDefaultValues(); // Load default values
			}
		}

		// Set up detail parameters
		SetUpDetailParms();

		// Perform action based on action code
		switch (Personas.CurrentAction) {
			case "I": // Blank record, no action required
				break;
			case "C": // Copy an existing record
				if (!LoadRow()) { // Load record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("Personaslist.aspx"); // No matching record, return to list
				}
				break;
			case "A": // Add new record
				Personas.SendEmail = true; // Send email on add success
				if (AddRow(Conn.GetRow(ref OldRecordset))) { // Add successful
					SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
					string sReturnUrl;
					if (ew_NotEmpty(Personas.CurrentDetailTable)) // Master/detail add
						sReturnUrl = Personas.DetailUrl;
					else
						sReturnUrl = Personas.ReturnUrl;
					if (ew_GetPageName(sReturnUrl) == "Personasview.aspx") sReturnUrl = Personas.ViewUrl; // View paging, return to view page with keyurl directly
					Page_Terminate(sReturnUrl); // Clean up and return
				} else {
					Personas.EventCancelled = true; // Event cancelled
					RestoreFormValues(); // Add failed, restore form values
				}
				break;
		}

		// Render row based on row type
		Personas.RowType = EW_ROWTYPE_ADD; // Render add type

		// Render row
		Personas.ResetAttrs();
		RenderRow();
	}

	// Confirm page
	public bool ConfirmPage = false;  // ASPX

	//
	// Get upload file
	//
	public void GetUploadFiles() {

		// Get upload data
		int index = ObjForm.Index; // Save form index
		ObjForm.Index = 0;
		bool confirmPage = ew_NotEmpty(ObjForm.GetValue("a_confirm"));
		ObjForm.Index = index; // Restore form index
	}

	//
	// Load default values
	//
	public void LoadDefaultValues() {
		Personas.IdArea.CurrentValue = System.DBNull.Value;
		Personas.IdArea.OldValue = Personas.IdArea.CurrentValue;
		Personas.IdCargo.CurrentValue = System.DBNull.Value;
		Personas.IdCargo.OldValue = Personas.IdCargo.CurrentValue;
		Personas.Documento.CurrentValue = System.DBNull.Value;
		Personas.Documento.OldValue = Personas.Documento.CurrentValue;
		Personas.Persona.CurrentValue = System.DBNull.Value;
		Personas.Persona.OldValue = Personas.Persona.CurrentValue;
		Personas.Activa.CurrentValue = System.DBNull.Value;
		Personas.Activa.OldValue = Personas.Activa.CurrentValue;
	}

	//
	// Load form values
	//
	public void LoadFormValues() {
		if (!Personas.IdArea.FldIsDetailKey) {
			Personas.IdArea.FormValue = ObjForm.GetValue("x_IdArea");
		}
		if (!Personas.IdCargo.FldIsDetailKey) {
			Personas.IdCargo.FormValue = ObjForm.GetValue("x_IdCargo");
		}
		if (!Personas.Documento.FldIsDetailKey) {
			Personas.Documento.FormValue = ObjForm.GetValue("x_Documento");
		}
		if (!Personas.Persona.FldIsDetailKey) {
			Personas.Persona.FormValue = ObjForm.GetValue("x_Persona");
		}
		if (!Personas.Activa.FldIsDetailKey) {
			Personas.Activa.FormValue = ObjForm.GetValue("x_Activa");
		}
	}

	//
	// Restore form values
	//
	public void RestoreFormValues() {
		LoadOldRecord();
		Personas.IdArea.CurrentValue = Personas.IdArea.FormValue;
		Personas.IdCargo.CurrentValue = Personas.IdCargo.FormValue;
		Personas.Documento.CurrentValue = Personas.Documento.FormValue;
		Personas.Persona.CurrentValue = Personas.Persona.FormValue;
		Personas.Activa.CurrentValue = Personas.Activa.FormValue;
		Personas.IdPersona.CurrentValue = Personas.IdPersona.FormValue;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = Personas.KeyFilter;

		// Row Selecting event
		Personas.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		Personas.CurrentFilter = sFilter;
		string sSql = Personas.SQL;

		// Write SQL for debug
		if (EW_DEBUG_ENABLED)
			ew_SetDebugMsg(sSql); // Show SQL for debugging
		try {
			RsRow = Conn.GetTempDataReader(sSql);
			if (!RsRow.Read()) {
				return false;
			}	else {
				LoadRowValues(ref RsRow);
				return true;
			}
		}	catch {
			if (EW_DEBUG_ENABLED) throw; 
			return false;
		}	finally {
			Conn.CloseTempDataReader();
		}
	}

	//
	// Load row values from recordset
	//
	public void LoadRowValues(ref SqlDataReader dr) {
		if (dr == null)
			return;
		string sDetailFilter;

		// Call Row Selected event
		OrderedDictionary row = Conn.GetRow(ref dr);
		Personas.Row_Selected(ref row);
		Personas.IdPersona.DbValue = row["IdPersona"];
		Personas.IdArea.DbValue = row["IdArea"];
		Personas.IdCargo.DbValue = row["IdCargo"];
		Personas.Documento.DbValue = row["Documento"];
		Personas.Persona.DbValue = row["Persona"];
		Personas.Activa.DbValue = (ew_ConvertToBool(row["Activa"])) ? "1" : "0";
	}

	// Load old record
	public bool LoadOldRecord() {

		// Load key values from Session
		bool bValidKey = true;
		if (ew_NotEmpty(Personas.GetKey("IdPersona")))
			Personas.IdPersona.CurrentValue = Personas.GetKey("IdPersona"); // IdPersona
		else
			bValidKey = false;

		// Load old recordset
		if (bValidKey) {
			Personas.CurrentFilter = Personas.KeyFilter;
			string sSql = Personas.SQL;			
			OldRecordset = Conn.GetTempDataReader(sSql);
			if (OldRecordset != null && OldRecordset.Read())
				LoadRowValues(ref OldRecordset); // Load row values
				return true;
		} else {
			OldRecordset = null;
		}
		return bValidKey;
	}

	//
	// Render row values based on field settings
	//
	public void RenderRow() {

		// Initialize urls
		// Row Rendering event

		Personas.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdPersona
		// IdArea
		// IdCargo
		// Documento
		// Persona
		// Activa
		//
		//  View  Row
		//

		if (Personas.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdPersona
				Personas.IdPersona.ViewValue = Personas.IdPersona.CurrentValue;
			Personas.IdPersona.ViewCustomAttributes = "";

			// IdArea
			if (ew_NotEmpty(Personas.IdArea.CurrentValue)) {
				sFilterWrk = "[IdArea] = " + ew_AdjustSql(Personas.IdArea.CurrentValue) + "";
			sSqlWrk = "SELECT [Area], [Codigo] FROM [dbo].[Areas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Area]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					Personas.IdArea.ViewValue = drWrk["Area"];
					Personas.IdArea.ViewValue = String.Concat(Personas.IdArea.ViewValue, ew_ValueSeparator(0, 1, Personas.IdArea), drWrk["Codigo"]);
				} else {
					Personas.IdArea.ViewValue = Personas.IdArea.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Personas.IdArea.ViewValue = System.DBNull.Value;
			}
			Personas.IdArea.ViewCustomAttributes = "";

			// IdCargo
			if (ew_NotEmpty(Personas.IdCargo.CurrentValue)) {
				sFilterWrk = "[IdCargo] = " + ew_AdjustSql(Personas.IdCargo.CurrentValue) + "";
			sSqlWrk = "SELECT [Cargo] FROM [dbo].[Cargos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Cargo]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					Personas.IdCargo.ViewValue = drWrk["Cargo"];
				} else {
					Personas.IdCargo.ViewValue = Personas.IdCargo.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Personas.IdCargo.ViewValue = System.DBNull.Value;
			}
			Personas.IdCargo.ViewCustomAttributes = "";

			// Documento
				Personas.Documento.ViewValue = Personas.Documento.CurrentValue;
			Personas.Documento.ViewCustomAttributes = "";

			// Persona
				Personas.Persona.ViewValue = Personas.Persona.CurrentValue;
			Personas.Persona.ViewCustomAttributes = "";

			// Activa
			if (Convert.ToString(Personas.Activa.CurrentValue) == "1") {
				Personas.Activa.ViewValue = (Personas.Activa.FldTagCaption(1) != "") ? Personas.Activa.FldTagCaption(1) : "Y";
			} else {
				Personas.Activa.ViewValue = (Personas.Activa.FldTagCaption(2) != "") ? Personas.Activa.FldTagCaption(2) : "N";
			}
			Personas.Activa.ViewCustomAttributes = "";

			// View refer script
			// IdArea

			Personas.IdArea.LinkCustomAttributes = "";
			Personas.IdArea.HrefValue = "";
			Personas.IdArea.TooltipValue = "";

			// IdCargo
			Personas.IdCargo.LinkCustomAttributes = "";
			Personas.IdCargo.HrefValue = "";
			Personas.IdCargo.TooltipValue = "";

			// Documento
			Personas.Documento.LinkCustomAttributes = "";
			Personas.Documento.HrefValue = "";
			Personas.Documento.TooltipValue = "";

			// Persona
			Personas.Persona.LinkCustomAttributes = "";
			Personas.Persona.HrefValue = "";
			Personas.Persona.TooltipValue = "";

			// Activa
			Personas.Activa.LinkCustomAttributes = "";
			Personas.Activa.HrefValue = "";
			Personas.Activa.TooltipValue = "";

		//
		//  Add Row
		//

		} else if (Personas.RowType == EW_ROWTYPE_ADD) { // Add row

			// IdArea
			Personas.IdArea.EditCustomAttributes = "";
			if (ew_NotEmpty(Personas.IdArea.SessionValue)) {
				Personas.IdArea.CurrentValue = Personas.IdArea.SessionValue;
			if (ew_NotEmpty(Personas.IdArea.CurrentValue)) {
				sFilterWrk = "[IdArea] = " + ew_AdjustSql(Personas.IdArea.CurrentValue) + "";
			sSqlWrk = "SELECT [Area], [Codigo] FROM [dbo].[Areas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Area]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					Personas.IdArea.ViewValue = drWrk["Area"];
					Personas.IdArea.ViewValue = String.Concat(Personas.IdArea.ViewValue, ew_ValueSeparator(0, 1, Personas.IdArea), drWrk["Codigo"]);
				} else {
					Personas.IdArea.ViewValue = Personas.IdArea.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				Personas.IdArea.ViewValue = System.DBNull.Value;
			}
			Personas.IdArea.ViewCustomAttributes = "";
			} else {
			sFilterWrk = "";
			sSqlWrk = "SELECT [IdArea], [Area] AS [DispFld], [Codigo] AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld] FROM [dbo].[Areas]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Area]";
			alwrk = Conn.GetRows(sSqlWrk);
			alwrk.Insert(0, new OrderedDictionary());
			((OrderedDictionary)alwrk[0]).Add("0", "");
			((OrderedDictionary)alwrk[0]).Add("1",  Language.Phrase("PleaseSelect"));
			((OrderedDictionary)alwrk[0]).Add("2", "");
			Personas.IdArea.EditValue = alwrk;
			}

			// IdCargo
			Personas.IdCargo.EditCustomAttributes = "";
			sFilterWrk = "";
			sSqlWrk = "SELECT [IdCargo], [Cargo] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld] FROM [dbo].[Cargos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [Cargo]";
			alwrk = Conn.GetRows(sSqlWrk);
			alwrk.Insert(0, new OrderedDictionary());
			((OrderedDictionary)alwrk[0]).Add("0", "");
			((OrderedDictionary)alwrk[0]).Add("1",  Language.Phrase("PleaseSelect"));
			Personas.IdCargo.EditValue = alwrk;

			// Documento
			Personas.Documento.EditCustomAttributes = "";
			Personas.Documento.EditValue = ew_HtmlEncode(Personas.Documento.CurrentValue);

			// Persona
			Personas.Persona.EditCustomAttributes = "";
			Personas.Persona.EditValue = ew_HtmlEncode(Personas.Persona.CurrentValue);

			// Activa
			Personas.Activa.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(Personas.Activa.FldTagCaption(1))) ? Personas.Activa.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(Personas.Activa.FldTagCaption(2))) ? Personas.Activa.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			Personas.Activa.EditValue = alwrk;

			// Edit refer script
			// IdArea

			Personas.IdArea.HrefValue = "";

			// IdCargo
			Personas.IdCargo.HrefValue = "";

			// Documento
			Personas.Documento.HrefValue = "";

			// Persona
			Personas.Persona.HrefValue = "";

			// Activa
			Personas.Activa.HrefValue = "";
		}
		if (Personas.RowType == EW_ROWTYPE_ADD ||
			Personas.RowType == EW_ROWTYPE_EDIT ||
			Personas.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			Personas.SetupFieldTitles();
		}

		// Row Rendered event
		if (Personas.RowType != EW_ROWTYPE_AGGREGATEINIT)
			Personas.Row_Rendered();
	}

	//
	// Validate form
	//
	public bool ValidateForm() {

		// Initialize
		gsFormError = "";

		// Check if validation required
		if (!EW_SERVER_VALIDATE) return (ew_Empty(gsFormError)); 
		if (ew_Empty(Personas.IdArea.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Personas.IdArea.FldCaption);
		if (ew_Empty(Personas.IdCargo.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Personas.IdCargo.FldCaption);
		if (ew_Empty(Personas.Documento.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Personas.Documento.FldCaption);
		if (ew_Empty(Personas.Persona.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Personas.Persona.FldCaption);
		if (ew_Empty(Personas.Activa.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + Personas.Activa.FldCaption);

		// Validate detail grid
		if (Personas.CurrentDetailTable == "VehiculosAutorizados" && VehiculosAutorizados.DetailAdd) {
			VehiculosAutorizados_grid = new cVehiculosAutorizados_grid(ParentPage); // Get detail page object (grid)
			VehiculosAutorizados_grid.ValidateGridForm();
			VehiculosAutorizados_grid.Dispose();
		}

		// Return validate result
		bool Valid = (ew_Empty(gsFormError));

		// Form_CustomValidate event
		string sFormCustomError = "";
		Valid = Valid && Form_CustomValidate(ref sFormCustomError);
		ew_AddMessage(ref gsFormError, sFormCustomError);
		return Valid;
	}

	//
	// Add record
	//
	public bool AddRow(OrderedDictionary RsOld) {
		bool result = false;
		OrderedDictionary RsNew = new OrderedDictionary();
		string sSql;
		string sFilter = "";
		bool bInsertRow;
		SqlDataReader RsChk = null;

		// Begin transaction
		if (ew_NotEmpty(Personas.CurrentDetailTable))
			Conn.BeginTrans();
		try {

		// IdArea
		Personas.IdArea.SetDbValue(ref RsNew, Personas.IdArea.CurrentValue, 0, false);

		// IdCargo
		Personas.IdCargo.SetDbValue(ref RsNew, Personas.IdCargo.CurrentValue, 0, false);

		// Documento
		Personas.Documento.SetDbValue(ref RsNew, Personas.Documento.CurrentValue, "", false);

		// Persona
		Personas.Persona.SetDbValue(ref RsNew, Personas.Persona.CurrentValue, "", false);

		// Activa
		Personas.Activa.SetDbValue(ref RsNew, (Personas.Activa.CurrentValue != "" && !Convert.IsDBNull(Personas.Activa.CurrentValue)), false, false);
		} catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw;
			FailureMessage = e.Message;
			return false;
		}

		// Row Inserting event
		bInsertRow = Personas.Row_Inserting(RsOld, ref RsNew);
		if (bInsertRow) {
			try {	
				Personas.Insert(ref RsNew);
				result = true;
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;		
				result = false;
			}
		} else {
			if (ew_NotEmpty(Personas.CancelMessage)) {
				FailureMessage = Personas.CancelMessage;
				Personas.CancelMessage = "";
			} else {
				FailureMessage = Language.Phrase("InsertCancelled");
			}
			result = false;
		}

		// Get insert ID if necessary
		if (result) {
			Personas.IdPersona.DbValue = Conn.GetLastInsertId();
			RsNew["IdPersona"] = Personas.IdPersona.DbValue;
		}

		// Add detail records
		if (result) {
			if (Personas.CurrentDetailTable == "VehiculosAutorizados" && VehiculosAutorizados.DetailAdd) {
			VehiculosAutorizados.IdPersona.SessionValue = Personas.IdPersona.CurrentValue; // Set master key
				ParentPage.VehiculosAutorizados_grid = new cVehiculosAutorizados_grid(ParentPage); // Get detail page object (grid)
				result = ParentPage.VehiculosAutorizados_grid.GridInsert();
				ParentPage.VehiculosAutorizados_grid.Dispose();
			}
		}

		// Commit/Rollback transaction
		if (ew_NotEmpty(Personas.CurrentDetailTable)) {
			if (result) {
				Conn.CommitTrans(); // Commit transaction
			} else {
				Conn.RollbackTrans(); // Rollback transaction
			}
		}
		if (result) {

			// Row Inserted event
			Personas.Row_Inserted(RsOld, RsNew);
		}
		return result;
	}

	// Set up master/detail based on QueryString
	public void SetUpMasterParms() {
		bool bValidMaster = false;
		string sMasterTblVar = "";

		// Get the keys for master table
		if (ew_NotEmpty(ew_Get(EW_TABLE_SHOW_MASTER))) {
			sMasterTblVar = ew_Get(EW_TABLE_SHOW_MASTER);
			if (ew_Empty(sMasterTblVar)) {
				bValidMaster = true;
				DbMasterFilter = "";
				DbDetailFilter = "";
			}
			if (sMasterTblVar == "Areas") {
				bValidMaster = true;				
				if (ew_NotEmpty(ew_Get("IdArea"))) {
					Areas.IdArea.QueryStringValue = ew_Get("IdArea");
					Personas.IdArea.QueryStringValue = Areas.IdArea.QueryStringValue;
					Personas.IdArea.SessionValue = Personas.IdArea.QueryStringValue;
					if (!Information.IsNumeric(Areas.IdArea.QueryStringValue)) bValidMaster = false;
				} else {
					bValidMaster = false;
				}
			}
		}
		if (bValidMaster) {

			// Save current master table
			Personas.CurrentMasterTable = sMasterTblVar;

			// Clear previous master session values
			if (sMasterTblVar != "Areas") {
				if (ew_Empty(Personas.IdArea.QueryStringValue)) Personas.IdArea.SessionValue = "";
			}
		}
		DbMasterFilter = Personas.MasterFilter; // Get master filter
		DbDetailFilter = Personas.DetailFilter; // Get detail filter
	}

	// Set up detail parms based on QueryString
	public void SetUpDetailParms() {
		bool bValidDetail = false;
		string sDetailTblVar = "";

		// Get the keys for master table
		if (HttpContext.Current.Request.QueryString[EW_TABLE_SHOW_DETAIL] != null) {
			sDetailTblVar = ew_Get(EW_TABLE_SHOW_DETAIL);
			Personas.CurrentDetailTable = sDetailTblVar;
		} else {
			sDetailTblVar = Personas.CurrentDetailTable;
		}
		if (ew_NotEmpty(sDetailTblVar)) {
			if (sDetailTblVar == "VehiculosAutorizados") {
				if (VehiculosAutorizados == null)
					VehiculosAutorizados = new cVehiculosAutorizados(this);
				if (VehiculosAutorizados.DetailAdd) {
					if (CopyRecord)
						VehiculosAutorizados.CurrentMode = "copy";
					else
						VehiculosAutorizados.CurrentMode = "add";
					VehiculosAutorizados.CurrentAction = "gridadd";

					// Save current master table to detail table
					VehiculosAutorizados.CurrentMasterTable = Personas.TableVar;
					VehiculosAutorizados.StartRecordNumber = 1;
					VehiculosAutorizados.IdPersona.FldIsDetailKey = true;
					VehiculosAutorizados.IdPersona.CurrentValue = Personas.IdPersona.CurrentValue;
					VehiculosAutorizados.IdPersona.SessionValue = VehiculosAutorizados.IdPersona.CurrentValue;
				}
			}
		}
	}

		// Page Load event
		public void Page_Load() {

			//HttpContext.Current.Response.Write("Page Load");
		}

		// Page Unload event
		public void Page_Unload() {

			//HttpContext.Current.Response.Write("Page Unload");
		}

		// Page Redirecting event
		public void Page_Redirecting(ref string url) {

			//url = newurl;
		}

		// Message Showing event
		// type = ""|"success"|"failure"
		public void Message_Showing(ref string msg, string type) {

			//msg = newmsg;
		}

		// Page Data Rendering event
		public void Page_DataRendering(ref string header) {

			// Example:
			//header = "your header";

		}

		// Page Data Rendered event
		public void Page_DataRendered(ref string footer) {

			// Example:
			//footer = "your footer";

		}

	// Form Custom Validate event
	public bool Form_CustomValidate(ref string CustomError) {

		//Return error message in CustomError
		return true;
	}
	}

	//
	// ASP.NET Page_Load event
	//

	protected void Page_Load(object sender, System.EventArgs e) {

		// Page init
		Personas_add = new cPersonas_add(this);
		NameValueCollection fv = new NameValueCollection();
		fv.Add(Request.Form);
		Session["EW_FORM_VALUES"] = fv;
		CurrentPage = Personas_add;

		//CurrentPageType = Personas_add.GetType();
		Personas_add.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		Personas_add.Page_Main();
	}

	//
	// ASP.NET Page_Unload event
	//

	protected void Page_Unload(object sender, System.EventArgs e) {
		if (Session["EW_FORM_VALUES"] != null) {
			((NameValueCollection)Session["EW_FORM_VALUES"]).Clear();
			Session.Remove("EW_FORM_VALUES");
		}

		// Dispose page object
		if (Personas_add != null)
			Personas_add.Dispose();
	}
}
