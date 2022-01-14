<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMstr.Master" AutoEventWireup="true" CodeBehind="PO_Entry.aspx.cs" Inherits="ERP_System.PO_Module.Forms.PO_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- <ItemTemplate>--%>

    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">PO Entry</h1>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                New PO
                            </div>

                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnsave" Text="SAVE" runat="server" CssClass="btn btn-primary" OnClick="btnsave_Click" />
                                            <asp:Button ID="btncancel" Text="New" runat="server" CssClass="btn btn-default" OnClick="btncancel_Click" />
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">


                                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                                <ContentTemplate>
                                                    <asp:Timer ID="resulttimer" runat="server" Interval="3000" Enabled="False" OnTick="resulttimer_Tick"></asp:Timer>
                                                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                        <ContentTemplate>
                                                            <strong>
                                                                <asp:Label ID="lblsaveresult" runat="server" Text=""></asp:Label></strong>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="resulttimer" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                        </div>
                                    </div>
                                    <!-- /.row (nested) -->
                                </div>

                                <!-- Nav tabs -->

                                <ul class="nav nav-pills">
                                    <li class="active"><a href="#Entry" data-toggle="tab">PO Header</a>
                                    </li>
                                    <li><a href="#Details" data-toggle="tab">PO Details</a>
                                    </li>

                                </ul>

                                <!-- Tab panes -->
                                <div class="tab-content">

                                    <div class="tab-pane fade in active" id="Entry">

                                        <div class="row">

                                            <div class="col-lg-6">

                                                <!-- /po no -->
                                                <div class="form-group">
                                                    <label for="disabledSelect">PO No</label>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <input class="form-control" runat="server" id="txtpo" type="text" placeholder="PO No" disabled>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <!-- /company code -->
                                                <div class="form-group">
                                                    <label>Company Code</label>
                                                    <asp:DropDownList ID="ddlcom" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlcom_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>

                                                </div>


                                                   <!-- /vendor type -->
                                                <div class="form-group">
                                                    <label>Vendor Type</label>
                                                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlven_type" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlven_type_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlcom" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>

                                                <!-- /vendor code -->
                                                <div class="form-group">
                                                    <label>Vendor Code</label>
                                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlven_code" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlven_code_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlven_type" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>


                                                <!-- /purchase term -->
                                                <div class="form-group">
                                                    <label>Purchase Term</label>
                                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlpur_term" runat="server" CssClass="form-control">
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="ddlven_code" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlcom" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnsave" />
        </Triggers>
    </asp:UpdatePanel>

    </div>

                                                <!-- / remark -->
    <div class="form-group">
        <label>Remark</label>

        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
            <ContentTemplate>
                <textarea runat="server" id="txtrmk" class="form-control" rows="3"></textarea>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnsave" />
            </Triggers>
        </asp:UpdatePanel>


    </div>

    </div>

                                            <div class="col-lg-6">

                                                <!-- /revision no -->
                                                <div class="form-group">
                                                    <label for="disabledSelect">Revision No</label>
                                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                        <ContentTemplate>
                                                            <input class="form-control" runat="server" id="txtrev" type="text" placeholder="Revision No" disabled>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>

                                             

                                                <!-- /vendor contact -->
                                                <div class="form-group">

                                                    <label for="disabledSelect">Vendor Contact</label>
                                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                                <ContentTemplate>
                                                                    <input class="form-control" runat="server" id="txtven_contact" type="text" placeholder="Vendor Contact">
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlven_code" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>


                                                </div>


                                                <!-- /order date -->
                                                <div class="form-group">

                                                    <label>Order Date</label>
                                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox CssClass="form-control" ID="txtadddate" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="Calendarextender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtadddate"
                                                                Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>



                                                </div>

                                                <!-- /term code -->
                                                <div class="form-group">
                                                    <label>Term Code</label>
                                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlterm_code" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlven_code" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>

                                                <!-- /place to delivery -->
                                                <div class="form-group">
                                                    <label>Place to delivery</label>
                                                    <asp:DropDownList ID="ddlplace" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="FGS">FGS | FINISH GOOD STORE</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>

                                            </div>


    </div>
                                    </div>

                                    <div class="tab-pane fade" id="Details">
                                        <div class="row">
                                            <div class="col-lg-6">


                                                <!-- /catalog no -->
                                                <div class="form-group">

                                                    <label>Catalog No</label>
                                                      <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                                        <ContentTemplate>
                                                    <asp:DropDownList ID="ddlcatalog" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcatalog_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                             </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnadd" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <!-- /description -->
                                                <div class="form-group">

                                                    <label>Description</label>
                                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <input class="form-control" runat="server" id="txtdesc" type="text" placeholder="Description">
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlcatalog" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>

                                                <!-- /ex description -->
                                                <div class="form-group">
                                                    <label>Extra Description</label>
                                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                <ContentTemplate>
                                                                    <input class="form-control" runat="server" id="txtextra_dsc" type="text" placeholder="Extra Description">
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlcatalog" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>

                                                <!-- /uom -->
                                                <div class="form-group">
                                                    <label>Unit of measurement</label>
                                                    <asp:DropDownList ID="ddluom" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>


                                                <!-- /details button -->
                                                <div class="form-group">


                                                    <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnadd_Click" />
                                                    <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-primary" Enabled="false" Visible="false" />
                                                    <asp:Button ID="btnclear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="btnclear_Click" />
                                                </div>


                                            </div>
                                            <!-- /.col-lg-6 (nested) -->
                                            <div class="col-lg-6">

                                                <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>

                                                                <!-- /order qty -->
                                                                <div class="form-group">
                                                                    <label>Order Quantity</label>
                                                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtqty" runat="server" CssClass="form-control" onchange="AutoFill()" AutoPostBack="true" OnTextChanged="txtqty_TextChanged" Text="0.00"></asp:TextBox>
                                                                            <%--  <input class="form-control" runat="server" id="txtorderqty" type="text" placeholder="Order Quantity" value="0.00" >--%>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="txtqty" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>


                                                                <!-- /unit price -->
                                                                <div class="form-group">

                                                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                                        <ContentTemplate>
                                                                            <label>Unit Price</label>
                                                                            <asp:TextBox ID="txtprice" runat="server" CssClass="form-control" onchange="AutoFill()" AutoPostBack="true" OnTextChanged="txtprice_TextChanged" Text="0.00"></asp:TextBox>
                                                                            <%--   <input class="form-control" runat="server" id="txtunit_price" type="text" placeholder="Unit Price" value="0.00" >--%>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="txtprice" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>

                                                                </div>


                                                                <!-- /total -->
                                                                <div class="form-group">
                                                                    <label>Total</label>
                                                                    <asp:TextBox ID="txt_total" runat="server" CssClass="form-control" Text="0.00"></asp:TextBox>
                                                                    <%--<input class="form-control" runat="server" id="txttotal" type="text" placeholder="Total" value="0.00" disabled >--%>
                                                                </div>

                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlcatalog" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                    </Triggers>
                                                </asp:UpdatePanel>



                                            </div>

                                        </div>


                                        <div class="row">

                                            <div class="col-lg-12">

                                                <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                            <ContentTemplate>

                                                                <div class="form-group">
                                                                    <!-- /gridview -->
                                                                    <strong>
                                                                        <asp:Label ID="lblresult" runat="server" Text=""></asp:Label></strong>

                                                                </div>

                                                                <div class="panel panel-default">

                                                                    <div class="panel-heading">
                                                                        PO Items
                                                                    </div>

                                                                    <div class="panel-body" style="overflow: auto;">

                                                                        <asp:GridView ID="dgvitems" runat="server" CssClass="table table-striped table-bordered table-hover" OnRowDeleting="dgvitems_RowDeleting" AutoGenerateColumns="false" OnRowDataBound="dgvitems_RowDataBound" Width="100%">
                                                                            <Columns>
                                                                                <%-- <asp:TemplateField>--%>
                                                                                <%-- <ItemTemplate>--%>

                                                                                <%--  <asp:LinkButton Text="Delete" runat="server" OnClientClick="Confirm()" CommandName="Delete" />--%>

                                                                                <asp:CommandField ShowDeleteButton="true" ButtonType="Button" />
                                                                                <asp:BoundField DataField="ctlno" HeaderText="Catalog No" />
                                                                                <asp:BoundField DataField="desc" HeaderText="Description" />
                                                                                <asp:BoundField DataField="exdesc" HeaderText="Extra Description" />
                                                                                <asp:BoundField DataField="o_qty" HeaderText="Order QTY" />
                                                                                <asp:BoundField DataField="uom" HeaderText="UOM" />
                                                                                <asp:BoundField DataField="uprice" HeaderText="Unit Price" />
                                                                                <asp:BoundField DataField="total" HeaderText="Total" />
                                                                                <%--    </ItemTemplate> --%>



                                                                                <%--   </asp:TemplateField>--%>
                                                                            </Columns>
                                                                               <HeaderStyle Wrap="False" />
                                                                                            <RowStyle Wrap="False" />
                                                                        </asp:GridView>
                                                                    </div>

                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnadd" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                    </Triggers>
                                                </asp:UpdatePanel>

                                                <!-- /.table-responsive -->

                                            </div>

                                            <!-- /.panel-body -->
                                        </div>


                                        <!-- /.panel -->
                                    </div>
    <!-- /.col-lg-12 -->
    </div>


                            </div>

                        </div>
                    </div>
                    <!-- /.panel-body -->
    </div>
                <!-- /.panel -->

    <!-- /.col-lg-12 -->
    </div>
            <!-- /.col-lg-12 -->
    </div>
            </div>

        </ContentTemplate>
        <triggers>
            <asp:AsyncPostBackTrigger ControlID="btnsave" />
        </triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function AutoFill() {
            var src1 = document.getElementById('<%=txtqty.ClientID%>').value;
            var src2 = document.getElementById('<%=txtprice.ClientID%>').value;
            document.getElementById('<%=txt_total.ClientID%>').value = src1 * src2;
        }
    </script>

    <%--  <script type="text/javascript">
var prm = Sys.WebForms.PageRequestManager.getInstance();
if (prm != null) {
    prm.add_endRequest(function (sender, e) {
        if (sender._postBackSettings.panelsToUpdate != null) {
            if (e.get_error() != null) {
                var ex = e.get_error();
                var mesg = "HttpStatusCode: " + ex.httpStatusCode;
                mesg += "\n\nName: " + ex.name;
                mesg += "\n\nMessage: " + ex.message;
                mesg += "\n\nDescription: " + ex.description;
                alert(mesg);
                e.set_errorHandled(true);
            }
        }
    });
};
</script>--%>
    <%--   <script type="text/javascript">
var prm = Sys.WebForms.PageRequestManager.getInstance();
if (prm != null) {
    prm.add_endRequest(function (sender, e) {
        if (sender._postBackSettings.panelsToUpdate != null) {
            if (e.get_error() != null) {
                e.set_errorHandled(true);
            }
        }
    });
};
</script>--%>
</asp:Content>
