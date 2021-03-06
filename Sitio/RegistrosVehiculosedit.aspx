<%@ Page ClassName="RegistrosVehiculosedit" Language="C#" CodePage="65001" MasterPageFile="masterpage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="RegistrosVehiculosedit.aspx.cs" Inherits="RegistrosVehiculosedit" CodeFileBaseClass="AspNetMaker9_ControlVehicular" %>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
<!--

// Create page object
var RegistrosVehiculos_edit = new ew_Page("RegistrosVehiculos_edit");

// page properties
RegistrosVehiculos_edit.PageID = "edit"; // page ID
RegistrosVehiculos_edit.FormID = "fRegistrosVehiculosedit"; // form ID 
var EW_PAGE_ID = RegistrosVehiculos_edit.PageID; // for backward compatibility

// extend page with ValidateForm function
RegistrosVehiculos_edit.ValidateForm = function(fobj) {
	ew_PostAutoSuggest(fobj);
	if (!this.ValidateRequired)
		return true; // ignore validation
	if (fobj.a_confirm && fobj.a_confirm.value == "F")
		return true;
	var i, elm, aelm, infix;
	var rowcnt = 1;
	for (i=0; i<rowcnt; i++) {
		infix = "";
		elm = fobj.elements["x" + infix + "_Placa"];
		if (elm && !ew_HasValue(elm))
			return ew_OnError(this, elm, ewLanguage.Phrase("EnterRequiredField") + " - <%= ew_JsEncode2(RegistrosVehiculos.Placa.FldCaption) %>");

		// Set up row object
		var row = {};
		row["index"] = infix;
		for (var j = 0; j < fobj.elements.length; j++) {
			var el = fobj.elements[j];
			var len = infix.length + 2;
			if (el.name.substr(0, len) == "x" + infix + "_") {
				var elname = "x_" + el.name.substr(len);
				if (ewLang.isObject(row[elname])) { // already exists
					if (ewLang.isArray(row[elname])) {
						row[elname][row[elname].length] = el; // add to array
					} else {
						row[elname] = [row[elname], el]; // convert to array
					}
				} else {
					row[elname] = el;
				}
			}
		}
		fobj.row = row;

		// Form Custom Validate event
		if (!this.Form_CustomValidate(fobj)) return false;
	}

	// Process detail page
	var detailpage = (fobj.detailpage) ? fobj.detailpage.value : "";
	if (detailpage != "") {
		return eval(detailpage+".ValidateForm(fobj)");
	}
	return true;
}

// extend page with Form_CustomValidate function
RegistrosVehiculos_edit.Form_CustomValidate =  
 function(fobj) { // DO NOT CHANGE THIS LINE!

 	// Your custom validation code here, return false if invalid. 
 	return true;
 }
<% if (EW_CLIENT_VALIDATE) { %>
RegistrosVehiculos_edit.ValidateRequired = true; // uses JavaScript validation
<% } else { %>
RegistrosVehiculos_edit.ValidateRequired = false; // no JavaScript validation
<% } %>

//-->
</script>
<script language="JavaScript" type="text/javascript">
<!--

// Write your client script here, no need to add script tags.
//-->

</script>
<p class="aspnetmaker ewTitle"><%= Language.Phrase("Edit") %>&nbsp;<%= Language.Phrase("TblTypeTABLE") %><%= RegistrosVehiculos.TableCaption %></p>
<p class="aspnetmaker"><a href="<%= RegistrosVehiculos.ReturnUrl %>"><%= Language.Phrase("GoBack") %></a></p>
<% RegistrosVehiculos_edit.ShowPageHeader(); %>
<% RegistrosVehiculos_edit.ShowMessage(); %>
<form name="fRegistrosVehiculosedit" id="fRegistrosVehiculosedit" method="post" onsubmit="this.action=location.pathname;return RegistrosVehiculos_edit.ValidateForm(this);">
<p />
<input type="hidden" name="a_table" id="a_table" value="RegistrosVehiculos" />
<input type="hidden" name="a_edit" id="a_edit" value="U" />
<table cellspacing="0" class="ewGrid"><tr><td class="ewGridContent">
<div class="ewGridMiddlePanel">
<table cellspacing="0" class="ewTable">
<% if (RegistrosVehiculos.IdRegistroVehiculo.Visible) { // IdRegistroVehiculo %>
	<tr id="r_IdRegistroVehiculo"<%= RegistrosVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosVehiculos.IdRegistroVehiculo.FldCaption %></td>
		<td<%= RegistrosVehiculos.IdRegistroVehiculo.CellAttributes %>><span id="el_IdRegistroVehiculo">
<div<%= RegistrosVehiculos.IdRegistroVehiculo.ViewAttributes %>><%= RegistrosVehiculos.IdRegistroVehiculo.EditValue %></div>
<input type="hidden" name="x_IdRegistroVehiculo" id="x_IdRegistroVehiculo" value="<%= ew_HtmlEncode(RegistrosVehiculos.IdRegistroVehiculo.CurrentValue) %>" />
</span><%= RegistrosVehiculos.IdRegistroVehiculo.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosVehiculos.IdTipoVehiculo.Visible) { // IdTipoVehiculo %>
	<tr id="r_IdTipoVehiculo"<%= RegistrosVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosVehiculos.IdTipoVehiculo.FldCaption %></td>
		<td<%= RegistrosVehiculos.IdTipoVehiculo.CellAttributes %>><span id="el_IdTipoVehiculo">
<select id="x_IdTipoVehiculo" name="x_IdTipoVehiculo"<%= RegistrosVehiculos.IdTipoVehiculo.EditAttributes %>>
<%
emptywrk = true;
if (ew_IsArrayList(RegistrosVehiculos.IdTipoVehiculo.EditValue)) {
	alwrk = (ArrayList)RegistrosVehiculos.IdTipoVehiculo.EditValue;
	for (int rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
		odwrk = (OrderedDictionary)alwrk[rowcntwrk];
		if (ew_SameStr(odwrk[0], RegistrosVehiculos.IdTipoVehiculo.CurrentValue)) {
			selwrk = " selected=\"selected\"";
			emptywrk = false;
		} else {
			selwrk = "";
		}
%>
<option value="<%= ew_HtmlEncode(odwrk[0]) %>"<%= selwrk %>>
<%= odwrk[1] %>
</option>
<%
	}
}
%>
</select>
</span><%= RegistrosVehiculos.IdTipoVehiculo.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosVehiculos.Placa.Visible) { // Placa %>
	<tr id="r_Placa"<%= RegistrosVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosVehiculos.Placa.FldCaption %><%= Language.Phrase("FieldRequiredIndicator") %></td>
		<td<%= RegistrosVehiculos.Placa.CellAttributes %>><span id="el_Placa">
<input type="text" name="x_Placa" id="x_Placa" size="10" maxlength="10" value="<%= RegistrosVehiculos.Placa.EditValue %>"<%= RegistrosVehiculos.Placa.EditAttributes %> />
</span><%= RegistrosVehiculos.Placa.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosVehiculos.FechaIngreso.Visible) { // FechaIngreso %>
	<tr id="r_FechaIngreso"<%= RegistrosVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosVehiculos.FechaIngreso.FldCaption %></td>
		<td<%= RegistrosVehiculos.FechaIngreso.CellAttributes %>><span id="el_FechaIngreso">
<div<%= RegistrosVehiculos.FechaIngreso.ViewAttributes %>><%= RegistrosVehiculos.FechaIngreso.EditValue %></div>
<input type="hidden" name="x_FechaIngreso" id="x_FechaIngreso" value="<%= ew_HtmlEncode(RegistrosVehiculos.FechaIngreso.CurrentValue) %>" />
</span><%= RegistrosVehiculos.FechaIngreso.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosVehiculos.FechaSalida.Visible) { // FechaSalida %>
	<tr id="r_FechaSalida"<%= RegistrosVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosVehiculos.FechaSalida.FldCaption %></td>
		<td<%= RegistrosVehiculos.FechaSalida.CellAttributes %>><span id="el_FechaSalida">
<div<%= RegistrosVehiculos.FechaSalida.ViewAttributes %>><%= RegistrosVehiculos.FechaSalida.EditValue %></div>
<input type="hidden" name="x_FechaSalida" id="x_FechaSalida" value="<%= ew_HtmlEncode(RegistrosVehiculos.FechaSalida.CurrentValue) %>" />
</span><%= RegistrosVehiculos.FechaSalida.CustomMsg %></td>
	</tr>
<% } %>
<% if (RegistrosVehiculos.Observaciones.Visible) { // Observaciones %>
	<tr id="r_Observaciones"<%= RegistrosVehiculos.RowAttributes %>>
		<td class="ewTableHeader"><%= RegistrosVehiculos.Observaciones.FldCaption %></td>
		<td<%= RegistrosVehiculos.Observaciones.CellAttributes %>><span id="el_Observaciones">
<div<%= RegistrosVehiculos.Observaciones.ViewAttributes %>><%= RegistrosVehiculos.Observaciones.EditValue %></div>
<input type="hidden" name="x_Observaciones" id="x_Observaciones" value="<%= ew_HtmlEncode(RegistrosVehiculos.Observaciones.CurrentValue) %>" />
</span><%= RegistrosVehiculos.Observaciones.CustomMsg %></td>
	</tr>
<% } %>
</table>
</div>
</td></tr></table>
<p />
<input type="submit" name="btnAction" id="btnAction" value="<%= ew_BtnCaption(Language.Phrase("EditBtn")) %>" />
</form>
<%
RegistrosVehiculos_edit.ShowPageFooter();
if (EW_DEBUG_ENABLED)
	ew_Write(ew_DebugMsg());
%>
<script language="JavaScript" type="text/javascript">
<!--

// Write your table-specific startup script here
// document.write("page loaded");
//-->

</script>
</asp:Content>
