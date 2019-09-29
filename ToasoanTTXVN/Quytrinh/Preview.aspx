<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Preview.aspx.cs" Inherits="ToasoanTTXVN.Quytrinh.Preview" %>

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
            var Frame_Width, Frame_Height, Frame_Column, _Ma_Tinbai;
            var Title_FontFamily, Title_FontSize, Title_Scale, Title_FontWeight, Title_Width, Title_LineHeight;
            var Sapo_FontFamily, Sapo_LineHeight, Sapo_FontSize, Sapo_Width, Sapo_FontWeight;
            var Content_FontFamily, Content_FontSize, Content_LineHeight;
            var TempIsImage, TempImageWidth, TempImageHeight;

            var Frame_WidthInsert, Frame_HeightInsert, ImageWidthInsert, ImageHeightInsert;

            //PARAMATER
            var ColPadding = 19.3;
            var RateMilli = 3.779527559;
            //---------------------------------//


            function ViewTemp() {
                SetvaluePara();
                var wwframe = Frame_Width + ColPadding;
                var tempRate = (wwframe * (100 / Frame_Column)) / 100;
                var wwTitle = Title_Width * tempRate - ColPadding;
                var wwSapo = Sapo_Width * tempRate - ColPadding;
                var wwwFrame1 = 0, wwwFrame2 = 0, wwwFrame3 = 0, wwwFrame4 = 0;
                var hhhFrame1 = 0, hhhFrame2 = 0, hhhFrame3 = 0, hhhFrame4 = 0;
                var colFrame1 = 0, colFrame2 = 0, colFrame3 = 0, colFrame4 = 0;
                var hhTitle = 0, hhSapo = 0, hhCaption = 0;
                var tempColumn = 0;
                $('#target').empty();
                $('.target').css("width", wwframe + "px");
                $('.target').css("height", Frame_Height + "px");
                $('.target').css("font-family", Content_FontFamily);
                $('.target').css("font-size", Content_FontSize + "pt");
                if (Content_LineHeight != "normal")
                    $('.target').css('line-height', Content_LineHeight + "pt");
                else
                    $('.target').css('line-height', Content_LineHeight);
                // Set Property for title
                var sizeScale;
                $('.magicTitle').css('width', (wwTitle) + "px");
                if (Title_Scale <= 100) {
                    if (Title_FontWeight == "Bold") {
                        sizeScale = wwTitle - ((wwTitle * (Title_Scale - 10)) / 100);
                        $('.magicTitle').css('transform', 'scaleX(' + (Title_Scale - 10) / 100 + ')');
                    }
                    else {
                        sizeScale = wwTitle - ((wwTitle * (Title_Scale - 10)) / 100);
                        $('.magicTitle').css('transform', 'scaleX(' + (Title_Scale - 5) / 100 + ')');
                    }
                }
                else {
                    if (Title_FontWeight == "Bold") {
                        sizeScale = wwTitle - ((wwTitle * (Title_Scale - 10)) / 100);
                        $('.magicTitle').css('transform', 'scaleX(' + (Title_Scale - 10) / 100 + ')');
                    }
                    else {
                        sizeScale = wwTitle - ((wwTitle * (Title_Scale - 10)) / 100);
                        $('.magicTitle').css('transform', 'scaleX(' + (Title_Scale - 5) / 100 + ')');
                    }
                }
                $('#newsTitle').css('width', wwTitle + "px");
                $('#newsTitle').css('font-family', Title_FontFamily);
                $('#newsTitle').css('font-size', Title_FontSize + "pt");
                $('.magicTitle').css('font-weight', Title_FontWeight);
                $('.magicTitle').css('width', (wwTitle + sizeScale) + "px");


                if (Title_LineHeight != "normal")
                    $('#newsTitle').css('line-height', Title_LineHeight + "pt");
                else
                    $('#newsTitle').css('line-height', Title_LineHeight);
                //END

                //Set Property Sapo
                $('.sapo').css('font-family', Sapo_FontFamily);
                $('.sapo').css('font-size', Sapo_FontSize + "pt");
                $('.sapo').css('width', wwSapo + "px");
                $('.sapo').css('font-weight', Sapo_FontWeight);
                if (Sapo_LineHeight != "normal")
                    $('.sapo').css('line-height', Sapo_LineHeight + "pt");
                else
                    $('.sapo').css('line-height', Sapo_LineHeight);
                //END

                if (TempIsImage == true) {
                    if (TempImageWidth <= tempRate)
                        tempColumn = 1;
                    else if (TempImageWidth > tempRate && TempImageWidth <= tempRate * 2)
                        tempColumn = 2;
                    else if (TempImageWidth > tempRate * 2 && TempImageWidth <= tempRate * 3)
                        tempColumn = 3;
                    else if (TempImageWidth > tempRate * 3 && TempImageWidth <= tempRate * 4)
                        tempColumn = 4;
                    else if (TempImageWidth > tempRate * 3 && TempImageWidth <= tempRate * 4)
                        tempColumn = 4;
                    else if (TempImageWidth > tempRate * 4 && TempImageWidth <= tempRate * 5)
                        tempColumn = 5;
                    else if (TempImageWidth > tempRate * 5 && TempImageWidth <= tempRate * 6)
                        tempColumn = 6;
                    $('.imgNote').css('width', TempImageWidth + 'px');
                }
                if (tempColumn > Sapo_Width)
                    tempColumn = Sapo_Width;
                if (Title_Width == Frame_Column) {
                    $('#target').html($('#hiddenTitle').html());
                    hhTitle = parseInt($('#newsTitle').height());
                    if (Sapo_Width == Title_Width) {
                        $($('#hiddenSapo').html()).appendTo('#target');
                        hhSapo = parseInt($('.sapo').height());
                        if (TempIsImage == false) {
                            wwwFrame1 = wwframe;
                            hhhFrame1 = Frame_Height - hhTitle - hhSapo;
                            colFrame1 = Frame_Column;
                            $("<div id='target1' class='target-inner'>value</div>").appendTo('#target');
                            $('#target1').css("width", wwwFrame1 + "px");
                        }
                        else {
                            $("<div id='imageColumn' class='imageColumn'>&nbsp;</div>").appendTo('#target');
                            $('.imageColumn').html($('#hiddenImage').html());
                            $('#imageColumn').css('width', (tempRate * tempColumn) - 0.1 + 'px');
                            $('.imageMagic').css('width', TempImageWidth + 'px');
                            $('.imageMagic').css('height', TempImageHeight + 'px');
                            hhCaption = $('.imgNote').height();
                            if (tempColumn == Sapo_Width) {
                                wwwFrame1 = wwframe;
                                colFrame1 = Frame_Column;
                                hhhFrame1 = Frame_Height - hhTitle - hhCaption - hhSapo - TempImageHeight - 15;
                                $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#target');
                                $('#target1').css("width", wwwFrame1 + "px");
                            }
                            else {
                                wwwFrame1 = (tempRate * tempColumn);
                                wwwFrame2 = wwframe - wwwFrame1;
                                colFrame1 = tempColumn;
                                colFrame2 = Frame_Column - colFrame1;
                                hhhFrame1 = Frame_Height - hhTitle - hhCaption - hhSapo - TempImageHeight - 15;
                                hhhFrame2 = Frame_Height - hhTitle - hhSapo;

                                $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#imageColumn');
                                $("<div id='target2' class='target-inner'>&nbsp;</div>").appendTo('#target');
                                $('#target1').css("width", wwwFrame1 + "px");
                                $('#target2').css("width", wwwFrame2 + "px");
                            }
                        }
                    }
                    else {
                        $("<div class='SapoColumn' id='SapoColumn'>&nbsp;</div>").appendTo('#target');
                        $('#SapoColumn').html($('#hiddenSapo').html());
                        $('#SapoColumn').css('width', (tempRate * Sapo_Width) + 'px');
                        hhSapo = parseInt($('.sapo').height());
                        if (TempIsImage == false) {
                            wwwFrame1 = Sapo_Width * tempRate;
                            hhhFrame1 = Frame_Height - hhTitle - hhSapo;
                            colFrame1 = Sapo_Width;
                            wwwFrame2 = wwframe - wwwFrame1;
                            hhhFrame2 = Frame_Height - hhTitle;
                            colFrame2 = Frame_Column - colFrame1;

                            $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#SapoColumn');
                            $("<div id='target2' class='target-inner'>&nbsp;</div>").appendTo('#target');
                            $('#target1').css("width", wwwFrame1 + "px");
                            $('#target2').css("width", wwwFrame2 + "px");
                        }
                        else {
                            $("<div id='imageColumn' class='imageColumn'>&nbsp;</div>").appendTo('#SapoColumn');
                            $('#imageColumn').html($('#hiddenImage').html());
                            $('#imageColumn').css('width', (tempRate * tempColumn) - 0.1 + 'px');
                            $('.imageMagic').css('width', TempImageWidth + 'px');
                            $('.imageMagic').css('height', TempImageHeight + 'px');
                            hhCaption = $('.imgNote').height();
                            if (tempColumn == Sapo_Width) {
                                wwwFrame1 = Sapo_Width * tempRate;
                                hhhFrame1 = Frame_Height - hhTitle - hhSapo - hhCaption - TempImageHeight - 15;
                                colFrame1 = Sapo_Width;
                                wwwFrame2 = wwframe - wwwFrame1;
                                hhhFrame2 = Frame_Height - hhTitle;
                                colFrame2 = Frame_Column - colFrame1;

                                $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#imageColumn');
                                $("<div id='target2' class='target-inner'>&nbsp;</div>").appendTo('#target');
                                $('#target1').css("width", wwwFrame1 + "px");
                                $('#target2').css("width", wwwFrame2 + "px");
                            }
                            else {
                                wwwFrame1 = (tempRate * tempColumn);
                                hhhFrame1 = Frame_Height - hhTitle - hhSapo - hhCaption - TempImageHeight - 15;
                                colFrame1 = tempColumn;
                                wwwFrame2 = (tempRate * Sapo_Width) - wwwFrame1;
                                colFrame2 = Sapo_Width - tempColumn;
                                hhhFrame2 = Frame_Height - hhTitle - hhSapo;
                                wwwFrame3 = wwframe - (tempRate * Sapo_Width);
                                colFrame3 = Frame_Column - Sapo_Width;
                                hhhFrame3 = Frame_Height - hhTitle;

                                $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#imageColumn');
                                $("<div id='target2' class='target-inner'>&nbsp;</div>").appendTo('#SapoColumn');
                                $("<div id='target3' class='target-inner'>&nbsp;</div>").appendTo('#target');
                                $('#target1').css("width", wwwFrame1 + "px");
                                $('#target2').css("width", wwwFrame2 + "px");
                                $('#target3').css("width", wwwFrame3 + "px");
                            }
                        }
                    }
                }
                else {
                    $("<div id='TitleColumn' class='TitleColumn'>&nbsp;</div>").appendTo('#target');
                    $('#TitleColumn').html($('#hiddenTitle').html());
                    $('#TitleColumn').css('width', (tempRate * Title_Width) + 'px');
                    $("<div class='SapoColumn' id='SapoColumn'>&nbsp;</div>").appendTo('#TitleColumn');
                    $('#SapoColumn').html($('#hiddenSapo').html());
                    $('#SapoColumn').css('width', (tempRate * Sapo_Width) - 0.1 + 'px');
                    hhTitle = parseInt($('#newsTitle').height());
                    hhSapo = parseInt($('.sapo').height());
                    if (Sapo_Width == Title_Width) {
                        if (TempIsImage == false) {
                            wwwFrame1 = tempRate * Title_Width;
                            hhhFrame1 = Frame_Height - hhTitle - hhSapo;
                            colFrame1 = Title_Width;
                            wwwFrame2 = wwframe - wwwFrame1;
                            hhhFrame2 = Frame_Height;
                            colFrame2 = Frame_Column - Title_Width;

                            $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#SapoColumn');
                            $("<div id='target2' class='target-inner'>&nbsp;</div>").appendTo('#target');
                            $('#target1').css("width", wwwFrame1 + "px");
                            $('#target2').css("width", wwwFrame2 + "px");
                        }
                        else {
                            $("<div id='imageColumn' class='imageColumn'>&nbsp;</div>").appendTo('#SapoColumn');
                            $('#imageColumn').html($('#hiddenImage').html());
                            $('#imageColumn').css('width', (tempRate * tempColumn) - 0.1 + 'px');
                            $('.imageMagic').css('width', TempImageWidth + 'px');
                            $('.imageMagic').css('height', TempImageHeight + 'px');
                            hhCaption = $('.imgNote').height();
                            if (tempColumn == Sapo_Width) {
                                wwwFrame1 = tempRate * Sapo_Width;
                                hhhFrame1 = Frame_Height - hhTitle - hhSapo - hhCaption - TempImageHeight - 15;
                                colFrame1 = Sapo_Width;
                                wwwFrame2 = wwframe - wwwFrame1;
                                hhhFrame2 = Frame_Height;
                                colFrame2 = Frame_Column - Sapo_Width;

                                $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#imageColumn');
                                $("<div id='target2' class='target-inner'>&nbsp;</div>").appendTo('#target');
                                $('#target1').css("width", wwwFrame1 + "px");
                                $('#target2').css("width", wwwFrame2 + "px");
                            }
                            else {
                                wwwFrame1 = (tempRate * tempColumn);
                                hhhFrame1 = Frame_Height - hhTitle - hhSapo - hhCaption - TempImageHeight - 15;
                                colFrame1 = tempColumn;
                                wwwFrame2 = (tempRate * Sapo_Width) - wwwFrame1;
                                hhhFrame2 = Frame_Height - hhTitle - hhSapo;
                                colFrame2 = Sapo_Width - tempColumn;
                                wwwFrame3 = wwframe - wwwFrame1 - wwwFrame2;
                                hhhFrame3 = Frame_Height;
                                colFrame3 = Frame_Column - Sapo_Width;
                                $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#imageColumn');
                                $("<div id='target2' class='target-inner'>&nbsp;</div>").appendTo('#SapoColumn');
                                $("<div id='target3' class='target-inner'>&nbsp;</div>").appendTo('#target');
                                $('#target1').css("width", wwwFrame1 + "px");
                                $('#target2').css("width", wwwFrame2 + "px");
                                $('#target3').css("width", wwwFrame3 + "px");
                            }
                        }
                    }
                    else {
                        if (TempIsImage == false) {
                            var hkhk;
                            wwwFrame1 = (Sapo_Width * tempRate);
                            hhhFrame1 = Frame_Height - hhTitle - hhSapo;
                            colFrame1 = Sapo_Width;
                            wwwFrame2 = (tempRate * Title_Width) - wwwFrame1;
                            hhhFrame2 = Frame_Height - hhTitle;
                            colFrame2 = Title_Width - Sapo_Width;
                            wwwFrame3 = wwframe - (tempRate * Title_Width);
                            hhhFrame3 = Frame_Height;
                            colFrame3 = Frame_Column - Title_Width;

                            $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#SapoColumn');
                            $("<div id='target2' class='target-inner'>&nbsp;</div>").appendTo('#TitleColumn');
                            $("<div id='target3' class='target-inner'>&nbsp;</div>").appendTo('#target');
                            $('#target1').css("width", wwwFrame1 + "px");
                            $('#target2').css("width", wwwFrame2 + "px");
                            $('#target3').css("width", wwwFrame3 + "px");
                        }
                        else {
                            $("<div id='imageColumn' class='imageColumn'>&nbsp;</div>").appendTo('#SapoColumn');
                            $('#imageColumn').html($('#hiddenImage').html());
                            $('#imageColumn').css('width', (tempRate * tempColumn) - 0.1 + 'px');
                            $('.imageMagic').css('width', TempImageWidth + 'px');
                            $('.imageMagic').css('height', TempImageHeight + 'px');
                            hhCaption = $('.imgNote').height();
                            if (tempColumn == Sapo_Width) {
                                wwwFrame1 = (Sapo_Width * tempRate);
                                hhhFrame1 = Frame_Height - hhTitle - hhSapo - hhCaption - TempImageHeight - 15;
                                colFrame1 = Sapo_Width;
                                wwwFrame2 = (tempRate * Title_Width) - wwwFrame1;
                                hhhFrame2 = Frame_Height - hhTitle;
                                colFrame2 = Title_Width - Sapo_Width;
                                wwwFrame3 = wwframe - (tempRate * Title_Width);
                                hhhFrame3 = Frame_Height;
                                colFrame3 = Frame_Column - Title_Width;
                                $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#imageColumn');
                                $("<div id='target2' class='target-inner'>&nbsp;</div>").appendTo('#TitleColumn');
                                $("<div id='target3' class='target-inner'>&nbsp;</div>").appendTo('#target');
                                $('#target1').css("width", wwwFrame1 + "px");
                                $('#target2').css("width", wwwFrame2 + "px");
                                $('#target3').css("width", wwwFrame3 + "px");
                            }
                            else {
                                wwwFrame1 = (tempColumn * tempRate);
                                hhhFrame1 = Frame_Height - hhTitle - hhSapo - -hhCaption - TempImageHeight - 15;
                                colFrame1 = tempColumn;
                                wwwFrame2 = (tempRate * Sapo_Width) - wwwFrame1;
                                hhhFrame2 = Frame_Height - hhTitle - hhSapo;
                                colFrame2 = Sapo_Width - tempColumn;
                                wwwFrame3 = (tempRate * (Title_Width - Sapo_Width));
                                hhhFrame3 = Frame_Height - hhTitle;
                                colFrame3 = Title_Width - Sapo_Width;
                                wwwFrame4 = wwframe - (tempRate * Title_Width);
                                hhhFrame4 = Frame_Height;
                                colFrame4 = Frame_Column - Title_Width;
                                $("<div id='target1' class='target-inner'>&nbsp;</div>").appendTo('#imageColumn');
                                $("<div id='target2' class='target-inner'>&nbsp;</div>").appendTo('#SapoColumn');
                                $("<div id='target3' class='target-inner'>&nbsp;</div>").appendTo('#TitleColumn');
                                $("<div id='target4' class='target-inner'>&nbsp;</div>").appendTo('#target');
                                $('#target1').css("width", wwwFrame1 + "px");
                                $('#target2').css("width", wwwFrame2 + "px");
                                $('#target3').css("width", wwwFrame3 + "px");
                                $('#target4').css("width", wwwFrame4 + "px");
                            }
                        }
                    }
                }
                if (wwwFrame2 == 0) {
                    if (hhhFrame1 > 0) {
                        $("#hiddenContent").columnize({
                            columns: colFrame1, accuracy: 1, buildOnce: true,
                            overflow: { height: hhhFrame1, id: "#TextExtend", doneFunc: function() { } },
                            target: "#target1", doneFunc: function() { }
                        });
                    }
                }
                else if (wwwFrame3 == 0) {
                    if (hhhFrame1 > 0) {
                        $("#hiddenContent").columnize({
                            columns: colFrame1, accuracy: 1, buildOnce: true,
                            overflow: { height: hhhFrame1, id: "#TextExtend",
                                doneFunc: function() {
                                    $('#TextExtend').columnize({
                                        columns: colFrame2, accuracy: 1, buildOnce: true,
                                        overflow: { height: hhhFrame2, id: "#TextExtend", doneFunc: function() { } },
                                        target: "#target2", doneFunc: function() { }
                                    });
                                }
                            }, target: "#target1", doneFunc: function() { }
                        });
                    }
                    else {
                        $("#hiddenContent").columnize({
                            columns: colFrame2, accuracy: 1, buildOnce: true,
                            overflow: { height: hhhFrame2, id: "#TextExtend", doneFunc: function() { }
                            }, target: "#target2", doneFunc: function() { }
                        });
                    }
                }
                else if (wwwFrame4 == 0) {
                    if (hhhFrame1 > 0) {
                        $("#hiddenContent").columnize({
                            columns: colFrame1, accuracy: 1, buildOnce: true,
                            overflow: { height: hhhFrame1, id: "#TextExtend",
                                doneFunc: function() {
                                    $('#TextExtend').columnize({
                                        columns: colFrame2, accuracy: 1, buildOnce: true,
                                        overflow: { height: hhhFrame2, id: "#TextExtend",
                                            doneFunc: function() {
                                                $('#TextExtend').columnize({
                                                    columns: colFrame3, accuracy: 1, buildOnce: true,
                                                    overflow: { height: hhhFrame3, id: "#TextExtend", doneFunc: function() { } },
                                                    target: "#target3", doneFunc: function() { }
                                                });
                                            }
                                        }, target: "#target2", doneFunc: function() { }
                                    });
                                }
                            }, target: "#target1", doneFunc: function() { }
                        });
                    } else {
                        $("#hiddenContent").columnize({
                            columns: colFrame2, accuracy: 1, buildOnce: true,
                            overflow: { height: hhhFrame2, id: "#TextExtend",
                                doneFunc: function() {
                                    $('#TextExtend').columnize({
                                        columns: colFrame3, accuracy: 1, buildOnce: true,
                                        overflow: { height: hhhFrame3, id: "#TextExtend",
                                            doneFunc: function() { }
                                        }, target: "#target3", doneFunc: function() { }
                                    });
                                }
                            }, target: "#target2", doneFunc: function() { }
                        });
                    }
                }
                else if (wwwFrame4 != 0) {
                    if (hhhFrame1 > 0) {
                        $("#hiddenContent").columnize({
                            columns: colFrame1, accuracy: 1, buildOnce: true,
                            overflow: { height: hhhFrame1, id: "#TextExtend",
                                doneFunc: function() {
                                    $('#TextExtend').columnize({
                                        columns: colFrame2, accuracy: 1, buildOnce: true,
                                        overflow: { height: hhhFrame2, id: "#TextExtend",
                                            doneFunc: function() {
                                                $('#TextExtend').columnize({
                                                    columns: colFrame3, accuracy: 1, buildOnce: true,
                                                    overflow: { height: hhhFrame3, id: "#TextExtend",
                                                        doneFunc: function() {
                                                            $('#TextExtend').columnize({
                                                                columns: colFrame4, accuracy: 1, buildOnce: true,
                                                                overflow: { height: hhhFrame4, id: "#TextExtend", doneFunc: function() { } },
                                                                target: "#target4", doneFunc: function() { }
                                                            });
                                                        }
                                                    }, target: "#target3", doneFunc: function() { }
                                                });
                                            }
                                        }, target: "#target2", doneFunc: function() { }
                                    });
                                }
                            }, target: "#target1", doneFunc: function() { }
                        });
                    } else {
                        $('#hiddenContent').columnize({
                            columns: colFrame2, accuracy: 1, buildOnce: true,
                            overflow: { height: hhhFrame2, id: "#TextExtend",
                                doneFunc: function() {
                                    $('#TextExtend').columnize({
                                        columns: colFrame3, accuracy: 1, buildOnce: true,
                                        overflow: { height: hhhFrame3, id: "#TextExtend",
                                            doneFunc: function() {
                                                $('#TextExtend').columnize({
                                                    columns: colFrame4, accuracy: 1, buildOnce: true,
                                                    overflow: { height: hhhFrame4, id: "#TextExtend", doneFunc: function() { } },
                                                    target: "#target4", doneFunc: function() { }
                                                });
                                            }
                                        }, target: "#target3", doneFunc: function() { }
                                    });
                                }
                            }, target: "#target2", doneFunc: function() { }
                        });
                    }
                }
                console.log("hhCaption: " + hhCaption);
                console.log("hhSapo: " + hhSapo);
                console.log("hhTitle: " + hhTitle);
                console.log("TempImageHeight: " + TempImageHeight);
                console.log("hhhFrame1: " + hhhFrame1);
                console.log("hhhFrame2: " + hhhFrame2);
                console.log("hhhFrame3: " + hhhFrame3);
                console.log("hhhFrame4: " + hhhFrame4);
            }




            function SetvaluePara() {
                _Ma_Tinbai = $('#txtMatinbai').val();
                Frame_Column = parseInt($("#selColumn option:selected").val());
                Frame_Width = parseInt($('#txtWidth').val()) * RateMilli;
                Frame_Height = parseInt($('#txtHeight').val()) * RateMilli;
                Frame_WidthInsert = parseInt($('#txtWidth').val());
                Frame_HeightInsert = parseInt($('#txtHeight').val());
                //TITLE
                Title_Width = parseInt($("#selWidthtitle option:selected").val());
                if (Title_Width > Frame_Column)
                    Title_Width = Frame_Column;
                Title_FontFamily = $('#txtFontFamilyTitle').val();
                Title_FontWeight = $("#selFontweightTitle option:selected").val();
                Title_FontSize = $('#txtFontSizeTitle').val();
                Title_Scale = parseInt($('#txtScaleTitle').val());
                Title_LineHeight = $('#txtLineheightitle').val();
                //SAPO
                Sapo_Width = parseInt($("#selWidthSapo option:selected").val());
                if (Sapo_Width > Title_Width)
                    Sapo_Width = Title_Width;
                Sapo_FontWeight = $("#selFontWeightSapo option:selected").val();
                Sapo_FontFamily = $('#txtFontFamilySapo').val();
                Sapo_FontSize = $('#txtfontsizeSapo').val();
                Sapo_LineHeight = $('#txtLineheighSapo').val();
                //CONTENT
                Content_FontSize = $('#txtFontSizeContent').val();
                Content_FontFamily = $('#txtFontFamily').val();
                Content_LineHeight = $('#txtLineheighContent').val();
                //IMAGE
                TempIsImage = $("#chkIsImage").prop("checked");
                TempImageWidth = parseInt($('#txtImagewidth').val()) * RateMilli;
                TempImageHeight = parseInt($('#txtImageHeight').val()) * RateMilli;
                ImageWidthInsert = parseInt($('#txtImagewidth').val());
                ImageHeightInsert = parseInt($('#txtImageHeight').val());
            }
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
                        $("#selColumn").val(data.TempColumns);
                        $("#selColumn option").removeAttr('selected');
                        $("#selColumn option[value=" + data.TempColumns + "]").attr('selected', 'selected');
                        $('#txtWidth').val(data.TempWidth);
                        $('#txtHeight').val(data.TempHeight);

                        $("#selWidthtitle").val(data.TempTitleWidth);
                        $("#selWidthtitle option").removeAttr('selected');
                        $("#selWidthtitle option[value=" + data.TempTitleWidth + "]").attr('selected', 'selected');

                        $("#selFontweightTitle").val(data.TempFontWeightTitle);
                        $("#selFontweightTitle option").removeAttr('selected');
                        $("#selFontweightTitle option[value=" + data.TempFontWeightTitle + "]").attr('selected', 'selected');

                        $('#txtFontFamilyTitle').val(data.TempFontfamilyTitle);
                        $('#txtFontSizeTitle').val(data.TempFontsizeTitle);
                        $('#txtScaleTitle').val(data.TempScaleTitle);
                        $('#txtLineheightitle').val(data.TempLineHeightTitle);


                        $("#selWidthSapo").val(data.TempSapoWidth);
                        $("#selWidthSapo option").removeAttr('selected');
                        $("#selWidthSapo option[value=" + data.TempSapoWidth + "]").attr('selected', 'selected');

                        $("#selFontWeightSapo").val(data.TempSapoFontWeight);
                        $("#selFontWeightSapo option").removeAttr('selected');
                        $("#selFontWeightSapo option[value=" + data.TempSapoFontWeight + "]").attr('selected', 'selected');

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
                            ViewTemp();
                        });

                    },
                    error: function(msg) { }
                });
            }

            $("#mycarouselTemP li").click(function() {
                var _idTemp = $(this).attr('id');
                bindTempByID(_idTemp, 0, 0);

            });
            $(window).load(function() {
                _Ma_Tinbai = $('#txtMatinbai').val();
                bindTempByID(0, _Ma_Tinbai, 1);
            });

            $('#selColumn').live("change", function() {
                ViewTemp();
            });
            $('#selWidthtitle').live("change", function() {
                ViewTemp();
            });
            $('#selFontweightTitle').live("change", function() {
                ViewTemp();
            });
            $('#selWidthSapo').live("change", function() {
                ViewTemp();
            });
            $('#selFontWeightSapo').live("change", function() {
                ViewTemp();
            });
            $('.inputtext').live("keypress", function(event) {
                var evt = event ? event : window.event;
                if (evt.keyCode == 13) {
                    ViewTemp();
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
                ViewTemp();
            });
            function saveTempl() {
                SetvaluePara();
                var dataPost = { Ma_Tinbai1: _Ma_Tinbai, column1: Frame_Column, width1: Frame_WidthInsert, height1: Frame_HeightInsert, fontFamilyTitle1: Title_FontFamily, fontSizeTitle1: Title_FontSize, ScaleTitle1: Title_Scale,
                    fontWeightTitle1: Title_FontWeight, widthTitle1: Title_Width, lineHeightTitle1: Title_LineHeight, fontFamilySapo1: Sapo_FontFamily, lineHeightSapo1: Sapo_LineHeight, fontSizeSapo1: Sapo_FontSize,
                    fontFamily1: Content_FontFamily, fontSize1: Content_FontSize, lineHeightContent1: Content_LineHeight, TempIsImage1: TempIsImage, TempImageWidth1: ImageWidthInsert, TempImageHeight1: ImageHeightInsert,
                    SapoWidth1: Sapo_Width, SapoFontWeight1: Sapo_FontWeight
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
            <%-- Title--%>
            <div style="background-color: #eee2d6; float: left; width: 266px; height: auto; padding: 10px;
                margin: 1px 2px 0 2px;">
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
                <div style="width: 50%; float: left;">
                    <p style="margin: 0; padding: 3px 0">
                        <span class="Titlelbl">Image Height</span></p>
                    <input id="txtImageHeight" style="width: 70px" class="inputtext" type="text" />
                    <span style="padding-left: 5px; float: left">(mm)</span>
                </div>
            </div>
        </div>
        <div id="hiddenContent">
            <div class="colsplits">
                <div id="ContentFunction">
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
            <%=Sapo%>
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
                HPC_Convert_Text('txtChangeCode', 'ContentFunction', FontID);
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
