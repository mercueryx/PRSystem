<%@ Page Language="C#" MasterPageFile="~/MenuMstr.Master" AutoEventWireup="true" CodeBehind="PR_Certify.aspx.cs" Inherits="ERP_System.PR_Module.Forms.PR_Certify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <style>
        .item_name_class {
                display: block;
               
                overflow: auto;
                width: 500px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>

            <div class="row">

                <div class="col-lg-12">
                    <h1 class="page-header">PR CERTIFY</h1>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-info">

                            <div class="panel-body">

                                <div class="row">

                                    <div class="col-lg-6">
                                        <%--  Department --%>
                                        <%--   <div class="form-group">
                                            <label>Department.</label>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddldpt" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>--%>

                                        <%--Requestor --%>
                                        <div class="form-group">
                                            <label>Requestor</label>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox CssClass="form-control" ID="txtnm" runat="server"></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>

                                    <div class="col-lg-6">

                                        <%-- search by request date--%>
                                        <div class="form-group">
                                            <label>Request Date</label>
                                            <asp:TextBox CssClass="form-control" ID="txtreq_date" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                            <cc1:CalendarExtender ID="Calendarextender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtreq_date"
                                                Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                        </div>






                                    </div>
                                </div>

                                <div class="row hidden">
                                    <div class="col-lg-6">
                                                                                        <!-- /department -->
                                                <div class="form-group">
                                                    <label for="disabledSelect">Department</label>
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlr_dpt" runat="server" CssClass="form-control" AutoPostBack="True" >
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <%--search button--%>
                                        <div class="form-group">

                                            <asp:Button ID="btnsearch" Text="Filter" runat="server" CssClass="btn btn-default" OnClick="btnsearch_Click" />

                                        </div>
                                    </div>
                                </div>

                                <%--tab title--%>
                                <ul class="nav nav-pills">
                                    <li class="active"><a href="#Header" data-toggle="tab">Pending PR</a>
                                    </li>
                                    <%--  <li><a href="#Details" data-toggle="tab">Packing Details</a>
                                    </li>--%>
                                </ul>

                                <div class="tab-content">
                                    <div class="tab-pane fade in active" id="Header">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="panel panel-default">

                                                    <div class="panel-body">

                                                        <div class="row">

                                                            <div class="col-lg-6">
                                                                <div class="form-group">
                                                                     <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                        <ContentTemplate>
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlall" runat="server" OnSelectedIndexChanged="ddlall_SelectedIndexChanged" AutoPostBack="true">
                                                                                <asp:ListItem Value="SELECT">SELECT ACTION</asp:ListItem>
                                                                                <asp:ListItem Value="PENDING">PENDING ALL</asp:ListItem>
                                                                                <asp:ListItem Value="CERTIFIED">CERTIFIED ALL</asp:ListItem>
                                                                                <asp:ListItem Value="REJECTED">REJECT ALL</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnsearch" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                             </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                         </div>
                                                                      <div class="form-group">
                                                                    <%-- <asp:Button ID="btnall" Text="Certify All" runat="server" CssClass="btn btn-primary" OnClick="btnall_Click" />--%>
                                                                    <asp:Button ID="btnsave" Text="Save" runat="server" CssClass="btn btn-primary" OnClick="btnsave_Click" />
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:Timer ID="resulttimer" runat="server" Interval="3000" Enabled="False" OnTick="resulttimer_Tick"></asp:Timer>
                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <ContentTemplate>
                                                                                    <strong>
                                                                                        <asp:Label ID="lblsaveresult" runat="server" Text=""></asp:Label>

                                                                                    </strong>
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

                                                        </div>

                                                    </div>
                                  
                                                    <div class="panel-body" style="overflow: auto;">
                                                           <div class="row">
                                                            <div class="col-lg-12 pull-right">
                                                                <em class="label_em">** Tips: Mouse Arrow Stay On List, Pressed on Keyboard Shift Key and Scroll Mouse Center the List will be move to left or right.</em>
                                                            </div>
                                                        </div>
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>

                                                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:GridView ID="dgvheader" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False" AllowPaging="true" PageSize="20" OnPageIndexChanging="dgvheader_PageIndexChanging">
                                                                            <Columns>
                                                                                <%-- <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:Button ID="btnselect" runat="server" Text="Select" CommandName="Select" CssClass="btn btn-info" />
                                                                                   
                                                                                    </ItemTemplate>

                                                                                </asp:TemplateField>--%>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlsts" runat="server">
                                                                                            <asp:ListItem Value="SELECT">SELECT ACTION</asp:ListItem>
                                                                                            <asp:ListItem Value="OPEN">OPEN</asp:ListItem>
                                                                                            <asp:ListItem Value="CERTIFIED">CERTIFY</asp:ListItem>
                                                                                            <asp:ListItem Value="REJECTED">REJECT</asp:ListItem>

                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>

                                                                                </asp:TemplateField>

                                                                                <asp:BoundField DataField="id" HeaderText="ID"></asp:BoundField>
                                                                                <asp:BoundField DataField="pr_rn" HeaderText="PR NO"></asp:BoundField>
                                                                                <asp:BoundField DataField="req" HeaderText="Requestor"></asp:BoundField>
                                                                                <asp:BoundField DataField="sec" HeaderText="Section"></asp:BoundField>
                                                                                 <asp:BoundField DataField="req_com" HeaderText="Company"></asp:BoundField>
                                                                                <asp:BoundField DataField="req_dt" HeaderText="Request Date"></asp:BoundField>
                                                                                <asp:BoundField DataField="itm" HeaderText="Item Name" ItemStyle-CssClass="item_name_class"></asp:BoundField>
                                                                                 <asp:BoundField DataField="qty" HeaderText="QTY" ></asp:BoundField>
                                                                                <asp:BoundField DataField="pur" HeaderText="Purpose/Remark"></asp:BoundField>
                                                                                <asp:BoundField DataField="lvl" HeaderText="Level"></asp:BoundField>
                                                                                  <asp:BoundField DataField="add_usn" HeaderText="Submited By"></asp:BoundField>
                                                                            </Columns>

                                                                            <HeaderStyle Wrap="False" />
                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
                                                                        <RowStyle Wrap="False" />

                                                                        </asp:GridView>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="btnsearch" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>

                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </div>

                                                </div>

                                                <!-- /.table-responsive -->

                                            </div>

                                            <!-- /.panel-body -->
                                        </div>
                                    </div>

                                    <%--     <div class="tab-pane fade" id="Details">

                                        <div class="row">

                                            <div class="col-lg-12">

                                                <div class="panel panel-default">

                                                    <div class="panel-body" style="overflow: auto;">

                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="dgvitems" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false">
                                                                    <Columns>


                                                                        <asp:BoundField DataField="catalog_no" HeaderText="Catalog No." />
                                                                        <asp:BoundField DataField="dsc" HeaderText="Description." />
                                                                        <asp:BoundField DataField="loc" HeaderText="Location." />
                                                                        <asp:BoundField DataField="req_qty" HeaderText="Req Qty." />
                                                                        <asp:BoundField DataField="o_qty" HeaderText="OnHand Qty." />


                                                                    </Columns>
                                                                    <HeaderStyle Wrap="False" />
                                                                    <RowStyle Wrap="False" />
                                                                </asp:GridView>

                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnsearch" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </div>

                                                </div>

                                                <!-- /.table-responsive -->

                                            </div>

                                            <!-- /.panel-body -->
                                        </div>


                                        <!-- /.panel -->
                                    </div>--%>
                                    <!-- /.col-lg-12 -->
                                </div>

                            </div>
                        </div>
                    </div>

                    <%--tab with gridview for header and details--%>



                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
            </div>
            </div>
        </ContentTemplate>
        <%--         <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                </Triggers>          --%>
    </asp:UpdatePanel>
</asp:Content>
