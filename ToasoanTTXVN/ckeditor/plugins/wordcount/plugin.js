/**
* CKEditor Word Count Plugin
*
* Adds a word count to the bottom right hand corner of any CKEditor instance
*
* @package wordcount
* @author Tim Carr
* @version 1
* @copyright n7 Studios 2010
*/


(function() {
    CKEDITOR.plugins.wordcount = {
};

var plugin = CKEDITOR.plugins.wordcount;

/**
* Shows the word count in the DIV element created via setTimeout()
* 
* @param obj CKEditor Editor Object
*/
function ShowWordCount(evt) {
    var editor = evt.editor;


    if ($('div#cke_wordcount_' + editor.name).length > 0) { // Check element exists
        // Because CKEditor uses Javascript to load parts of the editor, some of its elements are not immediately available in the DOM
        // Therefore, I use setTimeout.  There may be a better way of doing this.
        setTimeout(function() {
            var wordCount = GetWordCount(editor);            
            // Just display word count
            $('div#cke_wordcount_' + editor.name).html('Số từ: ' + wordCount);

        }, 1000);
    }
}

/**
* Takes the given HTML data, replaces all its HTML tags with nothing, splits the result by spaces, 
* and outputs the array length i.e. number of words.
* 
* @param string htmlData HTML Data
* @return int Word Count
*/

function GetWordCount(editorInstance) {

    var ketqua = 0;
    var wordCount = 0,
    charCount = 0,
    normalizedText = 0,
    text;
    text = editorInstance.getData();

    if (typeof text != 'undefined' && text != '') {
        charCount = text.length;

        var i = text.search(new RegExp("<body>", "i"));

        if (i != -1) {
            var j = text.search(new RegExp("</body>", "i"));
            text = text.substring(i + 6, j);

            normalizedText = text.
                            replace(/(\r\n|\n|\r)/gm, "").
                            replace(/^\s+|\s+$/g, "").
                            replace("&nbsp;", "");


            normalizedText = text.replace(/\s/g, "");



            normalizedText = strip(normalizedText).replace(/^([\s\t\r\n]*)$/, "");

            charCount = normalizedText.length;

        }

        normalizedText = text.
                        replace(/(\r\n|\n|\r)/gm, " ").
                        replace(/^\s+|\s+$/g, "").
                        replace("&nbsp;", " ");

        normalizedText = strip(normalizedText);

        var words = normalizedText.split(/\s+/);

        for (var wordIndex = words.length - 1; wordIndex >= 0; wordIndex--) {
            if (words[wordIndex].match(/^([\s\t\r\n]*)$/)) {
                words.splice(wordIndex, 1);

            }
        }
        ketqua = words.length;
    }
    else {
        ketqua = 0
    }
    return ketqua;
}
function strip(html) {
    var tmp = document.createElement("div");
    tmp.innerHTML = html;

    if (tmp.textContent == '' && typeof tmp.innerText == 'undefined') {
        return '';
    }
    return tmp.textContent || tmp.innerText;
}
/**
* Adds the plugin to CKEditor
*/
CKEDITOR.plugins.add('wordcount', {
    init: function(editor) {
        // Word Count Limit
        // Plugin options
        CKEDITOR.config.wordcount_maxWords = 500;
        // Word Count Label - setTimeout used as this element won't be available until after init
        setTimeout(function() {
            $('input[id=' + editor.name + 'WordCount]').val(GetWordCount(editor));
            $('td#cke_bottom_' + editor.name).append('<div id="cke_wordcount_' + editor.name + '" style="display: inline-block; float: right; text-align: right; margin-top: 5px; cursor:auto; font:13px Arial,Helvetica,Tahoma,Verdana,Sans-Serif;font-weight:bold; height:auto; padding:0; text-align:left; text-decoration:none; vertical-align:baseline; white-space:nowrap; width:auto;">Số từ: ' + GetWordCount(editor) + '</div>');

        }, 1000);

        editor.on('key', ShowWordCount);
        editor.on('change', ShowWordCount);
        editor.on('uiSpace', ShowWordCount);
        editor.on('dataReady', ShowWordCount);
        editor.on('afterPaste', ShowWordCount);
        

    }
});
})();


