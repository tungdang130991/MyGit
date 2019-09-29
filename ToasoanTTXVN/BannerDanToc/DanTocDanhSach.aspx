<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="DanTocDanhSach.aspx.cs" Inherits="ToasoanTTXVN.BannerDanToc.DanTocDanhSach" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Import Namespace="HPCBusinessLogic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center" style="text-align: left">
                <table border="0" cellpadding="1" cellspacing="1" style="float: left;">
                    <tr>
                        <td>
                            <img src="../Dungchung/Images/Icons/to-do-list-cheked-all-icon.png" width="16px" height="16px" />
                        </td>
                        <td style="vertical-align: middle">
                            <span class="TitlePanel">DANH SÁCH CÁC DÂN TỘC</span>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: center">
                <div class="classSearchHeader" style="margin-bottom: 5px;">
                    <table width="100%">
                        <tr>
                            <td style="width: 25%; text-align: right" class="Titlelbl">
                               Tên Dân tộc:
                            </td>
                            <td style="width: 50%; text-align: left">
                                <asp:TextBox ID="txt_tieude" TabIndex="4" Width="80%" runat="server" CssClass="inputtext"
                                    onkeypress="return clickButton(event,'ctl00_MainContent_btnSearch');">
                                </asp:TextBox>
                            </td>
                            <td style="width: 25%; text-align: left">
                                <asp:Button runat="server" ID="btnSearch" CssClass="iconFind" Font-Bold="true" OnClick="linkSearch_Click"
                                    Text="Tìm kiếm"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="classSearchHeader">
                    <table width="100%" cellspacing="2" cellpadding="2" border="0">
                        <tr>
                            <td colspan="2" style="height: 5px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:DataGrid runat="server" ID="grdListCate" AutoGenerateColumns="false" DataKeyField="ID"
                                    CssClass="Grid" OnEditCommand="grdListCategory_EditCommand" Width="100%" OnItemDataBound="grdListCategory_ItemDataBound">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundColumn Visible="False" DataField="ID">
                                            <HeaderStyle Width="1%"></HeaderStyle>
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                    ToolTip="Chọn tất cả"></asp:CheckBox>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                            <HeaderTemplate>
                                                Thứ tự
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPoss" runat="server" CssClass="inputtext" Width="20%" onKeyPress='return check_num(this,5,event)'
                                                    Text='<%#Eval("Order_Number")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                            <HeaderTemplate>
                                                Ảnh đại diện
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="gallery">
                                                    <div class="pictgalery" style="width: 120px;">
                                                        <%#UltilFunc.ReturnPath_Images(Eval("Ads_ImgVideo"))%>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Left" Width="15%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" Width="15%" CssClass="GridBorderVerSolid_Tittle">
                                            </ItemStyle>
                                            <HeaderTemplate>
                                                Tên Dân tộc
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "DisplayType")%>
                                                <asp:Label ID="lblMota" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DisplayType")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="6%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="6%"></ItemStyle>
                                            <HeaderTemplate>
                                                Người nhập
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserCreated"))%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="4%"></ItemStyle>
                                            <HeaderTemplate>
                                                Sửa
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" Width="15px" runat="server" ImageUrl="~/Dungchung/images/edit.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="Chỉnh sửa thông tin" CommandName="Edit"
                                                    CommandArgument="Edit" BorderStyle="None"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 60%; text-align: left">
                                <asp:Button runat="server" ID="btnUpdateOrderNumber" CssClass="iconSave" CausesValidation="false"
                                    Font-Bold="true" OnClick="linkSave_Click" Text="Lưu giữ vị trí"></asp:Button>
                                <asp:Button runat="server" ID="btnAddMenu2" CssClass="iconAddNew" CausesValidation="false"
                                    Font-Bold="true" OnClick="btnAddMenu_Click" Text="Thêm mới Dân tộc"></asp:Button>
                                <asp:Button runat="server" ID="LinkDelete1" CausesValidation="false" CssClass="iconDel"
                                    Text="Xóa" OnClick="btnLinkDelete_Click"></asp:Button>
                            </td>
                            <td style="text-align: right;" class="pageNav">
                                <cc1:CurrentPage runat="server" ID="curentPages" CssClass="pageNavTotal">
                                </cc1:CurrentPage>&nbsp;
                                <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged">
                                </cc1:Pager>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td class="datagrid_content_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_bottom_left">
            </td>
            <td class="datagrid_bottom_center">
            </td>
            <td class="datagrid_bottom_right">
            </td>
        </tr>
    </table>
    <!--Add by nvthai-->
    <div style="clear: both;" />
    <a id="btnAdd" runat="server" style="visibility: hidden"></a>
    <cc2:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnAdd"
        BackgroundCssClass="ModalPopupBG" PopupControlID="Paneltwo" OkControlID="btnOK"
        Drag="true" PopupDragHandleControlID="Paneltwo">
    </cc2:ModalPopupExtender>
    <div id="Paneltwo" style="display: none; width: 450px;">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader2">
                <div class="TitlebarLeft">
                    <asp:Literal runat="server" ID="Literal1">Xem slide show quảng cáo</asp:Literal>
                </div>
                <div class="TitlebarRight" onclick="cancelexit();">
                </div>
            </div>
            <div class="popup_Body">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table width="100%" style="background-color: white; border: 0px; text-align: center;">
                            <tr>
                                <td align="center">
                                    <div id="msg_slideshow" class="msg_slideshow">
                                        <div id="msg_wrapper" class="msg_wrapper">
                                        </div>
                                        <div id="msg_controls" class="msg_controls">
                                            <!-- right has to animate to 15px, default -110px -->
                                            <a href="#" id="msg_grid" class="msg_grid"></a><a href="#" id="msg_prev" class="msg_prev">
                                            </a><a href="#" id="msg_pause_play" class="msg_pause"></a>
                                            <!-- has to change to msg_play if paused-->
                                            <a href="#" id="msg_next" class="msg_next"></a>
                                        </div>
                                        <div id="msg_thumbs" class="msg_thumbs">
                                            <!-- top has to animate to 0px, default -230px -->
                                            <div class="msg_thumb_wrapper" style="display: none;">
                                                <asp:Repeater ID="rptSlideShows" runat="server">
                                                    <ItemTemplate>
                                                        <a href="#">
                                                            <img src="<%=Global.TinPath%><%#Eval("Ads_Images") %>" alt="<%=Global.TinPath%><%#Eval("Ads_Images") %>" /></a>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                            <a href="#" id="msg_thumb_next" class="msg_thumb_next"></a><a href="#" id="msg_thumb_prev"
                                                class="msg_thumb_prev"></a><a href="#" id="msg_thumb_close" class="msg_thumb_close">
                                                </a><span class="msg_loading"></span>
                                            <!-- show when next thumb wrapper loading -->
                                        </div>
                                    </div>
                                    </div>
                                    <asp:Button ID="btnOK" runat="server" Style="visibility: hidden" />
                                    <input id="btnExit" value="Thoát" type="button" class="buttonhide" runat="server"
                                        onserverclick="btnExit_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- The JavaScript -->

    <script type="text/javascript" src="../Dungchung/Scripts/jquery-1.4.2.min.js"></script>

    <script type="text/javascript">
        $(function() {
            /**
            * interval : time between the display of images
            * playtime : the timeout for the setInterval function
            * current  : number to control the current image
            * current_thumb : the index of the current thumbs wrapper
            * nmb_thumb_wrappers : total number	of thumbs wrappers
            * nmb_images_wrapper : the number of images inside of each wrapper
            */
            var interval = 4000;
            var playtime;
            var current = 0;
            var current_thumb = 0;
            var nmb_thumb_wrappers = $('#msg_thumbs .msg_thumb_wrapper').length;
            var nmb_images_wrapper = '<%=GetTotalsAds() %>';
            /**
            * start the slideshow
            */
            play();

            /**
            * show the controls when 
            * mouseover the main container
            */
            slideshowMouseEvent();
            function slideshowMouseEvent() {
                $('#msg_slideshow').unbind('mouseenter')
									   .bind('mouseenter', showControls)
									   .andSelf()
									   .unbind('mouseleave')
									   .bind('mouseleave', hideControls);
            }

            /**
            * clicking the grid icon,
            * shows the thumbs view, pauses the slideshow, and hides the controls
            */
            $('#msg_grid').bind('click', function(e) {
                hideControls();
                $('#msg_slideshow').unbind('mouseenter').unbind('mouseleave');
                pause();
                $('#msg_thumbs').stop().animate({ 'top': '0px' }, 500);
                e.preventDefault();
            });

            /**
            * closing the thumbs view,
            * shows the controls
            */
            $('#msg_thumb_close').bind('click', function(e) {
                showControls();
                slideshowMouseEvent();
                $('#msg_thumbs').stop().animate({ 'top': '-230px' }, 500);
                e.preventDefault();
            });

            /**
            * pause or play icons
            */
            $('#msg_pause_play').bind('click', function(e) {
                var $this = $(this);
                if ($this.hasClass('msg_play'))
                    play();
                else
                    pause();
                e.preventDefault();
            });

            /**
            * click controls next or prev,
            * pauses the slideshow, 
            * and displays the next or prevoius image
            */
            $('#msg_next').bind('click', function(e) {
                pause();
                next();
                e.preventDefault();
            });
            $('#msg_prev').bind('click', function(e) {
                pause();
                prev();
                e.preventDefault();
            });

            /**
            * show and hide controls functions
            */
            function showControls() {
                $('#msg_controls').stop().animate({ 'right': '15px' }, 500);
            }
            function hideControls() {
                $('#msg_controls').stop().animate({ 'right': '-110px' }, 500);
            }

            /**
            * start the slideshow
            */
            function play() {
                next();
                $('#msg_pause_play').addClass('msg_pause').removeClass('msg_play');
                playtime = setInterval(next, interval)
            }

            /**
            * stops the slideshow
            */
            function pause() {
                $('#msg_pause_play').addClass('msg_play').removeClass('msg_pause');
                clearTimeout(playtime);
            }

            /**
            * show the next image
            */
            function next() {
                ++current;
                showImage('r');
            }

            /**
            * shows the previous image
            */
            function prev() {
                --current;
                showImage('l');
            }

            /**
            * shows an image
            * dir : right or left
            */
            function showImage(dir) {
                /**
                * the thumbs wrapper being shown, is always 
                * the one containing the current image
                */
                alternateThumbs();

                /**
                * the thumb that will be displayed in full mode
                */
                var $thumb = $('#msg_thumbs .msg_thumb_wrapper:nth-child(' + current_thumb + ')')
								.find('a:nth-child(' + parseInt(current - nmb_images_wrapper * (current_thumb - 1)) + ')')
								.find('img');
                if ($thumb.length) {
                    var source = $thumb.attr('alt');
                    var $currentImage = $('#msg_wrapper').find('img');
                    if ($currentImage.length) {
                        $currentImage.fadeOut(function() {
                            $(this).remove();
                            $('<img />').load(function() {
                                var $image = $(this);
                                resize($image);
                                $image.hide();
                                $('#msg_wrapper').empty().append($image.fadeIn());
                            }).attr('src', source);
                        });
                    }
                    else {
                        $('<img />').load(function() {
                            var $image = $(this);
                            resize($image);
                            $image.hide();
                            $('#msg_wrapper').empty().append($image.fadeIn());
                        }).attr('src', source);
                    }

                }
                else { //this is actually not necessary since we have a circular slideshow
                    if (dir == 'r')
                        --current;
                    else if (dir == 'l')
                        ++current;
                    alternateThumbs();
                    return;
                }
            }

            /**
            * the thumbs wrapper being shown, is always 
            * the one containing the current image
            */
            function alternateThumbs() {
                $('#msg_thumbs').find('.msg_thumb_wrapper:nth-child(' + current_thumb + ')')
									.hide();
                current_thumb = Math.ceil(current / nmb_images_wrapper);
                /**
                * if we reach the end, start from the beggining
                */
                if (current_thumb > nmb_thumb_wrappers) {
                    current_thumb = 1;
                    current = 1;
                }
                /**
                * if we are at the beggining, go to the end
                */
                else if (current_thumb == 0) {
                    current_thumb = nmb_thumb_wrappers;
                    current = current_thumb * nmb_images_wrapper;
                }

                $('#msg_thumbs').find('.msg_thumb_wrapper:nth-child(' + current_thumb + ')')
									.show();
            }

            /**
            * click next or previous on the thumbs wrapper
            */
            $('#msg_thumb_next').bind('click', function(e) {
                next_thumb();
                e.preventDefault();
            });
            $('#msg_thumb_prev').bind('click', function(e) {
                prev_thumb();
                e.preventDefault();
            });
            function next_thumb() {
                var $next_wrapper = $('#msg_thumbs').find('.msg_thumb_wrapper:nth-child(' + parseInt(current_thumb + 1) + ')');
                if ($next_wrapper.length) {
                    $('#msg_thumbs').find('.msg_thumb_wrapper:nth-child(' + current_thumb + ')')
										.fadeOut(function() {
										    ++current_thumb;
										    $next_wrapper.fadeIn();
										});
                }
            }
            function prev_thumb() {
                var $prev_wrapper = $('#msg_thumbs').find('.msg_thumb_wrapper:nth-child(' + parseInt(current_thumb - 1) + ')');
                if ($prev_wrapper.length) {
                    $('#msg_thumbs').find('.msg_thumb_wrapper:nth-child(' + current_thumb + ')')
										.fadeOut(function() {
										    --current_thumb;
										    $prev_wrapper.fadeIn();
										});
                }
            }

            /**
            * clicking on a thumb, displays the image (alt attribute of the thumb)
            */
            $('#msg_thumbs .msg_thumb_wrapper > a').bind('click', function(e) {
                var $this = $(this);
                $('#msg_thumb_close').trigger('click');
                var idx = $this.index();
                var p_idx = $this.parent().index();
                current = parseInt(p_idx * nmb_images_wrapper + idx + 1);
                showImage();
                e.preventDefault();
            }).bind('mouseenter', function() {
                var $this = $(this);
                $this.stop().animate({ 'opacity': 1 });
            }).bind('mouseleave', function() {
                var $this = $(this);
                $this.stop().animate({ 'opacity': 0.5 });
            });

            /**
            * resize the image to fit in the container (400 x 400)
            */
            function resize($image) {
                var theImage = new Image();
                theImage.src = $image.attr("src");
                var imgwidth = theImage.width;
                var imgheight = theImage.height;

                var containerwidth = 400;
                var containerheight = 400;

                if (imgwidth > containerwidth) {
                    var newwidth = containerwidth;
                    var ratio = imgwidth / containerwidth;
                    var newheight = imgheight / ratio;
                    if (newheight > containerheight) {
                        var newnewheight = containerheight;
                        var newratio = newheight / containerheight;
                        var newnewwidth = newwidth / newratio;
                        theImage.width = newnewwidth;
                        theImage.height = newnewheight;
                    }
                    else {
                        theImage.width = newwidth;
                        theImage.height = newheight;
                    }
                }
                else if (imgheight > containerheight) {
                    var newheight = containerheight;
                    var ratio = imgheight / containerheight;
                    var newwidth = imgwidth / ratio;
                    if (newwidth > containerwidth) {
                        var newnewwidth = containerwidth;
                        var newratio = newwidth / containerwidth;
                        var newnewheight = newheight / newratio;
                        theImage.height = newnewheight;
                        theImage.width = newnewwidth;
                    }
                    else {
                        theImage.width = newwidth;
                        theImage.height = newheight;
                    }
                }
                $image.css({
                    'width': theImage.width,
                    'height': theImage.height
                });
            }
        });
    </script>

</asp:Content>
