<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviewOLD.aspx.cs" Inherits="ToasoanTTXVN.Quytrinh.PreviewOLD" %>

<%@ Import Namespace="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>News Preview</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Dungchung/Scripts/Ninja/ninja.css" rel="stylesheet" type="text/css" />

    <script src="../Dungchung/Scripts/Ninja/jquery.min.js" type="text/javascript"></script>

    <script src="../Dungchung/Scripts/Ninja/ninja.js" type="text/javascript"></script>

    <script src="../Dungchung/Scripts/Ninja/Lib.js" type="text/javascript"></script>

    <script language="JavaScript" src="../Dungchung/Scripts/JSDantrang/vietuni.js" type='text/javascript'>
    </script>

    <script language="JavaScript" src="../Dungchung/Scripts/JSDantrang/vumods.js" type='text/javascript'>
    </script>

    <script language="JavaScript" src="../Dungchung/Scripts/JSDantrang/vumaps.js" type='text/javascript'>
    </script>

    <script language="JavaScript" src="../Dungchung/Scripts/JSDantrang/vumaps2.js" type='text/javascript'>
    </script>

    <script src="../Dungchung/Scripts/Ninja/jquery.jcarousel.min.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="prev-wrapper">
        <div class="prev-news-title">
            Bài Viết :
            <%=Tieude%>
            <span runat="server" class="lblYesImage" id="lblYesImage"></span>
        </div>
        <div class="prev-template">
            <ul id="mycarouselTemP" class="jcarousel-skin-temp">
                <asp:Repeater runat="server" ID="rptTemplate">
                    <ItemTemplate>
                        <li id="<%#Eval("TempID") %>">
                            <img src="<%#HPCComponents.Global.TinPathBDT%><%#Eval("TempLogo") %>" alt=" <%#Eval("TempName")%>" /></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>

        <script type="text/javascript">
            var _width, _height, _column, _Ma_Tinbai;
            var _fontFamilyTitle, _fontSizeTitle, _ScaleTitle, _fontWeightTitle, _widthTitle, _lineHeightTitle;
            var _fontFamilySapo, _lineHeightSapo, _fontSizeSapo;
            var _fontFamily, _fontSize, _lineHeightContent;
            var _TempIsImage, _TempImageWidth, _TempImageHeight;
            var _SapoWidth, _SapoFontWeight;
            var _container;
            var _padding = 19.3;
            var _rateMilli = 3.779527559;
            var _wwframe, _wwTitle, _wwSapo, TemRate;
            var sizeScale;
            var _widthRoot, _heightRoot, _TempImageWidthRoot, _TempImageHeightRoot;

            $("#mycarouselTemP li").click(function() {
                var _idTemp = $(this).attr('id');
                bindTempByID(_idTemp, 0, 0);

            });
            $(window).load(function() {
                _Ma_Tinbai = $('#txtMatinbai').val();
                bindTempByID(0, _Ma_Tinbai, 1);
            });
            function bindTempByID(_idtemp_, _ma_tinbai, _type_) {
                $.ajax({
                    type: "POST",
                    url: "../Ajax/GetData.asmx/GetTemplateValue",
                    data: "{TempID:'" + _idtemp_ + "',Ma_Tinbai:'" + _ma_tinbai + "',TempType:'" + _type_ + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    cache: false,
                    async: true,
                    success: function(msg) {
                        var data = msg.d;
                        // $("#selColumn option:val=" + data.TempColumn + "").prop("selected", "selected");
                        $("#selColumn").val(data.TempColumn);
                        $("#selColumn option").removeAttr('selected');
                        $("#selColumn option[value=" + data.TempColumn + "]").attr('selected', 'selected');
                        $('#txtWidth').val(data.TempWidth);
                        $('#txtHeight').val(data.TempHeight);

                        $("#selWidthtitle").val(data.TempColumn);
                        $("#selWidthtitle option").removeAttr('selected');
                        $("#selWidthtitle option[value=" + data.TempTitleWidth + "]").attr('selected', 'selected');

                        $("#selFontweightTitle").val(data.TempColumn);
                        $("#selFontweightTitle option").removeAttr('selected');
                        $("#selFontweightTitle option[value=" + data.TempFontWeightTitle + "]").attr('selected', 'selected');

                        $('#txtFontFamilyTitle').val(data.TempFontfamilyTitle);
                        $('#txtFontSizeTitle').val(data.TempFontsizeTitle);
                        $('#txtScaleTitle').val(data.TempScaleTitle);
                        $('#txtLineheightitle').val(data.TempLineHeightTitle);


                        $("#selWidthSapo").val(data.TempColumn);
                        $("#selWidthSapo option").removeAttr('selected');
                        $("#selWidthSapo option[value=" + data.TempTitleWidth + "]").attr('selected', 'selected');

                        $("#selFontWeightSapo").val(data.TempColumn);
                        $("#selFontWeightSapo option").removeAttr('selected');
                        $("#selFontWeightSapo option[value=" + data.TempFontWeightTitle + "]").attr('selected', 'selected');

                        $('#txtFontFamilySapo').val(data.TempFontfamilySapo);
                        $('#txtfontsizeSapo').val(data.TempFontsizeSapo);
                        $('#txtLineheighSapo').val(data.TempLineHeightSapo);

                        $('#txtFontSizeContent').val(data.TempFontsize);
                        $('#txtFontFamily').val(data.TempFontfamily);
                        $('#txtLineheighContent').val(data.TempLineHeightContent);
                        if (data.TempIsImage.toLowerCase() == "false") {
                            $('#chkIsImage').prop('checked', false);
                            $('#txtImagewidth').prop('readonly', true);
                            $('#txtImageHeight').prop('readonly', true);
                            $('#txtImagewidth').css('background-color', '#e1ddd8');
                            $('#txtImageHeight').css('background-color', '#e1ddd8');
                        }
                        else {
                            $('#chkIsImage').prop('checked', true);
                            $('#txtImagewidth').prop('readonly', false);
                            $('#txtImageHeight').prop('readonly', false);
                            $('#txtImagewidth').css('background-color', '');
                            $('#txtImageHeight').css('background-color', '');
                        }
                        $('#txtImagewidth').val(data.TempImageWidth);
                        $('#txtImageHeight').val(data.TempImageHeight);

                    },
                    complete: function() {
                        $(document).ajaxComplete(function() {
                            viewTemp();
                        });

                    },
                    error: function(msg) { }
                });
            }
            function SetvaluePara() {
                _Ma_Tinbai = $('#txtMatinbai').val();
                _column = parseInt($("#selColumn option:selected").val());
                _width = parseInt($('#txtWidth').val()) * _rateMilli;
                _height = parseInt($('#txtHeight').val()) * _rateMilli;

                _widthTitle = parseInt($("#selWidthtitle option:selected").val());
                if (_widthTitle > _column)
                    _widthTitle = _column;
                _fontWeightTitle = $("#selFontweightTitle option:selected").val();
                _fontFamilyTitle = $('#txtFontFamilyTitle').val();
                _fontSizeTitle = $('#txtFontSizeTitle').val();
                _ScaleTitle = parseInt($('#txtScaleTitle').val());
                _lineHeightTitle = $('#txtLineheightitle').val();

                _SapoWidth = parseInt($("#selWidthSapo option:selected").val());
                if (_SapoWidth > _column)
                    _SapoWidth = _column;
                _SapoFontWeight = $("#selFontWeightSapo option:selected").val();
                _fontFamilySapo = $('#txtFontFamilySapo').val();
                _fontSizeSapo = $('#txtfontsizeSapo').val();
                _lineHeightSapo = $('#txtLineheighSapo').val();

                _fontSize = $('#txtFontSizeContent').val();
                _fontFamily = $('#txtFontFamily').val();
                _lineHeightContent = $('#txtLineheighContent').val();
                _TempIsImage = $("#chkIsImage").prop("checked");

                _TempImageWidth = parseInt($('#txtImagewidth').val()) * _rateMilli;
                _TempImageHeight = parseInt($('#txtImageHeight').val()) * _rateMilli;

                _widthRoot = parseInt($('#txtWidth').val());
                _heightRoot = parseInt($('#txtHeight').val());
                _TempImageWidthRoot = parseInt($('#txtImagewidth').val());
                _TempImageHeightRoot = parseInt($('#txtImageHeight').val());
            }
            function viewTemp() {
                SetvaluePara();
                _wwframe = _width + _padding;
                TemRate = (_wwframe * (100 / _column)) / 100;
                _wwTitle = _widthTitle * TemRate - _padding;
                _wwSapo = _SapoWidth * TemRate - _padding;
                _container = '#hiddenContent';
                $('#target').empty();
                $('.target').css("width", _wwframe + "px");
                $('.target').css("height", _height + "px");
                $('.target').css("font-family", _fontFamily);
                $('.target').css("font-size", _fontSize + "pt");
                if (_lineHeightContent != "normal")
                    $('.target').css('line-height', _lineHeightContent + "pt");
                else
                    $('.target').css('line-height', _lineHeightContent);
                $('.magicTitle').css('width', (_wwTitle) + "px");
                if (_ScaleTitle <= 100) {
                    if (_fontWeightTitle == "Bold") {
                        sizeScale = _wwTitle - ((_wwTitle * (_ScaleTitle - 10)) / 100);
                        $('.magicTitle').css('transform', 'scaleX(' + (_ScaleTitle - 10) / 100 + ')');
                    }
                    else {
                        sizeScale = _wwTitle - ((_wwTitle * (_ScaleTitle - 10)) / 100);
                        $('.magicTitle').css('transform', 'scaleX(' + (_ScaleTitle - 5) / 100 + ')');
                    }
                }
                else {
                    if (_fontWeightTitle == "Bold") {
                        sizeScale = _wwTitle - ((_wwTitle * (_ScaleTitle - 10)) / 100);
                        $('.magicTitle').css('transform', 'scaleX(' + (_ScaleTitle - 10) / 100 + ')');
                    }
                    else {
                        sizeScale = _wwTitle - ((_wwTitle * (_ScaleTitle - 10)) / 100);
                        $('.magicTitle').css('transform', 'scaleX(' + (_ScaleTitle - 5) / 100 + ')');
                    }
                }
                $('.magicTitle').css('font-weight', _fontWeightTitle);
                $('.magicTitle').css('width', (_wwTitle + sizeScale) + "px");
                //style Title  
                $('#newsTitle').css('font-family', _fontFamilyTitle);
                $('#newsTitle').css('font-size', _fontSizeTitle + "pt");
                $('#newsTitle').css('width', _wwTitle + "px");
                if (_lineHeightTitle != "normal")
                    $('#newsTitle').css('line-height', _lineHeightTitle + "pt");
                else
                    $('#newsTitle').css('line-height', _lineHeightTitle);

                //end
                //sapo
                $('.sapo').css('font-family', _fontFamilySapo);
                $('.sapo').css('font-size', _fontSizeSapo + "pt");
                $('.sapo').css('width', _wwSapo + "px");
                $('.sapo').css('font-weight', _SapoFontWeight);
                if (_lineHeightSapo != "normal")
                    $('.sapo').css('line-height', _lineHeightSapo + "pt");
                else
                    $('.sapo').css('line-height', _lineHeightSapo);
                //

                if (_widthTitle == _column) {
                    $('#target').html($('#hiddenTitle').html());
                    
                    var _hhTitle = parseInt($('#newsTitle').height());
                    if (_TempIsImage == false) {
                        $("<div id='target1' class='target-inner'>value</div>").appendTo('#target');
                        var _wwwFrame1 = _wwframe;
                        var _hhhFrame1 = _height - _hhTitle;
                        $('#target1').css("width", _wwwFrame1 + "px");
                        $('#target1').css("height", _hhhFrame1 + "px");
                        $(function() {
                            $(_container).columnize({
                                columns: _column,
                                accuracy: 1,
                                buildOnce: true,
                                overflow: {
                                    height: _hhhFrame1,
                                    id: "#TextExtend",
                                    doneFunc: function() {
                                    }
                                },
                                target: "#target1",
                                doneFunc: function() {
                                }
                            });
                        });
                    }
                    //_TempIsImage = true
                    else {
                        var column1;
                        if (_TempImageWidth <= TemRate)
                            column1 = 1;
                        else if (_TempImageWidth > TemRate && _TempImageWidth <= TemRate * 2)
                            column1 = 2;
                        else if (_TempImageWidth > TemRate * 2 && _TempImageWidth <= TemRate * 3)
                            column1 = 3;
                        else if (_TempImageWidth > TemRate * 3 && _TempImageWidth <= TemRate * 4)
                            column1 = 4;
                        else if (_TempImageWidth > TemRate * 3 && _TempImageWidth <= TemRate * 4)
                            column1 = 4;
                        else if (_TempImageWidth > TemRate * 4 && _TempImageWidth <= TemRate * 5)
                            column1 = 5;
                        $('.imageMagic').css('width', _TempImageWidth + 'px');
                        $('.imageMagic').css('height', _TempImageHeight + 'px');
                        $('.imageMagic').css('display', 'block');

                        if (column1 == _column) {
                            $("<div class='imageColumn'>&nbsp;</div>").appendTo('#target');
                            $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#target');
                            $('.imageColumn').html($('#hiddenImage').html());

                            var _wwwFrame1 = _wwframe;
                            //
                            $('.imgNote').css('width', _wwwFrame1 - _padding + 'px');
                            var imgHeightCaption = $('.imgNote').height();
                            //
                            var _hhhFrame1 = _height - _hhTitle - imgHeightCaption - _TempImageHeight - 15;
                            $('.imageColumn').css('width', _wwwFrame1 + 'px');
                            $('#target1').css("width", _wwwFrame1 + "px");
                            $('#target1').css("height", _hhhFrame1 + "px");
                            $(function() {
                                $(_container).columnize({
                                    columns: _column,
                                    accuracy: 1,
                                    buildOnce: true,
                                    overflow: {
                                        height: _hhhFrame1,
                                        id: "#TextExtend",
                                        doneFunc: function() {
                                        }
                                    },
                                    target: "#target1",
                                    doneFunc: function() {
                                    }
                                });
                            });
                        }
                        //column1 != column
                        else {

                            $("<div id='imageColumn' class='imageColumn'>&nbsp;</div>").appendTo('#target');
                            $('.imageColumn').html($('#hiddenImage').html());
                            $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#imageColumn');
                            $("<div id='target2' class='target-inner'>&nbsp;</div>").appendTo('#target');
                            var _wwwFrame1 = Math.round(TemRate * column1);

                            //
                            $('.imgNote').css('width', _wwwFrame1 - _padding + 'px');
                            var imgHeightCaption = $('.imgNote').height();
                            //

                            var _wwwFrame2 = _wwframe - _wwwFrame1;
                            var _hhhFrame1 = _height - _hhTitle - imgHeightCaption - _TempImageHeight - 15;
                            var _hhhFrame2 = _height - _hhTitle;
                            $('#target1').css("width", _wwwFrame1 + "px");
                            $('#target1').css("height", _hhhFrame1 + "px");
                            $('#target2').css("width", _wwwFrame2 + "px");
                            $('.imageColumn').css('width', _wwwFrame1 + 'px');
                            $(function() {
                                $(_container).columnize({
                                    columns: column1,
                                    accuracy: 1,
                                    buildOnce: true,
                                    overflow: {
                                        height: _hhhFrame1,
                                        id: "#TextExtend",
                                        doneFunc: function() {
                                            $('#TextExtend').columnize({
                                                columns: _column - column1,
                                                accuracy: 1,
                                                buildOnce: true,
                                                overflow: {
                                                    height: _hhhFrame2,
                                                    id: "#TextExtend",
                                                    doneFunc: function() {
                                                    }
                                                },
                                                target: "#target2",
                                                doneFunc: function() {
                                                }
                                            });
                                        }
                                    },
                                    target: "#target1",
                                    doneFunc: function() {
                                    }
                                });
                            });
                        }
                    }

                }
                //Column Title != _column
                else {
                    $("<div id='TitleColumn' class='TitleColumn'>&nbsp;</div>").appendTo('#target');
                    $('#TitleColumn').html($('#hiddenTitle').html());
                    var _hhTitle = parseInt($('#newsTitle').height());
                    //
                    if (_TempIsImage == false) {
                        $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#TitleColumn');
                        $("<div id='target2' class='target-inner'>&nbsp;</div>").appendTo('#target');
                        var _wwwFrame1 = TemRate * _widthTitle;
                        var _hhhFrame1 = _height - _hhTitle;
                        var _wwwFrame2 = _wwframe - _wwwFrame1;
                        var _hhhFrame2 = _height;
                        var _colFrame1 = _widthTitle;
                        var _colFrame2 = _column - _widthTitle;
                        $('#target1').css("width", _wwwFrame1 + "px");
                        $('#target1').css("height", _hhhFrame1 + "px");
                        $('#target2').css("width", _wwwFrame2 + "px");
                        $('#TitleColumn').css("width", _wwwFrame1 + "px");

                        $(function() {
                            $(_container).columnize({
                                columns: _colFrame1,
                                accuracy: 1,
                                buildOnce: true,
                                overflow: {
                                    height: _hhhFrame1,
                                    id: "#TextExtend",
                                    doneFunc: function() {
                                        $('#TextExtend').columnize({
                                            columns: _colFrame2,
                                            accuracy: 1,
                                            buildOnce: true,
                                            overflow: {
                                                height: _hhhFrame2,
                                                id: "#TextExtend",
                                                doneFunc: function() {
                                                }
                                            },
                                            target: "#target2",
                                            doneFunc: function() {
                                            }
                                        });
                                    }
                                },
                                target: "#target1",
                                doneFunc: function() {
                                }
                            });
                        });
                    }
                    // _TempIsImage =true
                    else {
                        var column1;
                        var TemRate = (_wwframe * (100 / _column)) / 100;
                        if (_TempImageWidth <= TemRate)
                            column1 = 1;
                        else if (_TempImageWidth > TemRate && _TempImageWidth <= TemRate * 2)
                            column1 = 2;
                        else if (_TempImageWidth > TemRate * 2 && _TempImageWidth <= TemRate * 3)
                            column1 = 3;
                        else if (_TempImageWidth > TemRate * 3 && _TempImageWidth <= TemRate * 4)
                            column1 = 4;
                        else if (_TempImageWidth > TemRate * 3 && _TempImageWidth <= TemRate * 4)
                            column1 = 4;
                        else if (_TempImageWidth > TemRate * 4 && _TempImageWidth <= TemRate * 5)
                            column1 = 5;
                        $('.imageMagic').css('width', _TempImageWidth + 'px');
                        $('.imageMagic').css('height', _TempImageHeight + 'px');
                        $('.imageMagic').css('display', 'block');
                        //
                        if (column1 == _widthTitle) {

                            $("<div id='imageColumn' class='imageColumn'>&nbsp;</div>").appendTo('#TitleColumn');
                            $('#imageColumn').html($('#hiddenImage').html());
                            $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#TitleColumn');
                            $("<div id='target2' class='target-inner'>&nbsp;</div>").appendTo('#target');
                            var _wwwFrame1 = TemRate * column1;
                            //
                            $('.imgNote').css('width', _wwwFrame1 - _padding + 'px');
                            var imgHeightCaption = $('.imgNote').height();
                            //

                            var _hhhFrame1 = _height - _hhTitle - imgHeightCaption - _TempImageHeight - 15;
                            var _wwwFrame2 = _wwframe - _wwwFrame1;
                            var _hhhFrame2 = _height;
                            var _colFrame1 = _widthTitle;
                            var _colFrame2 = _column - _widthTitle;
                            $('#target1').css("width", _wwwFrame1 + "px");
                            $('#target1').css("height", _hhhFrame1 + "px");
                            $('#target2').css("width", _wwwFrame2 + "px");
                            $('#TitleColumn').css("width", _wwwFrame1 + "px");
                            $(function() {
                                $(_container).columnize({
                                    columns: _colFrame1,
                                    accuracy: 1,
                                    buildOnce: true,
                                    overflow: {
                                        height: _hhhFrame1,
                                        id: "#TextExtend",
                                        doneFunc: function() {
                                            $('#TextExtend').columnize({
                                                columns: _colFrame2,
                                                accuracy: 1,
                                                buildOnce: true,
                                                overflow: {
                                                    height: _hhhFrame2,
                                                    id: "#TextExtend",
                                                    doneFunc: function() {
                                                    }
                                                },
                                                target: "#target2",
                                                doneFunc: function() {
                                                }
                                            });
                                        }
                                    },
                                    target: "#target1",
                                    doneFunc: function() {
                                    }
                                });
                            });
                        }
                        //column image != collum title
                        else {
                            $("<div id='imageColumn' class='imageColumn'>&nbsp;</div>").appendTo('#TitleColumn');
                            $('#imageColumn').html($('#hiddenImage').html());
                            $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#imageColumn');
                            $("<div id='target2' class='target-inner'>&nbsp;</div>").appendTo('#TitleColumn');
                            $("<div id='target3' class='target-inner'>&nbsp;</div>").appendTo('#target');
                            var _wwwFrame1 = (column1 * TemRate);
                            //
                            $('.imgNote').css('width', (_wwwFrame1 - _padding - 0.1) + 'px');
                            var imgHeightCaption = $('.imgNote').height();
                            //

                            var _wwwFrame2 = TemRate * (_widthTitle - column1);
                            var _wwwFrame3 = _wwframe - (_widthTitle * TemRate);

                            var _hhhFrame1 = _height - _hhTitle - imgHeightCaption - _TempImageHeight - 15;
                            var _hhhFrame2 = _height - _hhTitle;
                            var _hhhFrame3 = _height;
                            var _colFrame1 = column1;
                            var _colFrame2 = _widthTitle - column1;
                            var _colFrame3 = _column - _widthTitle;

                            $('#target1').css("width", _wwwFrame1 + "px");
                            $('#target1').css("height", _hhhFrame1 + "px");
                            $('#target2').css("width", _wwwFrame2 + "px");
                            $('#target3').css("width", _wwwFrame3 + "px");
                            $('#TitleColumn').css("width", _widthTitle * TemRate + "px");
                            $('#imageColumn').css('width', _wwwFrame1 - 0.1 + 'px');
                            $(function() {
                                $(_container).columnize({
                                    columns: _colFrame1,
                                    accuracy: 1,
                                    buildOnce: true,
                                    overflow: {
                                        height: _hhhFrame1,
                                        id: "#TextExtend",
                                        doneFunc: function() {
                                            $('#TextExtend').columnize({
                                                columns: _colFrame2,
                                                accuracy: 1,
                                                buildOnce: true,
                                                overflow: {
                                                    height: _hhhFrame2,
                                                    id: "#TextExtend",
                                                    doneFunc: function() {
                                                        $('#TextExtend').columnize({
                                                            columns: _colFrame3,
                                                            accuracy: 1,
                                                            buildOnce: true,
                                                            overflow: {
                                                                height: _hhhFrame3,
                                                                id: "#TextExtend",
                                                                doneFunc: function() {
                                                                }
                                                            },
                                                            target: "#target3",
                                                            doneFunc: function() {
                                                            }
                                                        });
                                                    }
                                                },
                                                target: "#target2",
                                                doneFunc: function() {
                                                }
                                            });
                                        }
                                    },
                                    target: "#target1",
                                    doneFunc: function() {
                                    }
                                });
                            });
                        }
                    }

                }
            }
            $('#selColumn').live("change", function() {
                viewTemp();
            });
            $('#selWidthtitle').live("change", function() {
                viewTemp();
            });
            $('#selFontweightTitle').live("change", function() {
                viewTemp();
            });
            $('.inputtext').live("keypress", function(event) {
                var evt = event ? event : window.event;
                if (evt.keyCode == 13) {
                    viewTemp();
                    return false;
                }
            });
            $('#chkIsImage').live("change", function() {
                var _statusPhoto = $(this).prop("checked");
                if (_statusPhoto == false) {

                    $('#txtImagewidth').prop('readonly', true);
                    $('#txtImageHeight').prop('readonly', true);
                    $('#txtImagewidth').css('background-color', '#e1ddd8');
                    $('#txtImageHeight').css('background-color', '#e1ddd8');
                }
                else {
                    $('#txtImagewidth').prop('readonly', false);
                    $('#txtImageHeight').prop('readonly', false);
                    $('#txtImagewidth').css('background-color', '');
                    $('#txtImageHeight').css('background-color', '');
                }
                viewTemp();
            });
            function saveTempl() {
                SetvaluePara();
                var dataPost = { Ma_Tinbai1: _Ma_Tinbai, column1: _column, width1: _widthRoot, height1: _heightRoot, fontFamilyTitle1: _fontFamilyTitle, fontSizeTitle1: _fontSizeTitle, ScaleTitle1: _ScaleTitle,
                    fontWeightTitle1: _fontWeightTitle, widthTitle1: _widthTitle, lineHeightTitle1: _lineHeightTitle, fontFamilySapo1: _fontFamilySapo, lineHeightSapo1: _lineHeightSapo, fontSizeSapo1: _fontSizeSapo,
                    fontFamily1: _fontFamily, fontSize1: _fontSize, lineHeightContent1: _lineHeightContent, TempIsImage1: _TempIsImage, TempImageWidth1: _TempImageWidthRoot, TempImageHeight1: _TempImageHeightRoot,
                    SapoWidth1: _SapoWidth, SapoFontWeight1: _SapoFontWeight
                };
                var jsonpost = JSON.stringify(dataPost);
                $.ajax({
                    type: "POST",
                    url: "../Ajax/GetData.asmx/SaveTemp",
                    data: jsonpost,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    cache: false,
                    async: true,
                    success: function(msg) {
                        if (msg.d == "1")
                            alert('Lưu thành công !');
                        else
                            alert('Error !');
                    },
                    error: function(msg) { alert('Error !'); }
                });
            }
            $('#btnSaveTemp').live("click", function() {
                saveTempl();
            });
           
        </script>

        <div style="width: 100%; float: left;">
            <a id="btnSaveTemp" href="javascript:void(0);">Save</a>
        </div>
        <div class="prev-main">
            <div class="in">
                <div id="target" class="target">
                </div>
                <div id="TextExtend">
                </div>
            </div>
        </div>
        <div class="Prev-editor">
            <div style="display: none">
                <input id="txtMatinbai" value="<%=IDNews %>" type="text" />
                <input id="txtIDs" type="text" />
            </div>
            <div style="padding: 0 10px; float: left; margin: 5px 0;">
                <div style="width: 100%; float: left; margin: 5px 0 8px 0">
                    <span class="Titlelbl" style="float: left; line-height: 30px; padding-right: 60px">Columns</span>
                    <select runat="server" id="selColumn" class="inputtext-sel" style="width: 110px">
                        <option value="1" selected="selected">1 column </option>
                        <option value="2">2 columns </option>
                        <option value="3">3 columns </option>
                        <option value="4">4 columns </option>
                        <option value="5">5 columns </option>
                        <option value="6">6 columns </option>
                    </select>
                </div>
                <div style="width: 100%; float: left; padding-bottom: 8px">
                    <div style="width: 50%; float: left">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Width </span>
                        </p>
                        <input id="txtWidth" style="width: 70px" class="inputtext" type="text" />
                        <span style="padding-left: 7px; float: left;">(mm)</span>
                    </div>
                    <div style="width: 50%; float: left">
                        <p style="margin: 0; padding: 3px 0">
                            <span class="Titlelbl">Height</span></p>
                        <input id="txtHeight" style="width: 70px" class="inputtext" type="text" />
                        <span style="padding-left: 7px; float: left">(mm)</span>
                    </div>
                </div>
            </div>
            <%-- Title--%>
            <div style="background-color: #eee2d6; float: left; width: 266px; height: auto; padding: 10px;
                margin: 5px 2px 0 2px; border-top: 1px solid #eb4f18">
                <div style="width: 100%; float: left; padding-bottom: 8px">
                    <span class="Titlelbl" style="float: left; line-height: 30px; width: 115px;">Width Title</span>
                    <select runat="server" id="selWidthtitle" class="inputtext-sel" style="width: 110px">
                        <option value="1" selected="selected">1 column </option>
                        <option value="2">2 columns </option>
                        <option value="3">3 columns </option>
                        <option value="4">4 columns </option>
                        <option value="5">5 columns </option>
                        <option value="6">6 columns </option>
                    </select>
                </div>
                <div style="width: 100%; float: left; padding-bottom: 8px">
                    <span class="Titlelbl" style="float: left; line-height: 30px; width: 115px;">Font-weight
                        Title</span>
                    <select runat="server" id="selFontweightTitle" class="inputtext-sel" style="width: 110px">
                        <option value="Bold" selected="selected">Bold </option>
                        <option value="Normal">Nomal</option>
                    </select>
                </div>
                <div style="width: 100%; float: left; padding-bottom: 8px">
                    <span class="Titlelbl" style="float: left; line-height: 30px; width: 115px;">Scale Title</span>
                    <input id="txtScaleTitle" style="width: 100px" class="inputtext" type="text" />
                    <span style="padding-left: 10px">(%)</span>
                </div>
                <div style="width: 100%; float: left; margin: 5px 0 8px 0">
                    <span class="Titlelbl" style="float: left; line-height: 30px; width: 115px;">Font-family
                        Title </span>
                    <input id="txtFontFamilyTitle" style="width: 138px" class="inputtext" type="text" />
                </div>
                <div style="width: 100%; float: left; margin: 5px 0 8px 0">
                    <span class="Titlelbl" style="float: left; line-height: 30px; width: 115px;">Font-size
                        Title</span>
                    <input id="txtFontSizeTitle" style="width: 100px" class="inputtext" type="text" />
                    <span style="padding-left: 10px">(pt)</span>
                </div>
                <div style="width: 100%; float: left; margin: 5px 0 0 0">
                    <span class="Titlelbl" style="float: left; line-height: 30px; width: 115px;">Line-height
                        Title</span>
                    <input id="txtLineheightitle" style="width: 100px" class="inputtext" type="text" />
                    <span style="padding-left: 10px">(pt)</span>
                </div>
            </div>
            <%-- Sapo --%>
            <div style="padding: 10px 10px 0 10px; float: left; border-top: 1px solid #eb4f18">
                <div style="width: 100%; float: left; padding-bottom: 8px">
                    <span class="Titlelbl" style="float: left; line-height: 30px; width: 115px;">Width Sapo</span>
                    <select runat="server" id="selWidthSapo" class="inputtext-sel" style="width: 110px">
                        <option value="1" selected="selected">1 column </option>
                        <option value="2">2 columns </option>
                        <option value="3">3 columns </option>
                        <option value="4">4 columns </option>
                        <option value="5">5 columns </option>
                        <option value="6">6 columns </option>
                    </select>
                </div>
                <div style="width: 100%; float: left; padding-bottom: 8px">
                    <span class="Titlelbl" style="float: left; line-height: 30px; width: 115px;">Font-weight
                        Sapo</span>
                    <select runat="server" id="selFontWeightSapo" class="inputtext-sel" style="width: 110px">
                        <option value="Bold" selected="selected">Bold </option>
                        <option value="Normal">Nomal</option>
                    </select>
                </div>
                <div style="width: 100%; float: left; margin: 5px 0">
                    <span class="Titlelbl" style="float: left; line-height: 30px; width: 115px;">Font-family
                        Sapo</span>
                    <input id="txtFontFamilySapo" style="width: 138px" class="inputtext" type="text" />
                </div>
                <div style="width: 100%; float: left; margin: 5px 0">
                    <span class="Titlelbl" style="float: left; line-height: 30px; width: 115px;">Font-size
                        Sapo</span>
                    <input id="txtfontsizeSapo" style="width: 100px" class="inputtext" type="text" />
                    <span style="padding-left: 10px">(pt)</span>
                </div>
                <div style="width: 100%; float: left; margin: 5px 0">
                    <span class="Titlelbl" style="float: left; line-height: 30px; width: 115px;">Line-height
                        Sapo</span>
                    <input id="txtLineheighSapo" style="width: 100px" class="inputtext" type="text" />
                    <span style="padding-left: 10px">(pt)</span>
                </div>
            </div>
            <%-- Content --%>
            <div style="background-color: #eee2d6; float: left; width: 266px; height: auto; padding: 10px;
                margin: 5px 2px 0 2px; border-top: 1px solid #eb4f18">
                <div style="width: 100%; float: left; margin: 0 0 5px 0">
                    <span class="Titlelbl" style="float: left; line-height: 30px; width: 115px;">Font-family
                        Content</span>
                    <input id="txtFontFamily" style="width: 138px" class="inputtext" type="text" />
                </div>
                <div style="width: 100%; float: left; margin: 5px 0">
                    <span class="Titlelbl" style="float: left; line-height: 30px; width: 115px;">Font-size
                        Content</span>
                    <input id="txtFontSizeContent" style="width: 100px" class="inputtext" type="text" />
                    <span style="padding-left: 10px">(pt)</span>
                </div>
                <div style="width: 100%; float: left; margin: 5px 0 0 0">
                    <span class="Titlelbl" style="float: left; line-height: 30px; width: 115px;">Line-height
                        Content</span>
                    <input id="txtLineheighContent" style="width: 100px" class="inputtext" type="text" />
                    <span style="padding-left: 10px">(pt)</span>
                </div>
            </div>
            <%-- Image --%>
            <div style="padding: 10px 10px 0 10px; float: left; border-top: 1px solid #eb4f18">
                <div style="width: 100%; float: left; border-bottom: solid 1px #ebe3e1; padding-bottom: 8px;
                    margin-bottom: 5px;">
                    <input id="chkIsImage" type="checkbox" />
                    <span class="Titlelbl" style="padding-left: 5px;">Image display</span>
                </div>
                <div style="width: 50%; float: left">
                    <p style="margin: 0; padding: 3px 0">
                        <span class="Titlelbl">Image Width</span></p>
                    <input id="txtImagewidth" style="width: 70px" class="inputtext" type="text" />
                    <span style="padding-left: 5px; float: left">(mm)</span>
                </div>
                <div style="width: 50%; float: left">
                    <p style="margin: 0; padding: 3px 0">
                        <span class="Titlelbl">Image Height</span></p>
                    <input id="txtImageHeight" style="width: 70px" class="inputtext" type="text" />
                    <span style="padding-left: 5px; float: left">(mm)</span>
                </div>
            </div>
        </div>
        <div id="hiddenContent">
            <div class="colsplits">
                <div id="convertContent">
                    <%=Noidung %>
                </div>
            </div>
        </div>
        <div id="hiddenTitle">
            <p id="newsTitle">
                <span class="magicTitle" id="magicTitle">
                    <%=Tieude%></span></p>
        </div>
        <div id="hiddenSapo">
            <div class="sapo" id="convertSapo">
                <%=Sapo%>
            </div>
        </div>
        <div id="hiddenImage">
            <img src="<%=ImageTB %>" class="imageMagic" alt="" />
            <p class="imgNote">
                <%=ChuthichAnh %></p>
        </div>
        <input type="hidden" id="txtChangeCode" />

        <script type="text/javascript">
            var FontID = '4';
            $(document).ready(function() {
                HPC_Convert_Text('txtChangeCode', 'convertContent', FontID);
                HPC_Convert_Text('txtChangeCode', 'magicTitle', FontID);
                HPC_Convert_Text('txtChangeCode', 'convertSapo', FontID);
            });
            $(document).ready(function() {
                $('#mycarouselTemP').jcarousel({
                    start: 0,
                    scroll: 1,
                    //auto: '0',//'2',
                    //wrap: 'circular',
                    animation: 'slow'
                });
            });
        </script>

    </div>
    </form>
</body>
</html>
