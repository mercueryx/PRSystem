<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMstr.Master" AutoEventWireup="true" CodeBehind="GRN_Edit.aspx.cs" Inherits="ERP_System.GRN_Module.Forms.GRN_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div class="row">

                <div class="col-lg-12">
                    <h1 class="page-header">GRN Edit</h1>
                </div>

                <div class="row">

                    <div class="col-lg-12">
                        <div class="panel panel-info">

                            <div class="panel-body">

                                <div class="panel panel-info">

                                    <div class="panel-body">
                                        <div class="row">

                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label for="disabledSelect">Search by GRN date</label>
                                                    <asp:TextBox CssClass="form-control" AutoPostBack="true" ID="txtsearch_date" runat="server" ReadOnly="false" AutoCompleteType="Disabled" OnTextChanged="txtsearch_date_TextChanged"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="Calendarextender2" PopupButtonID="imgPopup" runat="server" TargetControlID="txtsearch_date"
                                                        Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                                </div>
                                            </div>

                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <div class="form-group">
                                                        <label for="disabledSelect">Search by GRN No</label>
                                                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddls_grn" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddls_grn_SelectedIndexChanged" >
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="txtsearch_date" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">


                                        <%--timer and result--%>
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                                <ContentTemplate>
                                                    <asp:Timer ID="resulttimer" runat="server" Interval="3000" Enabled="False" OnTick="resulttimer_Tick"></asp:Timer>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                                    <asp:AsyncPostBackTrigger ControlID="btnupdate" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                        </div>


                                    </div>
                                </div>

                                <div class="row">

                                    <div class="col-lg-6">
                                        <%-- GRN_no--%>
                                        <div class="form-group">
                                            <label>GRN No</label>

                                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                <ContentTemplate>
                                                    <input class="form-control" runat="server" id="txtgrn" type="text" placeholder="GRN No" disabled>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddls_grn" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                        </div>

                                        <%--vendor--%>
                                        <div class="form-group">
                                            <label>Vendor</label>
                                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlven" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlven_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddls_grn" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>

                                        <%--remark--%>
                                        <div class="form-group">
                                            <label>Remark</label>
                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                <ContentTemplate>
                                                    <textarea runat="server" id="txtrmk" class="form-control" rows="3"></textarea>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddls_grn" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>

                                        <%--button--%>
                                        <div class="form-group">
                                            <asp:Button ID="btnupdate" Text="UPDATE HEADER" runat="server" CssClass="btn btn-primary" OnClick="btnupdate_Click" />
                                            <%--   <asp:Button ID="btncancel" Text="CANCEL" runat="server" CssClass="btn btn-default" />--%>
                                            <a href="#" data-file="my file 1" class="btn btn-default rename">Add new PO</a>
                                        </div>

                                    </div>

                                    <div class="col-lg-6">

                                        <%-- grn date--%>
                                        <div class="form-group">
                                            <label>GRN Date</label>
                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox CssClass="form-control" ID="txtgrndate" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="Calendarextender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtgrndate"
                                                        Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddls_grn" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>

                                        <%--  do no--%>
                                        <div class="form-group">
                                            <label>DO No</label>
                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                <ContentTemplate>
                                                    <input class="form-control" runat="server" id="txtdo" type="text" placeholder="DO No">
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddls_grn" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>


                                    </div>

                                </div>

                                <%--tab title--%>
                                <ul class="nav nav-pills">
                                    <li class="active"><a href="#Header" data-toggle="tab">GRN Header</a>
                                    </li>
                                    <li><a href="#Details" data-toggle="tab">GRN Details</a>
                                    </li>

                                </ul>

                                <%--gridview--%>
                                <div class="tab-content">

                                    <%--grn header--%>
                                    <div class="tab-pane fade in active" id="Header">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="panel panel-default">
                                                    <%-- <div class="panel-heading">
                                                        PO Header
                                                    </div>--%>
                                                    <div class="panel-body" style="overflow: auto;">

                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:GridView ID="dgvheader" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false" OnRowDeleting="dgvheader_RowDeleting">
                                                                                            <Columns>
                                                                                                <asp:CommandField ShowDeleteButton="true" ButtonType="Button" ItemStyle-HorizontalAlign="Center">
                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                </asp:CommandField>
                                                                                                <asp:BoundField DataField="po_no" HeaderText="PO Number" />
                                                                                                <asp:BoundField DataField="order_date" HeaderText="Order Date" />
                                                                                                <asp:BoundField DataField="ven_type" HeaderText="Vendor Type" />
                                                                                                <asp:BoundField DataField="add_usn" HeaderText="Submit by" />
                                                                                            </Columns>

                                                                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />

                                                                                            <RowStyle Wrap="False" />

                                                                                        </asp:GridView>
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:AsyncPostBackTrigger ControlID="btnaddnew" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="ddlven" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="ddls_grn" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnupdate" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </div>

                                                </div>

                                                <!-- /.table-responsive -->

                                            </div>

                                            <!-- /.panel-body -->
                                        </div>
                                    </div>

                                    <%--grn details--%>
                                    <div class="tab-pane fade" id="Details">

                                        <div class="row">

                                            <div class="col-lg-12">


                                                <div class="panel panel-default">

                                                    <%-- <div class="panel-heading">
                                                        GRN Details
                                                    </div>--%>

                                                    <div class="panel-body" style="overflow: auto;">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:GridView ID="dgvitems" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false" OnSelectedIndexChanged="dgvitems_SelectedIndexChanged">
                                                                                            <Columns>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Button ID="btnselect" runat="server" Text="Update Quantity" CommandName="Select" CssClass="btn btn-info" />
                                                                                                        <%-- <asp:Label ID="lblpo_no" Text='<%# Eval("po_no") %>' runat="server" Visible="false" />--%>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="po_no" HeaderText="PO No." />
                                                                                                <asp:BoundField DataField="ctlno" HeaderText="Catalog No." />
                                                                                                <asp:BoundField DataField="desc" HeaderText="Description." />
                                                                                                <asp:BoundField DataField="exdesc" HeaderText="Extra Description." />
                                                                                                <asp:BoundField DataField="uom" HeaderText="UOM." />
                                                                                                <asp:BoundField DataField="o_qty" HeaderText="Order Qty." />
                                                                                                <asp:TemplateField HeaderText="Received Qty">
                                                                                                    <ItemTemplate>

                                                                                                        <asp:TextBox ID="txtrecqty" runat="server" Text='<%# Eval("rec_qty") %>'></asp:TextBox>

                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="bal_qty" HeaderText="Balance Qty." />
                                                                                            </Columns>
                                                                                            <HeaderStyle Wrap="False" />
                                                                                            <RowStyle Wrap="False" />
                                                                                        </asp:GridView>
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:AsyncPostBackTrigger ControlID="btnaddnew" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="ddlven" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="ddls_grn" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnupdate" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                </div>

                                                <!-- /.table-responsive -->

                                            </div>

                                            <!-- /.panel-body -->
                                        </div>


                                        <!-- /.panel -->
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>

                    <%--tab with gridview for header and details--%>



                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
            </div>


            <div class="modal fade" id="basicModal" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Add new PO</h4>
                        </div>
                        <div class="modal-body">

                            <%--po no--%>
                            <div class="row">
                                <div class="col-lg-6">
                                    <%--po no--%>
                                    <div class="form-group">
                                        <label>PO to combine</label>
                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlpo" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlpo_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlven" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddls_grn" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnupdate" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                            </div>

                              <div class="row">
                                    <div class="col-lg-6">


                                        <%--timer and result--%>
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                <ContentTemplate>
                                                    <asp:Timer ID="resulttimer2" runat="server" Interval="3000" Enabled="False" OnTick="resulttimer2_Tick"></asp:Timer>
                                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                        <ContentTemplate>
                                                            <strong>
                                                                <asp:Label ID="lblsaveresult2" runat="server" Text=""></asp:Label></strong>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="resulttimer2" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnaddnew" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                        </div>


                                    </div>
                                </div>

                            <%--tab title--%>
                            <ul class="nav nav-pills">
                                <li class="active"><a href="#items_Header" data-toggle="tab">GRN Header</a>
                                </li>
                                <li><a href="#items_Details" data-toggle="tab">GRN Details</a>
                                </li>

                            </ul>

                            <%--gridview--%>
                            <div class="tab-content">

                                <%--grn header--%>
                                <div class="tab-pane fade in active" id="items_Header">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="panel panel-default">
                                                <%-- <div class="panel-heading">
                                                        PO Header
                                                    </div>--%>
                                                <div class="panel-body" style="overflow: auto;">

                                                    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:GridView ID="dgvadd_header" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false" OnRowDeleting="dgvadd_header_RowDeleting">
                                                                                        <Columns>
                                                                                            <asp:CommandField ShowDeleteButton="true" ButtonType="Button" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="po_no" HeaderText="PO Number" />
                                                                                            <asp:BoundField DataField="order_date" HeaderText="Order Date" />
                                                                                            <asp:BoundField DataField="ven_type" HeaderText="Vendor Type" />
                                                                                            <asp:BoundField DataField="add_usn" HeaderText="Submit by" />
                                                                                        </Columns>

                                                                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />


                                                                                        <RowStyle Wrap="False" />
                                                                                    </asp:GridView>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="ddlpo" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="ddlven" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddls_grn" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnaddnew" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>

                                            </div>

                                            <!-- /.table-responsive -->

                                        </div>

                                        <!-- /.panel-body -->
                                    </div>
                                </div>

                                <%--grn details--%>
                                <div class="tab-pane fade" id="items_Details">

                                    <div class="row">

                                        <div class="col-lg-12">


                                            <div class="panel panel-default">

                                                <%--  <div class="panel-heading">
                                                        GRN Details
                                                    </div>--%>

                                                <div class="panel-body" style="overflow: auto;">
                                                    <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:GridView ID="dgvadd_details" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false">
                                                                                        <Columns>

                                                                                            <asp:BoundField DataField="po_no" HeaderText="PO No." />
                                                                                            <asp:BoundField DataField="ctlno" HeaderText="Catalog No." />
                                                                                            <asp:BoundField DataField="desc" HeaderText="Description." />
                                                                                            <asp:BoundField DataField="exdesc" HeaderText="Extra Description." />
                                                                                            <asp:BoundField DataField="uom" HeaderText="UOM." />
                                                                                            <asp:BoundField DataField="o_qty" HeaderText="Order Qty." />
                                                                                            <asp:TemplateField HeaderText="Received Qty">
                                                                                                <ItemTemplate>

                                                                                                    <asp:TextBox ID="txtrecqty" runat="server" Text="0.00"></asp:TextBox>

                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="bal_qty" HeaderText="Balance Qty." />
                                                                                        </Columns>
                                                                                        <HeaderStyle Wrap="False" />
                                                                                        <RowStyle Wrap="False" />
                                                                                    </asp:GridView>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="ddlpo" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="ddlven" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddls_grn" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnaddnew" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                            </div>

                                            <!-- /.table-responsive -->

                                        </div>

                                        <!-- /.panel-body -->
                                    </div>


                                    <!-- /.panel -->
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer">
                             <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                           <%--    <asp:Button ID="btnclose" Text="Close" runat="server" CssClass="btn btn-primary" OnClick="btnclose_Click" />--%>
                             <asp:Button ID="btnaddnew" Text="Save changes" runat="server" CssClass="btn btn-primary" OnClick="btnaddnew_Click" />
                           
<%--                            <button type="button" class="btn btn-primary">Save changes</button>--%>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnupdate" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
