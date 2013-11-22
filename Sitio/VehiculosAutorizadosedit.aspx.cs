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

partial class VehiculosAutorizadosedit: AspNetMaker9_ControlVehicular {

	// Page object
	public cVehiculosAutorizados_edit VehiculosAutorizados_edit;	

	//
	// Page Class
	//
	public class cVehiculosAutorizados_edit: AspNetMakerPage, IDisposable {

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
				if (VehiculosAutorizados.UseTokenInUrl)
					Url += "t=" + VehiculosAutorizados.TableVar + "&"; // Add page token
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
			if (VehiculosAutorizados.UseTokenInUrl)	{
				bool Result = false;
				if (ObjForm != null)
					Result = (VehiculosAutorizados.TableVar == ObjForm.GetValue("t"));
				if (ew_NotEmpty(ew_Get("t")))
					Result = (VehiculosAutorizados.TableVar == ew_Get("t"));
				return Result;
			}
			return true;
		}

		// ASP.NET page object
		public VehiculosAutorizadosedit AspNetPage { 
			get { return (VehiculosAutorizadosedit)m_ParentPage; }
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

		// Personas	
		public cPersonas Personas { 
			get {				
				return ParentPage.Personas;
			}
			set {
				ParentPage.Personas = value;	
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

		// VehiculosPicoYPlacaHoras_grid	
		public cVehiculosPicoYPlacaHoras_grid VehiculosPicoYPlacaHoras_grid { 
			get {				
				return ParentPage.VehiculosPicoYPlacaHoras_grid;
			}
			set {
				ParentPage.VehiculosPicoYPlacaHoras_grid = value;	
			}	
		}		

		// VehiculosPicoYPlacaHoras	
		public cVehiculosPicoYPlacaHoras VehiculosPicoYPlacaHoras { 
			get {				
				return ParentPage.VehiculosPicoYPlacaHoras;
			}
			set {
				ParentPage.VehiculosPicoYPlacaHoras = value;	
			}	
		}		

		//
		//  Page class constructor
		//
		public cVehiculosAutorizados_edit(AspNetMaker9_ControlVehicular APage) {		
			m_ParentPage = APage;
			m_Page = this;
			m_PageID = "edit";
			m_PageObjName = "VehiculosAutorizados_edit";
			m_PageObjTypeName = "cVehiculosAutorizados_edit";

			// Initialize language object
			if (Language == null)
				Language = new cLanguage(this);

			// Initialize table object
			if (VehiculosAutorizados == null)
				VehiculosAutorizados = new cVehiculosAutorizados(this);
			if (Personas == null)
				Personas = new cPersonas(this);
			if (Usuarios == null)
				Usuarios = new cUsuarios(this);
			if (VehiculosPicoYPlacaHoras_grid == null)
				VehiculosPicoYPlacaHoras_grid = new cVehiculosPicoYPlacaHoras_grid(ParentPage);
			if (VehiculosPicoYPlacaHoras == null)
				VehiculosPicoYPlacaHoras = new cVehiculosPicoYPlacaHoras(this);

			// Table
			m_TableName = "VehiculosAutorizados";
			m_Table = VehiculosAutorizados;
			CurrentTable = VehiculosAutorizados;

			//CurrentTableType = VehiculosAutorizados.GetType();
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
			if (!Security.CanEdit) {
				Security.SaveLastUrl();
				Page_Terminate("VehiculosAutorizadoslist.aspx");
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
			VehiculosAutorizados.Dispose();
			Personas.Dispose();
			Usuarios.Dispose();
			VehiculosPicoYPlacaHoras_grid.Dispose();
			VehiculosPicoYPlacaHoras.Dispose();
			if (ew_NotEmpty(sRedirectUrl)) {
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.Redirect(sRedirectUrl); 
			}
		}

	public string DbMasterFilter = "";

	public string DbDetailFilter = ""; 

	//
	// Page main processing
	//
	public void Page_Main()
	{

		// Load key from QueryString
		if (ew_NotEmpty(ew_Get("IdVehiculoAutorizado"))) {
			VehiculosAutorizados.IdVehiculoAutorizado.QueryStringValue = ew_Get("IdVehiculoAutorizado");
		}

		// Set up master detail parameters
		SetUpMasterParms();
		if (ew_NotEmpty(ObjForm.GetValue("a_edit"))) {
			VehiculosAutorizados.CurrentAction = ObjForm.GetValue("a_edit"); // Get action code
			LoadFormValues(); // Get form values

			// Set up detail parameters
			SetUpDetailParms();

			// Validate Form
			if (!ValidateForm()) {
				VehiculosAutorizados.CurrentAction = ""; // Form error, reset action
				FailureMessage = gsFormError;
				VehiculosAutorizados.EventCancelled = true; // Event cancelled 
				RestoreFormValues(); // Restore form values if validate failed
			}
		} else {
			VehiculosAutorizados.CurrentAction = "I"; // Default action is display
		}

		// Check if valid key
		if (ew_Empty(VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue)) Page_Terminate("VehiculosAutorizadoslist.aspx"); // Invalid key, return to list

		// Set up detail parameters
		SetUpDetailParms();
		switch (VehiculosAutorizados.CurrentAction) {
			case "I": // Get a record to display
				if (!LoadRow()) { // Load Record based on key
					FailureMessage = Language.Phrase("NoRecord"); // No record found
					Page_Terminate("VehiculosAutorizadoslist.aspx"); // No matching record, return to list
				}
				break;
			case "U": // Update
				VehiculosAutorizados.SendEmail = true; // Send email on update success
				if (EditRow()) { // Update Record based on key
					SuccessMessage = Language.Phrase("UpdateSuccess"); // Update success
					string sReturnUrl;
					if (ew_NotEmpty(VehiculosAutorizados.CurrentDetailTable)) // Master/detail edit
						sReturnUrl = VehiculosAutorizados.DetailUrl;
					else
						sReturnUrl = VehiculosAutorizados.ReturnUrl;
					Page_Terminate(sReturnUrl); // Return to caller
				} else {
					VehiculosAutorizados.EventCancelled = true;
					RestoreFormValues(); // Restore form values if update failed
				}
				break;
		}

		// Render the record
		VehiculosAutorizados.RowType = EW_ROWTYPE_EDIT; // Render as edit

		// Render row
		VehiculosAutorizados.ResetAttrs();
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
	// Load form values
	//
	public void LoadFormValues() {
		if (!VehiculosAutorizados.IdVehiculoAutorizado.FldIsDetailKey)
			VehiculosAutorizados.IdVehiculoAutorizado.FormValue = ObjForm.GetValue("x_IdVehiculoAutorizado");
		if (!VehiculosAutorizados.IdTipoVehiculo.FldIsDetailKey) {
			VehiculosAutorizados.IdTipoVehiculo.FormValue = ObjForm.GetValue("x_IdTipoVehiculo");
		}
		if (!VehiculosAutorizados.Placa.FldIsDetailKey) {
			VehiculosAutorizados.Placa.FormValue = ObjForm.GetValue("x_Placa");
		}
		if (!VehiculosAutorizados.Autorizado.FldIsDetailKey) {
			VehiculosAutorizados.Autorizado.FormValue = ObjForm.GetValue("x_Autorizado");
		}
		if (!VehiculosAutorizados.IdPersona.FldIsDetailKey) {
			VehiculosAutorizados.IdPersona.FormValue = ObjForm.GetValue("x_IdPersona");
		}
		if (!VehiculosAutorizados.PicoyPlaca.FldIsDetailKey) {
			VehiculosAutorizados.PicoyPlaca.FormValue = ObjForm.GetValue("x_PicoyPlaca");
		}
		if (!VehiculosAutorizados.Lunes.FldIsDetailKey) {
			VehiculosAutorizados.Lunes.FormValue = ObjForm.GetValue("x_Lunes");
		}
		if (!VehiculosAutorizados.Martes.FldIsDetailKey) {
			VehiculosAutorizados.Martes.FormValue = ObjForm.GetValue("x_Martes");
		}
		if (!VehiculosAutorizados.Miercoles.FldIsDetailKey) {
			VehiculosAutorizados.Miercoles.FormValue = ObjForm.GetValue("x_Miercoles");
		}
		if (!VehiculosAutorizados.Jueves.FldIsDetailKey) {
			VehiculosAutorizados.Jueves.FormValue = ObjForm.GetValue("x_Jueves");
		}
		if (!VehiculosAutorizados.Viernes.FldIsDetailKey) {
			VehiculosAutorizados.Viernes.FormValue = ObjForm.GetValue("x_Viernes");
		}
		if (!VehiculosAutorizados.Sabado.FldIsDetailKey) {
			VehiculosAutorizados.Sabado.FormValue = ObjForm.GetValue("x_Sabado");
		}
		if (!VehiculosAutorizados.Domingo.FldIsDetailKey) {
			VehiculosAutorizados.Domingo.FormValue = ObjForm.GetValue("x_Domingo");
		}
		if (!VehiculosAutorizados.Marca.FldIsDetailKey) {
			VehiculosAutorizados.Marca.FormValue = ObjForm.GetValue("x_Marca");
		}
	}

	//
	// Restore form values
	//
	public void RestoreFormValues() {
		LoadRow();
		VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue = VehiculosAutorizados.IdVehiculoAutorizado.FormValue;
		VehiculosAutorizados.IdTipoVehiculo.CurrentValue = VehiculosAutorizados.IdTipoVehiculo.FormValue;
		VehiculosAutorizados.Placa.CurrentValue = VehiculosAutorizados.Placa.FormValue;
		VehiculosAutorizados.Autorizado.CurrentValue = VehiculosAutorizados.Autorizado.FormValue;
		VehiculosAutorizados.IdPersona.CurrentValue = VehiculosAutorizados.IdPersona.FormValue;
		VehiculosAutorizados.PicoyPlaca.CurrentValue = VehiculosAutorizados.PicoyPlaca.FormValue;
		VehiculosAutorizados.Lunes.CurrentValue = VehiculosAutorizados.Lunes.FormValue;
		VehiculosAutorizados.Martes.CurrentValue = VehiculosAutorizados.Martes.FormValue;
		VehiculosAutorizados.Miercoles.CurrentValue = VehiculosAutorizados.Miercoles.FormValue;
		VehiculosAutorizados.Jueves.CurrentValue = VehiculosAutorizados.Jueves.FormValue;
		VehiculosAutorizados.Viernes.CurrentValue = VehiculosAutorizados.Viernes.FormValue;
		VehiculosAutorizados.Sabado.CurrentValue = VehiculosAutorizados.Sabado.FormValue;
		VehiculosAutorizados.Domingo.CurrentValue = VehiculosAutorizados.Domingo.FormValue;
		VehiculosAutorizados.Marca.CurrentValue = VehiculosAutorizados.Marca.FormValue;
	}

	//
	// Load row based on key values
	//
	public bool LoadRow()	{
		SqlDataReader RsRow = null;
		string sFilter = VehiculosAutorizados.KeyFilter;

		// Row Selecting event
		VehiculosAutorizados.Row_Selecting(ref sFilter);

		// Load SQL based on filter
		VehiculosAutorizados.CurrentFilter = sFilter;
		string sSql = VehiculosAutorizados.SQL;

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
		VehiculosAutorizados.Row_Selected(ref row);
		VehiculosAutorizados.IdVehiculoAutorizado.DbValue = row["IdVehiculoAutorizado"];
		VehiculosAutorizados.IdTipoVehiculo.DbValue = row["IdTipoVehiculo"];
		VehiculosAutorizados.Placa.DbValue = row["Placa"];
		VehiculosAutorizados.Autorizado.DbValue = (ew_ConvertToBool(row["Autorizado"])) ? "1" : "0";
		VehiculosAutorizados.IdPersona.DbValue = row["IdPersona"];
		VehiculosAutorizados.PicoyPlaca.DbValue = (ew_ConvertToBool(row["PicoyPlaca"])) ? "1" : "0";
		VehiculosAutorizados.Lunes.DbValue = (ew_ConvertToBool(row["Lunes"])) ? "1" : "0";
		VehiculosAutorizados.Martes.DbValue = (ew_ConvertToBool(row["Martes"])) ? "1" : "0";
		VehiculosAutorizados.Miercoles.DbValue = (ew_ConvertToBool(row["Miercoles"])) ? "1" : "0";
		VehiculosAutorizados.Jueves.DbValue = (ew_ConvertToBool(row["Jueves"])) ? "1" : "0";
		VehiculosAutorizados.Viernes.DbValue = (ew_ConvertToBool(row["Viernes"])) ? "1" : "0";
		VehiculosAutorizados.Sabado.DbValue = (ew_ConvertToBool(row["Sabado"])) ? "1" : "0";
		VehiculosAutorizados.Domingo.DbValue = (ew_ConvertToBool(row["Domingo"])) ? "1" : "0";
		VehiculosAutorizados.Marca.DbValue = row["Marca"];
	}

	//
	// Render row values based on field settings
	//
	public void RenderRow() {

		// Initialize urls
		// Row Rendering event

		VehiculosAutorizados.Row_Rendering();

		//
		//  Common render codes for all row types
		//
		// IdVehiculoAutorizado
		// IdTipoVehiculo
		// Placa
		// Autorizado
		// IdPersona
		// PicoyPlaca
		// Lunes
		// Martes
		// Miercoles
		// Jueves
		// Viernes
		// Sabado
		// Domingo
		// Marca
		//
		//  View  Row
		//

		if (VehiculosAutorizados.RowType == EW_ROWTYPE_VIEW) { // View row

			// IdVehiculoAutorizado
				VehiculosAutorizados.IdVehiculoAutorizado.ViewValue = VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue;
			VehiculosAutorizados.IdVehiculoAutorizado.ViewCustomAttributes = "";

			// IdTipoVehiculo
			if (ew_NotEmpty(VehiculosAutorizados.IdTipoVehiculo.CurrentValue)) {
				sFilterWrk = "[IdTipoVehiculo] = " + ew_AdjustSql(VehiculosAutorizados.IdTipoVehiculo.CurrentValue) + "";
			sSqlWrk = "SELECT [TipoVehiculo] FROM [dbo].[TiposVehiculos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [TipoVehiculo]";
				drWrk = Conn.GetTempDataReader(sSqlWrk);
				if (drWrk.Read()) {
					VehiculosAutorizados.IdTipoVehiculo.ViewValue = drWrk["TipoVehiculo"];
				} else {
					VehiculosAutorizados.IdTipoVehiculo.ViewValue = VehiculosAutorizados.IdTipoVehiculo.CurrentValue;
				}
				Conn.CloseTempDataReader();
			} else {
				VehiculosAutorizados.IdTipoVehiculo.ViewValue = System.DBNull.Value;
			}
			VehiculosAutorizados.IdTipoVehiculo.ViewCustomAttributes = "";

			// Placa
				VehiculosAutorizados.Placa.ViewValue = VehiculosAutorizados.Placa.CurrentValue;
			VehiculosAutorizados.Placa.ViewCustomAttributes = "";

			// Autorizado
			if (Convert.ToString(VehiculosAutorizados.Autorizado.CurrentValue) == "1") {
				VehiculosAutorizados.Autorizado.ViewValue = (VehiculosAutorizados.Autorizado.FldTagCaption(1) != "") ? VehiculosAutorizados.Autorizado.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Autorizado.ViewValue = (VehiculosAutorizados.Autorizado.FldTagCaption(2) != "") ? VehiculosAutorizados.Autorizado.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Autorizado.ViewCustomAttributes = "";

			// IdPersona
			VehiculosAutorizados.IdPersona.ViewCustomAttributes = "";

			// PicoyPlaca
			if (Convert.ToString(VehiculosAutorizados.PicoyPlaca.CurrentValue) == "1") {
				VehiculosAutorizados.PicoyPlaca.ViewValue = (VehiculosAutorizados.PicoyPlaca.FldTagCaption(1) != "") ? VehiculosAutorizados.PicoyPlaca.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.PicoyPlaca.ViewValue = (VehiculosAutorizados.PicoyPlaca.FldTagCaption(2) != "") ? VehiculosAutorizados.PicoyPlaca.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.PicoyPlaca.ViewCustomAttributes = "";

			// Lunes
			if (Convert.ToString(VehiculosAutorizados.Lunes.CurrentValue) == "1") {
				VehiculosAutorizados.Lunes.ViewValue = (VehiculosAutorizados.Lunes.FldTagCaption(1) != "") ? VehiculosAutorizados.Lunes.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Lunes.ViewValue = (VehiculosAutorizados.Lunes.FldTagCaption(2) != "") ? VehiculosAutorizados.Lunes.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Lunes.ViewCustomAttributes = "";

			// Martes
			if (Convert.ToString(VehiculosAutorizados.Martes.CurrentValue) == "1") {
				VehiculosAutorizados.Martes.ViewValue = (VehiculosAutorizados.Martes.FldTagCaption(1) != "") ? VehiculosAutorizados.Martes.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Martes.ViewValue = (VehiculosAutorizados.Martes.FldTagCaption(2) != "") ? VehiculosAutorizados.Martes.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Martes.ViewCustomAttributes = "";

			// Miercoles
			if (Convert.ToString(VehiculosAutorizados.Miercoles.CurrentValue) == "1") {
				VehiculosAutorizados.Miercoles.ViewValue = (VehiculosAutorizados.Miercoles.FldTagCaption(1) != "") ? VehiculosAutorizados.Miercoles.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Miercoles.ViewValue = (VehiculosAutorizados.Miercoles.FldTagCaption(2) != "") ? VehiculosAutorizados.Miercoles.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Miercoles.ViewCustomAttributes = "";

			// Jueves
			if (Convert.ToString(VehiculosAutorizados.Jueves.CurrentValue) == "1") {
				VehiculosAutorizados.Jueves.ViewValue = (VehiculosAutorizados.Jueves.FldTagCaption(1) != "") ? VehiculosAutorizados.Jueves.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Jueves.ViewValue = (VehiculosAutorizados.Jueves.FldTagCaption(2) != "") ? VehiculosAutorizados.Jueves.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Jueves.ViewCustomAttributes = "";

			// Viernes
			if (Convert.ToString(VehiculosAutorizados.Viernes.CurrentValue) == "1") {
				VehiculosAutorizados.Viernes.ViewValue = (VehiculosAutorizados.Viernes.FldTagCaption(1) != "") ? VehiculosAutorizados.Viernes.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Viernes.ViewValue = (VehiculosAutorizados.Viernes.FldTagCaption(2) != "") ? VehiculosAutorizados.Viernes.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Viernes.ViewCustomAttributes = "";

			// Sabado
			if (Convert.ToString(VehiculosAutorizados.Sabado.CurrentValue) == "1") {
				VehiculosAutorizados.Sabado.ViewValue = (VehiculosAutorizados.Sabado.FldTagCaption(1) != "") ? VehiculosAutorizados.Sabado.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Sabado.ViewValue = (VehiculosAutorizados.Sabado.FldTagCaption(2) != "") ? VehiculosAutorizados.Sabado.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Sabado.ViewCustomAttributes = "";

			// Domingo
			if (Convert.ToString(VehiculosAutorizados.Domingo.CurrentValue) == "1") {
				VehiculosAutorizados.Domingo.ViewValue = (VehiculosAutorizados.Domingo.FldTagCaption(1) != "") ? VehiculosAutorizados.Domingo.FldTagCaption(1) : "Y";
			} else {
				VehiculosAutorizados.Domingo.ViewValue = (VehiculosAutorizados.Domingo.FldTagCaption(2) != "") ? VehiculosAutorizados.Domingo.FldTagCaption(2) : "N";
			}
			VehiculosAutorizados.Domingo.ViewCustomAttributes = "";

			// Marca
				VehiculosAutorizados.Marca.ViewValue = VehiculosAutorizados.Marca.CurrentValue;
			VehiculosAutorizados.Marca.ViewCustomAttributes = "";

			// View refer script
			// IdVehiculoAutorizado

			VehiculosAutorizados.IdVehiculoAutorizado.LinkCustomAttributes = "";
			VehiculosAutorizados.IdVehiculoAutorizado.HrefValue = "";
			VehiculosAutorizados.IdVehiculoAutorizado.TooltipValue = "";

			// IdTipoVehiculo
			VehiculosAutorizados.IdTipoVehiculo.LinkCustomAttributes = "";
			VehiculosAutorizados.IdTipoVehiculo.HrefValue = "";
			VehiculosAutorizados.IdTipoVehiculo.TooltipValue = "";

			// Placa
			VehiculosAutorizados.Placa.LinkCustomAttributes = "";
			VehiculosAutorizados.Placa.HrefValue = "";
			VehiculosAutorizados.Placa.TooltipValue = "";

			// Autorizado
			VehiculosAutorizados.Autorizado.LinkCustomAttributes = "";
			VehiculosAutorizados.Autorizado.HrefValue = "";
			VehiculosAutorizados.Autorizado.TooltipValue = "";

			// IdPersona
			VehiculosAutorizados.IdPersona.LinkCustomAttributes = "";
			VehiculosAutorizados.IdPersona.HrefValue = "";
			VehiculosAutorizados.IdPersona.TooltipValue = "";

			// PicoyPlaca
			VehiculosAutorizados.PicoyPlaca.LinkCustomAttributes = "";
			VehiculosAutorizados.PicoyPlaca.HrefValue = "";
			VehiculosAutorizados.PicoyPlaca.TooltipValue = "";

			// Lunes
			VehiculosAutorizados.Lunes.LinkCustomAttributes = "";
			VehiculosAutorizados.Lunes.HrefValue = "";
			VehiculosAutorizados.Lunes.TooltipValue = "";

			// Martes
			VehiculosAutorizados.Martes.LinkCustomAttributes = "";
			VehiculosAutorizados.Martes.HrefValue = "";
			VehiculosAutorizados.Martes.TooltipValue = "";

			// Miercoles
			VehiculosAutorizados.Miercoles.LinkCustomAttributes = "";
			VehiculosAutorizados.Miercoles.HrefValue = "";
			VehiculosAutorizados.Miercoles.TooltipValue = "";

			// Jueves
			VehiculosAutorizados.Jueves.LinkCustomAttributes = "";
			VehiculosAutorizados.Jueves.HrefValue = "";
			VehiculosAutorizados.Jueves.TooltipValue = "";

			// Viernes
			VehiculosAutorizados.Viernes.LinkCustomAttributes = "";
			VehiculosAutorizados.Viernes.HrefValue = "";
			VehiculosAutorizados.Viernes.TooltipValue = "";

			// Sabado
			VehiculosAutorizados.Sabado.LinkCustomAttributes = "";
			VehiculosAutorizados.Sabado.HrefValue = "";
			VehiculosAutorizados.Sabado.TooltipValue = "";

			// Domingo
			VehiculosAutorizados.Domingo.LinkCustomAttributes = "";
			VehiculosAutorizados.Domingo.HrefValue = "";
			VehiculosAutorizados.Domingo.TooltipValue = "";

			// Marca
			VehiculosAutorizados.Marca.LinkCustomAttributes = "";
			VehiculosAutorizados.Marca.HrefValue = "";
			VehiculosAutorizados.Marca.TooltipValue = "";

		//
		//  Edit Row
		//

		} else if (VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT) { // Edit row

			// IdVehiculoAutorizado
			VehiculosAutorizados.IdVehiculoAutorizado.EditCustomAttributes = "";
				VehiculosAutorizados.IdVehiculoAutorizado.EditValue = VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue;
			VehiculosAutorizados.IdVehiculoAutorizado.ViewCustomAttributes = "";

			// IdTipoVehiculo
			VehiculosAutorizados.IdTipoVehiculo.EditCustomAttributes = "";
			sFilterWrk = "";
			sSqlWrk = "SELECT [IdTipoVehiculo], [TipoVehiculo] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld] FROM [dbo].[TiposVehiculos]";
			sWhereWrk = "";
			if (sFilterWrk != "") {
				if (sWhereWrk != "") sWhereWrk += " AND ";
				sWhereWrk += "(" + sFilterWrk + ")";
			}
			if (sWhereWrk != "") sSqlWrk += " WHERE " + sWhereWrk;
			sSqlWrk += " ORDER BY [TipoVehiculo]";
			alwrk = Conn.GetRows(sSqlWrk);
			alwrk.Insert(0, new OrderedDictionary());
			((OrderedDictionary)alwrk[0]).Add("0", "");
			((OrderedDictionary)alwrk[0]).Add("1",  Language.Phrase("PleaseSelect"));
			VehiculosAutorizados.IdTipoVehiculo.EditValue = alwrk;

			// Placa
			VehiculosAutorizados.Placa.EditCustomAttributes = "";
			VehiculosAutorizados.Placa.EditValue = ew_HtmlEncode(VehiculosAutorizados.Placa.CurrentValue);

			// Autorizado
			VehiculosAutorizados.Autorizado.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Autorizado.FldTagCaption(1))) ? VehiculosAutorizados.Autorizado.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Autorizado.FldTagCaption(2))) ? VehiculosAutorizados.Autorizado.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Autorizado.EditValue = alwrk;

			// IdPersona
			VehiculosAutorizados.IdPersona.EditCustomAttributes = "";
			if (ew_NotEmpty(VehiculosAutorizados.IdPersona.SessionValue)) {
				VehiculosAutorizados.IdPersona.CurrentValue = VehiculosAutorizados.IdPersona.SessionValue;
			VehiculosAutorizados.IdPersona.ViewCustomAttributes = "";
			} else {
			}

			// PicoyPlaca
			VehiculosAutorizados.PicoyPlaca.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.PicoyPlaca.FldTagCaption(1))) ? VehiculosAutorizados.PicoyPlaca.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.PicoyPlaca.FldTagCaption(2))) ? VehiculosAutorizados.PicoyPlaca.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.PicoyPlaca.EditValue = alwrk;

			// Lunes
			VehiculosAutorizados.Lunes.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Lunes.FldTagCaption(1))) ? VehiculosAutorizados.Lunes.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Lunes.FldTagCaption(2))) ? VehiculosAutorizados.Lunes.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Lunes.EditValue = alwrk;

			// Martes
			VehiculosAutorizados.Martes.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Martes.FldTagCaption(1))) ? VehiculosAutorizados.Martes.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Martes.FldTagCaption(2))) ? VehiculosAutorizados.Martes.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Martes.EditValue = alwrk;

			// Miercoles
			VehiculosAutorizados.Miercoles.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Miercoles.FldTagCaption(1))) ? VehiculosAutorizados.Miercoles.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Miercoles.FldTagCaption(2))) ? VehiculosAutorizados.Miercoles.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Miercoles.EditValue = alwrk;

			// Jueves
			VehiculosAutorizados.Jueves.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Jueves.FldTagCaption(1))) ? VehiculosAutorizados.Jueves.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Jueves.FldTagCaption(2))) ? VehiculosAutorizados.Jueves.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Jueves.EditValue = alwrk;

			// Viernes
			VehiculosAutorizados.Viernes.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Viernes.FldTagCaption(1))) ? VehiculosAutorizados.Viernes.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Viernes.FldTagCaption(2))) ? VehiculosAutorizados.Viernes.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Viernes.EditValue = alwrk;

			// Sabado
			VehiculosAutorizados.Sabado.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Sabado.FldTagCaption(1))) ? VehiculosAutorizados.Sabado.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Sabado.FldTagCaption(2))) ? VehiculosAutorizados.Sabado.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Sabado.EditValue = alwrk;

			// Domingo
			VehiculosAutorizados.Domingo.EditCustomAttributes = "";
			alwrk = new ArrayList();
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "1");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Domingo.FldTagCaption(1))) ? VehiculosAutorizados.Domingo.FldTagCaption(1) : "Si");
			alwrk.Add(odwrk);
			odwrk = new OrderedDictionary();
			odwrk.Add("0", "0");
			odwrk.Add("1", (ew_NotEmpty(VehiculosAutorizados.Domingo.FldTagCaption(2))) ? VehiculosAutorizados.Domingo.FldTagCaption(2) : "No");
			alwrk.Add(odwrk);
			VehiculosAutorizados.Domingo.EditValue = alwrk;

			// Marca
			VehiculosAutorizados.Marca.EditCustomAttributes = "";
			VehiculosAutorizados.Marca.EditValue = ew_HtmlEncode(VehiculosAutorizados.Marca.CurrentValue);

			// Edit refer script
			// IdVehiculoAutorizado

			VehiculosAutorizados.IdVehiculoAutorizado.HrefValue = "";

			// IdTipoVehiculo
			VehiculosAutorizados.IdTipoVehiculo.HrefValue = "";

			// Placa
			VehiculosAutorizados.Placa.HrefValue = "";

			// Autorizado
			VehiculosAutorizados.Autorizado.HrefValue = "";

			// IdPersona
			VehiculosAutorizados.IdPersona.HrefValue = "";

			// PicoyPlaca
			VehiculosAutorizados.PicoyPlaca.HrefValue = "";

			// Lunes
			VehiculosAutorizados.Lunes.HrefValue = "";

			// Martes
			VehiculosAutorizados.Martes.HrefValue = "";

			// Miercoles
			VehiculosAutorizados.Miercoles.HrefValue = "";

			// Jueves
			VehiculosAutorizados.Jueves.HrefValue = "";

			// Viernes
			VehiculosAutorizados.Viernes.HrefValue = "";

			// Sabado
			VehiculosAutorizados.Sabado.HrefValue = "";

			// Domingo
			VehiculosAutorizados.Domingo.HrefValue = "";

			// Marca
			VehiculosAutorizados.Marca.HrefValue = "";
		}
		if (VehiculosAutorizados.RowType == EW_ROWTYPE_ADD ||
			VehiculosAutorizados.RowType == EW_ROWTYPE_EDIT ||
			VehiculosAutorizados.RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
			VehiculosAutorizados.SetupFieldTitles();
		}

		// Row Rendered event
		if (VehiculosAutorizados.RowType != EW_ROWTYPE_AGGREGATEINIT)
			VehiculosAutorizados.Row_Rendered();
	}

	//
	// Validate form
	//
	public bool ValidateForm() {

		// Initialize
		gsFormError = "";

		// Check if validation required
		if (!EW_SERVER_VALIDATE) return (ew_Empty(gsFormError)); 
		if (ew_Empty(VehiculosAutorizados.IdTipoVehiculo.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.IdTipoVehiculo.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Placa.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Placa.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Autorizado.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Autorizado.FldCaption);
		if (ew_Empty(VehiculosAutorizados.IdPersona.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.IdPersona.FldCaption);
		if (ew_Empty(VehiculosAutorizados.PicoyPlaca.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.PicoyPlaca.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Lunes.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Lunes.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Martes.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Martes.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Miercoles.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Miercoles.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Jueves.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Jueves.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Viernes.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Viernes.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Sabado.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Sabado.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Domingo.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Domingo.FldCaption);
		if (ew_Empty(VehiculosAutorizados.Marca.FormValue))
			ew_AddMessage(ref gsFormError, Language.Phrase("EnterRequiredField") + " - " + VehiculosAutorizados.Marca.FldCaption);

		// Validate detail grid
		if (VehiculosAutorizados.CurrentDetailTable == "VehiculosPicoYPlacaHoras" && VehiculosPicoYPlacaHoras.DetailEdit) {
			VehiculosPicoYPlacaHoras_grid = new cVehiculosPicoYPlacaHoras_grid(ParentPage); // Get detail page object (grid)
			VehiculosPicoYPlacaHoras_grid.ValidateGridForm();
			VehiculosPicoYPlacaHoras_grid.Dispose();
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
	// Update record based on key values
	//
	public bool EditRow()	{
		bool result = false;
		SqlDataReader RsEdit = null;
		SqlDataReader RsChk = null;
		string sSql;
		string sSqlChk;
		string sFilterChk;
		bool bUpdateRow;
		OrderedDictionary RsOld = null;
		string sIdxErrMsg;
		OrderedDictionary RsNew = new OrderedDictionary();
		string sFilter = VehiculosAutorizados.KeyFilter;
		VehiculosAutorizados.CurrentFilter = sFilter;
		sSql = VehiculosAutorizados.SQL;
		try {
			RsEdit = Conn.GetDataReader(sSql);
		}	catch (Exception e) {
			if (EW_DEBUG_ENABLED) throw; 
			FailureMessage = e.Message;
			RsEdit.Close();
			return false;
		}
		if (!RsEdit.Read())	{
			RsEdit.Close();
			return false; // Update Failed
		}	else {

			// Begin transaction
			if (ew_NotEmpty(VehiculosAutorizados.CurrentDetailTable))
				Conn.BeginTrans();
			try {
				RsOld = Conn.GetRow(ref RsEdit);

			//RsEdit.Close();
				// IdVehiculoAutorizado
				// IdTipoVehiculo

				VehiculosAutorizados.IdTipoVehiculo.SetDbValue(ref RsNew, VehiculosAutorizados.IdTipoVehiculo.CurrentValue, 0, VehiculosAutorizados.IdTipoVehiculo.ReadOnly);

				// Placa
				VehiculosAutorizados.Placa.SetDbValue(ref RsNew, VehiculosAutorizados.Placa.CurrentValue, "", VehiculosAutorizados.Placa.ReadOnly);

				// Autorizado
				VehiculosAutorizados.Autorizado.SetDbValue(ref RsNew, (VehiculosAutorizados.Autorizado.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Autorizado.CurrentValue)), false, VehiculosAutorizados.Autorizado.ReadOnly);

				// IdPersona
				VehiculosAutorizados.IdPersona.SetDbValue(ref RsNew, VehiculosAutorizados.IdPersona.CurrentValue, 0, VehiculosAutorizados.IdPersona.ReadOnly);

				// PicoyPlaca
				VehiculosAutorizados.PicoyPlaca.SetDbValue(ref RsNew, (VehiculosAutorizados.PicoyPlaca.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.PicoyPlaca.CurrentValue)), false, VehiculosAutorizados.PicoyPlaca.ReadOnly);

				// Lunes
				VehiculosAutorizados.Lunes.SetDbValue(ref RsNew, (VehiculosAutorizados.Lunes.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Lunes.CurrentValue)), false, VehiculosAutorizados.Lunes.ReadOnly);

				// Martes
				VehiculosAutorizados.Martes.SetDbValue(ref RsNew, (VehiculosAutorizados.Martes.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Martes.CurrentValue)), false, VehiculosAutorizados.Martes.ReadOnly);

				// Miercoles
				VehiculosAutorizados.Miercoles.SetDbValue(ref RsNew, (VehiculosAutorizados.Miercoles.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Miercoles.CurrentValue)), false, VehiculosAutorizados.Miercoles.ReadOnly);

				// Jueves
				VehiculosAutorizados.Jueves.SetDbValue(ref RsNew, (VehiculosAutorizados.Jueves.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Jueves.CurrentValue)), false, VehiculosAutorizados.Jueves.ReadOnly);

				// Viernes
				VehiculosAutorizados.Viernes.SetDbValue(ref RsNew, (VehiculosAutorizados.Viernes.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Viernes.CurrentValue)), false, VehiculosAutorizados.Viernes.ReadOnly);

				// Sabado
				VehiculosAutorizados.Sabado.SetDbValue(ref RsNew, (VehiculosAutorizados.Sabado.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Sabado.CurrentValue)), false, VehiculosAutorizados.Sabado.ReadOnly);

				// Domingo
				VehiculosAutorizados.Domingo.SetDbValue(ref RsNew, (VehiculosAutorizados.Domingo.CurrentValue != "" && !Convert.IsDBNull(VehiculosAutorizados.Domingo.CurrentValue)), false, VehiculosAutorizados.Domingo.ReadOnly);

				// Marca
				VehiculosAutorizados.Marca.SetDbValue(ref RsNew, VehiculosAutorizados.Marca.CurrentValue, "", VehiculosAutorizados.Marca.ReadOnly);
			} catch (Exception e) {
				if (EW_DEBUG_ENABLED) throw;
				FailureMessage = e.Message;
				RsEdit.Close();
				return false;
			}
			RsEdit.Close();

			// Row Updating event
			bUpdateRow = VehiculosAutorizados.Row_Updating(RsOld, ref RsNew);
			if (bUpdateRow) {
				try {
					if (RsNew.Count > 0)
						result = VehiculosAutorizados.Update(ref RsNew) > 0;
					else
						result = true; // No field to update

					// Update detail records
					if (result) {
						if (VehiculosAutorizados.CurrentDetailTable == "VehiculosPicoYPlacaHoras" && VehiculosPicoYPlacaHoras.DetailEdit) {
							ParentPage.VehiculosPicoYPlacaHoras_grid = new cVehiculosPicoYPlacaHoras_grid(ParentPage); // Get detail page object (grid)			
							result = ParentPage.VehiculosPicoYPlacaHoras_grid.GridUpdate();
							ParentPage.VehiculosPicoYPlacaHoras_grid.Dispose();
						}
					}

					// Commit/Rollback transaction
					if (ew_NotEmpty(VehiculosAutorizados.CurrentDetailTable)) {
						if (result) {
							Conn.CommitTrans(); // Commit transaction
						} else {
							Conn.RollbackTrans(); // Rollback transaction
						}
					}
				} catch (Exception e) {
					if (EW_DEBUG_ENABLED) throw; 
					FailureMessage = e.Message;
					return false;
				}
			}	else {
				if (ew_NotEmpty(VehiculosAutorizados.CancelMessage)) {
					FailureMessage = VehiculosAutorizados.CancelMessage;
					VehiculosAutorizados.CancelMessage = "";
				} else {
					FailureMessage = Language.Phrase("UpdateCancelled");
				}
				result = false;
			}
		}

		// Row Updated event
		if (result)
			VehiculosAutorizados.Row_Updated(RsOld, RsNew);
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
			if (sMasterTblVar == "Personas") {
				bValidMaster = true;				
				if (ew_NotEmpty(ew_Get("IdPersona"))) {
					Personas.IdPersona.QueryStringValue = ew_Get("IdPersona");
					VehiculosAutorizados.IdPersona.QueryStringValue = Personas.IdPersona.QueryStringValue;
					VehiculosAutorizados.IdPersona.SessionValue = VehiculosAutorizados.IdPersona.QueryStringValue;
					if (!Information.IsNumeric(Personas.IdPersona.QueryStringValue)) bValidMaster = false;
				} else {
					bValidMaster = false;
				}
			}
		}
		if (bValidMaster) {

			// Save current master table
			VehiculosAutorizados.CurrentMasterTable = sMasterTblVar;

			// Clear previous master session values
			if (sMasterTblVar != "Personas") {
				if (ew_Empty(VehiculosAutorizados.IdPersona.QueryStringValue)) VehiculosAutorizados.IdPersona.SessionValue = "";
			}
		}
		DbMasterFilter = VehiculosAutorizados.MasterFilter; // Get master filter
		DbDetailFilter = VehiculosAutorizados.DetailFilter; // Get detail filter
	}

	// Set up detail parms based on QueryString
	public void SetUpDetailParms() {
		bool bValidDetail = false;
		string sDetailTblVar = "";

		// Get the keys for master table
		if (HttpContext.Current.Request.QueryString[EW_TABLE_SHOW_DETAIL] != null) {
			sDetailTblVar = ew_Get(EW_TABLE_SHOW_DETAIL);
			VehiculosAutorizados.CurrentDetailTable = sDetailTblVar;
		} else {
			sDetailTblVar = VehiculosAutorizados.CurrentDetailTable;
		}
		if (ew_NotEmpty(sDetailTblVar)) {
			if (sDetailTblVar == "VehiculosPicoYPlacaHoras") {
				if (VehiculosPicoYPlacaHoras == null)
					VehiculosPicoYPlacaHoras = new cVehiculosPicoYPlacaHoras(this);
				if (VehiculosPicoYPlacaHoras.DetailEdit) {
					VehiculosPicoYPlacaHoras.CurrentMode = "edit";
					VehiculosPicoYPlacaHoras.CurrentAction = "gridedit";

					// Save current master table to detail table
					VehiculosPicoYPlacaHoras.CurrentMasterTable = VehiculosAutorizados.TableVar;
					VehiculosPicoYPlacaHoras.StartRecordNumber = 1;
					VehiculosPicoYPlacaHoras.IdVehiculoAutorizado.FldIsDetailKey = true;
					VehiculosPicoYPlacaHoras.IdVehiculoAutorizado.CurrentValue = VehiculosAutorizados.IdVehiculoAutorizado.CurrentValue;
					VehiculosPicoYPlacaHoras.IdVehiculoAutorizado.SessionValue = VehiculosPicoYPlacaHoras.IdVehiculoAutorizado.CurrentValue;
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
		VehiculosAutorizados_edit = new cVehiculosAutorizados_edit(this);
		NameValueCollection fv = new NameValueCollection();
		fv.Add(Request.Form);
		Session["EW_FORM_VALUES"] = fv;
		CurrentPage = VehiculosAutorizados_edit;

		//CurrentPageType = VehiculosAutorizados_edit.GetType();
		VehiculosAutorizados_edit.Page_Init();

		// Set buffer/cache option
		Response.Buffer = EW_RESPONSE_BUFFER;
		ew_Header(false);

		// Page main processing
		VehiculosAutorizados_edit.Page_Main();
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
		if (VehiculosAutorizados_edit != null)
			VehiculosAutorizados_edit.Dispose();
	}
}
