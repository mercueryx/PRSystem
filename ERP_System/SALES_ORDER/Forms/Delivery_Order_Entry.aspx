<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMstr.Master" AutoEventWireup="true" CodeBehind="Delivery_Order_Entry.aspx.cs" Inherits="ERP_System.SALES_ORDER.Forms.Delivery_Order_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>


    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Delivery Order Entry</h1>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <%--<div class="panel panel-info">--%>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnsave" Text="SAVE" runat="server" CssClass="btn btn-primary" OnClick="btnsave_Click" />
                                        <asp:Button ID="btnnew" Text="New" runat="server" CssClass="btn btn-default" OnClick="btnnew_Click" />

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
                                <li class="active"><a href="#Entry" data-toggle="tab">Delivery Order Header</a>
                                </li>
                                <li><a href="#Details" data-toggle="tab">Delivery Order Details</a>
                                </li>

                            </ul>

                            <!-- Tab panes -->
                            <div class="tab-content">

                                <div class="tab-pane fade in active" id="Entry">

                                    <div class="row">

                                        <div class="col-lg-6">

                                            <!-- /delivery no -->
                                            <div class="form-group">
                                                <label for="disabledSelect">Delivery Order No.</label>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <input class="form-control" runat="server" id="txtdelivery_no" type="text" disabled>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>

                                            <!-- /invoice no -->
                                            <div class="form-group">
                                                <label for="disabledSelect">Invoice No.</label>
                                                <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                                                    <ContentTemplate>
                                                        <input class="form-control" runat="server" id="txtinv_no" type="text" disabled>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>

                                            <!-- /order type -->
                                            <div class="form-group">
                                                <label>Order Type.</label>
                                                <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>

                                            <!-- /Bill to -->
                                            <div class="form-group">
                                                <label>Bill To.</label>

                                                <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlbillto" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlbillto_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddltype" />
                                                    </Triggers>
                                                </asp:UpdatePanel>


                                            </div>

                                            <!-- /sold to -->
                                            <div class="form-group">
                                                <label>Sold To.</label>
                                                <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                                    <ContentTemplate>

                                                        <asp:DropDownList ID="ddlsold" runat="server" CssClass="form-control" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddltype" />
                                                    </Triggers>
                                                </asp:UpdatePanel>

                                            </div>

                                            <!-- /ship to -->
                                            <div class="form-group">
                                                <label>Ship To.</label>
                                                <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                                    <ContentTemplate>

                                                        <asp:DropDownList ID="ddlshipto" runat="server" CssClass="form-control" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddltype" />
                                                    </Triggers>
                                                </asp:UpdatePanel>



                                            </div>

                                        </div>

                                        <div class="col-lg-6">

                                            <!-- /delivery term -->
                                            <div class="form-group">
                                                <label>Delivery Term.</label>


                                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddldelivery_term" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlbillto" />
                                                    </Triggers>
                                                </asp:UpdatePanel>


                                            </div>

                                            <!-- /payment term -->
                                            <div class="form-group">
                                                <label>Payment Term.</label>


                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlpay_term" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlbillto" />
                                                    </Triggers>
                                                </asp:UpdatePanel>


                                            </div>

                                            <%-- /Delivery order date--%>
                                            <div class="form-group">
                                                <label>Delivery Order Date</label>
                                                <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox CssClass="form-control" ID="txtd_date" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="Calendarextender2" PopupButtonID="imgPopup" runat="server" TargetControlID="txtd_date"
                                                            Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnnew" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>


                                            <%-- / ETD --%>
                                            <div class="form-group">
                                                <label>ETD</label>
                                                <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox CssClass="form-control" ID="txtetd" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="Calendarextender4" PopupButtonID="imgPopup" runat="server" TargetControlID="txtetd"
                                                            Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnnew" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>

                                            <%-- / ETA --%>
                                            <div class="form-group">
                                                <label>ETA</label>
                                                <asp:UpdatePanel ID="UpdatePanel43" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox CssClass="form-control" ID="txteta" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="Calendarextender5" PopupButtonID="imgPopup" runat="server" TargetControlID="txteta"
                                                            Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnnew" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>


                                        </div>


                                    </div>
                                </div>

                                <div class="tab-pane fade" id="Details">
                                    <div class="row">
                                        <div class="col-lg-6">

                                            <!-- /sc no -->
                                            <div class="form-group">

                                                <label>Sales Contract No.</label>
                                                <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                                    <ContentTemplate>

                                                        <asp:DropDownList ID="ddlscno" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlscno_SelectedIndexChanged">
                                                        </asp:DropDownList>

                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnadd" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>


                                            <!-- /catalog no -->
                                            <div class="form-group">

                                                <label>Catalog No.</label>
                                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
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

                                                <label>Description.</label>
                                                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <input class="form-control" runat="server" id="txtdesc" type="text" placeholder="Description.">
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
                                                <label>UOM.</label>
                                                <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddluom" runat="server" CssClass="form-control" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>




                                        </div>
                                        <!-- /.col-lg-6 (nested) -->
                                        <div class="col-lg-6">

                                            <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                                <ContentTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>

                                                            <!-- / qty -->
                                                            <div class="form-group">
                                                                <label>Quantity.</label>

                                                                <asp:TextBox ID="txtd_qty" runat="server" CssClass="form-control" AutoPostBack="true" Text="0.00" OnTextChanged="txtd_qty_TextChanged"></asp:TextBox>

                                                            </div>

                                                            <%--foc qty--%>
                                                            <div class="form-group">
                                                                <label>FOC Quantity.</label>
                                                                <asp:TextBox ID="txtadd_foc" runat="server" CssClass="form-control" AutoPostBack="true" Text="0.00" OnTextChanged="txtadd_foc_TextChanged"></asp:TextBox>

                                                            </div>

                                                            <!-- /amount -->
                                                            <div class="form-group">

                                                                <label>Amount.</label>
                                                                <asp:TextBox ID="txtamount" runat="server" CssClass="form-control" AutoPostBack="true" Text="0.00" OnTextChanged="txtamount_TextChanged"></asp:TextBox>
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
                                        <!-- /details button -->

                                    </div>

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnadd_Click" />
                                                <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-primary" Enabled="false" Visible="false" />
                                                <asp:Button ID="btnclear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="btnclear_Click" />
                                                <a href="#" data-file="my file 1" class="btn btn-default rename">Add FOC Items</a>
                                            </div>
                                        </div>

                                        <div class="row">

                                            <div class="col-lg-12">

                                                <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                                                            <ContentTemplate>
                                                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                                    <ContentTemplate>

                                                                        <div class="panel panel-default">

                                                                            <div class="panel-heading">
                                                                                Delivery Order Details
                                                                            </div>

                                                                            <div class="panel-body" style="overflow: auto;">

                                                                                <asp:GridView ID="dgvitems" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" Width="100%" OnRowDeleting="dgvitems_RowDeleting">
                                                                                    <Columns>
                                                                                        <%--   <asp:CommandField ShowSelectButton="true" ButtonType="Button" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:CommandField>--%>
                                                                                        <asp:CommandField ShowDeleteButton="true" ButtonType="Button" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:CommandField>
                                                                                        <%--   <asp:TemplateField HeaderText="tid" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblid" runat="server"
                                                                                                    Text='<%# Eval("no") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>--%>
                                                                                        <%--  <asp:BoundField DataField="no" HeaderText="No." Visible ="false" />--%>
                                                                                        <asp:BoundField DataField="sc_no" HeaderText="Sales Contract No." />
                                                                                        <asp:BoundField DataField="ctlno" HeaderText="Catalog No." />
                                                                                        <asp:BoundField DataField="dsc" HeaderText="Description." />
                                                                                        <asp:BoundField DataField="uom" HeaderText="UOM." />
                                                                                        <asp:BoundField DataField="qty" HeaderText="Quantity." />
                                                                                        <asp:BoundField DataField="foc_qty" HeaderText="FOC Qty." />
                                                                                        <asp:BoundField DataField="amount" HeaderText="Amount." />

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
                                                                <asp:AsyncPostBackTrigger ControlID="btnaddnew" />
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

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                                                            <ContentTemplate>
                                                                <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                                                                    <ContentTemplate>

                                                                        <div class="panel panel-default">

                                                                            <div class="panel-heading">
                                                                                Delivery Order FOC Items
                                                                            </div>

                                                                            <div class="panel-body" style="overflow: auto;">

                                                                                <asp:GridView ID="dgv_items_info" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" Width="100%" OnRowDeleting="dgv_items_info_RowDeleting">
                                                                                    <Columns>

                                                                                        <asp:CommandField ShowDeleteButton="true" ButtonType="Button" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:CommandField>

                                                                                        <%--  <asp:TemplateField HeaderText="tid" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblid" runat="server"
                                                                                                    Text='<%# Eval("id") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>--%>
                                                                                        <%--  <asp:TemplateField HeaderText="id" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblno" runat="server"
                                                                                                    Text='<%# Eval("no") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>--%>
                                                                                        <asp:TemplateField HeaderText="FOC Item.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblf_item" runat="server"
                                                                                                    Text='<%# Eval("ctlno") %>' Visible="true"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Item Description">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblf_idsc" runat="server"
                                                                                                    Text='<%# Eval("i_dsc") %>' Visible="true"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="UOM.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblf_uom" runat="server"
                                                                                                    Text='<%# Eval("uom") %>' Visible="true"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="QTY.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblf_qty" runat="server"
                                                                                                    Text='<%# Eval("qty") %>' Visible="true"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="CLAIM NO.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblf_claim" runat="server"
                                                                                                    Text='<%# Eval("claim") %>' Visible="true"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="GIFT CODE.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblf_gift" runat="server"
                                                                                                    Text='<%# Eval("gift") %>' Visible="true"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="DESCRIPTION.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblf_dsc" runat="server"
                                                                                                    Text='<%# Eval("dsc") %>' Visible="true"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="REMARK.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblf_rmk" runat="server"
                                                                                                    Text='<%# Eval("rmk") %>' Visible="true"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

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
                                                                <asp:AsyncPostBackTrigger ControlID="btnaddnew" />
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



            <div class="modal fade" id="basicModal" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Add Delivery Order FOC Item</h4>
                        </div>

                        <%--   add delivery order foc item--%>
                        <div class="modal-body">

                            <div class="row">

                                <div class="col-lg-12">
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-lg-6">

                                    <%--catalog no--%>
                                    <div class="form-group">
                                        <label>Catalog No.</label>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlf_catalog" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlf_catalog_SelectedIndexChanged">
                                                        </asp:DropDownList>

                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnadd_clear" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnaddnew" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <%--items description--%>
                                    <div class="form-group">
                                        <label>Items Description</label>
                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox CssClass="form-control" AutoPostBack="true" ID="txtf_idsc" runat="server"></asp:TextBox>

                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnadd_clear" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnaddnew" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <%--UOM--%>
                                    <div class="form-group">
                                        <label>UOM.</label>
                                        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlf_uom" runat="server" CssClass="form-control" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnadd_clear" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnaddnew" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <%--Qty--%>
                                    <div class="form-group">
                                        <label>QTY.</label>
                                        <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtf_iqty" runat="server" CssClass="form-control" AutoPostBack="true" Text="0.00" OnTextChanged="txtf_iqty_TextChanged"></asp:TextBox>
                                                        <%--  <input class="form-control" runat="server" id="txtorderqty" type="text" placeholder="Order Quantity." value="0.00" >--%>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnadd_clear" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnaddnew" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>

                                <div class="col-lg-6">

                                    <%--claim no--%>
                                    <div class="form-group">
                                        <label>Claim No.</label>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtf_claim" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                        <%--  <input class="form-control" runat="server" id="txtorderqty" type="text" placeholder="Order Quantity." value="0.00" >--%>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnadd_clear" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnaddnew" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <%--gift code--%>
                                    <div class="form-group">
                                        <label>Gift Code.</label>
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtf_gift" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                        <%--  <input class="form-control" runat="server" id="txtorderqty" type="text" placeholder="Order Quantity." value="0.00" >--%>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnadd_clear" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnaddnew" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <%-- description--%>
                                    <div class="form-group">
                                        <label>Description.</label>
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtf_dsc" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                        <%--  <input class="form-control" runat="server" id="txtorderqty" type="text" placeholder="Order Quantity." value="0.00" >--%>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnadd_clear" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnaddnew" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <%--remark--%>
                                    <div class="form-group">
                                        <label>Remark.</label>
                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtrmk" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                        <%--  <input class="form-control" runat="server" id="txtorderqty" type="text" placeholder="Order Quantity." value="0.00" >--%>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnadd_clear" />
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

                            <div class="row">
                                <div class="col-lg-6">



                                    <%--timer and result--%>
                                    <div class="form-group">
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
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


                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="btnadd_clear" Text="Clear" runat="server" CssClass="btn btn-primary" OnClick="btnadd_clear_Click" />
                            <asp:Button ID="btnaddnew" Text="Save changes" runat="server" CssClass="btn btn-primary" OnClick="btnaddnew_Click" />

                            <%--                            <button type="button" class="btn btn-primary">Save changes</button>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnsave" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
