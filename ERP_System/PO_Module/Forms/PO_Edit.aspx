<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMstr.Master" AutoEventWireup="true" CodeBehind="PO_Edit.aspx.cs" Inherits="ERP_System.PO_Module.Forms.PO_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">PO Edit</h1>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                Edit PO
                            </div>

                            <div class="panel-body">

                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label for="disabledSelect">Search by order date</label>
                                            <asp:TextBox CssClass="form-control" AutoPostBack="true" ID="txtsearch_date" runat="server" ReadOnly="false" AutoCompleteType="Disabled" OnTextChanged="txtsearch_date_TextChanged"></asp:TextBox>
                                            <cc1:CalendarExtender ID="Calendarextender2" PopupButtonID="imgPopup" runat="server" TargetControlID="txtsearch_date"
                                                Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <label for="disabledSelect">Search by PO No</label>
                                                <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddls_pono" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddls_pono_SelectedIndexChanged">
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
                                                    <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
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
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <!-- /company code -->
                                                <div class="form-group">
                                                    <label>Company Code</label>
                                                    <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                                        <ContentTemplate>
                                                           <%-- <asp:DropDownList ID="ddlcom" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcom_SelectedIndexChanged">
                                                            </asp:DropDownList>--%>
                                                              <input class="form-control" runat="server" id="txtcom" type="text" placeholder="Company Code" disabled>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <!-- /vendor type -->
                                                <div class="form-group">
                                                    <label>Vendor Type</label>
                                                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                        <ContentTemplate>
                                                           <%-- <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                <ContentTemplate>--%>
                                                                    <asp:DropDownList ID="ddlven_type" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlven_type_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                               <%-- </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlcom" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>--%>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
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
                                                                    <asp:DropDownList ID="ddlven_code" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlven_code_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlven_type" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <!-- /purchase term -->
                                                <div class="form-group">
                                                    <label>Purchase Term</label>
                                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                        <ContentTemplate>
                                                           <%-- <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                                                <ContentTemplate>--%>
                                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlpur_term" runat="server" CssClass="form-control">
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="ddlven_code" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                <%--</ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlcom" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>--%>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
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
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>


                                                </div>

                                                <div class="form-group">
                                                    <asp:Button ID="btnupdate" Text="UPDATE" runat="server" CssClass="btn btn-primary" OnClick="btnupdate_Click" />
                                                    <asp:Button ID="btncancel" Text="CANCEL" runat="server" CssClass="btn btn-default" OnClick="btncancel_Click" />
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
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
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
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
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
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
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
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>

                                                <!-- /place to delivery -->
                                                <div class="form-group">
                                                    <label>Place to delivery</label>
                                                    <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlplace" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="FGS">FGS | FINISH GOOD STORE</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
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
                                                   <%-- <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                        <ContentTemplate>--%>
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <input class="form-control" runat="server" id="txtdesc" type="text" placeholder="Description">
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlcatalog" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                      <%--  </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>--%>

                                                </div>

                                                <!-- /ex description -->
                                                <div class="form-group">
                                                    <label>Extra Description</label>
                                                   <%-- <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                        <ContentTemplate>--%>
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                <ContentTemplate>
                                                                    <input class="form-control" runat="server" id="txtextra_dsc" type="text" placeholder="Extra Description">
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlcatalog" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        <%--</ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>--%>

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
                                                    <%--<asp:Button ID="btni_update" runat="server" Text="Update" CssClass="btn btn-primary" />--%>
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
                                                                            <asp:TextBox ID="txtqty" runat="server" CssClass="form-control" onchange="AutoFill()" AutoPostBack="true" Text="0.00" OnTextChanged="txtqty_TextChanged"></asp:TextBox>
                                                                            <%--  <input class="form-control" runat="server" id="txtorderqty" type="text" placeholder="Order Quantity" value="0.00" >--%>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="txtqty" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>


                                                                <!-- /unit price -->
                                                                <div class="form-group">

                                                                 <%--   <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                                        <ContentTemplate>--%>
                                                                            <label>Unit Price</label>
                                                                            <asp:TextBox ID="txtprice" runat="server" CssClass="form-control" onchange="AutoFill()" AutoPostBack="true" Text="0.00" OnTextChanged="txtprice_TextChanged"></asp:TextBox>
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
                                                  <%--  </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
                                                    </Triggers>
                                                </asp:UpdatePanel>--%>



                                            </div>

                                        </div>


                                        <div class="row">

                                            <div class="col-lg-12">

                                                <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                            <ContentTemplate>

                                                                <%--   <div class="form-group">
                                                                    <!-- /gridview -->
                                                                    <strong>
                                                                        <asp:Label ID="lblresult" runat="server" Text=""></asp:Label></strong>

                                                                </div>--%>

                                                                <div class="panel panel-default">

                                                                    <div class="panel-heading">
                                                                        PO Items
                                                                    </div>

                                                                    <div class="panel-body" style="overflow: auto;">

                                                                        <asp:GridView ID="dgvitems" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" Width="100%" OnRowDeleting="dgvitems_RowDeleting" DataKeyNames="id">
                                                                            <Columns>
                                                                                <%-- <asp:TemplateField>--%>
                                                                                <%-- <ItemTemplate>--%>

                                                                                <%--  <asp:LinkButton Text="Delete" runat="server" OnClientClick="Confirm()" CommandName="Delete" />--%>

                                                                                <asp:CommandField ShowDeleteButton="true" ButtonType="Button" />
                                                                              <%--  <asp:BoundField DataField="id" HeaderText="id" Visible="false" />--%>
                                                                                <asp:BoundField DataField="catalog_no" HeaderText="Catalog No" />
                                                                                <asp:BoundField DataField="dsc" HeaderText="Description" />
                                                                                <asp:BoundField DataField="ext_dsc" HeaderText="Extra Description" />
                                                                                <asp:BoundField DataField="order_qty" HeaderText="Order QTY" />
                                                                                <asp:BoundField DataField="uom" HeaderText="UOM" />
                                                                                <asp:BoundField DataField="unit_price" HeaderText="Unit Price" />
                                                                                <asp:BoundField DataField="total_amt" HeaderText="Total" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddls_pono" />
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
            <asp:AsyncPostBackTrigger ControlID="txtsearch_date" />
        </triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function AutoFill() {
            var src1 = document.getElementById('<%=txtqty.ClientID%>').value;
            var src2 = document.getElementById('<%=txtprice.ClientID%>').value;
            document.getElementById('<%=txt_total.ClientID%>').value = src1 * src2;
        }
    </script>

</asp:Content>
