<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMstr.Master" AutoEventWireup="true" CodeBehind="Sales_Contract_Entry.aspx.cs" Inherits="ERP_System.SALES_ORDER.Forms.Sales_Contract_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>


    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Sales Contract Entry</h1>
                </div>

                <div class="row">

                    <div class="col-lg-12">

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
                                <li class="active"><a href="#Entry" data-toggle="tab">Sales Contract Header</a>
                                </li>
                                <li><a href="#Details" data-toggle="tab">Sales Contract Details</a>
                                </li>

                            </ul>

                            <!-- Tab panes -->
                            <div class="tab-content">

                                <div class="tab-pane fade in active" id="Entry">

                                    <div class="row">

                                        <div class="col-lg-6">

                                            <!-- /sc no -->
                                            <div class="form-group">
                                                <label for="disabledSelect">SC No.</label>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <input class="form-control" runat="server" id="txtsc_no" type="text" placeholder="SC No." disabled>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>

                                            <!-- /Group -->
                                            <div class="form-group">
                                                <label>Group.</label>

                                                <asp:DropDownList ID="ddlgroup" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </div>

                                            <!-- /Bill to -->
                                            <div class="form-group">
                                                <label>Bill To.</label>

                                                <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlbillto" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlbillto_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddltype" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlgroup" />
                                                    </Triggers>
                                                </asp:UpdatePanel>

                                            </div>

                                            <!-- /sold to -->
                                            <div class="form-group">
                                                <label>Sold To.</label>

                                                <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlsold" runat="server" CssClass="form-control" AutoPostBack="True">
                                                        </asp:DropDownList>

                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlgroup" />
                                                    </Triggers>
                                                </asp:UpdatePanel>


                                            </div>

                                            <!-- /signatory-->
                                            <div class="form-group">
                                                <label>Signatory</label>
                                                <asp:UpdatePanel ID="UpdatePanel43" runat="server">
                                                    <ContentTemplate>

                                                        <asp:DropDownList ID="ddlsignatory" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>

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
                                                <label for="disabledSelect">Revision No.</label>
                                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                    <ContentTemplate>
                                                        <input class="form-control" runat="server" id="txtrev" type="text" placeholder="Revision No." disabled>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                    </Triggers>
                                                </asp:UpdatePanel>

                                            </div>

                                            <!-- /type -->
                                            <div class="form-group">
                                                <label>Type.</label>
                                                <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>LOCAL</asp:ListItem>
                                                    <asp:ListItem>EXPORT</asp:ListItem>
                                                </asp:DropDownList>

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

                                            <%-- /sc date--%>
                                            <div class="form-group">
                                                <label>SC Date</label>
                                                <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox CssClass="form-control" ID="txtscdate" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="Calendarextender2" PopupButtonID="imgPopup" runat="server" TargetControlID="txtscdate"
                                                            Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btncancel" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>

                                        </div>


                                    </div>
                                </div>

                                <div class="tab-pane fade" id="Details">
                                    <div class="row">
                                        <div class="col-lg-6">

                                            <!-- /catalog no -->
                                            <div class="form-group">

                                                <label>Catalog No.</label>
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

                                            <!-- /ex description -->
                                            <div class="form-group">
                                                <label>Extra Description.</label>
                                                <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                            <ContentTemplate>
                                                                <input class="form-control" runat="server" id="txtextra_dsc" type="text" placeholder="Extra Description.">
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
                                                <label>Unit of measurement.</label>
                                                <asp:DropDownList ID="ddluom" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
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
                                                                <label>Order Quantity.</label>
                                                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtadd_qty" runat="server" CssClass="form-control" onchange="AutoFill()" AutoPostBack="true" OnTextChanged="txtadd_qty_TextChanged" Text="0.00"></asp:TextBox>
                                                                        <%--  <input class="form-control" runat="server" id="txtorderqty" type="text" placeholder="Order Quantity." value="0.00" >--%>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="txtadd_qty" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>

                                                            <div class="form-group">
                                                                <label>FOC Quantity.</label>
                                                                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtadd_foc" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtadd_foc_TextChanged" Text="0.00"></asp:TextBox>
                                                                        <%--  <input class="form-control" runat="server" id="txtorderqty" type="text" placeholder="Order Quantity." value="0.00" >--%>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="txtadd_foc" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>

                                                            <!-- /unit price -->
                                                            <div class="form-group">

                                                                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                                    <ContentTemplate>
                                                                        <label>Unit Price.</label>
                                                                        <asp:TextBox ID="txtprice" runat="server" CssClass="form-control" onchange="AutoFill()" AutoPostBack="true" Text="0.00" OnTextChanged="txtprice_TextChanged"></asp:TextBox>
                                                                        <%--   <input class="form-control" runat="server" id="txtunit_price" type="text" placeholder="Unit Price." value="0.00" >--%>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="txtprice" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>

                                                            </div>


                                                            <!-- /total -->
                                                            <div class="form-group">
                                                                <label>Total.</label>
                                                                <asp:TextBox ID="txt_total" runat="server" CssClass="form-control" Text="0.00"></asp:TextBox>
                                                                <%--<input class="form-control" runat="server" id="txttotal" type="text" placeholder="Total." value="0.00" disabled >--%>
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
                                                <asp:Button ID="btnclear" runat="server" Text="Clear" CssClass="btn btn-primary" />
                                                <a href="#" data-file="my file 1" class="btn btn-default rename">Add Details</a>
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
                                                                                Sales Contract Details
                                                                            </div>

                                                                            <div class="panel-body" style="overflow: auto;">

                                                                                <asp:GridView ID="dgvitems" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" Width="100%" OnRowDeleting="dgvitems_RowDeleting" OnSelectedIndexChanged="dgvitems_SelectedIndexChanged">
                                                                                    <Columns>
                                                                                        <asp:CommandField ShowSelectButton="true" ButtonType="Button" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:CommandField>
                                                                                        <asp:CommandField ShowDeleteButton="true" ButtonType="Button" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:CommandField>
                                                                                        <asp:TemplateField HeaderText="tid" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblid" runat="server"
                                                                                                    Text='<%# Eval("no") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <%--  <asp:BoundField DataField="no" HeaderText="No." Visible ="false" />--%>
                                                                                        <asp:BoundField DataField="ctlno" HeaderText="Catalog No." />
                                                                                        <asp:BoundField DataField="desc" HeaderText="Description." />
                                                                                        <asp:BoundField DataField="exdesc" HeaderText="Extra Description." />
                                                                                        <asp:BoundField DataField="o_qty" HeaderText="Order QTY." />
                                                                                        <asp:BoundField DataField="foc_qty" HeaderText="FOC QTY." />
                                                                                        <asp:BoundField DataField="uom" HeaderText="UOM." />
                                                                                        <asp:BoundField DataField="uprice" HeaderText="Unit Price." />
                                                                                        <asp:BoundField DataField="total" HeaderText="Total." />
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
                                                                                Sales Contract Delivery Details
                                                                            </div>

                                                                            <div class="panel-body" style="overflow: auto;">

                                                                                <asp:GridView ID="dgv_items_info" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" Width="100%" OnRowDeleting="dgv_items_info_RowDeleting">
                                                                                    <Columns>

                                                                                        <asp:CommandField ShowDeleteButton="true" ButtonType="Button" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:CommandField>

                                                                                        <asp:TemplateField HeaderText="tid" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblid" runat="server"
                                                                                                    Text='<%# Eval("id") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="id" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblno" runat="server"
                                                                                                    Text='<%# Eval("no") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ETD.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbletd" runat="server"
                                                                                                    Text='<%# Eval("etd") %>' Visible="true"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ETA">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbleta" runat="server"
                                                                                                    Text='<%# Eval("eta") %>' Visible="true"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="QTY.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbli_qty" runat="server"
                                                                                                    Text='<%# Eval("qty") %>' Visible="true"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="FOC QTY.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblfoc_qty" runat="server"
                                                                                                    Text='<%# Eval("foc_qty") %>' Visible="true"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="SHIPPED QTY.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbls_qty" runat="server"
                                                                                                    Text='<%# Eval("s_qty") %>' Visible="true"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="PLANNED QTY">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblp_qty" runat="server"
                                                                                                    Text='<%# Eval("p_qty") %>' Visible="true"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <%--   <asp:BoundField DataField="t_id" HeaderText="NO." Visible="false" />
                                                                                        <asp:BoundField DataField="etd" HeaderText="ETD." />
                                                                                        <asp:BoundField DataField="eta" HeaderText="ETA." />
                                                                                        <asp:BoundField DataField="qty" HeaderText="QTY." />
                                                                                        <asp:BoundField DataField="foc_qty" HeaderText="FOC QTY." />
                                                                                        <asp:BoundField DataField="s_qty" HeaderText="SHIPPED QTY." />
                                                                                        <asp:BoundField DataField="p_qty" HeaderText="PLANNED QTY." />--%>
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
                            <h4 class="modal-title" id="myModalLabel">Add Sales Contract Delivery Details</h4>
                        </div>

                        <%--   add sales contract delivery details--%>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <label>Catalog No.</label>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>

                                                <asp:DropDownList ID="ddladd_catalog" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcatalog_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnadd" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-lg-6">

                                    <%--ETD--%>
                                    <div class="form-group">
                                        <label>ETD</label>
                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox CssClass="form-control" AutoPostBack="true" ID="txtetd" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="Calendarextender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtetd"
                                                            Format="yyyy-MM-dd"></cc1:CalendarExtender>
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

                                    <%--ETA--%>
                                    <div class="form-group">

                                        <label>ETA</label>
                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox CssClass="form-control" AutoPostBack="true" ID="txteta" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="Calendarextender3" PopupButtonID="imgPopup" runat="server" TargetControlID="txteta"
                                                            Format="yyyy-MM-dd"></cc1:CalendarExtender>
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
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtqty" runat="server" CssClass="form-control" AutoPostBack="true" Text="0.00" OnTextChanged="txtqty_TextChanged"></asp:TextBox>
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

                                    <%--FOC Qty--%>
                                    <div class="form-group">
                                        <label>FOC QTY.</label>
                                        <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtfoc_qty" runat="server" CssClass="form-control" AutoPostBack="true" Text="0.00" OnTextChanged="txtfoc_qty_TextChanged"></asp:TextBox>
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

                                    <%--Shipped Qty--%>
                                    <div class="form-group">
                                        <label>Shipped QTY.</label>
                                        <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtshipped_qty" runat="server" CssClass="form-control" AutoPostBack="true" Text="0.00" OnTextChanged="txtshipped_qty_TextChanged"></asp:TextBox>
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

                                    <%--Planned Qty--%>
                                    <div class="form-group">
                                        <label>PLANNED QTY.</label>
                                        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                            <ContentTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtplan_qty" runat="server" CssClass="form-control" AutoPostBack="true" Text="0.00" OnTextChanged="txtplan_qty_TextChanged"></asp:TextBox>
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

    <script type="text/javascript">
        function AutoFill() {
            var src1 = document.getElementById('<%=txtadd_qty.ClientID%>').value;
            var src2 = document.getElementById('<%=txtprice.ClientID%>').value;
            document.getElementById('<%=txt_total.ClientID%>').value = src1 * src2;
        }
    </script>


</asp:Content>
