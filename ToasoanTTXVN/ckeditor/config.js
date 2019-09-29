/*
Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function(config) {
    config.uiColor = '#F0F0F0';
    config.enterMode = CKEDITOR.ENTER_BR;
    config.shiftEnterMode = CKEDITOR.ENTER_P;

    config.toolbar_Noidung = [
              ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
              ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Subscript', 'Superscript'],
              ['Styles', 'Format'],
              ['Font', 'FontSize', 'Maximize'],
              ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteWord', '-'],
              ['Undo', 'Redo', 'SelectAll', 'RemoveFormat'],
              ['TextColor', 'BGColor'], ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent'],
              ['Source']
    ];
    config.toolbar_NoidungBDT = [
		      ['Source'],
              ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print'],
              ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
              '/',
              ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Subscript', 'Superscript'],
              ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent'],
              ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
              ['Link', 'Unlink'],
              ['Table', 'HorizontalRule', 'SpecialChar', 'PageBreak'],
              '/',
              ['Styles', 'Format'], ['TextColor', 'BGColor'],
              '/',
              ['Font', 'FontSize', 'Maximize'],
              ['insert_image', 'Insertyoutube', 'InsertImage2']
    //['insert_image', 'Insert_Variables', 'InsertNewsRelates', 'InsertContents', 'Insertyoutube', 'Insertslideimage', 'InsertCrawl', 'InsertViewsBNN', 'WaterMark', 'InsertImage2'] //Button added to the toolbar
    ];
    config.toolbar_SapoBDT = [['Source', '-', 'PasteText', '-', 'Bold', 'Italic', 'Underline', '-', 'Subscript', 'Superscript']];
    config.entities = false;
    config.removePlugins = 'iframe';
    config.toolbar = 'Noidung';
    config.extraPlugins = 'wordcount,insert_image,Insertyoutube,InsertImage2';
    config.tabSpaces = 6;
};
